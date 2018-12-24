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
    public partial class FormConfigurarTermoResponsabilidade : Form
    {
        public FormConfigurarTermoResponsabilidade()
        {
            InitializeComponent();
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
    }
}
