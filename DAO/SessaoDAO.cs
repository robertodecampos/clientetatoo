using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.DAO
{
    class SessaoDAO : IDao<Sessao, MySqlTransaction>
    {
        private Connection _conn;

        public SessaoDAO(Connection conn) => _conn = conn;

        public int Insert(Sessao model, MySqlTransaction transaction)
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

        public int Remove(Sessao model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE sessoes SET" +
                         " removido = 1" +
                         " WHERE id = @id";

            List<MySqlParameter> parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Sessao model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE sessoes SET" +
                         " idTatuagem = @idTatuagem, valor = @valor, dataSessao = @dataSessao, parametros = @parametros, disparos = @disparos," +
                         " observacao = @observacao, pago = @pago" +
                         " WHERE id = @id";

            List<MySqlParameter> parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool SetById(Sessao model, int id, MySqlTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM sessoes a" +
                         " WHERE a.`id` = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} sessões com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public void MarcarComoPaga(Sessao model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível pagar um registro que não possuí identificador");

            string sql = "UPDATE sessoes SET" +
                         " pago = 1" +
                         " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            _conn.Execute(sql, parameters, transaction);
        }

        public List<Sessao> GetByIdTatuagem(int idTatuagem, MySqlTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idTatuagem", MySqlDbType.Int32) { Value = idTatuagem });

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

        public int CountByIdTatuagem(int idTatuagem, MySqlTransaction transaction)
        {
            string sql = "SELECT COUNT(a.id) qtde" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idTatuagem", MySqlDbType.Int32) { Value = idTatuagem });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            return int.Parse(dt.Rows[0]["qtde"].ToString());
        }

        public DateTime? GetDataSessaoOfFirstByIdTatuagem(int idTatuagem, MySqlTransaction transaction)
        {
            string sql = "SELECT a.`dataSessao`" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem" +
                         " ORDER BY a.`dataSessao`" +
                         " LIMIT 1";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idTatuagem", MySqlDbType.Int32) { Value = idTatuagem });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
                return DateTime.Parse(dt.Rows[0]["dataSessao"].ToString());
            else
                return null;
        }

        public DateTime? GetDataSessaoOfLastByIdTatuagem(int idTatuagem, MySqlTransaction transaction)
        {
            string sql = "SELECT a.`dataSessao`" +
                         " FROM sessoes a" +
                         " WHERE a.`idTatuagem` = @idTatuagem" +
                         " ORDER BY a.`dataSessao` DESC" +
                         " LIMIT 1";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idTatuagem", MySqlDbType.Int32) { Value = idTatuagem });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
                return DateTime.Parse(dt.Rows[0]["dataSessao"].ToString());
            else
                return null;
        }

        private List<MySqlParameter> GetParameters(Sessao model)
        {
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idTatuagem", MySqlDbType.Int32) { Value = model.IdTatuagem });
            parameters.Add(new MySqlParameter("@valor", MySqlDbType.Decimal) { Value = model.Valor });
            parameters.Add(new MySqlParameter("@dataSessao", MySqlDbType.Date) { Value = model.DataSessao });
            parameters.Add(new MySqlParameter("@parametros", MySqlDbType.String) { Value = model.Parametros });
            parameters.Add(new MySqlParameter("@disparos", MySqlDbType.String) { Value = model.Disparos });
            parameters.Add(new MySqlParameter("@observacao", MySqlDbType.String) { Value = model.Observacao });
            parameters.Add(new MySqlParameter("@pago", MySqlDbType.Int16) { Value = model.Pago });

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
