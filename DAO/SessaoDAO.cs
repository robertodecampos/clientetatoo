using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    class SessaoDAO : IDao<Sessao, SQLiteTransaction>
    {
        private Connection _conn;

        public SessaoDAO(Connection conn) => _conn = conn;

        public int Insert(Sessao model, SQLiteTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador!");

            string sql = "INSERT INTO sessoes (idTatuagem, valor, dataSessao, parametros, disparos, observacao, pago)" +
                         " VALUES (@idTatuagem, @valor, @dataSessao, @parametros, @disparos, @observacao, @pago)";

            var parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Sessao model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE sessoes SET" +
                         " removido = 1" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Sessao model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE sessoes SET" +
                         " idTatuagem = @idTatuagem, valor = @valor, dataSessao = @dataSessao, parametros = @parametros, disparos = @disparos," +
                         " observacao = @observacao, pago = @pago" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool SetById(Sessao model, int id, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM sessoes a" +
                         " WHERE a.`id` = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} sessões com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public void MarcarComoPaga(Sessao model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível pagar um registro que não possuí identificador");

            string sql = "UPDATE sessoes SET" +
                         " pago = 1" +
                         " WHERE id = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            _conn.Execute(sql, parameters, transaction);
        }

        public List<Sessao> GetByIdTatuagem(int idTatuagem, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idTatuagem", DbType.Int32) { Value = idTatuagem });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var sessoes = new List<Sessao>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var sessao = new Sessao();
                PreencherModel(sessao, dr);
                sessoes.Add(sessao);
            }

            return sessoes;
        }

        public int CountByIdTatuagem(int idTatuagem, SQLiteTransaction transaction)
        {
            string sql = "SELECT COUNT(a.id) qtde" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idTatuagem", DbType.Int32) { Value = idTatuagem });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            return int.Parse(dt.Rows[0]["qtde"].ToString());
        }

        public DateTime? GetDataSessaoOfFirstByIdTatuagem(int idTatuagem, SQLiteTransaction transaction)
        {
            string sql = "SELECT a.`dataSessao`" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem" +
                         " ORDER BY a.`dataSessao`" +
                         " LIMIT 1";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idTatuagem", DbType.Int32) { Value = idTatuagem });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
                return DateTime.Parse(dt.Rows[0]["dataSessao"].ToString());
            else
                return null;
        }

        public DateTime? GetDataSessaoOfLastByIdTatuagem(int idTatuagem, SQLiteTransaction transaction)
        {
            string sql = "SELECT a.`dataSessao`" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem" +
                         " ORDER BY a.`dataSessao` DESC" +
                         " LIMIT 1";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idTatuagem", DbType.Int32) { Value = idTatuagem });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
                return DateTime.Parse(dt.Rows[0]["dataSessao"].ToString());
            else
                return null;
        }

        private List<SQLiteParameter> GetParameters(Sessao model)
        {
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idTatuagem", DbType.Int32) { Value = model.IdTatuagem });
            parameters.Add(new SQLiteParameter("@valor", DbType.Decimal) { Value = model.Valor });
            parameters.Add(new SQLiteParameter("@dataSessao", DbType.Date) { Value = model.DataSessao });
            parameters.Add(new SQLiteParameter("@parametros", DbType.String) { Value = model.Parametros });
            parameters.Add(new SQLiteParameter("@disparos", DbType.String) { Value = model.Disparos });
            parameters.Add(new SQLiteParameter("@observacao", DbType.String) { Value = model.Observacao });
            parameters.Add(new SQLiteParameter("@pago", DbType.Int16) { Value = model.Pago });

            return parameters;
        }

        private void PreencherModel(Sessao model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.IdTatuagem = int.Parse(dr["idTatuagem"].ToString());
            model.Valor =  decimal.Parse(dr["valor"].ToString());
            model.DataSessao = DateTime.Parse(dr["dataSessao"].ToString());
            model.Parametros = dr["parametros"].ToString();
            model.Disparos = dr["disparos"].ToString();
            model.Observacao = dr["observacao"].ToString();
            model.Pago = bool.Parse(dr["pago"].ToString());
        }

        public void Dispose() { }
    }
}
