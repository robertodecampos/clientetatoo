using ClienteTatoo.Control;
using ClienteTatoo.Exceptions;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormTatuagens : Form
    {
        private enum PassoCadastro { TermoResponsabilidade, Informacoes, Pesquisa }

        private int IdCliente { get; set; }
        private List<Tatuagem> Tatuagens { get; set; }

        public FormTatuagens(int idCliente)
        {
            InitializeComponent();

            IdCliente = idCliente;

            OrganizarColunas();

            CarregarTatuagens();
        }

        private void CarregarTatuagens()
        {
            lsvTatuagens.Items.Clear();

            using (var conn = new Connection())
            {
                Tatuagens = Tatuagem.GetByIdCliente(IdCliente, conn, null);

                foreach (Tatuagem tatuagem in Tatuagens)
                {
                    var item = new ListViewItem();

                    item.Text = tatuagem.Id.ToString();
                    item.SubItems.Add(tatuagem.Local);
                    item.SubItems.Add(tatuagem.Desenho);

                    int qtdeSessoes = Sessao.CountByIdTatuagem(tatuagem.Id, conn, null);

                    item.SubItems.Add(qtdeSessoes.ToString());

                    DateTime? dataPrimeiraSessao = null, dataUltimaSessao = null;

                    if (qtdeSessoes > 0)
                    {
                        dataPrimeiraSessao = Sessao.GetDataSessaoOfFirstByIdTatuagem(tatuagem.Id, conn, null);

                        if (qtdeSessoes > 1)
                            dataUltimaSessao = Sessao.GetDataSessaoOfLastByIdTatuagem(tatuagem.Id, conn, null);
                        else
                            dataUltimaSessao = dataPrimeiraSessao;
                    }

                    item.SubItems.Add(dataPrimeiraSessao == null ? "Sem sessão" : ((DateTime)dataPrimeiraSessao).ToString("dd/MM/yyyy"));
                    item.SubItems.Add(dataUltimaSessao == null ? "Sem sessão" : ((DateTime)dataUltimaSessao).ToString("dd/MM/yyyy"));

                    lsvTatuagens.Items.Add(item);
                }
            }
        }

        private void OrganizarColunas()
        {
            int width = lsvTatuagens.ClientSize.Width;

            for (int i = 0; i < lsvTatuagens.Columns.Count; i++)
                width -= lsvTatuagens.Columns[i].Width;

            int sobra = width % 2;

            lsvTatuagens.Columns[1].Width += (width / 2) + sobra;
            lsvTatuagens.Columns[2].Width += width / 2;
        }

        private void FormTatuagens_Resize(object sender, EventArgs e) => OrganizarColunas();

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var conn = new Connection())
            using (var termoResponsabilidade = new TermoResponsabilidade())
            {
                if (!termoResponsabilidade.SetCurrent(conn, null))
                {
                    MessageBox.Show("Não existe nehum termo de responsabilidade cadastrado!\nCadastre um termo de responsabilidade antes de cadastrar uma tatuagem", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                using (var frmTermoResponsabilidade = new FormTermoResponsabilidade())
                using (var frmTatuagem = new FormTatuagem(TipoAcao.Cadastro))
                using (var frmPesquisa = new FormPesquisa(TipoPergunta.Tatuagem, PesquisaControl.TipoFonte.Grande, false))
                {
                    frmTatuagem.btnSalvar.Text = "Avançar";
                    frmPesquisa.btnVoltar.Visible = true;

                    PassoCadastro passo = PassoCadastro.TermoResponsabilidade;

                    bool finalizado = false;

                    while (!finalizado)
                    {
                        DialogResult dr;

                        switch (passo)
                        {
                            case PassoCadastro.TermoResponsabilidade:
                                if (frmTermoResponsabilidade.ShowDialog() == DialogResult.OK)
                                    passo = PassoCadastro.Informacoes;
                                else
                                    return;
                                break;
                            case PassoCadastro.Informacoes:
                                dr = frmTatuagem.ShowDialog();
                                if (dr == DialogResult.OK)
                                    passo = PassoCadastro.Pesquisa;
                                else if (dr == DialogResult.Retry)
                                    passo = PassoCadastro.TermoResponsabilidade;
                                else
                                    return;
                                break;
                            case PassoCadastro.Pesquisa:
                                dr = frmPesquisa.ShowDialog();
                                if (dr == DialogResult.OK)
                                    finalizado = true;
                                else if (dr == DialogResult.Retry)
                                    passo = PassoCadastro.Informacoes;
                                else
                                    return;
                                break;
                        }
                    }

                    using (var conn = new Connection())
                    using (SQLiteTransaction transaction = conn.BeginTransaction())
                    using (var tatuagem = new Tatuagem())
                    {
                        try
                        {
                            tatuagem.IdCliente = IdCliente;
                            tatuagem.IdTermoResponsabilidade = frmTermoResponsabilidade.IdTermoResponsabilidade;
                            frmTatuagem.SetDadosInModel(tatuagem);
                            tatuagem.Salvar(conn, transaction);

                            Resposta.SalvarRespostas(TipoPergunta.Tatuagem, tatuagem.Id, frmPesquisa.Respostas, conn, transaction);

                            transaction.Commit();

                            MessageBox.Show("Tatuagem inserida com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CarregarTatuagens();
                        }
                        catch (Exception erro)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ocorreu um erro ao inserir a tatuagem!\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } catch (PerguntasNotFoundException erro)
            {
                MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lsvTatuagens_SelectedIndexChanged(object sender, EventArgs e)
        {
            int qtdeSelecionada = lsvTatuagens.SelectedIndices.Count;

            btnAlterar.Visible = (qtdeSelecionada == 1);
            btnVisualizarRespostas.Visible = (qtdeSelecionada == 1);
            btnVisualizarTermoResponsabilidade.Visible = (qtdeSelecionada == 1 && (Tatuagens[lsvTatuagens.SelectedIndices[0]].IdTermoResponsabilidade != 0));
            btnSessoes.Visible = (qtdeSelecionada == 1);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (lsvTatuagens.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione uma tatuagem para ser alterada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (lsvTatuagens.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Selecione somente uma tatuagem para ser alterada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTatuagem = Tatuagens[lsvTatuagens.SelectedIndices[0]].Id;

            using (var tatuagem = new Tatuagem())
            {
                using (var conn = new Connection())
                {
                    if (!tatuagem.SetById(idTatuagem, conn, null))
                    {
                        MessageBox.Show($"Não foi possível encontrar a tatuagem com o código `{idTatuagem}`!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CarregarTatuagens();
                        return;
                    }
                }

                using (var frmTatuagem = new FormTatuagem(TipoAcao.Edicao, tatuagem))
                {
                    if (frmTatuagem.ShowDialog() != DialogResult.OK)
                        return;

                    using (var conn = new Connection())
                    {
                        try
                        {
                            frmTatuagem.SetDadosInModel(tatuagem);
                            tatuagem.Salvar(conn, null);
                            MessageBox.Show("Tatuagem alterada com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CarregarTatuagens();
                        } catch (Exception erro)
                        {
                            MessageBox.Show("Ocoreu um erro ao salvar as alterações!\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnSessoes_Click(object sender, EventArgs e)
        {
            if (lsvTatuagens.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione uma tatuagem para visualizar as sessões!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (lsvTatuagens.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Selecione somente uma tatuagem para visualizar as sessões!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTatuagem = Tatuagens[lsvTatuagens.SelectedIndices[0]].Id;

            using (var frmSessoes = new FormSessoes(idTatuagem))
            {
                frmSessoes.ShowDialog();
            }
        }

        private void btnVisualizarRespostas_Click(object sender, EventArgs e)
        {
            if (lsvTatuagens.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione uma tatuagem para visualizar as respostas!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (lsvTatuagens.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Selecione somente uma tatuagem para visualizar as respostas!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var frmPesquisa = new FormPesquisa(TipoPergunta.Tatuagem, PesquisaControl.TipoFonte.Normal, true, Tatuagens[lsvTatuagens.SelectedIndices[0]].Id))
                {
                    frmPesquisa.ShowDialog();
                }
            } catch (PerguntasNotFoundException erro)
            {
                MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVisualizarTermoResponsabilidade_Click(object sender, EventArgs e)
        {
            if (lsvTatuagens.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione uma tatuagem para visualizar o termo de responsabilidade!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (lsvTatuagens.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Selecione somente uma tatuagem para visualizar o termo de responsabilidade!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frmTermoResponsabilidade = new FormTermoResponsabilidade(Tatuagens[lsvTatuagens.SelectedIndices[0]].IdTermoResponsabilidade))
            {
                frmTermoResponsabilidade.ShowDialog();
            }
        }
    }
}
