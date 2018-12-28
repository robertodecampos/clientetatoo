using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.Model
{
    class TipoLogradouro : IDisposable
    {
        public string Nome { get; set; }

        public static IList<TipoLogradouro> GetAll(Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new TipoLogradouroDAO(connection))
            {
                return dao.GetAll(transaction);
            }
        }

        public void Dispose() { }
    }
}
