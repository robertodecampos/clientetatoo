using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using ClienteTatoo.Utils;

namespace DataBaseUpdate
{
    class Program
    {
#if DEBUG
        private const string caminhoDataBase = "clientetatoo.db";
#else
        private const string caminhoDataBase = @"C:\ProgramData\Cliente Tatoo\clientetatoo.db";
#endif

        private enum CodeResponse { Success = 1, Error = 0 };

        static int Main(string[] args)
        {
            if (!File.Exists(caminhoDataBase))
                return (int)CodeResponse.Error;

            int versionDataBase = GetVersionDataBase();

            var directory = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\scripts_database");

            using (var conn = new Connection())
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    int versionFile;

                    if (!int.TryParse(file.Name.Replace(".sql", string.Empty), out versionFile))
                        continue;

                    if (versionDataBase > versionFile)
                        continue;

                    using (SQLiteTransaction transaction = conn.BeginTransaction())
                    {
                        string[] conteudo = File.ReadAllLines(file.FullName);

                        string sql = "";

                        foreach (string linha in conteudo)
                            sql += string.Format(linha + "{0}", Environment.NewLine);

                        try
                        {
                            conn.Execute(sql);

                            transaction.Commit();
                        } catch
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }

            return (int)CodeResponse.Success;
        }

        private static int GetVersionDataBase()
        {
            using (var conn = new Connection())
            {
                string sql = "PRAGMA user_version";

                DataTable dt = conn.ExecuteReader(sql);

                return int.Parse(dt.Rows[0]["user_version"].ToString());
            }
        }
    }
}
