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
    class RespostaDAO : IDao<Resposta, SQLiteTransaction>
    {
        private Connection _conn;

        public RespostaDAO(Connection conn) => _conn = conn;

        public int Insert(Resposta model, SQLiteTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador!");

            string sql = "INSERT INTO respostas (idPergunta, descricao, especificar)" +
                         " VALUES (@idPergunta, @descricao, @especificar)";

            var parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Resposta model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE respostas SET" +
                         " removida = 1" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Resposta model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE respostas SET" +
                         " idPergunta = @idPergunta, descricao = @descricao, especificar = @especificar, ativada = @ativada" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });
            parameters.Add(new SQLiteParameter("@ativada", DbType.Int16) { Value = model.Ativada });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool SetById(Resposta model, int id, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM respostas a" +
                         " WHERE a.`id` = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} respostas com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public List<Resposta> GetByIdPergunta(int idPergunta, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM respostas a" +
                         " WHERE a.`idPergunta` = @idPergunta" +
                         " AND NOT a.`removida`";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = idPergunta });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var respostas = new List<Resposta>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var resposta = new Resposta();
                PreencherModel(resposta, dr);
                respostas.Add(resposta);
            }

            return respostas;
        }

        public List<Resposta> GetAtivasByIdPergunta(int idPergunta, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM respostas a" +
                         " WHERE a.`idPergunta` = @idPergunta" +
                         " AND NOT a.`removida` AND a.`ativada`";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = idPergunta });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var respostas = new List<Resposta>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var resposta = new Resposta();
                PreencherModel(resposta, dr);
                respostas.Add(resposta);
            }

            return respostas;
        }

        private List<SQLiteParameter> GetParameters(Resposta model)
        {
            var parameters = new List<SQLiteParameter>();

            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = model.IdPergunta });
            parameters.Add(new SQLiteParameter("@descricao", DbType.String) { Value = model.Descricao });
            parameters.Add(new SQLiteParameter("@especificar", DbType.Int16) { Value = model.Especificar });

            return parameters;
        }

        private void PreencherModel(Resposta model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.IdPergunta = int.Parse(dr["idPergunta"].ToString());
            model.Descricao = dr["descricao"].ToString();
            model.Especificar = (int.Parse(dr["especificar"].ToString()) == 1);
            model.Ativada = (int.Parse(dr["ativada"].ToString()) == 1);
        }

        public void Dispose() {}
    }
}
