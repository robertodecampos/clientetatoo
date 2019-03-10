using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    class TatuagemDAO : IDao<Tatuagem, SQLiteTransaction>
    {
        private Connection _conn;

        public TatuagemDAO(Connection conn) => _conn = conn;

        public int Insert(Tatuagem model, SQLiteTransaction transaction)
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

        public int Remove(Tatuagem model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE tatuagens SET" +
                         " removido = 1" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Tatuagem model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE tatuagens SET" +
                         " idCliente = @idCliente, local = @local, desenho = @desenho, idTermoResponsabilidade = @idTermoResponsabilidade" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public List<Tatuagem> GetAll(SQLiteTransaction transaction)
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

        public List<Tatuagem> GetByIdCliente(int idCliente, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM tatuagens a" +
                         " WHERE a.`idCliente` = @idCliente";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idCliente", DbType.Int32) { Value = idCliente });

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

        public bool SetById(Tatuagem model, int id, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM tatuagens a" +
                         " WHERE a.`id` = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} tatuagens com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        private List<SQLiteParameter> GetParameters(Tatuagem model)
        {
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idCliente", DbType.Int32) { Value = model.IdCliente });
            parameters.Add(new SQLiteParameter("@local", DbType.String) { Value = model.Local });
            parameters.Add(new SQLiteParameter("@desenho", DbType.String) { Value = model.Desenho });
            parameters.Add(new SQLiteParameter("@idTermoResponsabilidade", DbType.Int32) { Value = model.IdTermoResponsabilidade });

            return parameters;
        }

        private void PreencherModel(Tatuagem model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.IdCliente = int.Parse(dr["idCliente"].ToString());
            model.Local = dr["local"].ToString();
            model.Desenho = dr["desenho"].ToString();
            model.IdTermoResponsabilidade = int.Parse(dr["idTermoResponsabilidade"].ToString());
        }

        public void Dispose() { }
    }
}
