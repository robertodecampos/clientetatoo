using ClienteTatoo.Exceptions;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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
        private int Id { get; set; }

        public FormTatuagem(TipoAcao tipoAcao)
        {
            InitializeComponent();

            btnAlterarPesquisa.Location = btnVoltar.Location;

            switch (tipoAcao)
            {
                case TipoAcao.Cadastro:
                    btnVoltar.Visible = true;
                    break;
                case TipoAcao.Edicao:
                    btnAlterarPesquisa.Visible = true;
                    break;
            }
        }

        public FormTatuagem(TipoAcao tipoAcao, Tatuagem tatuagem) : this(tipoAcao)
        {
            Id = tatuagem.Id;
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

        private void btnAlterarPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmPesquisa = new FormPesquisa(TipoPergunta.Tatuagem, Control.PesquisaControl.TipoFonte.Grande, false, Id))
                {
                    if (frmPesquisa.ShowDialog() != DialogResult.OK)
                        return;

                    using (var conn = new Connection())
                    using (SQLiteTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            Resposta.SalvarRespostas(TipoPergunta.Tatuagem, Id, frmPesquisa.Respostas, conn, transaction);

                            transaction.Commit();
                        }
                        catch (Exception erro)
                        {
                            transaction.Rollback();

                            MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } catch (PerguntasNotFoundException erro)
            {
                MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
