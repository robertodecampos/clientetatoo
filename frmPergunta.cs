using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
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

        public FormPergunta(int idAlternativa, TipoPergunta tipoPergunta) : this(tipoPergunta)
        {
            Pergunta.IdAlternativa = idAlternativa;
        }

        public FormPergunta(Pergunta pergunta) : this(pergunta.Tipo)
        {
            Pergunta.Id = pergunta.Id;
            Pergunta.IdAlternativa = pergunta.IdAlternativa;

            txtDescricao.Text = pergunta.Descricao;
            txtCodigoImportacao.Text = pergunta.CodigoImportacao;
            if (pergunta.Dissertativa)
                rbDissertativa.Checked = true;
            else if (pergunta.AlternativaUnica)
                rbSelecaoUnica.Checked = true;
            else
                rbMultiplaSelecao.Checked = true;
            cbxAlternativaObrigatoria.Checked = pergunta.Obrigatoria;
        }

        private bool IsValid()
        {
            string mensagem;

            if (Pergunta.IsValid(out mensagem))
            {
                if (!rbDissertativa.Checked && !rbMultiplaSelecao.Checked && !rbSelecaoUnica.Checked)
                    mensagem = "Selecione o tipo de alternativa para essa pergunta!";
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
                Pergunta.CodigoImportacao = txtCodigoImportacao.Text;
                Pergunta.Dissertativa = rbDissertativa.Checked;
                Pergunta.AlternativaUnica = rbSelecaoUnica.Checked;
                Pergunta.Obrigatoria = cbxAlternativaObrigatoria.Checked;

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
