using System;
using System.Windows.Forms;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo
{
    public partial class frmAdicionarUsuario : Form
    {
        public frmAdicionarUsuario()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (var usuario = new Usuario())
            using (var connection = new Connection(Database.Local))
            {
                usuario.Nome = edtNome.Text;
                usuario.Login = edtLogin.Text;
                usuario.Ativo = true;

                string mensagem = null;

                if (!usuario.Inserir(edtSenha.Text, edtConfirmacaoSenha.Text, out mensagem, connection, null))
                {
                    MessageBox.Show(mensagem, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }

                MessageBox.Show("O usuário " + usuario.Nome + " foi adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}