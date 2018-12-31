using ClienteTatoo.Model;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormClientes : Form
    {
        private enum PassoCadastroCliente { pccTermoResponsabilidade, pccDadosPessoais };

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
            using (var frmDadosPessoais = new FormDadosPessoaisCliente(TipoFormulario.tfCadastro))
            using (var frmTermoResponsabilidade = new FormTermoResponsabilidade())
            using (var cliente = new Cliente())
            {
                PassoCadastroCliente passo = PassoCadastroCliente.pccTermoResponsabilidade;
                bool cadastroFinalizado = false;

                while (!cadastroFinalizado)
                {
                    switch (passo)
                    {
                        case PassoCadastroCliente.pccTermoResponsabilidade:
                            frmTermoResponsabilidade.ShowDialog();
                            if (frmTermoResponsabilidade.DialogResult == DialogResult.OK)
                                passo = PassoCadastroCliente.pccDadosPessoais;
                            else
                                return;
                            break;

                        case PassoCadastroCliente.pccDadosPessoais:
                            frmDadosPessoais.ShowDialog();
                            if (frmDadosPessoais.DialogResult == DialogResult.OK)
                                cadastroFinalizado = true;
                            else if (frmDadosPessoais.DialogResult == DialogResult.Abort)
                                passo = PassoCadastroCliente.pccTermoResponsabilidade;
                            else
                                return;
                            break;
                    }
                }

                using (var conn = new Utils.Connection())
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    cliente.IdTermoResponsabilidade = frmTermoResponsabilidade.IdTermoResponsabilidade;
                    frmDadosPessoais.SetDadosInModel(cliente);
                    try
                    {
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
            btnAlterarInformacoesPessoais.Visible = (lsvClientes.SelectedIndices.Count == 1);
        }

        private void btnAlterarInformacoesPessoais_Click(object sender, EventArgs e)
        {
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

                using (var frmDadosPessoais = new FormDadosPessoaisCliente(TipoFormulario.tfEdicao, cliente))
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

            lsvClientes.Scrollable = false; // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
            lsvClientes.Scrollable = true;  // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
        }

        private void lsvClientes_Resize(object sender, EventArgs e) => OrganizarColunas();
    }
}
