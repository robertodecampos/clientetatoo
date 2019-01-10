using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    public class EnderecoDAO : IDao<Endereco, SQLiteTransaction>
    {
        private readonly Connection _connection;

        public EnderecoDAO(Connection connection)
        {
            _connection = connection;
        }

        public int Insert(Endereco model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Remove(Endereco model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Update(Endereco model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public bool SearchByCep(Endereco model, string cep, SQLiteTransaction transaction = null)
        {
            DataTable dt;
            string sql;
            List<SQLiteParameter> parameters;

            sql = "SELECT a.`log_tipo_logradouro` tipoLogradouro, a.`log_no` logradouro, b.`bai_no` bairro, c.`loc_nu_sequencial` idCidade, d.`ufe_sg` ufEstado" +
                  " FROM `log_logradouro` a" +
                  " INNER JOIN `log_bairro` b ON b.`bai_nu_sequencial` IN(a.`bai_nu_sequencial_ini`, a.`bai_nu_sequencial_fim`)" +
                  " INNER JOIN `log_localidade` c ON b.`loc_nu_sequencial` = c.`loc_nu_sequencial`" +
                  " INNER JOIN `log_faixa_uf` d ON c.`ufe_sg` = d.`ufe_sg`" +
                  " WHERE a.`cep` = @cep";

            parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@cep", DbType.String) { Value = cep });

            dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                model.Cep = cep;
                model.TipoLogradouro = dt.Rows[0]["tipoLogradouro"].ToString();
                model.Logradouro = dt.Rows[0]["logradouro"].ToString();
                model.Bairro = dt.Rows[0]["bairro"].ToString();
                model.IdCidade = int.Parse(dt.Rows[0]["idCidade"].ToString());
                model.Uf = dt.Rows[0]["ufEstado"].ToString();

                return true;
            }

            sql = "SELECT a.`loc_nu_sequencial` idCidade, b.`ufe_sg` ufEstado" +
                  " FROM `log_localidade` a" +
                  " INNER JOIN `log_faixa_uf` b ON a.`ufe_sg` = b.`ufe_sg`" +
                  " WHERE a.`cep` = @cep";

            parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@cep", DbType.String) { Value = cep });

            dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                model.Cep = cep;
                model.IdCidade = int.Parse(dt.Rows[0]["idCidade"].ToString());
                model.Uf = dt.Rows[0]["ufEstado"].ToString();

                return true;
            }

            sql = "SELECT b.`loc_nu_sequencial` idCidade, c.`ufe_sg` ufEstado" +
                   " FROM `log_faixa_localidade` a" +
                   " INNER JOIN `log_localidade` b ON a.`loc_nu_sequencial` = b.`loc_nu_sequencial`" +
                   " INNER JOIN `log_faixa_uf` c ON b.`ufe_sg` = c.`ufe_sg`" +
                   " WHERE(@part1 BETWEEN a.`loc_rad1_ini` AND a.`loc_rad1_fim` AND @part2 BETWEEN a.`loc_suf1_ini` AND a.`loc_suf1_fim`)" +
                   " OR (@part1 BETWEEN a.`loc_rad2_ini` AND a.`loc_rad2_fim` AND @part2 BETWEEN a.`loc_suf2_ini` AND a.`loc_suf2_fim`)";

            parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@part1", DbType.String) { Value = cep.Substring(0, 5) });
            parameters.Add(new SQLiteParameter("@part2", DbType.String) { Value = cep.Substring(5, 3) });

            dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                model.Cep = cep;
                model.IdCidade = int.Parse(dt.Rows[0]["idCidade"].ToString());
                model.Uf = dt.Rows[0]["ufEstado"].ToString();

                return true;
            }

            sql = "SELECT a.`ufe_sg` ufEstado" +
                  " FROM `log_faixa_uf` a" +
                  " WHERE(@part1 BETWEEN a.`ufe_rad1_ini` AND a.`ufe_rad1_fim` AND @part2 BETWEEN a.`ufe_suf1_ini` AND a.`ufe_suf1_fim`)" +
                  " OR (@part1 BETWEEN a.`ufe_rad2_ini` AND a.`ufe_rad2_fim` AND @part2 BETWEEN a.`ufe_suf2_ini` AND a.`ufe_suf2_fim`)";

            parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@part1", DbType.String) { Value = cep.Substring(0, 5) });
            parameters.Add(new SQLiteParameter("@part2", DbType.String) { Value = cep.Substring(5, 3) });

            dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                model.Cep = cep;
                model.Uf = dt.Rows[0]["ufEstado"].ToString();

                return true;
            }

            return false;
        }

        public void Dispose() { }
    }
}
