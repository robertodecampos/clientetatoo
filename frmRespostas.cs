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
    public partial class FormRespostas : Form
    {
        private List<Resposta> Respostas { get; set; }
        private int IdPergunta { get; set; }

        public FormRespostas(int idPergunta)
        {
            InitializeComponent();

            IdPergunta = idPergunta;

            OrganizarColunas();

            CarregarRespostas();
        }

        private void CarregarRespostas()
        {
            using (var conn = new Connection())
            {
                Respostas = Resposta.GetByIdPergunta(IdPergunta, false, conn, null);
            }

            lsvRespostas.Items.Clear();

            foreach (Resposta resposta in Respostas)
            {
                var item = new ListViewItem();

                item.Text = resposta.Id.ToString();
                item.SubItems.Add(resposta.Descricao);
                item.SubItems.Add(resposta.Especificar ? "Sim" : "Não");
                item.SubItems.Add(resposta.Ativada ? "Sim" : "Não");

                lsvRespostas.Items.Add(item);
            }

            AtivarDesativarAcoes();
        }

        private void OrganizarColunas()
        {
            int width = lsvRespostas.ClientSize.Width;

            for (int i = 0; i < lsvRespostas.Columns.Count; i++)
                width -= lsvRespostas.Columns[i].Width;

            lsvRespostas.Columns[1].Width += width;
        }

        private bool GetVisibleAtivarDesativar()
        {
            int qtdeAtivas = 0;

            for (int i = 0; i < lsvRespostas.SelectedIndices.Count; i++)
            {
                if (Respostas[lsvRespostas.SelectedIndices[i]].Ativada)
                    qtdeAtivas += 1;
            }

            if ((lsvRespostas.SelectedIndices.Count > 0) && ((qtdeAtivas == lsvRespostas.SelectedIndices.Count) || (qtdeAtivas == 0)))
            {
                btnAtivarDesativar.Text = qtdeAtivas == 0 ? "Ativar" : "Desativar";
                return true;
            }
            else
                return false;
        }

        private void AtivarDesativarAcoes()
        {
            btnRemover.Visible = (lsvRespostas.SelectedIndices.Count > 0);
            btnAtivarDesativar.Visible = GetVisibleAtivarDesativar();
            btnAlterar.Visible = lsvRespostas.SelectedIndices.Count == 1;
            btnSubPerguntas.Visible = lsvRespostas.SelectedIndices.Count == 1;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frmResposta = new FormResposta(IdPergunta))
            {
                if (frmResposta.ShowDialog() == DialogResult.OK)
                    CarregarRespostas();
            }
        }

        private void lsvRespostas_SelectedIndexChanged(object sender, EventArgs e) => AtivarDesativarAcoes();

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (lsvRespostas.SelectedIndices.Count != 1)
                return;

            using (var frmResposta = new FormResposta(Respostas[lsvRespostas.SelectedIndices[0]]))
            {
                if (frmResposta.ShowDialog() == DialogResult.OK)
                    CarregarRespostas();
            }
        }

        private void btnAtivarDesativar_Click(object sender, EventArgs e)
        {
            if (!GetVisibleAtivarDesativar())
                return;

            bool ativar = !Respostas[lsvRespostas.SelectedIndices[0]].Ativada;
            int qtdeSelecionada = lsvRespostas.SelectedIndices.Count;

            string mensagemRespostasSelecionadas = $" {(ativar ? "ativar" : "desativar")}" +
                $" {(qtdeSelecionada == 1 ? $"a pergunta \"{Respostas[lsvRespostas.SelectedIndices[0]].Descricao}\"" : $"as {qtdeSelecionada} respostas selecionadas")}?\n" +
                "Essa ação poderá ser revertida posteriormente!";

            if (MessageBox.Show("Deseja realmente" + mensagemRespostasSelecionadas, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (var conn = new Connection())
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < qtdeSelecionada; i++)
                    {
                        Respostas[lsvRespostas.SelectedIndices[i]].Ativada = ativar;
                        Respostas[lsvRespostas.SelectedIndices[i]].Salvar(conn, transaction);
                    }

                    transaction.Commit();
                    MessageBox.Show($"{(qtdeSelecionada == 1 ? $"Resposta {(ativar ? "ativada" : "desativada")}" : $"Respostas {(ativar ? "ativadas" : "desativadas")}")} com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarRespostas();
                }
                catch (Exception erro)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ocorreu um erro ao" + mensagemRespostasSelecionadas + "\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lsvRespostas.SelectedIndices.Count == 0)
                return;

            int qtdeSelecionada = lsvRespostas.SelectedIndices.Count;

            string mensagemRespostasSelecionadas = $" {(qtdeSelecionada == 1 ? $"a resposta \"{Respostas[lsvRespostas.SelectedIndices[0]].Descricao}\"" : $"as {qtdeSelecionada} respostas selecionadas")}?\n" +
                "Essa ação não poderá ser revertida posteriormente!";

            if (MessageBox.Show("Deseja realmente remover " + mensagemRespostasSelecionadas, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (var conn = new Connection())
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < qtdeSelecionada; i++)
                        Respostas[lsvRespostas.SelectedIndices[i]].Remover(conn, transaction);

                    transaction.Commit();
                    MessageBox.Show($"{(qtdeSelecionada == 1 ? "Resposta removida" : "Respostas removidas")} com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarRespostas();
                }
                catch (Exception erro)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ocorreu um erro ao remover " + mensagemRespostasSelecionadas + "\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSubPerguntas_Click(object sender, EventArgs e)
        {
            if (lsvRespostas.SelectedIndices.Count != 1)
                return;

            using (var conn = new Connection())
            using (var pergunta = new Pergunta())
            {
                pergunta.SetById(IdPergunta, conn, null);

                using (var frmPerguntas = new FormPerguntas(Respostas[lsvRespostas.SelectedIndices[0]].Id, pergunta.Tipo))
                {
                    frmPerguntas.ShowDialog();
                }
            }
        }
    }
}
