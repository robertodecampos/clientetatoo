using System;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    class TipoLogradouro : IDisposable
    {
        public string Nome { get; set; }

        public static List<TipoLogradouro> GetAll(SQLiteTransaction transaction)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new TipoLogradouroDAO(conn))
            {
                return dao.GetAll(transaction);
            }
        }

        public void Dispose() { }
    }
}
