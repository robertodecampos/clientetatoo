﻿using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormTatuagens : Form
    {
        private enum PassoCadastro { TermoResponsabilidade, Informacoes }

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
            using (var conn = new Connection())
            {
                Tatuagens = Tatuagem.GetByIdCliente(IdCliente, conn, null);
            }

            lsvTatuagens.Items.Clear();

            foreach (Tatuagem tatuagem in Tatuagens)
            {
                var item = new ListViewItem();

                item.Text = tatuagem.Id.ToString();
                item.SubItems.Add(tatuagem.Local);
                item.SubItems.Add(tatuagem.Desenho);

                lsvTatuagens.Items.Add(item);
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

            lsvTatuagens.Scrollable = false; // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
            lsvTatuagens.Scrollable = true;  // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
        }

        private void FormTatuagens_Resize(object sender, EventArgs e) => OrganizarColunas();

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frmTermoResponsabilidade = new FormTermoResponsabilidade())
            using (var frmTatuagem = new FormTatuagem(TipoAcao.Cadastro))
            {
                PassoCadastro passo = PassoCadastro.TermoResponsabilidade;

                bool finalizado = false;

                while (!finalizado)
                {
                    switch (passo)
                    {
                        case PassoCadastro.TermoResponsabilidade:
                            if (frmTermoResponsabilidade.ShowDialog() == DialogResult.OK)
                                passo = PassoCadastro.Informacoes;
                            else
                                return;
                            break;
                        case PassoCadastro.Informacoes:
                            DialogResult dr = frmTatuagem.ShowDialog();
                            if (dr == DialogResult.OK)
                                finalizado = true;
                            else if (dr == DialogResult.Cancel)
                                passo = PassoCadastro.TermoResponsabilidade;
                            else return;
                            break;
                    }
                }

                using (var conn = new Connection())
                using (var tatuagem = new Tatuagem())
                {
                    try
                    {
                        tatuagem.IdCliente = IdCliente;
                        tatuagem.IdTermoResponsabilidade = frmTermoResponsabilidade.IdTermoResponsabilidade;
                        frmTatuagem.SetDadosInModel(tatuagem);
                        tatuagem.Salvar(conn, null);
                        MessageBox.Show("Tatuagem inserida com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarTatuagens();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Ocorreu um erro ao inserir a tatuagem!\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void lsvTatuagens_SelectedIndexChanged(object sender, EventArgs e) => btnAlterar.Visible = (lsvTatuagens.SelectedIndices.Count == 1);

        private void btnAlterar_Click(object sender, EventArgs e)
        {
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
    }
}