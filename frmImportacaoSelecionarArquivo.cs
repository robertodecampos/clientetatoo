﻿using System;
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
    public partial class frmImportacaoSelecionarArquivo : Form
    {
        public frmImportacaoSelecionarArquivo()
        {
            InitializeComponent();
        }

        private void btnProcurarArquivo_Click(object sender, EventArgs e)
        {
            if (ofdArquivo.ShowDialog() != DialogResult.OK)
                return;

            edtArquivo.Text = ofdArquivo.FileName;
        }
    }
}