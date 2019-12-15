using ClienteTatoo.Model;
using ClienteTatoo.TO;
using ClienteTatoo.Utils;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class frmImporatacaoVisualizarDados : Form
    {
        private List<ImportacaoClienteTO> lstDados;
        private string caminhoArquivo;

        private frmImporatacaoVisualizarDados()
        {
            InitializeComponent();
        }

        public frmImporatacaoVisualizarDados(List<ImportacaoClienteTO> lstDados, string caminhoArquivo) : this()
        {
            this.lstDados = lstDados;
            this.caminhoArquivo = caminhoArquivo;

            lstDadosImportacao.Columns.Add(new ColumnHeader() {
                Text = "Nome"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "CPF"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Data de Nascimento"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "E-mail"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Telefone"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Celular"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "CEP"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Tipo de Logradouro"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Logradouro"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Número"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Complemento"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Bairro"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Cidade"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Estado"
            });

            lstDadosImportacao.Columns.Add(new ColumnHeader()
            {
                Text = "Mensagem"
            });

            PreencherListView();
        }

        private void PreencherListView()
        {
            using (var conn = new Connection(Database.Local))
            using (var transaction = conn.BeginTransaction())
            {
                int qtdeSucesso = 0, qtdeProblema = 0;

                foreach (ImportacaoClienteTO dadosCliente in lstDados)
                {
                    Estado estado = null;

                    if (dadosCliente.Cidade != null)
                    {
                        estado = new Estado();
                        estado.GetByUf(dadosCliente.Cidade.Uf);
                    }

                    ListViewItem item = new ListViewItem()
                    {
                        Text = dadosCliente.Cliente.Nome
                    };

                    item.SubItems.Add(dadosCliente.Cliente.Cpf);
                    item.SubItems.Add(dadosCliente.Cliente.DataNascimento?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(dadosCliente.Cliente.Email);
                    item.SubItems.Add(dadosCliente.Cliente.Telefone);
                    item.SubItems.Add(dadosCliente.Cliente.Celular);
                    item.SubItems.Add(dadosCliente.Cliente.Cep);
                    item.SubItems.Add(dadosCliente.Cliente.TipoLogradouro);
                    item.SubItems.Add(dadosCliente.Cliente.Logradouro);
                    item.SubItems.Add(dadosCliente.Cliente.Numero);
                    item.SubItems.Add(dadosCliente.Cliente.Complemento);
                    item.SubItems.Add(dadosCliente.Cliente.Bairro);
                    item.SubItems.Add(dadosCliente.Cidade?.Nome);
                    item.SubItems.Add(estado?.Nome);

                    if (!string.IsNullOrEmpty(dadosCliente.Cliente.Celular))
                    {
                        if (Cliente.ExistsByCelular(dadosCliente.Cliente.Celular, 0, conn, transaction))
                        {
                            dadosCliente.Mensagem = "Já existe um cliente com esse celular no sistema!";
                            dadosCliente.Ignorar = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(dadosCliente.Cliente.Email) && !dadosCliente.Ignorar)
                    {
                        if (Cliente.ExistsByEmail(dadosCliente.Cliente.Email, 0, conn, transaction))
                        {
                            dadosCliente.Mensagem = "Já existe um cliente com esse e-mail no sistema!";
                            dadosCliente.Ignorar = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(dadosCliente.Cliente.Nome) && !dadosCliente.Ignorar)
                    {
                        if (Cliente.ExistsByNome(dadosCliente.Cliente.Nome, 0, conn, transaction))
                        {
                            dadosCliente.Mensagem = "Já existe um cliente com esse nome no sistema!";
                            dadosCliente.Ignorar = true;
                        }
                    }

                    if (string.IsNullOrEmpty(dadosCliente.Cliente.Celular) && string.IsNullOrEmpty(dadosCliente.Cliente.Email) && string.IsNullOrEmpty(dadosCliente.Cliente.Nome) && !dadosCliente.Ignorar)
                    {
                        dadosCliente.Mensagem = "Não foi possível verificar duplicidade, esse registro não possui Celular, E-mail e Nome!";
                        dadosCliente.Ignorar = true;
                    }

                    try
                    {
                        dadosCliente.Cliente.Salvar(true, conn, transaction);
                    } catch (Exception e)
                    {
                        dadosCliente.Mensagem = e.Message;
                        dadosCliente.Ignorar = true;
                    }

                    if (dadosCliente.Ignorar)
                    {
                        item.SubItems.Add(dadosCliente.Mensagem);
                        qtdeProblema += 1;
                    }
                    else
                    {
                        item.SubItems.Add("Será importado com sucesso!");
                        qtdeSucesso += 1;
                    }

                    lstDadosImportacao.Items.Add(item);

                    lblLinhasSucesso.Text = qtdeSucesso.ToString();
                    lblLinhasProblema.Text = qtdeProblema.ToString();
                }

                transaction.Rollback();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            using (var arquivo = new ExcelPackage(new FileInfo(caminhoArquivo)))
            using (var worksheet = arquivo.Workbook.Worksheets[1])
            using (var conexao = new Connection(Database.Local))
            {
                int i, qtdeIgnorados = 0;
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                worksheet.Cells[1, colCount + 1].Value = "Mensagem Importação";

                for (i = 2; i <= rowCount; i++)
                {
                    ImportacaoClienteTO dadosCliente = lstDados[i - 2];

                    if (dadosCliente.Ignorar)
                    {
                        worksheet.Cells[i, colCount + 1].Value = dadosCliente.Mensagem;
                        qtdeIgnorados++;
                        continue;
                    }

                    List<Pergunta> perguntas = Pergunta.GetAllAtivas(conexao, null);

                    SQLiteTransaction transaction = conexao.BeginTransaction();

                    try
                    {
                        dadosCliente.Cliente.Id = 0;
                        dadosCliente.Cliente.Salvar(true, conexao, transaction);
                        dadosCliente.Tatuagem.IdCliente = dadosCliente.Cliente.Id;
                        dadosCliente.Tatuagem.Salvar(conexao, transaction);
                        dadosCliente.Sessao.IdTatuagem = dadosCliente.Tatuagem.Id;
                        dadosCliente.Sessao.Salvar(conexao, transaction);

                        foreach (Resposta resposta in dadosCliente.Respostas)
                        {
                            if (perguntas.Where(pergunta => pergunta.Id == resposta.IdPergunta).ToArray()[0].Tipo == TipoPergunta.Cliente)
                            {
                                resposta.IdReferencia = dadosCliente.Cliente.Id;
                            } else
                            {
                                resposta.IdReferencia = dadosCliente.Tatuagem.Id;
                            }

                            resposta.Salvar(conexao, transaction);
                        }

                        transaction.Commit();

                        worksheet.Cells[i, colCount + 1].Value = "Importado com sucesso!";
                    }
                    catch (Exception erro)
                    {
                        worksheet.Cells[i, colCount + 1].Value = erro.Message;
                        transaction.Rollback();
                    }
                }

                if (qtdeIgnorados == 0)
                {
                    MessageBox.Show("Importação finalizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string mensagem = "Existem linhas com problemas e por isso não foram importadas, deseja salvar o arquivo com as inconsistências?";

                    if (MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        using (var sfdArquivo = new SaveFileDialog())
                        {
                            sfdArquivo.Filter = "Arquivo Excel|*.xlsx";

                            if (sfdArquivo.ShowDialog() == DialogResult.OK)
                            {
                                if (File.Exists(sfdArquivo.FileName))
                                {
                                    File.Delete(sfdArquivo.FileName);
                                }

                                arquivo.SaveAs(new FileInfo(sfdArquivo.FileName));
                            }
                        }
                    }
                }
            }

            Close();
        }
    }
}
