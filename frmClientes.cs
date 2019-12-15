using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows.Forms;
using ClienteTatoo.Model;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;
using ClienteTatoo.Control;
using ClienteTatoo.Exceptions;

namespace ClienteTatoo
{
    public partial class FormClientes : Form
    {
        private List<Cliente> clientes;
        private List<ClienteFilter> filtros;
        private FormFiltroCliente frmFiltro = new FormFiltroCliente();

        private enum PassoCadastro {DadosPessoais, Pesquisa};

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
            try
            {
                using (var frmDadosPessoais = new FormDadosPessoaisCliente())
                using (var frmPesquisa = new FormPesquisa(TipoPergunta.Cliente, PesquisaControl.TipoFonte.Grande, false))
                using (var cliente = new Cliente())
                {
                    frmDadosPessoais.btnOk.Text = "Avançar";
                    frmPesquisa.btnVoltar.Visible = true;

                    PassoCadastro passo = PassoCadastro.DadosPessoais;
                    bool cadastroFinalizado = false;

                    while (!cadastroFinalizado)
                    {
                        switch (passo)
                        {
                            case PassoCadastro.DadosPessoais:
                                if (frmDadosPessoais.ShowDialog() == DialogResult.OK)
                                    passo = PassoCadastro.Pesquisa;
                                else
                                    return;
                                break;
                            case PassoCadastro.Pesquisa:
                                DialogResult dialogResult = frmPesquisa.ShowDialog();

                                if (dialogResult == DialogResult.OK)
                                    cadastroFinalizado = true;
                                else if (dialogResult == DialogResult.Retry)
                                    passo = PassoCadastro.DadosPessoais;
                                else
                                    return;
                                break;
                        }
                    }

                    using (var conn = new Connection())
                    using (SQLiteTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            frmDadosPessoais.SetDadosInModel(cliente);
                            cliente.Salvar(false, conn, transaction);

                            Resposta.SalvarRespostas(TipoPergunta.Cliente, cliente.Id, frmPesquisa.Respostas, conn, transaction);

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
            } catch (PerguntasNotFoundException erro)
            {
                MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnRemover.Visible = (qtdeSelecionado > 0);
            btnAlterar.Visible = (qtdeSelecionado == 1);
            btnVisualizarPesquisa.Visible = (qtdeSelecionado == 1);
            btnTatuagens.Visible = (qtdeSelecionado == 1);
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
            if (lsvClientes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione um cliente para ser removido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            if (lsvClientes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione um cliente para visualizar as tatuagens!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (lsvClientes.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Selecione somente um cliente para visualizar as tatuagens!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

        private void versãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmVersao = new FormVersao())
            {
                frmVersao.ShowDialog();
            }
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            using (var frmPerguntas = new FormPerguntas(TipoPergunta.Cliente))
            {
                frmPerguntas.ShowDialog();
            }
        }

        private void tatuagensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            using (var frmPerguntas = new FormPerguntas(TipoPergunta.Tatuagem))
            {
                frmPerguntas.ShowDialog();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (lsvClientes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione um cliente para ser alterado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (lsvClientes.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Selecione somente um cliente para ser alterado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                    frmDadosPessoais.btnAlterarPesquisa.Visible = true;

                    if (frmDadosPessoais.ShowDialog() != DialogResult.OK)
                        return;

                    frmDadosPessoais.SetDadosInModel(cliente);
                }

                using (var conn = new Connection())
                {
                    cliente.Salvar(false, conn, null);
                    MessageBox.Show("Informações do cliente salva com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarClientes();
                }
            }
        }

        private void btnVisualizarPesquisa_Click(object sender, EventArgs e)
        {
            if (lsvClientes.SelectedIndices.Count != 1)
                return;

            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            try
            {
                using (var frmPesquisa = new FormPesquisa(TipoPergunta.Cliente, PesquisaControl.TipoFonte.Normal, true, clientes[lsvClientes.SelectedIndices[0]].Id))
                {
                    if (frmPesquisa.ShowDialog() != DialogResult.OK)
                        return;

                    using (var conn = new Connection())
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            Resposta.SalvarRespostas(TipoPergunta.Cliente, clientes[lsvClientes.SelectedIndices[0]].Id, frmPesquisa.Respostas, conn, transaction);

                            transaction.Commit();
                        }
                        catch (Exception erro)
                        {
                            transaction.Rollback();
                            MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } catch (PerguntasNotFoundException erro)
            {
                MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            using (var frmUsuarios = new frmUsuarios())
            {
                frmUsuarios.ShowDialog();
            }
        }

        private void importarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            {
                frmLogin.ShowDialog();
                if (!frmLogin.Logado)
                    return;
            }

            using (var frmImportacaoSelecionarArquivo = new frmImportacaoSelecionarArquivo())
            {
                frmImportacaoSelecionarArquivo.ShowDialog();
            }
        }
    }
}
