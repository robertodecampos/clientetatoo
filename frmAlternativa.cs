using ClienteTatoo.Model;
using ClienteTatoo.Utils;
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
    public partial class FormAlternativa : Form
    {
        private Alternativa Alternativa { get; set; }

        public FormAlternativa(int idPergunta)
        {
            InitializeComponent();

            Alternativa = new Alternativa();

            Alternativa.IdPergunta = idPergunta;
        }

        public FormAlternativa(Alternativa alternativa) : this(alternativa.IdPergunta)
        {
            Alternativa.Id = alternativa.Id;

            txtDescricao.Text = alternativa.Descricao;
            cbxEspecificar.Checked = alternativa.Especificar;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (var conn = new Connection())
            {
                Alternativa.Descricao = txtDescricao.Text;
                Alternativa.Especificar = cbxEspecificar.Checked;

                try
                {
                    Alternativa.Salvar(conn, null);
                    MessageBox.Show("Alternativa salva com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Ocorreu um erro ao salvar a alternativa:\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }
        }
    }
}
