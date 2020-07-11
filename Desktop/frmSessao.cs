using ClienteTatoo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormSessao : Form
    {
        public FormSessao()
        {
            InitializeComponent();
        }

        public FormSessao(Sessao sessao) : this()
        {
            txtValor.Text = sessao.Valor.ToString("#,##0.00", new CultureInfo("pt-BR"));
            txtParametros.Text = sessao.Parametros;
            dtpDataSessao.Value = sessao.DataSessao;
            txtDisparos.Text = sessao.Disparos;
            txtObservacao.Text = sessao.Observacao;
        }

        public void SetDadosInModel(Sessao model)
        {
            model.Valor = GetValor();
            model.Parametros = txtParametros.Text;
            model.DataSessao = dtpDataSessao.Value;
            model.Disparos = txtDisparos.Text;
            model.Observacao = txtObservacao.Text;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (GetValor() == decimal.Zero)
            {
                MessageBox.Show("O campo `Valor` está em um formato desconhecido!\nDigite somente números, virgura para separar as casas decimais e ponto para separar os milhares!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            using (var sessao = new Sessao())
            {
                SetDadosInModel(sessao);

                string mensagem;
                if (!sessao.IsValid(out mensagem))
                {
                    MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
            }
        }

        private decimal GetValor()
        {
            decimal valor;
            if (!decimal.TryParse(txtValor.Text.Replace(".", "").Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out valor))
                valor = decimal.Zero;

            return valor;
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            txtValor.Text = GetValor().ToString("#,##0.00", new CultureInfo("pt-BR"));
        }
    }
}
