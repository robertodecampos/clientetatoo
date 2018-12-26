using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ClienteTatoo.Model
{
    class TermoResponsabilidade : IDisposable
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

        public void Salvar(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TermoResponsabilidadeDAO(conn))
            {
                dao.Insert(this, transaction);
            }
        }

        public void SetCurrent(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TermoResponsabilidadeDAO(conn))
            {
                dao.SetCurrent(this, transaction);
            }
        }

        public static bool Exists(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TermoResponsabilidadeDAO(conn))
            {
                return dao.Exists(transaction);
            }
        }

        public void Dispose() { }
    }
}
