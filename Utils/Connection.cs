using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;

namespace ClienteTatoo.Utils
{
    public enum Database { Local, Endereco }

    public class Connection : IConnection, IDisposable
    {
        private readonly SQLiteConnection _conn;

        private string local = "Data Source=clientetatoo.db;Version=3;";
        private string endereco = "Data Source=enderecamento.db;Version=3;";

        public Connection() : this(Database.Local) { }

        public Connection(Database database)
        {
            string connectionString = "";

            switch (database)
            {
                case Database.Local:
                    connectionString = local;
                    break;
                case Database.Endereco:
                    connectionString = endereco;
                    break;
            }

            _conn = new SQLiteConnection(connectionString);
            _conn.Open();
        }

        private void SetParametersToCommand(SQLiteCommand command, IList<SQLiteParameter> parameters)
        {
            foreach (SQLiteParameter parameter in parameters)
                command.Parameters.Add(parameter);
        }

        public int Execute(string sql, IList<SQLiteParameter> parameters = null, SQLiteTransaction transaction = null)
        {
            using (var command = new SQLiteCommand(sql, _conn))
            {
                if (parameters != null)
                    SetParametersToCommand(command, parameters);

                if (transaction != null)
                    command.Transaction = transaction;

                return command.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteReader(string sql, IList<SQLiteParameter> parameters = null, SQLiteTransaction transaction = null)
        {
            using (var command = new SQLiteCommand(sql, _conn))
            {
                if (parameters != null)
                    SetParametersToCommand(command, parameters);

                if (transaction != null)
                    command.Transaction = transaction;

                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(dr);

                    dr.Close();

                    return dt;
                }
            }
        }

        public int UltimoIdInserido() => (int)_conn.LastInsertRowId;

        public SQLiteTransaction BeginTransaction() => _conn.BeginTransaction();

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
