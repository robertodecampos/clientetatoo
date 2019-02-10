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
    public partial class FormResposta : Form
    {
        private Resposta Resposta { get; set; }

        public FormResposta(int idPergunta)
        {
            InitializeComponent();

            Resposta = new Resposta();

            Resposta.IdPergunta = idPergunta;
        }

        public FormResposta(Resposta resposta) : this(resposta.IdPergunta)
        {
            Resposta.Id = resposta.Id;

            txtDescricao.Text = resposta.Descricao;
            cbxEspecificar.Checked = resposta.Especificar;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (var conn = new Connection())
            {
                Resposta.Descricao = txtDescricao.Text;
                Resposta.Especificar = cbxEspecificar.Checked;

                try
                {
                    Resposta.Salvar(conn, null);
                    MessageBox.Show("Resposta salva com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Ocorreu um erro ao salvar a resposta:\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }
        }
    }
}
