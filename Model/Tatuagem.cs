using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ClienteTatoo.Model
{
    public class Tatuagem : IDisposable
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Local { get; set; }
        public string Desenho { get; set; }
        public int IdTermoResponsabilidade { get; set; }

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            if (string.IsNullOrEmpty(Local))
            {
                mensagem = "O campo `Local` é obrigatório!";
                return false;
            }

            if (string.IsNullOrEmpty(Desenho))
            {
                mensagem = "O campo `Desenho` é obrigatório!";
                return false;
            }

            return true;
        }

        public bool IsValid()
        {
            string mensagem;

            return IsValid(out mensagem);
        }

        public void Salvar(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TatuagemDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Remover(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TatuagemDAO(conn))
            {
                dao.Remove(this, transaction);
            }
        }

        public bool SetById(int id, Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TatuagemDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static List<Tatuagem> GetAll(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TatuagemDAO(conn))
            {
                return dao.GetAll(transaction);
            }
        }

        public static List<Tatuagem> GetByIdCliente(int idCliente, Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new TatuagemDAO(conn))
            {
                return dao.GetByIdCliente(idCliente, transaction);
            }
        }

        public void Dispose() { }
    }
}
