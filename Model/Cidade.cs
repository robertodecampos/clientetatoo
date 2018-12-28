using MySql.Data.MySqlClient;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;

namespace ClienteTatoo.Model
{
    public class Cidade : IDisposable
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public bool GetById(int id, Connection conn, MySqlTransaction transaction = null)
        {
            using (var dao = new CidadeDAO(conn))
            {
                return dao.GetById(this, id, transaction);
            }
        }

        public static List<Cidade> GetAll(Connection conn, MySqlTransaction transaction = null)
        {
            using (var dao = new CidadeDAO(conn))
            {
                return dao.GetCidades(transaction);
            }
        }

        public static List<Cidade> GetByUf(string uf, Connection conn, MySqlTransaction transaction = null)
        {
            using (var dao = new CidadeDAO(conn))
            {
                return dao.GetByUf(uf, transaction);
            }
        }

        public void Dispose() { }
    }
}
