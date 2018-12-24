using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ClienteTatoo.Utils
{
    public class Connection : IConnection, IDisposable
    {
        private readonly MySqlConnection _conn;

        private string local = "Server=localhost;Database=cliente_tatoo;Uid=cliente_tatoo;Pwd=tatoocli@3409;SslMode=none;";

        public Connection()
        {
            _conn = new MySqlConnection(local);
            _conn.Open();
        }

        private void SetParametersToCommand(MySqlCommand command, IList<MySqlParameter> parameters)
        {
            foreach (MySqlParameter parameter in parameters)
                command.Parameters.Add(parameter);
        }

        public int Execute(string sql, IList<MySqlParameter> parameters = null, MySqlTransaction transaction = null)
        {
            using (var command = new MySqlCommand(sql, _conn))
            {
                if (parameters != null)
                    SetParametersToCommand(command, parameters);

                if (transaction != null)
                    command.Transaction = transaction;

                return command.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteReader(string sql, IList<MySqlParameter> parameters = null, MySqlTransaction transaction = null)
        {
            using (var command = new MySqlCommand(sql, _conn))
            {
                if (parameters != null)
                    SetParametersToCommand(command, parameters);

                if (transaction != null)
                    command.Transaction = transaction;

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(dr);

                    dr.Close();

                    return dt;
                }
            }
        }

        public int UltimoIdInserido()
        {
            string sql = "SELECT @@IDENTITY id";

            using (var rows = ExecuteReader(sql, null, null)) { return int.Parse(rows.Rows[0]["id"].ToString()); }
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
