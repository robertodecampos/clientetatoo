using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public enum TipoFormulario { tfCadastro, tfEdicao }

    public partial class FormDadosPessoaisCliente : Form
    {
        private List<Estado> Estados { get; set; }
        private List<Cidade> Cidades { get; set; }
        private TipoFormulario TipoFormulario { get; set; }
        public Cliente Cliente { get; set; }

        public FormDadosPessoaisCliente(TipoFormulario tipoFormulario)
        {
            InitializeComponent();

            txtCpf.Clear();
            dtpDataNascimento.Value = DateTime.Now;
            dtpDataNascimento.MaxDate = DateTime.Now;
            txtTelefone.Clear();
            txtCelular.Clear();
            txtCep.Clear();

            TipoFormulario = tipoFormulario;

            switch (TipoFormulario)
            {
                case TipoFormulario.tfCadastro:
                    btnOk.Text = "Avançar";
                    btnAbort.Visible = true;
                    break;
                case TipoFormulario.tfEdicao:
                    btnOk.Text = "Salvar";
                    break;
            }

            using (var conn = new Connection())
            {
                cmbEstado.Items.Clear();
                cmbEstado.Items.Add("Selecione o Estado...");

                Estados = Estado.GetAll(conn, null);

                foreach (Estado estado in Estados)
                    cmbEstado.Items.Add(estado.Nome);

                cmbEstado.SelectedIndex = 0;

                cmbTipoLogradouro.Items.Clear();

                IList<TipoLogradouro> tiposLogradouro = TipoLogradouro.GetAll(conn, null);

                foreach (TipoLogradouro tipoLogradouro in tiposLogradouro)
                    cmbTipoLogradouro.Items.Add(tipoLogradouro.Nome);

                cmbTipoLogradouro.Text = "";

            }
        }

        public FormDadosPessoaisCliente(TipoFormulario tipoFormulario, Cliente cliente) : this(tipoFormulario)
        {
            if (cliente == null)
                throw new NullReferenceException("O parâmetro cliente não pode ser nulo!");

            txtNome.Text = cliente.Nome;
            txtCpf.Text = cliente.Cpf;
            dtpDataNascimento.Value = cliente.DataNascimento;
            txtTelefone.Text = cliente.Telefone;
            txtCelular.Text = cliente.Celular;
            txtEmail.Text = cliente.Email;
            try
            {
                //txtCep.Changed
            }
            finally
            {

            }
        }

        private void FormDadosPessoaisCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Cancel)
                return;

            if (TipoFormulario != TipoFormulario.tfCadastro)
                return;

            if (MessageBox.Show("Deseja realmente cancelar o cadastro do cliente?\nAs informações não serão salvas!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex - 1;

            if (index >= 0)
                CarregarCidades(Estados[((ComboBox)sender).SelectedIndex - 1].Uf);
            else
            {
                cmbCidade.Items.Clear();
                cmbCidade.Items.Add("Primeiro Selecione a Cidade");
                cmbCidade.SelectedIndex = 0;
                cmbCidade.Enabled = false;
            }
        }

        private void CarregarCidades(string uf)
        {
            using (var conn = new Connection())
            {
                cmbCidade.Items.Clear();
                cmbCidade.Items.Add("Selecione a Cidade...");

                Cidades = Cidade.GetByUf(uf, conn, null);

                foreach (Cidade cidade in Cidades)
                    cmbCidade.Items.Add(cidade.Nome);

                cmbCidade.SelectedIndex = 0;

                cmbCidade.Enabled = true;
            }
        }

        private void txtCep_TextChanged(object sender, EventArgs e)
        {
            string cep = ((MaskedTextBox)sender).Text.Replace("-", "").Trim();

            if (cep.Length != 8)
                return;

            using (var conexao = new Connection())
            {
                var endereco = new Endereco();
                if (!endereco.SearchByCep(cep, conexao, null))
                {
                    cmbEstado.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(endereco.Uf))
                {
                    try
                    {
                        cmbEstado.SelectedIndexChanged -= cmbEstado_SelectedIndexChanged;
                        cmbEstado.SelectedIndex = Estados.FindIndex(estado => estado.Uf == endereco.Uf) + 1;
                        CarregarCidades(endereco.Uf);
                        if (endereco.IdCidade != 0)
                            cmbCidade.SelectedIndex = Cidades.FindIndex(cidade => cidade.Id == endereco.IdCidade) + 1;
                    }
                    finally
                    {
                        cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
                    }
                }

                if (!string.IsNullOrEmpty(endereco.TipoLogradouro))
                {
                    cmbTipoLogradouro.Text = endereco.TipoLogradouro;
                    txtLogradouro.Focus();
                }

                if (!string.IsNullOrEmpty(endereco.Logradouro))
                {
                    txtLogradouro.Text = endereco.Logradouro;
                    txtComplemento.Focus();
                }

                if (!string.IsNullOrEmpty(endereco.Complemento))
                {
                    txtComplemento.Text = endereco.Complemento;
                    txtBairro.Focus();
                }

                if (!string.IsNullOrEmpty(endereco.Bairro))
                {
                    txtBairro.Text = endereco.Bairro;
                    txtNumero.Focus();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cliente = new Cliente();

            Cliente.Nome = txtNome.Text.Trim();
            Cliente.DataNascimento = dtpDataNascimento.Value;
            Cliente.Cpf = txtCpf.Text.Replace(" ", "").Replace(".", "").Replace("-", "");
            Cliente.Email = txtEmail.Text.Trim();
            Cliente.Telefone = txtTelefone.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
            Cliente.Celular = txtCelular.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", "");
            Cliente.Cep = txtCep.Text.Replace(" ", "").Replace("-", "");
            Cliente.Uf = "";
            if (cmbEstado.SelectedIndex > 0)
                Cliente.Uf = Estados[cmbEstado.SelectedIndex - 1].Uf;
            Cliente.IdCidade = 0;
            if (cmbCidade.SelectedIndex > 0)
                Cliente.IdCidade = Cidades[cmbCidade.SelectedIndex - 1].Id;
            Cliente.TipoLogradouro = cmbTipoLogradouro.Text;
            Cliente.Logradouro = txtLogradouro.Text;
            Cliente.Complemento = txtComplemento.Text;
            Cliente.Bairro = txtBairro.Text;
            Cliente.Numero = txtNumero.Text;

            string mensagem = null;
            if (!Cliente.IsValid(out mensagem))
            {
                MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
        }
    }
}
