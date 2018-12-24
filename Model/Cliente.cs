using ClienteTatoo.DAO;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.Model
{
    class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
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
        public int IdTermoResponsabilidade { get; set; }

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            if (String.IsNullOrEmpty(Nome))
            {
                mensagem = "O campo nome é obrigatório!";
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
            using (var dao = new ClienteDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }
    }
}
