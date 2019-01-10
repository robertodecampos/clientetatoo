using System;
using System.Data.SQLite;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Usuario: IDisposable
    {
        public static bool Login(string login, string senha, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new UsuarioDAO(conn))
            {
                return dao.Login(login, senha, transaction);
            }
        }

        public void Dispose() { }
    }
}
