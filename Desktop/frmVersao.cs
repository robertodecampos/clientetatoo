using ClienteTatoo.Utils;
using System.Data;
using System.Windows.Forms;

namespace ClienteTatoo
{
    public partial class FormVersao : Form
    {
        public FormVersao()
        {
            InitializeComponent();

            lblSistema.Text = Application.ProductVersion;

            using (var conn = new Connection())
            {
                DataTable dt = conn.ExecuteReader("PRAGMA user_version", null, null);
                lblBancoDados.Text = dt.Rows[0]["user_version"].ToString();
            }

            using (var conn = new Connection(Database.Endereco))
            {
                DataTable dt = conn.ExecuteReader("SELECT versao FROM log_controle", null, null);
                lblBancoEndereco.Text = dt.Rows[0]["versao"].ToString();
            }
        }
    }
}
