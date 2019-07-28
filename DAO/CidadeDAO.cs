using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    public class CidadeDAO : IDao<Cidade, SQLiteTransaction>
    {
        private readonly Connection _connection;

        public CidadeDAO(Connection connection)
        {
            _connection = connection;
        }

        public int Insert(Cidade model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Remove(Cidade model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Update(Cidade model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public bool GetById(Cidade model, int id, SQLiteTransaction transaction = null)
        {
            string sql =
                " SELECT                                              " +
                "   a.`loc_nu_sequencial` idCidade, a.`loc_no` cidade " +
                " FROM `log_localidade` a                             " +
                " WHERE                                               " +
                "   a.`loc_nu_sequencial` = @id                       ";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return false;

            DistributeData(model, dt.Rows[0]);

            return true;
        }

        public bool GetByCidadeAndUf(Cidade model, string cidade, string uf, SQLiteTransaction transaction = null)
        {
            string sql =
                " SELECT                                              " +
                "   a.`loc_nu_sequencial` idCidade, a.`loc_no` cidade " +
                " FROM log_localidade a                               " +
                " WHERE                                               " +
                "  @cidade IN (a.loc_no, a.loc_nosub)                 " +
                "  AND a.ufe_sg = @uf                                 ";

            var parameters = new List<SQLiteParameter>()
            {
                new SQLiteParameter("@cidade", DbType.String) { Value = cidade },
                new SQLiteParameter("@uf", DbType.String) { Value = uf }
            };

            DataTable dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count != 1)
                throw new Exception($"Existem {dt.Rows.Count} cidades com nome '{cidade}' e o estado '{uf}'!");

            DistributeData(model, dt.Rows[0]);

            return true;
        }

        public List<Cidade> GetCidades(SQLiteTransaction transaction = null)
        {
            string sql = "SELECT a.`loc_nu_sequencial` idCidade, a.`loc_no` cidade" +
                         " FROM `log_localidade` a";

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

        public List<Cidade> GetByUf(string uf, SQLiteTransaction transaction = null)
        {
            string sql = "SELECT a.`loc_nu_sequencial` idCidade, a.`loc_no` cidade" +
                         " FROM `log_localidade` a" +
                         " WHERE a.`ufe_sg` = @uf";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@uf", DbType.String) { Value = uf });

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
