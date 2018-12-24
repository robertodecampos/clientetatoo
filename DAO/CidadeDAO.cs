using MySql.Data.MySqlClient;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClienteTatoo.DAO
{
    public class CidadeDAO : IDao<Cidade, MySqlTransaction>
    {
        private readonly Connection _connection;

        public CidadeDAO(Connection connection)
        {
            _connection = connection;
        }

        public int Insert(Cidade model, MySqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public int Remove(Cidade model, MySqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public int Update(Cidade model, MySqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public bool GetById(Cidade model, int id, MySqlTransaction transaction = null)
        {
            string sql = "SELECT a.`loc_nu_sequencial` idCidade, a.`loc_no` cidade" +
                         " FROM `endereco`.`log_localidade` a" +
                         " WHERE a.`loc_nu_sequencial` = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = _connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return false;

            DistributeData(model, dt.Rows[0]);

            return true;
        }

        public IList<Cidade> GetCidades(MySqlTransaction transaction = null)
        {
            string sql = "SELECT a.`loc_nu_sequencial` idCidade, a.`loc_no` cidade" +
                         " FROM `endereco`.`log_localidade` a";

            DataTable dt = _connection.ExecuteReader(sql, null, transaction);

            var cidades = new List<Cidade>();

            foreach (DataRow row in dt.Rows)
            {
                var cidade = new Cidade();

                DistributeData(cidade, row);

                cidades.Add(cidade);
            }

            return cidades;
        }

        public IList<Cidade> GetByUf(string uf, MySqlTransaction transaction = null)
        {
            string sql = "SELECT a.`loc_nu_sequencial` idCidade, a.`loc_no` cidade" +
                         " FROM `endereco`.`log_localidade` a" +
                         " WHERE a.`ufe_sg` = @uf";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@uf", MySqlDbType.String) { Value = uf });

            DataTable dt = _connection.ExecuteReader(sql, parameters, transaction);

            var cidades = new List<Cidade>();

            foreach (DataRow row in dt.Rows)
            {
                var cidade = new Cidade();

                DistributeData(cidade, row);

                cidades.Add(cidade);
            }

            return cidades;
        }

        private void DistributeData(Cidade cidade, DataRow dr)
        {
            cidade.Id = int.Parse(dr["idCidade"].ToString());
            cidade.Nome = dr["cidade"].ToString();
        }

        public void Dispose() { }
    }
}
