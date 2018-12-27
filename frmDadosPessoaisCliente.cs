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
        private TipoFormulario TipoFormulario { get; set; }
        public Cliente Cliente { get; set; }

        public FormDadosPessoaisCliente(TipoFormulario tipoFormulario)
        {
            InitializeComponent();

            txtCpf.Clear();
            dtpDataNascimento.Value = DateTime.Now;
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

                foreach (Estado estado in Estado.GetAll(conn, null))
                    cmbEstado.Items.Add(estado.Nome);

                cmbEstado.SelectedIndex = 0;
            }
        }

        public FormDadosPessoaisCliente(TipoFormulario tipoFormulario, Cliente cliente) : this(tipoFormulario) => Cliente = cliente;

        private void FormDadosPessoaisCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Cancel)
                return;

            if (TipoFormulario != TipoFormulario.tfCadastro)
                return;

            if (MessageBox.Show("Deseja realmente cancelar o cadastro do cliente?\nAs informações não serão salvas!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }
    }
}
