using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    class TermoResponsabilidadeDAO : IDao<TermoResponsabilidade, SQLiteTransaction>
    {
        private Connection _conn;

        public TermoResponsabilidadeDAO(Connection conn) => _conn = conn;

        public int Insert(TermoResponsabilidade model, SQLiteTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí um identificador!");

            if (!model.isValid().Key)
                throw new Exception("Não é possível inserir, existe inconsistências!");

            string sql = "INSERT INTO termo_responsabilidade (termo, dataCadastro) VALUES (@termo, DATETIME('now', 'localtime'))";

            var parameters = new List<SQLiteParameter>();

            parameters.Add(new SQLiteParameter("@termo", DbType.String) { Value = model.Termo });

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(TermoResponsabilidade model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Update(TermoResponsabilidade model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public bool SetCurrent(TermoResponsabilidade model, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM termo_responsabilidade a" +
                         " ORDER BY a.id DESC" +
                         " LIMIT 1";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            if (dt.Rows.Count == 0)
                return false;

            PreencherModel(model, dt.Rows[0]);
            return true;
        }

        public void SetById(int id, TermoResponsabilidade model, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM termo_responsabilidade a" +
                         " WHERE a.id = @id";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 1)
            {
                PreencherModel(model, dt.Rows[0]);
            }
            else if (dt.Rows.Count == 0)
                throw new Exception($"Não existe nenhum termo de responsabilidade cadastrado o id {id}!");
            else
                throw new Exception($"Existem {dt.Rows.Count} termos de responsabilidade cadastrados com o id {id}!");
        }

        public bool Exists(SQLiteTransaction transaction)
        {
            string sql = "SELECT COUNT(a.id) qtde" +
                         " FROM termo_responsabilidade a" +
                         " ORDER BY a.id DESC" +
                         " LIMIT 1";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            return (int.Parse(dt.Rows[0]["qtde"].ToString()) > 0);
        }

        private void PreencherModel(TermoResponsabilidade model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.Termo = dr["termo"].ToString();
            DateTime dataCadastro;
            if (DateTime.TryParse(dr["dataCadastro"].ToString(), out dataCadastro))
                model.DataCadastro = dataCadastro;
        }

        public void Dispose() { }
    }
}
