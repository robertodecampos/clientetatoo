using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormSessoes : Form
    {
        private int idTatuagem;
        private List<Sessao> sessoes;

        public FormSessoes(int idTatuagem)
        {
            InitializeComponent();

            this.idTatuagem = idTatuagem;

            OrganizarColunas();

            CarregarSessoes();
        }

        private void OrganizarColunas()
        {
            int width = lsvSessoes.ClientSize.Width;

            for (int i = 0; i < lsvSessoes.Columns.Count; i++)
                width -= lsvSessoes.Columns[i].Width;

            int sobra = width % 2;

            lsvSessoes.Columns[1].Width += (width / 2) + sobra;
            lsvSessoes.Columns[2].Width += width / 2;

            lsvSessoes.Scrollable = false; // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
            lsvSessoes.Scrollable = true;  // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
        }

        private void CarregarSessoes()
        {
            using (var conn = new Connection())
            {
                sessoes = Sessao.GetByIdTatuagem(idTatuagem, conn, null);
            }

            lsvSessoes.Items.Clear();

            foreach (Sessao sessao in sessoes)
            {
                var item = new ListViewItem();

                item.Text = sessao.Id.ToString();
                item.SubItems.Add(sessao.Parametros);
                item.SubItems.Add(sessao.Disparos);
                item.SubItems.Add(sessao.DataSessao.ToString("dd/MM/yyyy"));
                item.SubItems.Add(sessao.Valor.ToString("R$ #,##0.00", new CultureInfo("pt-BR")));
                item.SubItems.Add(sessao.Pago ? "Sim" : "Não");

                lsvSessoes.Items.Add(item);
            }
        }

        private void FormSessoes_Resize(object sender, EventArgs e) => OrganizarColunas();

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frmSessao = new FormSessao())
            {
                if (frmSessao.ShowDialog() != DialogResult.OK)
                    return;

                using (var conn = new Connection())
                using (var sessao = new Sessao())
                {
                    try
                    {
                        sessao.IdTatuagem = idTatuagem;
                        sessao.Pago = false;
                        frmSessao.SetDadosInModel(sessao);

                        sessao.Salvar(conn, null);

                        MessageBox.Show("Sessão cadastrada com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CarregarSessoes();
                    } catch (Exception erro)
                    {
                        MessageBox.Show("Ocorreu um erro ao cadastrar a sessão!\n" + erro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int idSessao = sessoes[lsvSessoes.SelectedIndices[0]].Id;

            using (var sessao = new Sessao())
            {
                using (var conn = new Connection())
                {
                    if (!sessao.SetById(idSessao, conn, null))
                    {
                        MessageBox.Show($"Não foi encontrada nenhuma sessão com o id `{idSessao}`!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                using (var frmSessao = new FormSessao(sessao))
                {
                    if (frmSessao.ShowDialog() != DialogResult.OK)
                        return;

                    using (var conn = new Connection())
                    {
                        try
                        {
                            frmSessao.SetDadosInModel(sessao);

                            sessao.Salvar(conn, null);

                            MessageBox.Show("Sessão alterada com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CarregarSessoes();
                        }
                        catch (Exception erro)
                        {
                            MessageBox.Show("Ocorreu um erro ao alterar a sessão!\n" + erro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void lsvSessoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int qtdeSelecionada = lsvSessoes.SelectedIndices.Count;

            btnAlterar.Visible = (qtdeSelecionada == 1);
            btnPagar.Visible = ((qtdeSelecionada == 1) && (!sessoes[lsvSessoes.SelectedIndices[0]].Pago));
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente marcar esta sessão como paga?\nEsta ação não poderá ser revertida!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            Sessao sessao = sessoes[lsvSessoes.SelectedIndices[0]];

            try
            {
                using (var conn = new Connection())
                {
                    sessao.MarcarComoPaga(conn, null);
                }

                MessageBox.Show("Sessão marcada como paga com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarSessoes();
            } catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro ao marcar esta sessão como paga!\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}