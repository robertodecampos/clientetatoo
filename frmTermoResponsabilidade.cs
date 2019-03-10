using System;
using System.Windows.Forms;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo
{
    public partial class FormTermoResponsabilidade : Form
    {
        private TermoResponsabilidade termoResponsabilidade = new TermoResponsabilidade();

        public int IdTermoResponsabilidade { get; private set; }

        public FormTermoResponsabilidade()
        {
            InitializeComponent();

            using (var conn = new Connection())
            {
                termoResponsabilidade.SetCurrent(conn, null);
                rtbTermoResponsabilidade.Text = termoResponsabilidade.Termo;
                IdTermoResponsabilidade = termoResponsabilidade.Id;
            }
        }

        private void cbxConcordo_CheckedChanged(object sender, EventArgs e) => btnAvancar.Enabled = cbxConcordo.Checked;

        private void FormTermoResponsabilidade_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(DialogResult == DialogResult.Cancel))
                return;

            if (MessageBox.Show("Deseja realmente cancelar o cadastro do cliente?\nAs informações não serão salvas!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }
    }
}
