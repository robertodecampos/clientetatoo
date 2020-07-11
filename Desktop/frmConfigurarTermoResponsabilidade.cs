using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormConfigurarTermoResponsabilidade : Form
    {
        public FormConfigurarTermoResponsabilidade()
        {
            InitializeComponent();

            FontFamily[] families = new InstalledFontCollection().Families;

            foreach (FontFamily family in families)
            {
                toolStripFontFamily.Items.Add(family.Name);
            }
        }

        private void FormConfigurarTermoResponsabilidade_Load(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new Connection())
                {
                    if (TermoResponsabilidade.Exists(conn, null))
                    {
                        var termoResponsabilidade = new TermoResponsabilidade();
                        termoResponsabilidade.SetCurrent(conn, null);
                        rtbTermo.Text = termoResponsabilidade.Termo;
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro ao carregar o termo: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new Connection())
                {
                    var termoResponsabilidade = new TermoResponsabilidade();
                    termoResponsabilidade.Termo = rtbTermo.Text;
                    termoResponsabilidade.Salvar(conn, null);

                    this.Close();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro ao salvar o termo: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripCopiar_Click(object sender, EventArgs e)
        {
            rtbTermo.Copy();
        }

        private void toolStripColar_Click(object sender, EventArgs e)
        {
            rtbTermo.Paste();
        }

        private void rtbTermo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                toolStripFontFamily.Text = rtbTermo.SelectionFont.FontFamily.Name;
                toolStripTamanho.Text = rtbTermo.SelectionFont.Size.ToString();
            }
            catch (Exception erro)
            {
                toolStripFontFamily.Text = String.Empty;
                toolStripTamanho.Text = String.Empty;
            }
        }

        private void toolStripFontFamily_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(toolStripFontFamily.Text))
                return;

            if (toolStripFontFamily.SelectedText == rtbTermo.SelectionFont.Name)
                return;

            rtbTermo.SelectionFont = new Font(toolStripFontFamily.Text, rtbTermo.SelectionFont.Size, rtbTermo.SelectionFont.Style);
        }

        private void toolStripTamanho_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(toolStripTamanho.Text))
                return;

            if (float.Parse(toolStripTamanho.Text) == rtbTermo.SelectionFont.Size)
                return;

            rtbTermo.SelectionFont = new Font(rtbTermo.SelectionFont.Name, float.Parse(toolStripTamanho.Text), rtbTermo.SelectionFont.Style);
        }

        private void toolStripNegrito_Click(object sender, EventArgs e)
        {
            rtbTermo.SelectionFont = new Font(rtbTermo.SelectionFont, rtbTermo.SelectionFont.Style ^ FontStyle.Bold);
        }

        private void toolStripItalico_Click(object sender, EventArgs e)
        {
            rtbTermo.SelectionFont = new Font(rtbTermo.SelectionFont, rtbTermo.SelectionFont.Style ^ FontStyle.Italic);
        }

        private void toolStripSublinhado_Click(object sender, EventArgs e)
        {
            rtbTermo.SelectionFont = new Font(rtbTermo.SelectionFont, rtbTermo.SelectionFont.Style ^ FontStyle.Underline);
        }

        private void toolStripRiscado_Click(object sender, EventArgs e)
        {
            rtbTermo.SelectionFont = new Font(rtbTermo.SelectionFont, rtbTermo.SelectionFont.Style ^ FontStyle.Strikeout);
        }

        private void toolStripCorFonte_Click(object sender, EventArgs e)
        {
            cdSelecionarCor.Color = rtbTermo.SelectionColor;

            if (cdSelecionarCor.ShowDialog() == DialogResult.OK)
                rtbTermo.SelectionColor = cdSelecionarCor.Color;
        }

        private void toolStripCorFundo_Click(object sender, EventArgs e)
        {
            cdSelecionarCor.Color = rtbTermo.SelectionBackColor;

            if (cdSelecionarCor.ShowDialog() == DialogResult.OK)
                rtbTermo.SelectionBackColor = cdSelecionarCor.Color;
        }

        private void toolStripEsquerda_Click(object sender, EventArgs e)
        {
            rtbTermo.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripCentralizado_Click(object sender, EventArgs e)
        {
            rtbTermo.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripDireita_Click(object sender, EventArgs e)
        {
            rtbTermo.SelectionAlignment = HorizontalAlignment.Right;
        }
    }
}
