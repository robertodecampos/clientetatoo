using ClienteTatoo.Model;
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

        public FormClientes()
        {
            InitializeComponent();
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

        private void txtCadastrar_Click(object sender, EventArgs e)
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
                    cliente.Nome = frmDadosPessoais.Cliente.Nome;
                    cliente.DataNascimento = frmDadosPessoais.Cliente.DataNascimento;
                    cliente.Cpf = frmDadosPessoais.Cliente.Cpf;
                    cliente.Email = frmDadosPessoais.Cliente.Email;
                    cliente.Telefone = frmDadosPessoais.Cliente.Telefone;
                    cliente.Celular = frmDadosPessoais.Cliente.Celular;
                    cliente.Cep = frmDadosPessoais.Cliente.Cep;
                    cliente.Uf = frmDadosPessoais.Cliente.Uf;
                    cliente.IdCidade = frmDadosPessoais.Cliente.IdCidade;
                    cliente.TipoLogradouro = frmDadosPessoais.Cliente.TipoLogradouro;
                    cliente.Logradouro = frmDadosPessoais.Cliente.Logradouro;
                    cliente.Complemento = frmDadosPessoais.Cliente.Complemento;
                    cliente.Bairro = frmDadosPessoais.Cliente.Bairro;
                    cliente.Numero = frmDadosPessoais.Cliente.Numero;

                    try
                    {
                        cliente.Salvar(conn, transaction);
                        transaction.Commit();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Ocorreu um erro ao salvar o cliente:\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
