using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ClienteTatoo.DAO
{
    class PerguntaDAO : IDao<Pergunta, SQLiteTransaction>
    {
        private Connection _conn;

        public PerguntaDAO(Connection conn) => _conn = conn;

        public int Insert(Pergunta model, SQLiteTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador!");

            string sql = "INSERT INTO perguntas (idAlternativa, descricao, codigoImportacao, alternativaUnica, dissertativa, obrigatoria, tipo)" +
                         " VALUES (@idAlternativa, @descricao, @codigoImportacao, @alternativaUnica, @dissertativa, @obrigatoria, @tipo)";

            var parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Pergunta model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE perguntas SET" +
                         " removida = 1" +
                         " WHERE id = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Pergunta model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE perguntas SET" +
                         " idAlternativa = @idAlternativa, descricao = @descricao, codigoImportacao = @codigoImportacao, alternativaUnica = @alternativaUnica," +
                         " tipo = @tipo, dissertativa = @dissertativa, obrigatoria = @obrigatoria, ativada = @ativada" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });
            parameters.Add(new SQLiteParameter("@ativada", DbType.Int16) { Value = model.Ativada });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool SetById(Pergunta model, int id, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM perguntas a" +
                         " WHERE a.`id` = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} perguntas com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public List<Pergunta> GetPrincipaisByTipoPergunta(TipoPergunta tipoPergunta, SQLiteTransaction transaction)
        {
            string tipo = "";
            switch (tipoPergunta)
            {
                case TipoPergunta.Cliente:
                    tipo = "cliente";
                    break;
                case TipoPergunta.Tatuagem:
                    tipo = "tatuagem";
                    break;
            }

            string sql = "SELECT *" +
                         " FROM perguntas a" +
                         $" WHERE a.`tipo` = '{tipo}'" +
                         " AND a.`idAlternativa` IS NULL" +
                         " AND NOT a.`removida`";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            var perguntas = new List<Pergunta>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var pergunta = new Pergunta();
                PreencherModel(pergunta, dr);
                perguntas.Add(pergunta);
            }

            return perguntas;
        }

        public List<Pergunta> GetPrincipaisAtivasByTipoPergunta(TipoPergunta tipoPergunta, SQLiteTransaction transaction)
        {
            string tipo = "";
            switch (tipoPergunta)
            {
                case TipoPergunta.Cliente:
                    tipo = "cliente";
                    break;
                case TipoPergunta.Tatuagem:
                    tipo = "tatuagem";
                    break;
            }

            string sql = "SELECT *" +
                         " FROM perguntas a" +
                         $" WHERE a.`tipo` = '{tipo}'" +
                         " AND a.`idAlternativa` IS NULL" +
                         " AND NOT a.`removida` AND a.`ativada`";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            var perguntas = new List<Pergunta>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var pergunta = new Pergunta();
                PreencherModel(pergunta, dr);
                perguntas.Add(pergunta);
            }

            return perguntas;
        }

        public List<Pergunta> GetByIdAlternativa(int idAlternativa, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM perguntas a" +
                         " WHERE a.`idAlternativa` = @idAlternativa" +
                         " AND NOT a.`removida`";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idAlternativa", DbType.Int32) { Value = idAlternativa });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var perguntas = new List<Pergunta>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var pergunta = new Pergunta();
                PreencherModel(pergunta, dr);
                perguntas.Add(pergunta);
            }

            return perguntas;
        }

        public List<Pergunta> GetAtivasByIdAlternativa(int idAlternativa, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM perguntas a" +
                         " WHERE a.`idAlternativa` = @idAlternativa" +
                         " AND NOT a.`removida` AND a.`ativada`";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idAlternativa", DbType.Int32) { Value = idAlternativa });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var perguntas = new List<Pergunta>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var pergunta = new Pergunta();
                PreencherModel(pergunta, dr);
                perguntas.Add(pergunta);
            }

            return perguntas;
        }

        private List<SQLiteParameter> GetParameters(Pergunta model)
        {
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idAlternativa", DbType.Int32) { Value = model.IdAlternativa });
            parameters.Add(new SQLiteParameter("@descricao", DbType.String) { Value = model.Descricao });
            parameters.Add(new SQLiteParameter("@codigoImportacao", DbType.String) { Value = model.CodigoImportacao });
            parameters.Add(new SQLiteParameter("@alternativaUnica", DbType.Int16) { Value = model.AlternativaUnica });
            parameters.Add(new SQLiteParameter("@dissertativa", DbType.Int16) { Value = model.Dissertativa });
            parameters.Add(new SQLiteParameter("@obrigatoria", DbType.Int16) { Value = model.Obrigatoria });

            string tipo = null;

            switch (model.Tipo)
            {
                case TipoPergunta.Cliente:
                    tipo = "cliente";
                    break;
                case TipoPergunta.Tatuagem:
                    tipo = "tatuagem";
                    break;
            }

            parameters.Add(new SQLiteParameter("@tipo", DbType.String) { Value = tipo });

            return parameters;
        }

        private void PreencherModel(Pergunta model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            if (dr["idAlternativa"].ToString() == "")
                model.IdAlternativa = null;
            else
                model.IdAlternativa = int.Parse(dr["idAlternativa"].ToString());
            model.Descricao = dr["descricao"].ToString();
            model.CodigoImportacao = dr["codigoImportacao"].ToString();
            model.AlternativaUnica = (int.Parse(dr["alternativaUnica"].ToString()) == 1);
            model.Dissertativa = (int.Parse(dr["dissertativa"].ToString()) == 1);
            model.Obrigatoria = (int.Parse(dr["obrigatoria"].ToString()) == 1);
            model.Ativada = (int.Parse(dr["ativada"].ToString()) == 1);

            switch (dr["tipo"].ToString())
            {
                case "cliente":
                    model.Tipo = TipoPergunta.Cliente;
                    break;
                case "tatuagem":
                    model.Tipo = TipoPergunta.Tatuagem;
                    break;
            }
        }

        public void Dispose() {}
    }
}
