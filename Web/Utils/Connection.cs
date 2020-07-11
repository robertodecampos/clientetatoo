using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Utils
{
    public class Connection : IDisposable
    {
        private readonly MySqlConnection connection;

        public Connection(IConfiguration conf)
        {
            connection = new MySqlConnection(conf.GetConnectionString("DefaultConnection"));
            connection.Open();
        }

        private void SetParametersToCommand(MySqlCommand command, IList<MySqlParameter> parameters)
        {
            foreach (MySqlParameter parameter in parameters)
                command.Parameters.Add(parameter);
        }

        public int Execute(string sql, IList<MySqlParameter> parameters = null, MySqlTransaction transaction = null)
        {
            using (var command = new MySqlCommand(sql, connection))
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
            using (var command = new MySqlCommand(sql, connection))
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

        public MySqlTransaction BeginTransaction() => connection.BeginTransaction();

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
