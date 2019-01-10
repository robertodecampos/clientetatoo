using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows.Forms;
using ClienteTatoo.Model;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;

namespace ClienteTatoo
{
    public partial class FormClientes : Form
    {
        private List<Cliente> clientes;
        private List<ClienteFilter> filtros;
        private FormFiltroCliente frmFiltro = new FormFiltroCliente();

        public FormClientes()
        {
            InitializeComponent();

            filtros = new List<ClienteFilter>();
            cmbOrdenacao.SelectedIndex = 0;
            CarregarClientes();
            OrganizarColunas();
        }

        private void CarregarClientes()
        {
            using (var conn = new Connection())
            {
                var ordenacao = new List<ClienteOrdenation>();

                switch (cmbOrdenacao.SelectedIndex)
                {
                    case 0:
                        ordenacao.Add(new ClienteOrdenation(FieldOrdenationCliente.Codigo, TypeOrder.toAsc));
                        break;
                    case 1:
                        ordenacao.Add(new ClienteOrdenation(FieldOrdenationCliente.Nome, TypeOrder.toAsc));
                        break;
                    case 2:
                        ordenacao.Add(new ClienteOrdenation(FieldOrdenationCliente.DataNascimento, TypeOrder.toAsc));
                        break;
                }

                clientes = Cliente.GetAll(filtros, ordenacao, conn, null);
            }

            lsvClientes.Items.Clear();

            foreach (Cliente cliente in clientes)
            {
                var item = new ListViewItem();

                item.Text = cliente.Id.ToString();
                item.SubItems.Add(cliente.Nome);
                if (cliente.DataNascimento != null)
                    item.SubItems.Add(((DateTime)cliente.DataNascimento).ToString("dd/MM/yyyy"));
                else
                    item.SubItems.Add("Não informada");
                item.SubItems.Add(string.IsNullOrEmpty(cliente.Cpf) ? "Não Informado" : long.Parse(cliente.Cpf).ToString(@"000\.000\.000-00"));
                item.SubItems.Add(string.IsNullOrEmpty(cliente.Telefone) ? "Não Informado" : long.Parse(cliente.Telefone).ToString(@"(00)\ 0000-0000"));
                item.SubItems.Add(string.IsNullOrEmpty(cliente.Celular) ? "Não Informado" : long.Parse(cliente.Celular).ToString(@"(00)\ 0\.0000-0000"));

                lsvClientes.Items.Add(item);
            }
        }

        private void termoDeResponsabilidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            using (var frmConfigurarTermoResponsabilidade = new FormConfigurarTermoResponsabilidade())
            {
                frmLogin.ShowDialog();
                if (frmLogin.Logado)
                    frmConfigurarTermoResponsabilidade.ShowDialog();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            using (var frmDadosPessoais = new FormDadosPessoaisCliente())
            using (var cliente = new Cliente())
            {
                if (frmDadosPessoais.ShowDialog() != DialogResult.OK)
                    return;

                using (var conn = new Connection())
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        frmDadosPessoais.SetDadosInModel(cliente);
                        cliente.Salvar(conn, transaction);
                        transaction.Commit();
                        CarregarClientes();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Ocorreu um erro ao salvar o cliente:\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                    }
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (frmFiltro.ShowDialog() != DialogResult.OK)
                return;

            filtros = frmFiltro.Filtro;
            CarregarClientes();
        }

        private void cmbOrdenacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarClientes();
        }

        private new void Dispose()
        {
            frmFiltro.Dispose();
            base.Dispose();
        }

        private void lsvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int qtdeSelecionado = lsvClientes.SelectedIndices.Count;
            btnAlterarInformacoesPessoais.Visible = (qtdeSelecionado == 1);
            btnRemover.Visible = (qtdeSelecionado > 0);
            btnTatuagens.Visible = (qtdeSelecionado == 1);
        }

        private void btnAlterarInformacoesPessoais_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            int idCliente = clientes[lsvClientes.SelectedIndices[0]].Id;

            using (var cliente = new Cliente())
            {
                using (var conn = new Connection())
                {
                    if (!cliente.SetById(idCliente, conn, null))
                    {
                        MessageBox.Show($"Não foi possível encontrar o cliente com id `{idCliente}`", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CarregarClientes();
                        return;
                    }
                }

                using (var frmDadosPessoais = new FormDadosPessoaisCliente(cliente))
                {
                    if (frmDadosPessoais.ShowDialog() != DialogResult.OK)
                        return;

                    frmDadosPessoais.SetDadosInModel(cliente);
                }

                using (var conn = new Connection())
                {
                    cliente.Salvar(conn, null);
                    MessageBox.Show("Informações do cliente salva com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarClientes();
                }
            }
        }

        private void OrganizarColunas()
        {
            int width = lsvClientes.ClientSize.Width;

            for (int i = 0; i < lsvClientes.Columns.Count; i++)
                width -= lsvClientes.Columns[i].Width;

            lsvClientes.Columns[1].Width += width;
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            // Verificando se o usuário deseja realmente remover os clientes selecionados
            if (MessageBox.Show($"Deseja realmente remover os {lsvClientes.SelectedIndices.Count} clientes selecionados?\nEssa ação não poderá ser revertida!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            using (var conn = new Connection())
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < lsvClientes.SelectedIndices.Count; i++)
                    {
                        clientes[lsvClientes.SelectedIndices[i]].Remover(conn, transaction);
                    }

                    transaction.Commit();

                    MessageBox.Show($"{lsvClientes.SelectedIndices.Count} cliente(s) foi(ram) removido(s) com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarClientes();
                }
                catch (Exception erro)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ocorreu um erro ao remover os clientes, a ação foi canacelada!\n{erro.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTatuagens_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            int idCliente = clientes[lsvClientes.SelectedIndices[0]].Id;

            using (var frmTatuagens = new FormTatuagens(idCliente))
            {
                frmTatuagens.ShowDialog();
            }
        }

        private void FormClientes_Resize(object sender, EventArgs e) => OrganizarColunas();
    }
}
