using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    class UsuarioDAO : IDao<Usuario, SQLiteTransaction>
    {
        private Connection _conn;

        public UsuarioDAO(Connection conn) => _conn = conn;

        public int Insert(Usuario model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Insert(Usuario model, string senha, SQLiteTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador!");

            string mensagem = null;

            if (!model.IsValid(out mensagem, _conn, transaction))
                throw new Exception("Erro ao validar usuário: " + mensagem);

            string sql = "INSERT INTO usuario (login, senha, nome, ativo)" +
                         " VALUES (@login, @senha, @nome, @ativo)";

            var parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@senha", DbType.String) { Value = Cryptography.EncondeMD5(senha) });

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Usuario model, SQLiteTransaction transaction) => throw new NotImplementedException();

        public int Update(Usuario model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            string mensagem = null;

            if (!model.IsValid(out mensagem, _conn, transaction))
                throw new Exception("Erro ao validar usuário: " + mensagem);

            string sql = " UPDATE usuario SET                             " +
                         "   login = @login, nome = @nome, ativo = @ativo " +
                         " WHERE id = @id                                 ";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool Login(string login, string senha, SQLiteTransaction transaction)
        {
            string sql = " SELECT *             " +
                         " FROM usuario a       " +
                         " WHERE login = @login " +
                         "   AND senha = @senha " +
                         "   AND ativo = 1      ";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@login", DbType.String) { Value = login });
            parameters.Add(new SQLiteParameter("@senha", DbType.String) { Value = Cryptography.EncondeMD5(senha) });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            return (dt.Rows.Count == 1);
        }

        public bool ExistsByLogin(string login, int idUsuario, SQLiteTransaction transaction)
        {
            string sql =
                " SELECT                   " +
                "   a.`id`                 " +
                " FROM usuario a           " +
                " WHERE a.`login` = @login " +
                "   AND a.`id` != @id      ";

            var parameters = new List<SQLiteParameter> {
                new SQLiteParameter("@login", DbType.String) { Value = login },
                new SQLiteParameter("@id", DbType.Int32) { Value = idUsuario }
            };

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            return (dt.Rows.Count > 0);
        }

        public bool SetById(Usuario model, int id, SQLiteTransaction transaction)
        {
            string sql = " SELECT *           " +
                         " FROM usuario a     " +
                         " WHERE a.`id` = @id ";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} usuários com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public List<Usuario> GetAll(SQLiteTransaction transaction)
        {
            string sql =
                " SELECT                                   " +
                "   a.`id`, a.`login`, a.`nome`, a.`ativo` " +
                " FROM usuario a                           ";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            var usuarios = new List<Usuario>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var usuario = new Usuario();
                PreencherModel(usuario, dr);
                usuarios.Add(usuario);
            }

            return usuarios;
        }

        private void PreencherModel(Usuario model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.Login = dr["login"].ToString();
            model.Nome = dr["nome"].ToString();
            model.Ativo = (int.Parse(dr["ativo"].ToString()) == 1);
        }

        private List<SQLiteParameter> GetParameters(Usuario model)
        {
            var parameters = new List<SQLiteParameter>();

            parameters.Add(new SQLiteParameter("@login", DbType.String) { Value = model.Login });
            parameters.Add(new SQLiteParameter("@nome", DbType.String) { Value = model.Nome });
            parameters.Add(new SQLiteParameter("@ativo", DbType.Int32) { Value = model.Ativo });

            return parameters;
        }

        public void Dispose() { }
    }
}
