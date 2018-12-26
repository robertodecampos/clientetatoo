using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo
{
    public partial class FormTermoResponsabilidade : Form
    {
        private TermoResponsabilidade termoResponsabilidade = new TermoResponsabilidade();

        public FormTermoResponsabilidade()
        {
            InitializeComponent();

            using (var conn = new Connection())
            {
                termoResponsabilidade.SetCurrent(conn, null);
                rtbTermoResponsabilidade.Text = termoResponsabilidade.Termo;
            }
        }

        private void cbxConcordo_CheckedChanged(object sender, EventArgs e) => btnAvancar.Enabled = cbxConcordo.Checked;
    }
}
