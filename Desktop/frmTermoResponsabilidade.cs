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

        public FormTermoResponsabilidade(int? idTermoResponsabilidade = null)
        {
            InitializeComponent();

            if (idTermoResponsabilidade != null)
            {
                cbxConcordo.Enabled = false;
                cbxConcordo.Checked = true;
                btnAvancar.Text = "Ok";
            }

            using (var conn = new Connection())
            {
                if (idTermoResponsabilidade == null)
                    termoResponsabilidade.SetCurrent(conn, null);
                else
                    termoResponsabilidade.SetById((int)idTermoResponsabilidade, conn, null);
                rtbTermoResponsabilidade.Text = termoResponsabilidade.Termo;
                IdTermoResponsabilidade = termoResponsabilidade.Id;
            }
        }

        private void cbxConcordo_CheckedChanged(object sender, EventArgs e) => btnAvancar.Enabled = cbxConcordo.Checked;
    }
}
