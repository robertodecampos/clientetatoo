using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.Model
{
    public enum TipoPergunta { Cliente, Tatuagem };

    public class Pergunta : IDisposable
    {
        public int Id { get; set; }
        public int? IdResposta { get; set; } = null;
        public string Descricao { get; set; }
        public bool RespostaUnica { get; set; }
        public bool RespostaDissertativa { get; set; }
        public bool Obrigatoria { get; set; }
        public bool Ativada { get; set; } = true;
        public TipoPergunta Tipo { get; set; }

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            if ((Descricao != null) && (Descricao.Trim().Length < 3))
            {
                mensagem = "O campo `Descrição` é obrigatório e deve ter no mínimo 3 caracteres!";
                return false;
            }

            return true;
        }

        public bool IsValid()
        {
            string mensagem;
            return IsValid(out mensagem);
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

        public static List<Pergunta> GetPrincipais(bool somenteAtivas, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                if (somenteAtivas)
                    return dao.GetPrincipaisAtivas(transaction);
                else
                    return dao.GetPrincipais(transaction);
            }
        }

        public static List<Pergunta> GetByIdResposta(int idResposta, bool somenteAtivas, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new PerguntaDAO(conn))
            {
                if (somenteAtivas)
                    return dao.GetAtivasByIdResposta(idResposta, transaction);
                else
                    return dao.GetByIdResposta(idResposta, transaction);
            }
        }

        public void Dispose() {}
    }
}
