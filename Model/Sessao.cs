using System;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.DAO;
using ClienteTatoo.Utils;

namespace ClienteTatoo.Model
{
    public class Sessao : IDisposable
    {
        public int Id { get; set; }
        public int IdTatuagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataSessao { get; set; }
        public string Parametros { get; set; }
        public string Disparos { get; set; }
        public string Observacao { get; set; }
        public bool Pago { get; set; }

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            if (Valor <= 0)
            {
                mensagem = "O campo `Valor` é obrigatório e deve ser maior que 0(zero)!";
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
            using (var dao = new SessaoDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Remover(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new SessaoDAO(conn))
            {
                dao.Remove(this, transaction);
            }
        }

        public bool SetById(int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new SessaoDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public void MarcarComoPaga(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new SessaoDAO(conn))
            {
                dao.MarcarComoPaga(this, transaction);
            }
        }

        public static List<Sessao> GetByIdTatuagem(int idTatuagem, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new SessaoDAO(conn))
            {
                return dao.GetByIdTatuagem(idTatuagem, transaction);
            }
        }

        public static int CountByIdTatuagem(int idTatuagem, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new SessaoDAO(conn))
            {
                return dao.CountByIdTatuagem(idTatuagem, transaction);
            }
        }

        public static DateTime? GetDataSessaoOfFirstByIdTatuagem(int idTatuagem, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new SessaoDAO(conn))
            {
                return dao.GetDataSessaoOfFirstByIdTatuagem(idTatuagem, transaction);
            }
        }

        public static DateTime? GetDataSessaoOfLastByIdTatuagem(int idTatuagem, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new SessaoDAO(conn))
            {
                return dao.GetDataSessaoOfLastByIdTatuagem(idTatuagem, transaction);
            }
        }

        public void Dispose() { }
    }
}
