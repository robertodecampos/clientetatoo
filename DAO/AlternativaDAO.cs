using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.DAO
{
    class AlternativaDAO : IDao<Alternativa, SQLiteTransaction>
    {
        private Connection _conn;

        public AlternativaDAO(Connection conn) => _conn = conn;

        public int Insert(Alternativa model, SQLiteTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador!");

            string sql = "INSERT INTO alternativas (idPergunta, descricao)" +
                         " VALUES (@idPergunta, @descricao)";

            var parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Alternativa model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE alternativas SET" +
                         " removida = 1" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Alternativa model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE alternativas SET" +
                         " idPergunta = @idPergunta, descricao = @descricao, ativada = @ativada" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });
            parameters.Add(new SQLiteParameter("@ativada", DbType.Int16) { Value = model.Ativada });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool SetById(Alternativa model, int id, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM alternativas a" +
                         " WHERE a.`id` = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} alternativas com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public List<Alternativa> GetByIdPergunta(int idPergunta, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM alternativas a" +
                         " WHERE a.`idPergunta` = @idPergunta" +
                         " AND NOT a.`removida`";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = idPergunta });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var alternativas = new List<Alternativa>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var alternativa = new Alternativa();
                PreencherModel(alternativa, dr);
                alternativas.Add(alternativa);
            }

            return alternativas;
        }

        public List<Alternativa> GetAtivasByIdPergunta(int idPergunta, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM alternativas a" +
                         " WHERE a.`idPergunta` = @idPergunta" +
                         " AND NOT a.`removida` AND a.`ativada`";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = idPergunta });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var alternativas = new List<Alternativa>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var alternativa = new Alternativa();
                PreencherModel(alternativa, dr);
                alternativas.Add(alternativa);
            }

            return alternativas;
        }

        public Alternativa GetAtivaByIdPerguntaAndDescricao(int idPergunta, string descricao, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM alternativas a" +
                         " WHERE a.`idPergunta` = @idPergunta" +
                         " AND a.`descricao` GLOB @descricao" +
                         " AND NOT a.`removida` AND a.`ativada`";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = idPergunta });
            parameters.Add(new SQLiteParameter("@descricao", DbType.String) { Value = $"*{Connection.ReplaceCaractersToGlob(descricao)}*" });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count != 1)
                return null;

            var alternativa = new Alternativa();
            PreencherModel(alternativa, dt.Rows[0]);

            return alternativa;
        }

        private List<SQLiteParameter> GetParameters(Alternativa model)
        {
            var parameters = new List<SQLiteParameter>();

            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = model.IdPergunta });
            parameters.Add(new SQLiteParameter("@descricao", DbType.String) { Value = model.Descricao });

            return parameters;
        }

        private void PreencherModel(Alternativa model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.IdPergunta = int.Parse(dr["idPergunta"].ToString());
            model.Descricao = dr["descricao"].ToString();
            model.Ativada = (int.Parse(dr["ativada"].ToString()) == 1);
        }

        public void Dispose() {}
    }
}
