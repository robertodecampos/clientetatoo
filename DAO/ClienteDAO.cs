using ClienteTatoo.Model;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.DAO
{
    class ClienteDAO : IDao<Cliente, MySqlTransaction>
    {
        private Connection _conn;

        public ClienteDAO(Connection conn) => _conn = conn;

        public int Insert(Cliente model, MySqlTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador");

            if (!model.IsValid(_conn, transaction))
                throw new Exception("Existem informações inconsistentes!");

            string sql = "INSERT INTO clientes (" +
                         "nome, cpf, dataNascimento, cep, tipoLogradouro, logradouro, numero, bairro, complemento, idCidade, uf, telefone, celular, email, idTermoResponsabilidade" +
                         ") VALUES (" +
                         "@nome, @cpf, @dataNascimento, @cep, @tipoLogradouro, @logradouro, @numero, @bairro, @complemento, @idCidade, @uf, @telefone, @celular, @email, @idTermoResponsabilidade" +
                         ")";

            List<MySqlParameter> parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Cliente model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(Cliente model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (model.IsValid(_conn, transaction))
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE clientes SET" +
                         " nome = @nome, cpf = @cpf, dataNascimento = @dataNascimento, cep = @cep, tipoLogradouro = @tipoLogradouro," +
                         " logradouro = @logradouro, numero = @numero, bairro = @bairro, complemento = @complemento, idCidade = @idCidade," +
                         " uf = @uf, telefone = @telefone, celular = @celular, email = @email, idTermoResponsabilidade = @idTermoResponsabilidade" +
                         " WHERE id = @id";

            List<MySqlParameter> parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public List<Cliente> GetAll(List<ClienteFilter> filtros, List<ClienteOrdenation> ordenacoes, MySqlTransaction transaction)
        {
            var parameters = new List<MySqlParameter>();

            string filtro = Filtrar(filtros, "a", parameters);
            string ordem = Ordenar(ordenacoes, "a");

            string sql = "SELECT *" +
                         " FROM clientes a" +
                         " WHERE a.`removido` = 0" + (filtro != "" ? $" AND {filtro}" : "") +
                         (ordem != "" ? $" ORDER BY {ordem}" : "");

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            var clientes = new List<Cliente>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
            {
                var cliente = new Cliente();
                PreencherModel(cliente, dr);
                clientes.Add(cliente);
            }

            return clientes;
        }

        public bool ExistsByCpf(string cpf, int id, MySqlTransaction transaction)
        {
            string sql = "SELECT COUNT(a.`id`) qtde" +
                         " FROM `clientes` a" +
                         " WHERE a.`id` <> @id AND a.`removido` = 0 AND a.`cpf` = @cpf";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = cpf });
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            return int.Parse(dt.Rows[0]["qtde"].ToString()) > 0;
        }

        private void PreencherModel(Cliente model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.Nome = dr["nome"].ToString();
            model.Cpf = dr["cpf"].ToString();
            DateTime dataNascimento;
            if (DateTime.TryParse(dr["dataNascimento"].ToString(), out dataNascimento))
                model.DataNascimento = dataNascimento;
            else
                model.DataNascimento = null;
            model.Cep = dr["cep"].ToString();
            model.TipoLogradouro = dr["tipoLogradouro"].ToString();
            model.Logradouro = dr["logradouro"].ToString();
            model.Numero = dr["numero"].ToString();
            model.Bairro = dr["bairro"].ToString();
            model.Complemento = dr["complemento"].ToString();
            model.IdCidade = int.Parse(dr["idCidade"].ToString());
            model.Uf = dr["uf"].ToString();
            model.Telefone = dr["telefone"].ToString();
            model.Celular = dr["celular"].ToString();
            model.Email = dr["email"].ToString();
            model.IdTermoResponsabilidade = int.Parse(dr["idTermoResponsabilidade"].ToString());
        }

        private List<MySqlParameter> GetParameters(Cliente model)
        {
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@nome", MySqlDbType.String) { Value = model.Nome });
            parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = model.Cpf });
            parameters.Add(new MySqlParameter("@dataNascimento", MySqlDbType.Date) { Value = model.DataNascimento });
            parameters.Add(new MySqlParameter("@cep", MySqlDbType.String) { Value = model.Cep });
            parameters.Add(new MySqlParameter("@tipoLogradouro", MySqlDbType.String) { Value = model.TipoLogradouro });
            parameters.Add(new MySqlParameter("@logradouro", MySqlDbType.String) { Value = model.Logradouro });
            parameters.Add(new MySqlParameter("@numero", MySqlDbType.String) { Value = model.Numero });
            parameters.Add(new MySqlParameter("@bairro", MySqlDbType.String) { Value = model.Bairro });
            parameters.Add(new MySqlParameter("@complemento", MySqlDbType.String) { Value = model.Complemento });
            parameters.Add(new MySqlParameter("@idCidade", MySqlDbType.Int32) { Value = model.IdCidade });
            parameters.Add(new MySqlParameter("@uf", MySqlDbType.String) { Value = model.Uf });
            parameters.Add(new MySqlParameter("@telefone", MySqlDbType.String) { Value = model.Telefone });
            parameters.Add(new MySqlParameter("@celular", MySqlDbType.String) { Value = model.Celular });
            parameters.Add(new MySqlParameter("@email", MySqlDbType.String) { Value = model.Email });
            parameters.Add(new MySqlParameter("@idTermoResponsabilidade", MySqlDbType.Int32) { Value = model.IdTermoResponsabilidade });

            return parameters;
        }

        private string Filtrar(List<ClienteFilter> filtros, string aliasCliente, List<MySqlParameter> parameters)
        {
            if (filtros == null)
                throw new NullReferenceException("O parâmetros `filtros` não pode ser nulo!");

            if (filtros.Count == 0)
                return "";

            string filtro = "";

            for (int i = 0; i < filtros.Count; i++)
            {
                if (i > 0)
                    filtro += " AND ";

                switch (filtros[i].Field)
                {
                    case FieldFilterCliente.ffcNome:
                        filtro += $"{aliasCliente}.`nome` LIKE @nome";
                        parameters.Add(new MySqlParameter("@nome", MySqlDbType.String) { Value = $"%{(string)filtros[i].Value}%" });
                        break;
                    case FieldFilterCliente.ffcCpf:
                        filtro += $"{aliasCliente}.`cpf` = @cpf";
                        parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = $"{(string)filtros[i].Value}" });
                        break;
                    case FieldFilterCliente.ffcEmail:
                        filtro += $"{aliasCliente}.`email` LIKE @email";
                        parameters.Add(new MySqlParameter("@email", MySqlDbType.String) { Value = $"%{(string)filtros[i].Value}%" });
                        break;
                    case FieldFilterCliente.ffcTelefone:
                        filtro += $"{aliasCliente}.`telefone` LIKE @telefone";
                        parameters.Add(new MySqlParameter("@telefone", MySqlDbType.String) { Value = $"%{(string)filtros[i].Value}%" });
                        break;
                    case FieldFilterCliente.ffcCelular:
                        filtro += $"{aliasCliente}.`celular` LIKE @celular";
                        parameters.Add(new MySqlParameter("@celular", MySqlDbType.String) { Value = $"%{(string)filtros[i].Value}%" });
                        break;
                }
            }

            return filtro;
        }

        private string Ordenar(List<ClienteOrdenation> ordering, string aliasCliente)
        {
            if (ordering == null)
                throw new NullReferenceException("O parâmetro `ordering` não pode ser nulo!");

            if (ordering.Count == 0)
                return "";

            string order = "";

            for (int i = 0; i < ordering.Count; i++)
            {
                if (i > 0)
                    order += ", ";

                switch (ordering[i].Field)
                {
                    case FieldOrdenationCliente.Codigo:
                        order += $"{aliasCliente}.`id`";
                        break;
                    case FieldOrdenationCliente.Nome:
                        order += $"{aliasCliente}.`nome`";
                        break;
                    case FieldOrdenationCliente.DataNascimento:
                        order += $"{aliasCliente}.`dataNascimento`";
                        break;
                }

                if (ordering[i].Type == TypeOrder.toDesc)
                    order += " DESC";
            }

            return order;
        }

        public void Dispose() { }
    }
}
