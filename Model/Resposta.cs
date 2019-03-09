using System.Collections.Generic;
using System.Data.SQLite;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Resposta
    {
        public int Id { get; set; }
        public int IdPergunta { get; set; }
        public int IdReferencia { get; set; }
        public int IdAlternativa { get; set; }
        public string RespostaDissertativa { get; set; }

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            if (IdPergunta == 0)
            {
                mensagem = "A propriedade `IdPergunta` não foi informada!";
                return false;
            }

            if (IdReferencia == 0)
            {
                mensagem = "A propriedade `IdReferencia` não foi informada!";
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

        public static IList<Resposta> GetByTipoPerguntaAndIdReferencia(TipoPergunta tipoPergunta, int idReferencia, bool somenteAtivas, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new RespostaDAO(conn))
            {
                return dao.GetByTipoPerguntaAndIdReferencia(tipoPergunta, idReferencia, somenteAtivas, transaction);
            }
        }

        public static void LimparRespostasByTipoPerguntaAndIdPerguntaAndIdReferencia(TipoPergunta tipoPergunta, int idPergunta, int idReferencia, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new RespostaDAO(conn))
            {
                dao.LimparRespostasByTipoPerguntaAndIdPerguntaAndIdReferencia(tipoPergunta, idPergunta, idReferencia, transaction);
            }
        }

        public static void SalvarRespostas(TipoPergunta tipoPergunta, int idReferencia, IList<KeyValuePair<Pergunta, IList<Resposta>>> perguntasRespostas, Connection conn, SQLiteTransaction transaction)
        {
            foreach (KeyValuePair<Pergunta, IList<Resposta>> perguntaRespostas in perguntasRespostas)
            {
                Resposta.LimparRespostasByTipoPerguntaAndIdPerguntaAndIdReferencia(TipoPergunta.Cliente, perguntaRespostas.Key.Id, idReferencia, conn, transaction);

                foreach (Resposta resposta in perguntaRespostas.Value)
                {
                    resposta.IdReferencia = idReferencia;

                    resposta.Salvar(conn, transaction);
                }
            }
        }
    }
}
