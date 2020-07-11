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
                    DateTime.TryParse(worksheet.Cells[row, 6].Value?.ToString().Trim(), out dataNascimento);

                    int idCidade = 0;

                    var cidade = new Cidade();
                    if (cidade.GetByCidadeAndUf(worksheet.Cells[row, 19].Value?.ToString().Trim(), worksheet.Cells[row, 20].Value?.ToString().Trim(), null))
                        idCidade = cidade.Id;

                    var item = new ImportacaoClienteTO();

                    item.Cliente.Nome = worksheet.Cells[row, 4].Value?.ToString().Trim() + " " + worksheet.Cells[row, 5].Value?.ToString().Trim();
                    item.Cliente.DataNascimento = dataNascimento;
                    item.Cliente.Cpf = worksheet.Cells[row, 7].Value?.ToString().Trim().Replace(".", String.Empty).Replace("-", String.Empty);
                    item.Cliente.Email = worksheet.Cells[row, 8].Value?.ToString().Trim();
                    item.Cliente.Telefone = worksheet.Cells[row, 9].Value?.ToString().Trim() + worksheet.Cells[row, 10].Value?.ToString().Trim();
                    item.Cliente.Celular = worksheet.Cells[row, 11].Value?.ToString().Trim() + worksheet.Cells[row, 12].Value?.ToString().Trim();
                    item.Cliente.Cep = worksheet.Cells[row, 13].Value?.ToString().Trim();
                    item.Cliente.TipoLogradouro = worksheet.Cells[row, 14].Value?.ToString().Trim();
                    item.Cliente.Logradouro = worksheet.Cells[row, 15].Value?.ToString().Trim();
                    item.Cliente.Numero = worksheet.Cells[row, 16].Value?.ToString().Trim();
                    item.Cliente.Complemento = worksheet.Cells[row, 17].Value?.ToString().Trim();
                    item.Cliente.Bairro = worksheet.Cells[row, 18].Value?.ToString().Trim();
                    item.Cliente.IdCidade = idCidade;

                    if (idCidade != 0)
                        item.Cidade = cidade;

                    item.Tatuagem.Local = worksheet.Cells[row, 21].Value?.ToString();

                    DateTime dataUltimaSessao;
                    DateTime.TryParse(string.IsNullOrEmpty(worksheet.Cells[row, 3].Value?.ToString()) ? worksheet.Cells[row, 2].Value?.ToString().Trim() : worksheet.Cells[row, 3].Value?.ToString().Trim(), out dataUltimaSessao);

                    Decimal valorSessao;
                    Decimal.TryParse(worksheet.Cells[row, 1].Value?.ToString(), out valorSessao);

                    item.Sessao.DataSessao = dataUltimaSessao;
                    item.Sessao.Valor = valorSessao;
                    item.Sessao.Parametros = worksheet.Cells[row, 22].Value?.ToString();
                    item.Sessao.Disparos = worksheet.Cells[row, 23].Value?.ToString();
                    item.Sessao.Pago = true;

                    for (int col = 24; col <= colCount; col++)
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

        private void btnModeloImportacao_Click(object sender, EventArgs e)
        {
            string fileName = String.Empty;

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo Ecxel|*.xls";
                saveFileDialog.DefaultExt = "xls";
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                fileName = saveFileDialog.FileName;
            }

            using (var file = new ExcelPackage())
            using (var sheet = file.Workbook.Worksheets.Add("Modelo"))
            {
                sheet.Cells[1, 1].Value = "valor_pago";
                sheet.Cells[1, 2].Value = "data_primeira_visita";
                sheet.Cells[1, 3].Value = "data_ultima_visita";
                sheet.Cells[1, 4].Value = "nome";
                sheet.Cells[1, 5].Value = "sobrenome";
                sheet.Cells[1, 6].Value = "data_nascimento";
                sheet.Cells[1, 7].Value = "cpf";
                sheet.Cells[1, 8].Value = "email";
                sheet.Cells[1, 9].Value = "ddd_telefone";
                sheet.Cells[1, 10].Value = "telefone";
                sheet.Cells[1, 11].Value = "ddd_celular";
                sheet.Cells[1, 12].Value = "celular";
                sheet.Cells[1, 13].Value = "cep";
                sheet.Cells[1, 14].Value = "tipo_logradouro";
                sheet.Cells[1, 15].Value = "logradouro";
                sheet.Cells[1, 16].Value = "numero";
                sheet.Cells[1, 17].Value = "complemento";
                sheet.Cells[1, 18].Value = "bairro";
                sheet.Cells[1, 19].Value = "cidade";
                sheet.Cells[1, 20].Value = "estado";
                sheet.Cells[1, 21].Value = "area_corpo";
                sheet.Cells[1, 22].Value = "parametros";
                sheet.Cells[1, 23].Value = "quantidade_disparos";

                using (var conexao = new Connection(Database.Local))
                {
                    List<Pergunta> perguntas = Pergunta.GetAllAtivas(conexao, null);

                    int i = 0;
                    foreach (Pergunta pergunta in perguntas)
                    {
                        sheet.Cells[1, 24 + i].Value = pergunta.CodigoImportacao;
                        i++;
                    }
                }

                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                sheet.Cells[sheet.Dimension.Address].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                file.SaveAs(new FileInfo(fileName));
            }

            MessageBox.Show("Arquivo salvo com sucesso no local selecionado", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
