using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Estado
    {
        public string Uf { get; set; }
        public string Nome { get; set; }

        public bool GetByUf(string uf, SQLiteTransaction transaction = null)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new EstadoDAO(conn))
            {
                return dao.GetByUf(this, uf, transaction);
            }
        }

        public static List<Estado> GetAll(SQLiteTransaction transaction = null)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new EstadoDAO(conn))
            {
                return dao.GetEstados(transaction);
            }
        }
    }
}
