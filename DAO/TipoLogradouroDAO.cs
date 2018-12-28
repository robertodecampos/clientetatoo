using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;

namespace ClienteTatoo.DAO
{
    class TipoLogradouroDAO : IDao<TipoLogradouro, MySqlTransaction>
    {
        private Connection Conexao { get; set; }

        public TipoLogradouroDAO(Connection conexao) => Conexao = conexao;

        public int Insert(TipoLogradouro model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Remove(TipoLogradouro model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(TipoLogradouro model, MySqlTransaction transaction) => throw new NotImplementedException();

        public List<TipoLogradouro> GetAll(MySqlTransaction transaction)
        {
            string sql = "SELECT  a.`tipologradouro`" +
                         " FROM `endereco`.`log_tipo_logr` a";

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
