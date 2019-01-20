using System;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class TermoResponsabilidade : IDisposable
    {
        public int Id { get; set; }
        public string Termo { get; set; }
        public DateTime DataCadastro { get; set; }

        public KeyValuePair<bool, IList<string>> isValid()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(Termo.Trim()))
                erros.Add("O termo é necessário ser preenchido!");

            return new KeyValuePair<bool, IList<string>>(erros.Count == 0, erros);
        }

        public void Salvar(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new TermoResponsabilidadeDAO(conn))
            {
                dao.Insert(this, transaction);
            }
        }

        public bool SetCurrent(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new TermoResponsabilidadeDAO(conn))
            {
                return dao.SetCurrent(this, transaction);
            }
        }

        public void SetById(int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new TermoResponsabilidadeDAO(conn))
            {
                dao.SetById(id, this, transaction);
            }
        }

        public static bool Exists(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new TermoResponsabilidadeDAO(conn))
            {
                return dao.Exists(transaction);
            }
        }

        public void Dispose() { }
    }
}
