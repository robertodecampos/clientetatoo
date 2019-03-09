using ClienteTatoo.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClienteTatoo.Control
{
    public class AlternativaControl : Panel
    {
        public delegate void AlternativaHandler(int idAlternativa, bool marcado);

        public event AlternativaHandler CheckedChanged;

        public Alternativa Alternativa { get; private set; }
        public bool Checked {
            get
            {
                if (MultiplaEscolha)
                    return chxMultiplaEscolha.Checked;
                else
                    return rbEscolhaUnica.Checked;
            }
            set
            {
                if (MultiplaEscolha)
                    chxMultiplaEscolha.Checked = value;
                else
                    rbEscolhaUnica.Checked = value;
            }
        }
        public override string Text {
            get
            {
                return txtDissertativa.Text;
            }
            set
            {
                txtDissertativa.Text = value;
            }
        }
        private bool MultiplaEscolha { get; set; }

        private TextBox txtDissertativa;
        private CheckBox chxMultiplaEscolha;
        private RadioButton rbEscolhaUnica;

        public AlternativaControl(Alternativa alternativa, bool multiplaEscolha, PesquisaControl.TipoFonte tipoFonte, bool somenteLeitura) : base()
        {
            if (tipoFonte == PesquisaControl.TipoFonte.Grande)
                Font = new Font(Font.Name, 12, Font.Style, GraphicsUnit.Point);

            txtDissertativa = new TextBox();
            txtDissertativa.Top = 0;
            txtDissertativa.Left = 8;
            txtDissertativa.Width = ClientSize.Width - 16;
            txtDissertativa.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            txtDissertativa.Enabled = !somenteLeitura;
            Controls.Add(txtDissertativa);
            
            chxMultiplaEscolha = new CheckBox();
            chxMultiplaEscolha.Top = 0;
            chxMultiplaEscolha.Left = 8;
            chxMultiplaEscolha.Width = ClientSize.Width - 16;
            chxMultiplaEscolha.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            chxMultiplaEscolha.CheckedChanged += chxMultiplaEscolha_CheckedChanged;
            chxMultiplaEscolha.Enabled = !somenteLeitura;
            Controls.Add(chxMultiplaEscolha);
            
            rbEscolhaUnica = new RadioButton();
            rbEscolhaUnica.Top = 0;
            rbEscolhaUnica.Left = 8;
            rbEscolhaUnica.Width = ClientSize.Width - 16;
            rbEscolhaUnica.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            rbEscolhaUnica.CheckedChanged += rbEscolhaUnica_CheckedChanged;
            rbEscolhaUnica.Enabled = !somenteLeitura;
            Controls.Add(rbEscolhaUnica);

            Alternativa = alternativa;
            MultiplaEscolha = multiplaEscolha;

            if (alternativa == null)
                ExibirTextBox();
            else
            {
                chxMultiplaEscolha.Text = Alternativa.Descricao;
                rbEscolhaUnica.Text = Alternativa.Descricao;

                if (MultiplaEscolha)
                    ExibirCheckBox();
                else
                    ExibirRadioButton();
            }

            Height = txtDissertativa.Height + (Height - ClientSize.Height);
        }

        public AlternativaControl(Alternativa alternativa, PesquisaControl.TipoFonte tipoFonte, bool somenteLeitura) : this(alternativa, false, tipoFonte, somenteLeitura) { }

        public Resposta GetResposta()
        {
            var resposta = new Resposta();

            if (txtDissertativa.Visible)
                resposta.RespostaDissertativa = txtDissertativa.Text;
            else
                resposta.IdAlternativa = Alternativa.Id;

            return resposta;
        }

        protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

        private void ExibirTextBox()
        {
            txtDissertativa.Visible = true;
            chxMultiplaEscolha.Visible = false;
            rbEscolhaUnica.Visible = false;
        }

        private void ExibirCheckBox()
        {
            chxMultiplaEscolha.Visible = true;
            txtDissertativa.Visible = false;
            rbEscolhaUnica.Visible = false;
        }

        private void ExibirRadioButton()
        {
            rbEscolhaUnica.Visible = true;
            chxMultiplaEscolha.Visible = false;
            txtDissertativa.Visible = false;
        }

        private void chxMultiplaEscolha_CheckedChanged(object sender, EventArgs e) => CheckedChanged(Alternativa.Id, chxMultiplaEscolha.Checked);

        private void rbEscolhaUnica_CheckedChanged(object sender, EventArgs e) => CheckedChanged(Alternativa.Id, rbEscolhaUnica.Checked);
    }
}
