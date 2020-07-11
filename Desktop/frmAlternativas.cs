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
    public partial class FormAlternativas : Form
    {
        private List<Alternativa> Alternativas { get; set; }
        private int IdPergunta { get; set; }

        public FormAlternativas(int idPergunta)
        {
            InitializeComponent();

            IdPergunta = idPergunta;

            OrganizarColunas();

            CarregarAlternativas();
        }

        private void CarregarAlternativas()
        {
            using (var conn = new Connection())
            {
                Alternativas = Alternativa.GetByIdPergunta(IdPergunta, false, conn, null);
            }

            lsvAlternativas.Items.Clear();

            foreach (Alternativa alternativa in Alternativas)
            {
                var item = new ListViewItem();

                item.Text = alternativa.Id.ToString();
                item.SubItems.Add(alternativa.Descricao);
                item.SubItems.Add(alternativa.Ativada ? "Sim" : "Não");

                lsvAlternativas.Items.Add(item);
            }

            AtivarDesativarAcoes();
        }

        private void OrganizarColunas()
        {
            int width = lsvAlternativas.ClientSize.Width;

            for (int i = 0; i < lsvAlternativas.Columns.Count; i++)
                width -= lsvAlternativas.Columns[i].Width;

            lsvAlternativas.Columns[1].Width += width;
        }

        private bool GetVisibleAtivarDesativar()
        {
            int qtdeAtivas = 0;

            for (int i = 0; i < lsvAlternativas.SelectedIndices.Count; i++)
            {
                if (Alternativas[lsvAlternativas.SelectedIndices[i]].Ativada)
                    qtdeAtivas += 1;
            }

            if ((lsvAlternativas.SelectedIndices.Count > 0) && ((qtdeAtivas == lsvAlternativas.SelectedIndices.Count) || (qtdeAtivas == 0)))
            {
                btnAtivarDesativar.Text = qtdeAtivas == 0 ? "Ativar" : "Desativar";
                return true;
            }
            else
                return false;
        }

        private void AtivarDesativarAcoes()
        {
            btnRemover.Visible = (lsvAlternativas.SelectedIndices.Count > 0);
            btnAtivarDesativar.Visible = GetVisibleAtivarDesativar();
            btnAlterar.Visible = lsvAlternativas.SelectedIndices.Count == 1;
            btnSubPerguntas.Visible = lsvAlternativas.SelectedIndices.Count == 1;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frmAlternativa = new FormAlternativa(IdPergunta))
            {
                if (frmAlternativa.ShowDialog() == DialogResult.OK)
                    CarregarAlternativas();
            }
        }

        private void lsvAlternativas_SelectedIndexChanged(object sender, EventArgs e) => AtivarDesativarAcoes();

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (lsvAlternativas.SelectedIndices.Count != 1)
                return;

            using (var frmAlternativa = new FormAlternativa(Alternativas[lsvAlternativas.SelectedIndices[0]]))
            {
                if (frmAlternativa.ShowDialog() == DialogResult.OK)
                    CarregarAlternativas();
            }
        }

        private void btnAtivarDesativar_Click(object sender, EventArgs e)
        {
            if (!GetVisibleAtivarDesativar())
                return;

            bool ativar = !Alternativas[lsvAlternativas.SelectedIndices[0]].Ativada;
            int qtdeSelecionada = lsvAlternativas.SelectedIndices.Count;

            string mensagemAlternativasSelecionadas = $" {(ativar ? "ativar" : "desativar")}" +
                $" {(qtdeSelecionada == 1 ? $"a pergunta \"{Alternativas[lsvAlternativas.SelectedIndices[0]].Descricao}\"" : $"as {qtdeSelecionada} alternativas selecionadas")}?\n" +
                "Essa ação poderá ser revertida posteriormente!";

            if (MessageBox.Show("Deseja realmente" + mensagemAlternativasSelecionadas, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (var conn = new Connection())
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < qtdeSelecionada; i++)
                    {
                        Alternativas[lsvAlternativas.SelectedIndices[i]].Ativada = ativar;
                        Alternativas[lsvAlternativas.SelectedIndices[i]].Salvar(conn, transaction);
                    }

                    transaction.Commit();
                    MessageBox.Show($"{(qtdeSelecionada == 1 ? $"Alternativa {(ativar ? "ativada" : "desativada")}" : $"Alternativas {(ativar ? "ativadas" : "desativadas")}")} com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarAlternativas();
                }
                catch (Exception erro)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ocorreu um erro ao" + mensagemAlternativasSelecionadas + "\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lsvAlternativas.SelectedIndices.Count == 0)
                return;

            int qtdeSelecionada = lsvAlternativas.SelectedIndices.Count;

            string mensagemAlternativasSelecionadas = $" {(qtdeSelecionada == 1 ? $"a alternativa \"{Alternativas[lsvAlternativas.SelectedIndices[0]].Descricao}\"" : $"as {qtdeSelecionada} alternativas selecionadas")}?\n" +
                "Essa ação não poderá ser revertida posteriormente!";

            if (MessageBox.Show("Deseja realmente remover " + mensagemAlternativasSelecionadas, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (var conn = new Connection())
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < qtdeSelecionada; i++)
                        Alternativas[lsvAlternativas.SelectedIndices[i]].Remover(conn, transaction);

                    transaction.Commit();
                    MessageBox.Show($"{(qtdeSelecionada == 1 ? "Alternativa removida" : "Alternativas removidas")} com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarAlternativas();
                }
                catch (Exception erro)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ocorreu um erro ao remover " + mensagemAlternativasSelecionadas + "\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSubPerguntas_Click(object sender, EventArgs e)
        {
            if (lsvAlternativas.SelectedIndices.Count != 1)
                return;

            using (var conn = new Connection())
            using (var pergunta = new Pergunta())
            {
                pergunta.SetById(IdPergunta, conn, null);

                using (var frmPerguntas = new FormPerguntas(Alternativas[lsvAlternativas.SelectedIndices[0]].Id, pergunta.Tipo))
                {
                    frmPerguntas.ShowDialog();
                }
            }
        }
    }
}
