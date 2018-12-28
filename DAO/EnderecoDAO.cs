using MySql.Data.MySqlClient;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTatoo.DAO
{
    public class EnderecoDAO : IDao<Endereco, MySqlTransaction>
    {
        private readonly Connection _connection;

        public EnderecoDAO(Connection connection)
        {
            _connection = connection;
        }

        public int Insert(Endereco model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Remove(Endereco model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(Endereco model, MySqlTransaction transaction) => throw new NotImplementedException();

        public bool SearchByCep(Endereco model, string cep, MySqlTransaction transaction = null)
        {
            DataTable dt;
            string sql;
            List<MySqlParameter> parameters;

            sql = "SELECT a.`log_nome` logradouro, b.`bai_no` bairro, c.`loc_nu_sequencial` idCidade, d.`ufe_sg` ufEstado" +
                  " FROM `endereco`.`log_logradouro` a" +
                  " INNER JOIN `endereco`.`log_bairro` b ON b.`bai_nu_sequencial` IN(a.`bai_nu_sequencial_ini`, a.`bai_nu_sequencial_fim`)" +
                  " INNER JOIN `endereco`.`log_localidade` c ON b.`loc_nu_sequencial` = c.`loc_nu_sequencial`" +
                  " INNER JOIN `endereco`.`log_faixa_uf` d ON c.`ufe_sg` = d.`ufe_sg`" +
                  " WHERE a.`cep` = @cep";

            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@cep", MySqlDbType.String) { Value = cep });

            dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                model.Cep = cep;
                model.Logradouro = dt.Rows[0]["logradouro"].ToString();
                model.Bairro = dt.Rows[0]["bairro"].ToString();
                model.IdCidade = int.Parse(dt.Rows[0]["idCidade"].ToString());
                model.Uf = dt.Rows[0]["ufEstado"].ToString();

                return true;
            }

            sql = "SELECT a.`loc_nu_sequencial` idCidade, b.`ufe_sg` ufEstado" +
                  " FROM `endereco`.`log_localidade` a" +
                  " INNER JOIN `endereco`.`log_faixa_uf` b ON a.`ufe_sg` = b.`ufe_sg`" +
                  " WHERE a.`cep` = @cep";

            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@cep", MySqlDbType.String) { Value = cep });

            dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                model.Cep = cep;
                model.IdCidade = int.Parse(dt.Rows[0]["idCidade"].ToString());
                model.Uf = dt.Rows[0]["ufEstado"].ToString();

                return true;
            }

            sql = "SELECT b.`loc_nu_sequencial` idCidade, c.`ufe_sg` ufEstado" +
                   " FROM `endereco`.`log_faixa_localidade` a" +
                   " INNER JOIN `endereco`.`log_localidade` b ON a.`loc_nu_sequencial` = b.`loc_nu_sequencial`" +
                   " INNER JOIN `endereco`.`log_faixa_uf` c ON b.`ufe_sg` = c.`ufe_sg`" +
                   " WHERE(@part1 BETWEEN a.`loc_rad1_ini` AND a.`loc_rad1_fim` AND @part2 BETWEEN a.`loc_suf1_ini` AND a.`loc_suf1_fim`)" +
                   " OR (@part1 BETWEEN a.`loc_rad2_ini` AND a.`loc_rad2_fim` AND @part2 BETWEEN a.`loc_suf2_ini` AND a.`loc_suf2_fim`)";

            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@part1", MySqlDbType.String) { Value = cep.Substring(0, 5) });
            parameters.Add(new MySqlParameter("@part2", MySqlDbType.String) { Value = cep.Substring(5, 3) });

            dt = _connection.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                model.Cep = cep;
                model.IdCidade = int.Parse(dt.Rows[0]["idCidade"].ToString());
                model.Uf = dt.Rows[0]["ufEstado"].ToString();

                return true;
            }

            sql = "SELECT a.`ufe_sg` ufEstado" +
                  " FROM `endereco`.`log_faixa_uf` a" +
                  " WHERE(@part1 BETWEEN a.`ufe_rad1_ini` AND a.`ufe_rad1_fim` AND @part2 BETWEEN a.`ufe_suf1_ini` AND a.`ufe_suf1_fim`)" +
                  " OR (@part1 BETWEEN a.`ufe_rad2_ini` AND a.`ufe_rad2_fim` AND @part2 BETWEEN a.`ufe_suf2_ini` AND a.`ufe_suf2_fim`)";

            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@part1", MySqlDbType.String) { Value = cep.Substring(0, 5) });
            parameters.Add(new MySqlParameter("@part2", MySqlDbType.String) { Value = cep.Substring(5, 3) });

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
