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
    class Usuario
    {
        public static bool Login(string login, string senha, Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new UsuarioDAO(conn))
            {
                return dao.Login(login, senha, transaction);
            }
        }
    }
}
