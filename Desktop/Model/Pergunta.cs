using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ClienteTatoo.Model
{
    public enum TipoPergunta { Cliente, Tatuagem };

    public class Pergunta : IDisposable
    {
        public int Id { get; set; }
        public int? IdAlternativa { get; set; } = null;
        public string Descricao { get; set; }
        public string CodigoImportacao { get; set; }
        public bool AlternativaUnica { get; set; }
        public bool Dissertativa { get; set; }
        public bool Obrigatoria { get; set; }
        public bool Ativada { get; set; } = true;
        public int ColunasAlternativas { get; set; }
        public TipoPergunta Tipo { get; set; }

        public bool IsValid(out string mensagem, Connection conn, SQLiteTransaction transaction)
        {
            mensagem = "";

            if ((Descricao != null) && (Descricao.Trim().Length < 3))
            {
                mensagem = "O campo `Descrição` é obrigatório e deve ter no mínimo 3 caracteres!";
                return false;
            }

            if (string.IsNullOrEmpty(CodigoImportacao))
            {
                mensagem = "O campo `Código de Importação` é obrigatório!";
                return false;
            }

            if (ExistsByCodigoImportacao(CodigoImportacao, Id, conn, transaction))
            {
                mensagem = "Já existe uma pergunta cadastrada com este Código de Importação!";
                return false;
            }

            if ((AlternativaUnica || !Dissertativa) && (ColunasAlternativas <= 0))
            {
                mensagem = "É necessário ter ao menos 1 coluna para perguntas com alternativas!";
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
            using (var dao = new PerguntaDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Remover(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                dao.Remove(this, transaction);
            }
        }

        public bool SetById(int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static List<Pergunta> GetAllAtivas(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                return dao.GetAllAtivas(transaction);
            }
        }

        public static List<Pergunta> GetPrincipaisByTipoPergunta(TipoPergunta tipoPergunta, bool somenteAtivas, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                if (somenteAtivas)
                    return dao.GetPrincipaisAtivasByTipoPergunta(tipoPergunta, transaction);
                else
                    return dao.GetPrincipaisByTipoPergunta(tipoPergunta, transaction);
            }
        }

        public static List<Pergunta> GetByIdAlternativa(int idAlternativa, bool somenteAtivas, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                if (somenteAtivas)
                    return dao.GetAtivasByIdAlternativa(idAlternativa, transaction);
                else
                    return dao.GetByIdAlternativa(idAlternativa, transaction);
            }
        }

        public static Pergunta GetAtivaByCodigoImportacao(string codigoImportacao, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                return dao.GetAtivaByCodigoImportacao(codigoImportacao, transaction);
            }
        }

        public static bool ExistsByCodigoImportacao(string codigoImportacao, int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                return dao.ExistsByCodigoImportacao(codigoImportacao, id, transaction);
            }
        }

        public void Dispose() {}
    }
}
