using ClienteTatoo.DAO;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ClienteTatoo.Model
{
    public class Cliente : IDisposable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cep { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public int IdCidade { get; set; }
        public string Uf { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public bool IsValid(Connection conn, MySqlTransaction transaction, out string mensagem)
        {
            mensagem = "";

            if (string.IsNullOrEmpty(Nome))
            {
                mensagem = "O campo `Nome` é obrigatório!";
                return false;
            }

            if (string.IsNullOrEmpty(Cpf))
            {
                mensagem = "O campo `CPF` é obrigatório!";
                return false;
            }

            if (!Validation.IsCpf(Cpf))
            {
                mensagem = "O campo `CPF` não contém um CPF válido!";
                return false;
            }

            if (ExistsByCpf(Cpf, Id, conn, transaction))
            {
                mensagem = "Este CPF já existe no sistema!";
                return false;
            }

            if (string.IsNullOrEmpty(Telefone))
            {
                mensagem = "O campo `Telefone` é obrigatório!";
                return false;
            }

            if (Telefone.Length != 10)
            {
                mensagem = "O campo `Telefone` deve conter 10 dígitos!";
                return false;
            }

            if (!Validation.IsNumeric(Telefone))
            {
                mensagem = "O campos `Telefone` deve conter somente números!";
                return false;
            }

            if (string.IsNullOrEmpty(Celular))
            {
                mensagem = "O campo `Celular` é obrigatório!";
                return false;
            }

            if (Celular.Length != 11)
            {
                mensagem = "O campo `Celular` deve conter 11 dígitos!";
                return false;
            }

            if (!Validation.IsNumeric(Celular))
            {
                mensagem = "O campos `Celular` deve conter somente números!";
                return false;
            }

            return true;
        }

        public bool IsValid(Connection conn, MySqlTransaction transaction)
        {
            string mensagem;

            return IsValid(conn, transaction, out mensagem);
        }

        public void Salvar(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Remover(Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                dao.Remove(this, transaction);
            }
        }

        public bool SetById(int id, Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static List<Cliente> GetAll(List<ClienteFilter> filtros, List<ClienteOrdenation> ordenacoes, Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.GetAll(filtros, ordenacoes, transaction);
            }
        }

        public static bool ExistsByCpf(string cpf, int id, Connection conn, MySqlTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.ExistsByCpf(cpf, id, transaction);
            }
        }

        public void Dispose() { }
    }
}
