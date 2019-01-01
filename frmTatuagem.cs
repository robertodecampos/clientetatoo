using ClienteTatoo.Model;
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
    public enum TipoAcao { Cadastro, Edicao }

    public partial class FormTatuagem : Form
    {
        public FormTatuagem(TipoAcao tipoAcao)
        {
            InitializeComponent();

            switch (tipoAcao)
            {
                case TipoAcao.Cadastro:
                    btnVoltar.Visible = true;
                    break;
            }
        }

        public FormTatuagem(TipoAcao tipoAcao, Tatuagem tatuagem) : this(tipoAcao)
        {
            txtLocal.Text = tatuagem.Local;
            txtDesenho.Text = tatuagem.Desenho;
        }

        public void SetDadosInModel(Tatuagem tatuagem)
        {
            tatuagem.Local = txtLocal.Text.Trim();
            tatuagem.Desenho = txtDesenho.Text.Trim();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (var tatuagem = new Tatuagem())
            {
                SetDadosInModel(tatuagem);

                string mensagem;

                if (!tatuagem.IsValid(out mensagem))
                {
                    MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
            }
        }
    }
}
