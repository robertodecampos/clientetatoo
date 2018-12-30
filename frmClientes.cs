﻿using ClienteTatoo.Model;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
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
    public partial class FormClientes : Form
    {
        private enum PassoCadastroCliente { pccTermoResponsabilidade, pccDadosPessoais };

        private List<Cliente> clientes;
        private List<ClienteFilter> filtros;
        private FormFiltroCliente frmFiltro = new FormFiltroCliente();

        public FormClientes()
        {
            InitializeComponent();

            filtros = new List<ClienteFilter>();
            cmbOrdenacao.SelectedIndex = 0;
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            using (var conn = new Connection())
            {
                var ordenacao = new List<ClienteOrdenation>();

                switch (cmbOrdenacao.SelectedIndex)
                {
                    case 0:
                        ordenacao.Add(new ClienteOrdenation(FieldOrdenationCliente.Codigo, TypeOrder.toAsc));
                        break;
                    case 1:
                        ordenacao.Add(new ClienteOrdenation(FieldOrdenationCliente.Nome, TypeOrder.toAsc));
                        break;
                    case 2:
                        ordenacao.Add(new ClienteOrdenation(FieldOrdenationCliente.DataNascimento, TypeOrder.toAsc));
                        break;
                }

                clientes = Cliente.GetAll(filtros, ordenacao, conn, null);
            }

            lsvClientes.Items.Clear();

            foreach (Cliente cliente in clientes)
            {
                var item = new ListViewItem();

                item.Text = cliente.Id.ToString();
                item.SubItems.Add(cliente.Nome);
                if (cliente.DataNascimento != null)
                    item.SubItems.Add(((DateTime)cliente.DataNascimento).ToString("dd/MM/yyyy"));
                else
                    item.SubItems.Add("Não informada");
                item.SubItems.Add(cliente.Cpf);
                item.SubItems.Add(cliente.Telefone);
                item.SubItems.Add(cliente.Celular);

                lsvClientes.Items.Add(item);
            }
        }

        private void termoDeResponsabilidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmLogin = new FormLogin())
            using (var frmConfigurarTermoResponsabilidade = new FormConfigurarTermoResponsabilidade())
            {
                frmLogin.ShowDialog();
                if (frmLogin.Logado)
                    frmConfigurarTermoResponsabilidade.ShowDialog();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            using (var frmDadosPessoais = new FormDadosPessoaisCliente(TipoFormulario.tfCadastro))
            using (var frmTermoResponsabilidade = new FormTermoResponsabilidade())
            using (var cliente = new Cliente())
            {
                PassoCadastroCliente passo = PassoCadastroCliente.pccTermoResponsabilidade;
                bool cadastroFinalizado = false;

                while (!cadastroFinalizado)
                {
                    switch (passo)
                    {
                        case PassoCadastroCliente.pccTermoResponsabilidade:
                            frmTermoResponsabilidade.ShowDialog();
                            if (frmTermoResponsabilidade.DialogResult == DialogResult.OK)
                                passo = PassoCadastroCliente.pccDadosPessoais;
                            else
                                return;
                            break;

                        case PassoCadastroCliente.pccDadosPessoais:
                            frmDadosPessoais.ShowDialog();
                            if (frmDadosPessoais.DialogResult == DialogResult.OK)
                                cadastroFinalizado = true;
                            else if (frmDadosPessoais.DialogResult == DialogResult.Abort)
                                passo = PassoCadastroCliente.pccTermoResponsabilidade;
                            else
                                return;
                            break;
                    }
                }

                using (var conn = new Utils.Connection())
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    cliente.IdTermoResponsabilidade = frmTermoResponsabilidade.IdTermoResponsabilidade;
                    cliente.Nome = frmDadosPessoais.Cliente.Nome;
                    cliente.DataNascimento = frmDadosPessoais.Cliente.DataNascimento;
                    cliente.Cpf = frmDadosPessoais.Cliente.Cpf;
                    cliente.Email = frmDadosPessoais.Cliente.Email;
                    cliente.Telefone = frmDadosPessoais.Cliente.Telefone;
                    cliente.Celular = frmDadosPessoais.Cliente.Celular;
                    cliente.Cep = frmDadosPessoais.Cliente.Cep;
                    cliente.Uf = frmDadosPessoais.Cliente.Uf;
                    cliente.IdCidade = frmDadosPessoais.Cliente.IdCidade;
                    cliente.TipoLogradouro = frmDadosPessoais.Cliente.TipoLogradouro;
                    cliente.Logradouro = frmDadosPessoais.Cliente.Logradouro;
                    cliente.Complemento = frmDadosPessoais.Cliente.Complemento;
                    cliente.Bairro = frmDadosPessoais.Cliente.Bairro;
                    cliente.Numero = frmDadosPessoais.Cliente.Numero;

                    try
                    {
                        cliente.Salvar(conn, transaction);
                        transaction.Commit();
                        CarregarClientes();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Ocorreu um erro ao salvar o cliente:\n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                    }
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (frmFiltro.ShowDialog() != DialogResult.OK)
                return;

            filtros = frmFiltro.Filtro;
            CarregarClientes();
        }

        private void cmbOrdenacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarClientes();
        }

        private new void Dispose()
        {
            frmFiltro.Dispose();
            base.Dispose();
        }
    }
}
