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
    public partial class FormTatuagens : Form
    {
        private int IdCliente { get; set; }
        private List<Tatuagem> Tatuagens { get; set; }

        public FormTatuagens(int idCliente)
        {
            InitializeComponent();

            IdCliente = idCliente;

            OrganizarColunas();

            CarregarTatuagens();
        }

        private void CarregarTatuagens()
        {
            using (var conn = new Connection())
            {
                Tatuagens = Tatuagem.GetByIdCliente(IdCliente, conn, null);
            }

            lsvTatuagens.Items.Clear();

            foreach (Tatuagem tatuagem in Tatuagens)
            {
                var item = new ListViewItem();

                item.Text = tatuagem.Id.ToString();
                item.SubItems.Add(tatuagem.Local);
                item.SubItems.Add(tatuagem.Desenho);
            }
        }

        private void OrganizarColunas()
        {
            int width = lsvTatuagens.ClientSize.Width;

            for (int i = 0; i < lsvTatuagens.Columns.Count; i++)
                width -= lsvTatuagens.Columns[i].Width;

            int sobra = width % 2;

            lsvTatuagens.Columns[1].Width += (width / 2) + sobra;
            lsvTatuagens.Columns[2].Width += width / 2;

            lsvTatuagens.Scrollable = false; // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
            lsvTatuagens.Scrollable = true;  // Gambiarra para evitar o scroll ficar aparecendo em baixo sem necessidade
        }

        private void FormTatuagens_Resize(object sender, EventArgs e) => OrganizarColunas();
    }
}
