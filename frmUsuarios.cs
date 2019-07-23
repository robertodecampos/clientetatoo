using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo
{
    public partial class frmUsuarios : Form
    {
        List<Usuario> usuarios;

        public frmUsuarios()
        {
            InitializeComponent();

            listarUsuarios();
        }

        private void listarUsuarios()
        {
            try
            {
                using (var conexao = new Connection(Database.Local))
                {
                    usuarios = Usuario.GetAll(conexao, null);
                }

                lstUsuarios.Items.Clear();

                foreach (Usuario usuario in usuarios)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = usuario.Id.ToString();
                    item.SubItems.Add(usuario.Nome);
                    item.SubItems.Add(usuario.Ativo ? "Sim" : "Não");

                    lstUsuarios.Items.Add(item);
                }
            } catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro ao listar os usuários!\n" + e.Message, string.Empty);
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frmAdicionarUsuario = new frmAdicionarUsuario())
            {
                if (frmAdicionarUsuario.ShowDialog() == DialogResult.OK)
                    listarUsuarios();
            }
        }

        private void lstUsuarios_MouseClick(object sender, MouseEventArgs e) => btnModificar.Visible = (lstUsuarios.SelectedIndices.Count == 1);

        private void btnModificar_Click(object sender, EventArgs e)
        {
            using (var frmModificarUsuario = new frmModificarUsuario(usuarios[lstUsuarios.SelectedIndices[0]].Id))
            {
                if (frmModificarUsuario.ShowDialog() == DialogResult.OK)
                    listarUsuarios();
            }
        }
    }
}
