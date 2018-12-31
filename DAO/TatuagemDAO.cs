using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClienteTatoo.DAO
{
    class TatuagemDAO : IDao<Tatuagem, MySqlTransaction>
    {
        private Connection _conn;

        public TatuagemDAO(Connection conn) => _conn = conn;

        public int Insert(Tatuagem model, MySqlTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador!");

            string sql = "INSERT INTO tatuagens (idCliente, local, desenho, idTermoResponsabilidade)" +
                         " VALUES (@idCliente, @local, @desenho, @idTermoResponsabilidade)";

            var parameters = GetParameters(model);
            

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Tatuagem model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE tatuagens SET" +
                         " removido = 1" +
                         " WHERE id = @id";

            List<MySqlParameter> parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Tatuagem model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE tatuagens SET" +
                         " idCliente = @idCliente, local = @local, desenho = @desenho, idTermoResponsabilidade = @idTermoResponsabilidade" +
                         " WHERE id = @id";

            List<MySqlParameter> parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public List<Tatuagem> GetAll(MySqlTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM tatuagens a";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            var tatuagens = new List<Tatuagem>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var tatuagem = new Tatuagem();
                PreencherModel(tatuagem, dr);
                tatuagens.Add(tatuagem);
            }

            return tatuagens;
        }

        public List<Tatuagem> GetByIdCliente(int idCliente, MySqlTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM tatuagens a" +
                         " WHERE a.`idCliente` = @idCliente";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idCliente", MySqlDbType.Int32) { Value = idCliente });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var tatuagens = new List<Tatuagem>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var tatuagem = new Tatuagem();
                PreencherModel(tatuagem, dr);
                tatuagens.Add(tatuagem);
            }

            return tatuagens;
        }

        public bool SetById(Tatuagem model, int id, MySqlTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM tatuagens a" +
                         " WHERE a.`id` = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} tatuagens com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        private List<MySqlParameter> GetParameters(Tatuagem model)
        {
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idCliente", MySqlDbType.Int32) { Value = model.IdCliente });
            parameters.Add(new MySqlParameter("@local", MySqlDbType.String) { Value = model.Local });
            parameters.Add(new MySqlParameter("@desenho", MySqlDbType.String) { Value = model.Desenho });
            parameters.Add(new MySqlParameter("@idTermoResponsabilidade", MySqlDbType.Int32) { Value = model.IdTermoResponsabilidade });

            return parameters;
        }

        private void PreencherModel(Tatuagem model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.IdCliente = int.Parse(dr["idCliente"].ToString());
            model.Local = dr["local"].ToString();
            model.Desenho = dr["desenho"].ToString();
        }

        public void Dispose() { }
    }
}
