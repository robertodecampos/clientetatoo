using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using ClienteTatoo.Model;
using ClienteTatoo.TO;
using ClienteTatoo.Utils;

namespace ClienteTatoo
{
    public partial class frmImportacaoSelecionarArquivo : Form
    {
        private List<ImportacaoClienteTO> items;

        public frmImportacaoSelecionarArquivo()
        {
            InitializeComponent();

            items = new List<ImportacaoClienteTO>();
        }

        private void btnProcurarArquivo_Click(object sender, EventArgs e)
        {
            if (ofdArquivo.ShowDialog() != DialogResult.OK)
                return;

            edtArquivo.Text = ofdArquivo.FileName;

            items.Clear();

            using (var arquivo = new ExcelPackage(new FileInfo(ofdArquivo.FileName)))
            using (var worksheet = arquivo.Workbook.Worksheets[1])
            using (var conexao = new Connection(Database.Local))
            {
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    DateTime dataNascimento;
                    DateTime.TryParse(worksheet.Cells[row, 7].Value?.ToString().Trim(), out dataNascimento);

                    int idCidade = 0;

                    var cidade = new Cidade();
                    if (cidade.GetByCidadeAndUf(worksheet.Cells[row, 22].Value?.ToString().Trim(), worksheet.Cells[row, 23].Value?.ToString().Trim(), null))
                        idCidade = cidade.Id;

                    var item = new ImportacaoClienteTO();

                    item.Cliente.Nome = worksheet.Cells[row, 5].Value?.ToString().Trim() + " " + worksheet.Cells[row, 6].Value?.ToString().Trim();
                    item.Cliente.DataNascimento = dataNascimento;
                    item.Cliente.Cpf = worksheet.Cells[row, 9].Value?.ToString().Trim().Replace(".", String.Empty).Replace("-", String.Empty);
                    item.Cliente.Email = worksheet.Cells[row, 11].Value?.ToString().Trim();
                    item.Cliente.Telefone = worksheet.Cells[row, 12].Value?.ToString().Trim() + worksheet.Cells[row, 13].Value?.ToString().Trim();
                    item.Cliente.Celular = worksheet.Cells[row, 14].Value?.ToString().Trim() + worksheet.Cells[row, 15].Value?.ToString().Trim();
                    item.Cliente.Cep = worksheet.Cells[row, 16].Value?.ToString().Trim();
                    item.Cliente.TipoLogradouro = worksheet.Cells[row, 17].Value?.ToString().Trim();
                    item.Cliente.Logradouro = worksheet.Cells[row, 18].Value?.ToString().Trim();
                    item.Cliente.Numero = worksheet.Cells[row, 19].Value?.ToString().Trim();
                    item.Cliente.Complemento = worksheet.Cells[row, 20].Value?.ToString().Trim();
                    item.Cliente.Bairro = worksheet.Cells[row, 21].Value?.ToString().Trim();
                    item.Cliente.IdCidade = idCidade;

                    if (idCidade != 0)
                        item.Cidade = cidade;

                    item.Tatuagem.Local = worksheet.Cells[row, 24].Value?.ToString();

                    DateTime dataUltimaSessao;
                    DateTime.TryParse(string.IsNullOrEmpty(worksheet.Cells[row, 3].Value?.ToString()) ? worksheet.Cells[row, 2].Value?.ToString().Trim() : worksheet.Cells[row, 3].Value?.ToString().Trim(), out dataUltimaSessao);

                    Decimal valorSessao;
                    Decimal.TryParse(worksheet.Cells[row, 1].Value?.ToString(), out valorSessao);

                    item.Sessao.DataSessao = dataUltimaSessao;
                    item.Sessao.Valor = valorSessao;
                    item.Sessao.Parametros = worksheet.Cells[row, 25].Value?.ToString();
                    item.Sessao.Disparos = worksheet.Cells[row, 26].Value?.ToString();
                    item.Sessao.Pago = true;

                    for (int col = 27; col <= colCount; col++)
                    {
                        Pergunta pergunta = Pergunta.GetAtivaByCodigoImportacao(worksheet.Cells[1, col].Value?.ToString().Trim(), conexao, null);

                        if (pergunta == null)
                            continue;

                        var resposta = new Resposta()
                        {
                            IdPergunta = pergunta.Id
                        };

                        if (pergunta.Dissertativa)
                            resposta.RespostaDissertativa = worksheet.Cells[row, col].Value?.ToString().Trim();
                        else
                        {
                            Alternativa alternativa = Alternativa.GetAtivaByIdPerguntaAndDescricao(pergunta.Id, worksheet.Cells[row, col].Value?.ToString().Trim(), conexao, null);

                            if (alternativa == null)
                                continue;

                            resposta.IdAlternativa = alternativa.Id;
                        }

                        item.Respostas.Add(resposta);
                    }

                    items.Add(item);
                }
            }

            lblQtdeClientesCarregados.Text = items.Count.ToString();
            lblClientesCarregados.Visible = true;
            lblQtdeClientesCarregados.Visible = true;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(edtArquivo.Text))
            {
                MessageBox.Show("Por favor, selecione um arquivo para ser importado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frmImporatacaoVisualizarDados = new frmImporatacaoVisualizarDados(items, ofdArquivo.FileName))
            {
                frmImporatacaoVisualizarDados.ShowDialog();
                Close();
            }
        }
    }
}
