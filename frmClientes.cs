using ClienteTatoo.Model;
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
            }
        }
    }
}
