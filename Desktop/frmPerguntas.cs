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
    public partial class FormPerguntas : Form
    {
        private List<Pergunta> Perguntas { get; set; }
        private TipoPergunta TipoPergunta { get; set; }
        private int? IdAlternativa { get; set; }

        private FormPerguntas(TipoPergunta tipoPergunta, int? idAlternativa)
        {
            InitializeComponent();

            TipoPergunta = tipoPergunta;
            IdAlternativa = idAlternativa;

            switch (tipoPergunta)
            {
                case TipoPergunta.Cliente:
                    Text += " para Clientes";
                    break;
                case TipoPergunta.Tatuagem:
                    Text += " para Tatuagens";
                    break;
            }

            OrganizarColunas();

            CarregarPerguntas();
        }

        public FormPerguntas(TipoPergunta tipoPergunta) : this(tipoPergunta, null) { }

        public FormPerguntas(int idAlternativa, TipoPergunta tipoPergunta) : this(tipoPergunta, idAlternativa) { }

        private void AtivarDesativarAcoes()
        {
            btnRemover.Visible = (lsvPerguntas.SelectedIndices.Count > 0);
            btnAtivarDesativar.Visible = GetVisibleAtivarDesativar();
            btnAlterar.Visible = lsvPerguntas.SelectedIndices.Count == 1;
            btnConfigurarAlternativas.Visible = ((lsvPerguntas.SelectedIndices.Count == 1) && !Perguntas[lsvPerguntas.SelectedIndices[0]].Dissertativa);
        }

        private void CarregarPerguntas()
        {
            using (var conn = new Connection())
            {
                if (IdAlternativa == null)
                    Perguntas = Pergunta.GetPrincipaisByTipoPergunta(TipoPergunta, false, conn, null);
                else
                    Perguntas = Pergunta.GetByIdAlternativa((int)IdAlternativa, false, conn, null);
            }

            lsvPerguntas.Items.Clear();

            foreach (Pergunta pergunta in Perguntas)
            {
                var item = new ListViewItem();

                item.Text = pergunta.Id.ToString();
                item.SubItems.Add(pergunta.Descricao);
                if (pergunta.Dissertativa)
                    item.SubItems.Add("Dissertativa");
                else if (pergunta.AlternativaUnica)
                    item.SubItems.Add("Seleção Única");
                else
                    item.SubItems.Add("Múltipla Seleção");
                item.SubItems.Add(pergunta.Obrigatoria ? "Sim" : "Não");
                item.SubItems.Add(pergunta.Ativada ? "Sim" : "Não");

                lsvPerguntas.Items.Add(item);
            }

            AtivarDesativarAcoes();
        }

        private void OrganizarColunas()
        {
            int width = lsvPerguntas.ClientSize.Width;

            for (int i = 0; i < lsvPerguntas.Columns.Count; i++)
                width -= lsvPerguntas.Columns[i].Width;

            lsvPerguntas.Columns[1].Width += width;
        }

        private bool GetVisibleAtivarDesativar()
        {
            int qtdeAtivas = 0;

            for (int i = 0; i < lsvPerguntas.SelectedIndices.Count; i++)
            {
                if (Perguntas[lsvPerguntas.SelectedIndices[i]].Ativada)
                    qtdeAtivas += 1;
            }

            if ((lsvPerguntas.SelectedIndices.Count > 0) && ((qtdeAtivas == lsvPerguntas.SelectedIndices.Count) || (qtdeAtivas == 0)))
            {
                btnAtivarDesativar.Text = qtdeAtivas == 0 ? "Ativar" : "Desativar";
                return true;
            }
            else
                return false;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frmPergunta = (IdAlternativa == null ? new FormPergunta(TipoPergunta) : new FormPergunta((int)IdAlternativa, TipoPergunta)))
            {
                if (frmPergunta.ShowDialog() == DialogResult.OK)
                    CarregarPerguntas();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (lsvPerguntas.SelectedIndices.Count != 1)
                return;

            using (var frmPergunta = new FormPergunta(Perguntas[lsvPerguntas.SelectedIndices[0]]))
            {
                if (frmPergunta.ShowDialog() == DialogResult.OK)
                    CarregarPerguntas();
            }
        }

        private void lsvPerguntas_SelectedIndexChanged(object sender, EventArgs e) => AtivarDesativarAcoes();

        private void btnAtivarDesativar_Click(object sender, EventArgs e)
        {
            if (!GetVisibleAtivarDesativar())
                return;

            bool ativar = !Perguntas[lsvPerguntas.SelectedIndices[0]].Ativada;
            int qtdeSelecionada = lsvPerguntas.SelectedIndices.Count;

            string mensagemPerguntasSelecionadas = $" {(ativar ? "ativar" : "desativar")}" +
                $" {(qtdeSelecionada == 1 ? $"a pergunta \"{Perguntas[lsvPerguntas.SelectedIndices[0]].Descricao}\"" : $"as {qtdeSelecionada} perguntas selecionadas")}?\n" +
                "Essa ação poderá ser revertida posteriormente!";

            if (MessageBox.Show("Deseja realmente" + mensagemPerguntasSelecionadas, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (var conn = new Connection())
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < qtdeSelecionada; i++)
                    {
                        Perguntas[lsvPerguntas.SelectedIndices[i]].Ativada = ativar;
                        Perguntas[lsvPerguntas.SelectedIndices[i]].Salvar(conn, transaction);
                    }

                    transaction.Commit();
                    MessageBox.Show($"{(qtdeSelecionada == 1 ? $"Pergunta {(ativar ? "ativada" : "desativada")}" : $"Perguntas {(ativar ? "ativadas" : "desativadas")}")} com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarPerguntas();
                } catch (Exception erro)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ocorreu um erro ao" + mensagemPerguntasSelecionadas + "\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnConfigurarAlternativas_Click(object sender, EventArgs e)
        {
            if ((lsvPerguntas.SelectedIndices.Count != 1) || Perguntas[lsvPerguntas.SelectedIndices[0]].Dissertativa)
                return;

            using (var frmAlternativas = new FormAlternativas(Perguntas[lsvPerguntas.SelectedIndices[0]].Id))
            {
                frmAlternativas.ShowDialog();
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lsvPerguntas.SelectedIndices.Count == 0)
                return;

            int qtdeSelecionada = lsvPerguntas.SelectedIndices.Count;

            string mensagemPerguntasSelecionadas = $" {(qtdeSelecionada == 1 ? $"a pergunta \"{Perguntas[lsvPerguntas.SelectedIndices[0]].Descricao}\"" : $"as {qtdeSelecionada} perguntas selecionadas")}?\n" +
                "Essa ação não poderá ser revertida posteriormente!";

            if (MessageBox.Show("Deseja realmente remover " + mensagemPerguntasSelecionadas, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (var conn = new Connection())
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < qtdeSelecionada; i++)
                        Perguntas[lsvPerguntas.SelectedIndices[i]].Remover(conn, transaction);

                    transaction.Commit();
                    MessageBox.Show($"{(qtdeSelecionada == 1 ? "Pergunta removida" : "Perguntas removidas")} com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarPerguntas();
                }
                catch (Exception erro)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ocorreu um erro ao remover " + mensagemPerguntasSelecionadas + "\n\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
