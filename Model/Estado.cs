using MySql.Data.MySqlClient;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using System.Collections.Generic;

namespace ClienteTatoo.Model
{
    public class Estado
    {
        public string Uf { get; set; }
        public string Nome { get; set; }

        public bool GetByUf(string uf, Connection conn, MySqlTransaction transaction = null)
        {
            using (var dao = new EstadoDAO(conn))
            {
                return dao.GetByUf(this, uf, transaction);
            }
        }

        public static IList<Estado> GetAll(Connection conn, MySqlTransaction transaction = null)
        {
            using (var dao = new EstadoDAO(conn))
            {
                return dao.GetEstados(transaction);
            }
        }
    }
}
