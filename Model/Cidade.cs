using System;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Cidade : IDisposable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }

        public bool GetById(int id, SQLiteTransaction transaction = null)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new CidadeDAO(conn))
            {
                return dao.GetById(this, id, transaction);
            }
        }

        public bool GetByCidadeAndUf(string cidade, string uf, SQLiteTransaction transaction = null)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new CidadeDAO(conn))
            {
                return dao.GetByCidadeAndUf(this, cidade, uf, transaction);
            }
        }

        public static List<Cidade> GetAll(SQLiteTransaction transaction = null)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new CidadeDAO(conn))
            {
                return dao.GetCidades(transaction);
            }
        }

        public static List<Cidade> GetByUf(string uf, SQLiteTransaction transaction = null)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new CidadeDAO(conn))
            {
                return dao.GetByUf(uf, transaction);
            }
        }

        public void Dispose() { }
    }
}
