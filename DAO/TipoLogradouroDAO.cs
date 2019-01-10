using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    class TipoLogradouroDAO : IDao<TipoLogradouro, SQLiteTransaction>
    {
        private Connection Conexao { get; set; }

        public TipoLogradouroDAO(Connection conexao) => Conexao = conexao;

        public int Insert(TipoLogradouro model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Remove(TipoLogradouro model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Update(TipoLogradouro model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public List<TipoLogradouro> GetAll(SQLiteTransaction transaction)
        {
            string sql = "SELECT  a.`tipologradouro`" +
                         " FROM `log_tipo_logr` a";

            DataTable dt = Conexao.ExecuteReader(sql, null, transaction);

            var tiposLogradouro = new List<TipoLogradouro>();

            foreach (DataRow row in dt.Rows)
            {
                var tipoLogradouro = new TipoLogradouro();

                DistributeData(tipoLogradouro, row);

                tiposLogradouro.Add(tipoLogradouro);
            }

            return tiposLogradouro;
        }

        private void DistributeData(TipoLogradouro model, DataRow dr)
        {
            model.Nome = dr["tipologradouro"].ToString();
        }

        public void Dispose() { }
    }
}
