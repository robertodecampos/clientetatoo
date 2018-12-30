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
    public partial class FormFiltroCliente : Form
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public FormFiltroCliente()
        {
            InitializeComponent();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Nome = txtNome.Text;
            Cpf = txtCpf.Text.Replace(".", "").Replace("-", "").Replace(" ", ""); ;
            Email = txtEmail.Text;
            Telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            Celular = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", "").Replace(" ", "");
        }

        private void FormFiltroCliente_Load(object sender, EventArgs e)
        {
            txtNome.Text = Nome;
            txtCpf.Text = Cpf;
            txtEmail.Text = Email;
            txtTelefone.Text = Telefone;
            txtCelular.Text = Celular;
        }
    }
}
