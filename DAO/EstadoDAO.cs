using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    public class EstadoDAO : IDao<Estado, SQLiteTransaction>
    {
        private readonly Connection _connection;

        public EstadoDAO(Connection connection)
        {
            _connection = connection;
        }

        public int Insert(Estado model, SQLiteTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public int Remove(Estado model, SQLiteTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public int Update(Estado model, SQLiteTransaction transaction)
        {
            throw new NotImplementedException();
        }

        private void DistributeData(Estado model, DataRow dr)
        {
            model.Uf = dr["ufe_sg"].ToString();
            model.Nome = dr["ufe_no"].ToString();
        }

        public bool GetByUf(Estado model, string uf, SQLiteTransaction transaction = null)
        {
            string sql = "SELECT a.`ufe_sg`, a.`ufe_no`" +
                         " FROM `log_faixa_uf` a" +
                         " WHERE a.`ufe_sg` = @uf";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@uf", DbType.String) { Value = uf });

            DataTable dt = _connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return false;

            DistributeData(model, dt.Rows[0]);

            return true;
        }

        public List<Estado> GetEstados(SQLiteTransaction transaction = null)
        {
            string sql = "SELECT  a.`ufe_sg`, a.`ufe_no`" +
                         " FROM `log_faixa_uf` a";

            DataTable dt = _connection.ExecuteReader(sql, null, transaction);

            var estados = new List<Estado>();

            foreach (DataRow row in dt.Rows)
            {
                var estado = new Estado();

                DistributeData(estado, row);

                estados.Add(estado);
            }

            return estados;
        }

        public void Dispose() { }
    }
}
