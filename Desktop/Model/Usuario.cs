using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Usuario: IDisposable
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public bool IsValid(out string mensagem, Connection connection, SQLiteTransaction transaction)
        {
            mensagem = string.Empty;

            if (string.IsNullOrEmpty(Nome))
            {
                mensagem = "O nome do usuário é obrigatório!";
                return false;
            }

            if (string.IsNullOrEmpty(Login))
            {
                mensagem = "O login do usuário é obrigatório!";
                return false;
            }

            if (ExistsByLogin(Login, Id, connection, transaction))
            {
                mensagem = "Este login já esta sendo utilizado por outro usuário!";
                return false;
            }

            return true;
        }

        public bool IsValid(Connection connection, SQLiteTransaction transaction)
        {
            string mensagem;

            return IsValid(out mensagem, connection, transaction);
        }

        public bool Inserir(string senha, string confirmacaoSenha, out string mensagem, Connection connection, SQLiteTransaction transaction)
        {
            if (!IsValid(out mensagem, connection, transaction))
                return false;

            if (string.IsNullOrEmpty(senha))
            {
                mensagem = "A senha do usuário é obrigatória!";
                return false;
            }

            if (string.IsNullOrEmpty(confirmacaoSenha))
            {
                mensagem = "A confirmação de senha do usuário é obrigatória!";
                return false;
            }

            if (!string.Equals(senha, confirmacaoSenha, StringComparison.InvariantCulture))
            {
                mensagem = "As senhas não coincidem!";
                return false;
            }

            using (var dao = new UsuarioDAO(connection))
            {
                return (dao.Insert(this, senha, transaction) == 1);
            }
        }

        public bool Salvar(out string mensagem, Connection connection, SQLiteTransaction transaction)
        {
            if (!IsValid(out mensagem, connection, transaction))
                return false;

            using (var dao = new UsuarioDAO(connection))
            {
                return (dao.Update(this, transaction) > 0);
            }
        }

        public bool SetById(int id, Connection connection, SQLiteTransaction transaction)
        {
            using (var dao = new UsuarioDAO(connection))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static bool ExistsByLogin(string login, int id, Connection connection, SQLiteTransaction transaction)
        {
            using (var dao = new UsuarioDAO(connection))
            {
                return dao.ExistsByLogin(login, id, transaction);
            }
        }

        public static bool Logar(string login, string senha, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new UsuarioDAO(conn))
            {
                return dao.Login(login, senha, transaction);
            }
        }

        public static List<Usuario> GetAll(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new UsuarioDAO(conn))
            {
                return dao.GetAll(transaction);
            }
        }

        public void Dispose() { }
    }
}
