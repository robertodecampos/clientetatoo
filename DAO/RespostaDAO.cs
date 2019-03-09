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

            string sql = "INSERT INTO respostas (idPergunta, idReferencia, idAlternativa, respostaDissertativa)" +
                         " VALUES (@idPergunta, @idReferencia, @idAlternativa, @respostaDissertativa)";

            var parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Resposta model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Update(Resposta model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid())
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE respostas SET" +
                         " idPergunta = @idPergunta, idReferencia = @idreferencia, idAlternativa = @idAlternativa," +
                         " respostaDissertativa = @respostaDissertativa" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public void LimparRespostasByTipoPerguntaAndIdPerguntaAndIdReferencia(TipoPergunta tipoPergunta, int idPergunta, int idReferencia, SQLiteTransaction transaction)
        {
            // Dele com sub-select porque o SQLite não suporta delete com join
            string sql = "DELETE FROM respostas" +
                         " WHERE id IN (" +
                         "   SELECT a.id" +
                         "   FROM respostas a" +
                         "   INNER JOIN perguntas b ON a.idPergunta = b.id AND b.tipo = @tipoPergunta" +
                         "   LEFT JOIN alternativas c ON a.idAlternativa = c.id" +
                         "   WHERE a.idPergunta = @idPergunta AND a.idReferencia = @idReferencia AND (" +
                         "     ((NOT b.dissertativa) AND (NOT b.alternativaUnica) AND c.ativada AND (NOT c.removida)) OR" +
                         "     (b.alternativaUnica) OR (b.dissertativa)" +
                         "   )" +
                         " )";

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

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@tipoPergunta", DbType.String) { Value = tipo });
            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = idPergunta });
            parameters.Add(new SQLiteParameter("@idReferencia", DbType.Int32) { Value = idReferencia });

            _conn.Execute(sql, parameters, transaction);
        }

        public IList<Resposta> GetByTipoPerguntaAndIdReferencia(TipoPergunta tipoPergunta, int idReferencia, bool somenteAtivas, SQLiteTransaction transaction)
        {
            string sql = "SELECT a.*" +
                         " FROM respostas a" +
                         " INNER JOIN perguntas b ON a.idPergunta = b.id AND b.tipo = @tipoPergunta AND NOT b.removida" +
                         " LEFT JOIN alternativas c ON a.idAlternativa = c.id AND NOT c.removida" +
                         " WHERE a.idreferencia = @idReferencia" +
                         "   AND (((c.id IS NULL) AND b.dissertativa) OR c.id IS NOT NULL)";

            if (somenteAtivas)
                sql += " AND (b.ativada AND ((c.id IS NOT NULL) AND c.ativada) OR (c.id IS NULL))";

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

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@tipoPergunta", DbType.String) { Value = tipo });
            parameters.Add(new SQLiteParameter("@idReferencia", DbType.Int32) { Value = idReferencia });

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

        public void Dispose() {}

        private List<SQLiteParameter> GetParameters(Resposta model)
        {
            var parameters = new List<SQLiteParameter>();

            parameters.Add(new SQLiteParameter("@idPergunta", DbType.Int32) { Value = model.IdPergunta });
            parameters.Add(new SQLiteParameter("@idReferencia", DbType.Int32) { Value = model.IdReferencia });
            parameters.Add(new SQLiteParameter("@idAlternativa", DbType.Int32) { Value = model.IdAlternativa });
            parameters.Add(new SQLiteParameter("@respostaDissertativa", DbType.String) { Value = model.RespostaDissertativa });

            return parameters;
        }

        private void PreencherModel(Resposta model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.IdPergunta = int.Parse(dr["idPergunta"].ToString());
            model.IdReferencia = int.Parse(dr["idReferencia"].ToString());
            model.IdAlternativa = int.Parse(dr["idAlternativa"].ToString());
            model.RespostaDissertativa = dr["respostaDissertativa"].ToString();
        }
    }
}
