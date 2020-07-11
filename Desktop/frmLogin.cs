using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormLogin : Form
    {
        public bool Logado { get; private set; } = false;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new Connection())
                {
                    if (!Usuario.Logar(txtLogin.Text, txtSenha.Text, conn, null))
                    {
                        MessageBox.Show("Usuário e/ou senha inválido(s)!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Logado = true;
                    Close();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro ao tentar logar: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogar.PerformClick();
        }
    }
}
