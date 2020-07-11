using System;
using System.Data.SQLite;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Endereco : IDisposable
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public int IdCidade { get; set; }
        public string Cep { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }

        public bool SearchByCep(string cep, SQLiteTransaction transaction = null)
        {
            using (var conn = new Connection(Database.Endereco))
            using (var dao = new EnderecoDAO(conn))
            {
                return dao.SearchByCep(this, cep, transaction);
            }
        }

        public void Dispose() { }
    }
}
