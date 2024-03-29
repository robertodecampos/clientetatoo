﻿using ClienteTatoo.Model.Filter;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
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
        public List<ClienteFilter> Filtro { get; set; }

        public FormFiltroCliente()
        {
            InitializeComponent();
            Filtro = new List<ClienteFilter>();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string cpf = txtCpf.Text.Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);

            if ((cpf != string.Empty) && !Validation.IsCpf(cpf))
            {
                MessageBox.Show("Este CPF é inválido!\nDigite um CPF válido ou deixe o campo vazio!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            Nome = txtNome.Text.Trim();
            Cpf = cpf;
            Email = txtEmail.Text.Trim();
            Telefone = txtTelefone.Text.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);
            Celular = txtTelefone.Text.Replace("(", string.Empty).Replace(")", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);

            Filtro.Clear();

            if (Nome != string.Empty)
                Filtro.Add(new ClienteFilter(FieldFilterCliente.ffcNome, Nome));

            if (Cpf != string.Empty)
                Filtro.Add(new ClienteFilter(FieldFilterCliente.ffcCpf, Cpf));

            if (Email != string.Empty)
                Filtro.Add(new ClienteFilter(FieldFilterCliente.ffcEmail, Email));

            if (Telefone != string.Empty)
                Filtro.Add(new ClienteFilter(FieldFilterCliente.ffcTelefone, Telefone));

            if (Celular != string.Empty)
                Filtro.Add(new ClienteFilter(FieldFilterCliente.ffcCelular, Celular));
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
