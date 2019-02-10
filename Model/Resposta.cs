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
    public class Resposta : IDisposable
    {
        public int Id { get; set; }
        public int IdPergunta { get; set; }
        public string Descricao { get; set; }
        public bool Especificar { get; set; }
        public bool Ativada { get; set; } = true;

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            if (Descricao.Trim().Length < 3)
            {
                mensagem = "O campo `Descrição` é obrigatório e deve ter ao menos 3 caracteres!";
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
            using (var dao = new RespostaDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Remover(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new RespostaDAO(conn))
            {
                dao.Remove(this, transaction);
            }
        }

        public bool SetById(int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new RespostaDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static List<Resposta> GetByIdPergunta(int idPergunta, bool somenteAtivas, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new RespostaDAO(conn))
            {
                if (somenteAtivas)
                    return dao.GetAtivasByIdPergunta(idPergunta, transaction);
                else
                    return dao.GetByIdPergunta(idPergunta, transaction);
            }
        }

        public void Dispose() {}
    }
}
