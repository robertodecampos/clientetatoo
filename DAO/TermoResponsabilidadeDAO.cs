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
    class TermoResponsabilidadeDAO : IDao<TermoResponsabilidade, MySqlTransaction>
    {
        private Connection _conn;

        public TermoResponsabilidadeDAO(Connection conn) => _conn = conn;

        public int Insert(TermoResponsabilidade model, MySqlTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí um identificador!");

            if (!model.isValid().Key)
                throw new Exception("Não é possível inserir, existe inconsistências!");

            string sql = "INSERT INTO termo_responsabilidade (termo, dataCadastro) VALUES (@termo, NOW())";

            var parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@termo", MySqlDbType.String) { Value = model.Termo });

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(TermoResponsabilidade model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(TermoResponsabilidade model, MySqlTransaction transaction) => throw new NotImplementedException();

        public void SetCurrent(TermoResponsabilidade model, MySqlTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM termo_responsabilidade a" +
                         " ORDER BY a.id DESC" +
                         " LIMIT 1";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            if (dt.Rows.Count == 1)
            {
                PreencherModel(model, dt.Rows[0]);
            }
            else if (dt.Rows.Count == 0)
                throw new Exception($"Não existe nenhum termo de responsabilidade cadastrado!");
        }

        public bool Exists(MySqlTransaction transaction)
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
