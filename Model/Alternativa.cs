using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Alternativa : IDisposable
    {
        public int Id { get; set; }
        public int IdPergunta { get; set; }
        public string Descricao { get; set; }
        public bool Ativada { get; set; } = true;

        public bool IsValid(out string mensagem, Connection conn, SQLiteTransaction transaction)
        {
            mensagem = "";

            if (Descricao.Trim().Length < 3)
            {
                mensagem = "O campo `Descrição` é obrigatório e deve ter ao menos 3 caracteres!";
                return false;
            }

            if (Alternativa.ExistsByIdPerguntaAndDescricao(Id, IdPergunta, Descricao.Trim(), conn, transaction))
            {
                mensagem = "Já existe uma resposta ativa com essa descrição para essa pergunta!";
                return false;
            }

            return true;
        }

        public bool IsValid(Connection conn, SQLiteTransaction transaction)
        {
            string mensagem;
            return IsValid(out mensagem, conn, transaction);
        }

        public void Salvar(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new AlternativaDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Remover(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new AlternativaDAO(conn))
            {
                dao.Remove(this, transaction);
            }
        }

        public bool SetById(int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new AlternativaDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static List<Alternativa> GetByIdPergunta(int idPergunta, bool somenteAtivas, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new AlternativaDAO(conn))
            {
                if (somenteAtivas)
                    return dao.GetAtivasByIdPergunta(idPergunta, transaction);
                else
                    return dao.GetByIdPergunta(idPergunta, transaction);
            }
        }

        public static Alternativa GetAtivaByIdPerguntaAndDescricao(int idPergunta, string descricao, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new AlternativaDAO(conn))
            {
                return dao.GetAtivaByIdPerguntaAndDescricao(idPergunta, descricao, transaction);
            }
        }

        public static bool ExistsByIdPerguntaAndDescricao(int idAlternativa, int idPergunta, string descricao, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new AlternativaDAO(conn))
            {
                return dao.ExistsByIdPerguntaAndDescricao(idAlternativa, idPergunta, descricao, transaction);
            }
        }

        public void Dispose() {}
    }
}
