using System;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.DAO;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;

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

        public bool IsValid(bool isImportacao, Connection conn, SQLiteTransaction transaction, out string mensagem)
        {
            mensagem = "";

            if (string.IsNullOrEmpty(Nome))
            {
                mensagem = "O campo `Nome` é obrigatório!";
                return false;
            }

            if ((DataNascimento != null) && ((DataNascimento.Value.Year < (DateTime.Now.Year - 120)) || (DataNascimento.Value.Date > DateTime.Now.Date)))
            {
                mensagem = "A data de nascimento não é uma data válida!";
                return false;
            }

            if (!isImportacao && string.IsNullOrEmpty(Cpf))
            {
                mensagem = "O campo `CPF` é obrigatório!";
                return false;
            }

            if (!string.IsNullOrEmpty(Cpf) && !Validation.IsCpf(Cpf))
            {
                mensagem = "O campo `CPF` não contém um CPF válido!";
                return false;
            }

            if (ExistsByCpf(Cpf, Id, conn, transaction))
            {
                mensagem = "Este CPF já existe no sistema!";
                return false;
            }

            if ((Telefone.Length != 10) && (Telefone.Length != 11) && (Telefone.Length != 0))
            {
                mensagem = "O campo `Telefone` não é obrigatório, mas se preenchido, não pode estar incompleto";
                return false;
            }

            if (!Validation.IsNumeric(Telefone))
            {
                mensagem = "O campos `Telefone` deve conter somente números!";
                return false;
            }

            if (!isImportacao && string.IsNullOrEmpty(Celular))
            {
                mensagem = "O campo `Celular` é obrigatório!";
                return false;
            }

            if (!string.IsNullOrEmpty(Celular) && Celular.Length != 11)
            {
                mensagem = "O campo `Celular` deve conter 11 dígitos!";
                return false;
            }

            if (!Validation.IsNumeric(Celular))
            {
                mensagem = "O campos `Celular` deve conter somente números!";
                return false;
            }

            if (!string.IsNullOrEmpty(Celular) && ExistsByCelular(Celular, Id, conn, transaction))
            {
                mensagem = "Este Celular já existe no sistema!";
                return false;
            }

            return true;
        }

        public bool IsValid(bool isImportacao, Connection conn, SQLiteTransaction transaction)
        {
            string mensagem;

            return IsValid(isImportacao, conn, transaction, out mensagem);
        }

        public void Salvar(bool isImportacao, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, isImportacao, transaction);
                else
                    dao.Update(this, isImportacao, transaction);
            }
        }

        public void Remover(Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                dao.Remove(this, transaction);
            }
        }

        public bool SetById(int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static List<Cliente> GetAll(List<ClienteFilter> filtros, List<ClienteOrdenation> ordenacoes, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.GetAll(filtros, ordenacoes, transaction);
            }
        }

        public static bool ExistsByCelular(string celular, int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.ExistsByCelular(celular, id, transaction);
            }
        }

        public static bool ExistsByCpf(string cpf, int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.ExistsByCpf(cpf, id, transaction);
            }
        }

        public static bool ExistsByEmail(string email, int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.ExistsByEmail(email, id, transaction);
            }
        }

        public static bool ExistsByNome(string nome, int id, Connection conn, SQLiteTransaction transaction)
        {
            using (var dao = new ClienteDAO(conn))
            {
                return dao.ExistsByNome(nome, id, transaction);
            }
        }

        public void Dispose() { }
    }
}
