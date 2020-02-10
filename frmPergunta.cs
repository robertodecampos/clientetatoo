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

            AlterarCampoColunasAlternativas();
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
            numColunasAlternativas.Value = pergunta.ColunasAlternativas;

            AlterarCampoColunasAlternativas();
        }

        private bool IsValid()
        {
            string mensagem;

            using (var conn = new Connection(Database.Local))
            {
                if (Pergunta.IsValid(out mensagem, conn, null))
                {
                    if (!rbDissertativa.Checked && !rbMultiplaSelecao.Checked && !rbSelecaoUnica.Checked)
                        mensagem = "Selecione o tipo de alternativa para essa pergunta!";
                }
            }

            if (mensagem == string.Empty)
                return true;
            else
            {
                MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void AlterarCampoColunasAlternativas()
        {
            numColunasAlternativas.Enabled = (rbMultiplaSelecao.Checked || rbSelecaoUnica.Checked);
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
                Pergunta.ColunasAlternativas = (int)numColunasAlternativas.Value;

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

        private void rbDissertativa_CheckedChanged(object sender, EventArgs e)
        {
            AlterarCampoColunasAlternativas();
        }

        private void rbMultiplaSelecao_CheckedChanged(object sender, EventArgs e)
        {
            AlterarCampoColunasAlternativas();
        }

        private void rbSelecaoUnica_CheckedChanged(object sender, EventArgs e)
        {
            AlterarCampoColunasAlternativas();
        }
    }
}
