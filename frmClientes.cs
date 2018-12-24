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
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
        }

        private void termoDeResponsabilidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmConfigurarTermoResponsabilidade = new FormConfigurarTermoResponsabilidade())
            {
                frmConfigurarTermoResponsabilidade.ShowDialog();
            }
        }
    }
}
