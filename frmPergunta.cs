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
    public partial class FormPergunta : Form
    {
        private Pergunta Pergunta;

        public FormPergunta(TipoPergunta tipoPergunta)
        {
            InitializeComponent();

            Pergunta = new Pergunta();

            Pergunta.Tipo = tipoPergunta;
        }

        public FormPergunta(int idResposta, TipoPergunta tipoPergunta) : this(tipoPergunta)
        {
            Pergunta.IdResposta = idResposta;
        }

        public FormPergunta(Pergunta pergunta) : this(pergunta.Tipo)
        {
            Pergunta.Id = pergunta.Id;
            Pergunta.IdResposta = pergunta.IdResposta;

            txtDescricao.Text = pergunta.Descricao;
            if (pergunta.RespostaDissertativa)
                rbDissertativa.Checked = true;
            else if (pergunta.RespostaUnica)
                rbSelecaoUnica.Checked = true;
            else
                rbMultiplaSelecao.Checked = true;
            cbxRespostaObrigatoria.Checked = pergunta.Obrigatoria;
        }

        private bool IsValid()
        {
            string mensagem;

            if (Pergunta.IsValid(out mensagem))
            {
                if (!rbDissertativa.Checked && !rbMultiplaSelecao.Checked && !rbSelecaoUnica.Checked)
                    mensagem = "Selecione o tipo de resposta para essa pergunta!";
            }

            if (mensagem == string.Empty)
                return true;
            else
            {
                MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Pergunta.Descricao = txtDescricao.Text;
                Pergunta.RespostaDissertativa = rbDissertativa.Checked;
                Pergunta.RespostaUnica = rbSelecaoUnica.Checked;
                Pergunta.Obrigatoria = cbxRespostaObrigatoria.Checked;

                if (!IsValid())
                {
                    DialogResult = DialogResult.None;
                    return;
                }

                using (var conn = new Connection(Database.Local))
                {
                    Pergunta.Salvar(conn, null);
                    MessageBox.Show("Pergunta salva com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro ao salvar a pergunta:\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
}
