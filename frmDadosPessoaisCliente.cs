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
        private Cliente Cliente { get; set; }
        private int IdCliente { get; set; } = 0;

        public FormDadosPessoaisCliente()
        {
            InitializeComponent();

            txtCpf.Clear();
            txtTelefone.Clear();
            txtCelular.Clear();
            txtCep.Clear();

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Selecione o Estado...");

            Estados = Estado.GetAll(null);

            foreach (Estado estado in Estados)
                cmbEstado.Items.Add(estado.Nome);

            cmbEstado.SelectedIndex = 0;

            cmbTipoLogradouro.Items.Clear();

            IList<TipoLogradouro> tiposLogradouro = TipoLogradouro.GetAll(null);

            foreach (TipoLogradouro tipoLogradouro in tiposLogradouro)
                cmbTipoLogradouro.Items.Add(tipoLogradouro.Nome);

            cmbTipoLogradouro.Text = "";
        }

        public FormDadosPessoaisCliente(Cliente cliente) : this()
        {
            if (cliente == null)
                throw new NullReferenceException("O parâmetro cliente não pode ser nulo!");

            try
            {
                txtCep.TextChanged -= txtCep_TextChanged;

                IdCliente = cliente.Id;

                txtNome.Text = cliente.Nome;
                txtCpf.Text = cliente.Cpf;
                if (cliente.DataNascimento != null)
                    txtDataNascimento.Text = ((DateTime)cliente.DataNascimento).ToString("dd/MM/yyyy");
                txtTelefone.Text = cliente.Telefone;
                txtCelular.Text = cliente.Celular;
                txtEmail.Text = cliente.Email;
                txtCep.Text = cliente.Cep;
                if (!string.IsNullOrEmpty(cliente.Uf))
                {
                    cmbEstado.SelectedIndex = Estados.FindIndex(estado => estado.Uf == cliente.Uf) + 1;
                    if (cliente.IdCidade != 0)
                        cmbCidade.SelectedIndex = Cidades.FindIndex(cidade => cidade.Id == cliente.IdCidade) + 1;
                }
                cmbTipoLogradouro.Text = cliente.TipoLogradouro;
                txtLogradouro.Text = cliente.Logradouro;
                txtComplemento.Text = cliente.Complemento;
                txtBairro.Text = cliente.Bairro;
                txtNumero.Text = cliente.Numero;
            }
            finally
            {
                txtCep.TextChanged += txtCep_TextChanged;
            }
        }

        public void SetDadosInModel(Cliente model)
        {
            model.Nome = Cliente.Nome;
            model.DataNascimento = Cliente.DataNascimento;
            model.Cpf = Cliente.Cpf;
            model.Email = Cliente.Email;
            model.Telefone = Cliente.Telefone;
            model.Celular = Cliente.Celular;
            model.Cep = Cliente.Cep;
            model.Uf = Cliente.Uf;
            model.IdCidade = Cliente.IdCidade;
            model.TipoLogradouro = Cliente.TipoLogradouro;
            model.Logradouro = Cliente.Logradouro;
            model.Complemento = Cliente.Complemento;
            model.Bairro = Cliente.Bairro;
            model.Numero = Cliente.Numero;
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
            cmbCidade.Items.Clear();
            cmbCidade.Items.Add("Selecione a Cidade...");

            Cidades = Cidade.GetByUf(uf, null);

            foreach (Cidade cidade in Cidades)
                cmbCidade.Items.Add(cidade.Nome);

            cmbCidade.SelectedIndex = 0;

            cmbCidade.Enabled = true;
        }

        private void txtCep_TextChanged(object sender, EventArgs e)
        {
            string cep = ((MaskedTextBox)sender).Text.Replace("-", "").Trim();

            if (cep.Length != 8)
                return;

            var endereco = new Endereco();
            if (!endereco.SearchByCep(cep, null))
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cliente = new Cliente();

            Cliente.Id = IdCliente;

            Cliente.Nome = txtNome.Text.Trim();

            DateTime dataNascimento;
            
            if (!DateTime.TryParse(txtDataNascimento.Text, out dataNascimento))
            {
                MessageBox.Show("A data não está em um formato válido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            Cliente.DataNascimento = dataNascimento;
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
            using (var conn = new Connection())
            {
                if (!Cliente.IsValid(conn, null, out mensagem))
                {
                    MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                }
            }
        }
    }
}
