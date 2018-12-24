using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.DAO
{
    class UsuarioDAO : IDao<Usuario, MySqlTransaction>
    {
        private Connection _conn;

        public UsuarioDAO(Connection conn) => _conn = conn;

        public int Insert(Usuario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Remove(Usuario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(Usuario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public bool Login(string login, string senha, MySqlTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM usuario a" +
                         " WHERE login = @login AND senha = MD5(@senha)";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@login", MySqlDbType.String) { Value = login });
            parameters.Add(new MySqlParameter("@senha", MySqlDbType.String) { Value = senha });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            return (dt.Rows.Count == 1);
        }

        public void Dispose() { }
    }
}
