using System;
using System.Windows.Forms;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo
{
    public partial class frmModificarUsuario : Form
    {
        private int id;

        public frmModificarUsuario(int idUsuario)
        {
            InitializeComponent();

            id = idUsuario;

            using (var connection = new Connection(Database.Local))
            using (var usuario = new Usuario())
            {
                if (!usuario.SetById(id, connection, null))
                {
                    Load += (s, e) => Close();
                    return;
                }

                edtNome.Text = usuario.Nome;
                cbxAtivo.Checked = usuario.Ativo;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (var connection = new Connection(Database.Local))
            using (var usuario = new Usuario())
            {
                if (!usuario.SetById(id, connection, null))
                {
                    MessageBox.Show("Não foi possível encontrar um usuário com o id " + id + "!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                usuario.Nome = edtNome.Text;
                usuario.Ativo = cbxAtivo.Checked;

                string mensagem = null;

                if (!usuario.Salvar(out mensagem, connection, null))
                {
                    MessageBox.Show(mensagem, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }

                MessageBox.Show("Usuário modificado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
