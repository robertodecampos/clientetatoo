using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;

namespace ClienteTatoo.Utils
{
    public enum Database { Local, Endereco }

    public class Connection : IConnection, IDisposable
    {
        private readonly SQLiteConnection _conn;

#if DEBUG
        private string local = "Data Source=clientetatoo.db;Version=3";
        private string endereco = "Data Source=enderecamento.db;Version=3;";
#else
        private string local = @"Data Source=C:\ProgramData\Cliente Tatoo\clientetatoo.db;Version=3;";
        private string endereco = @"Data Source=C:\ProgramData\Cliente Tatoo\enderecamento.db;Version=3;";
#endif

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

        public static string ReplaceCaractersToGlob(string value)
        {
            char[] acentuacaoA = { 'a', 'á', 'à', 'â', 'ã', 'ä', 'A', 'Á', 'À', 'Â', 'Ã', 'Ä' };
            char[] acentuacaoE = { 'e', 'é', 'è', 'ê', 'ë', 'E', 'É', 'È', 'Ê', 'Ë' };
            char[] acentuacaoI = { 'i', 'í', 'ì', 'î', 'ï', 'I', 'Í', 'Ì', 'Î', 'Ï' };
            char[] acentuacaoO = { 'o', 'ó', 'ò', 'ô', 'õ', 'ö', 'O', 'Ó', 'Ò', 'Ô', 'Õ', 'Ö' };
            char[] acentuacaoU = { 'u', 'ú', 'ù', 'û', 'ü', 'U', 'Ú', 'Ù', 'Û', 'Ü' };

            var posicoesReplace = new List<KeyValuePair<int, string>>();

            int i = 0;
            foreach (char caracter in value )
            {
                if (acentuacaoA.Contains(caracter))
                    posicoesReplace.Add(new KeyValuePair<int, string>(i, "[aáàâãäAÁÀÂÃÄ]"));
                else if (acentuacaoE.Contains(caracter))
                    posicoesReplace.Add(new KeyValuePair<int, string>(i, "[eéèêëEÉÈÊË]"));
                else if (acentuacaoI.Contains(caracter))
                    posicoesReplace.Add(new KeyValuePair<int, string>(i, "[iíìîïIÍÌÎÏ]"));
                else if (acentuacaoO.Contains(caracter))
                    posicoesReplace.Add(new KeyValuePair<int, string>(i, "[oóòôõöOÓÒÔÕÖ]"));
                else if (acentuacaoU.Contains(caracter))
                    posicoesReplace.Add(new KeyValuePair<int, string>(i, "[uúùûüUÚÙÛÜ]"));

                i++;
            }

            for (i = posicoesReplace.Count - 1; i >= 0; i--)
            {
                KeyValuePair<int, string> posicao = posicoesReplace[i];

                value = value.Substring(0, posicao.Key) + value.Substring(posicao.Key + 1, value.Length - posicao.Key);
            }

            return value;
        }

        public int UltimoIdInserido() => (int)_conn.LastInsertRowId;

        public SQLiteTransaction BeginTransaction() => _conn.BeginTransaction();

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
