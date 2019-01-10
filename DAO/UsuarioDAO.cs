using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    class UsuarioDAO : IDao<Usuario, SQLiteTransaction>
    {
        private Connection _conn;

        public UsuarioDAO(Connection conn) => _conn = conn;

        public int Insert(Usuario model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Remove(Usuario model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Update(Usuario model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public bool Login(string login, string senha, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM usuario a" +
                         " WHERE login = @login AND senha = @senha";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@login", DbType.String) { Value = login });
            parameters.Add(new SQLiteParameter("@senha", DbType.String) { Value = Cryptography.EncondeMD5(senha) });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            return (dt.Rows.Count == 1);
        }

        public void Dispose() { }
    }
}
