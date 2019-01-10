using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using ClienteTatoo.Model;
using ClienteTatoo.Model.Filter;
using ClienteTatoo.Model.Ordenation;
using ClienteTatoo.Utils;

namespace ClienteTatoo.DAO
{
    class ClienteDAO : IDao<Cliente, SQLiteTransaction>
    {
        private Connection _conn;

        public ClienteDAO(Connection conn) => _conn = conn;

        public int Insert(Cliente model, SQLiteTransaction transaction)
        {
            if (model.Id != 0)
                throw new Exception("Não é possível inserir um registro que já possuí identificador");

            if (!model.IsValid(_conn, transaction))
                throw new Exception("Existem informações inconsistentes!");

            string sql = "INSERT INTO clientes (" +
                         "nome, cpf, dataNascimento, cep, tipoLogradouro, logradouro, numero, bairro, complemento, idCidade, uf, telefone, celular, email" +
                         ") VALUES (" +
                         "@nome, @cpf, @dataNascimento, @cep, @tipoLogradouro, @logradouro, @numero, @bairro, @complemento, @idCidade, @uf, @telefone, @celular, @email" +
                         ")";

            List<SQLiteParameter> parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Cliente model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível remover um registro que não possuí identificador");

            string sql = "UPDATE clientes SET" +
                         " removido = 1" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public int Update(Cliente model, SQLiteTransaction transaction)
        {
            if (model.Id == 0)
                throw new Exception("Não é possível alterar um registro que não possuí identificador");

            if (!model.IsValid(_conn, transaction))
                throw new Exception("Existem informações inconsistentes!");

            string sql = "UPDATE clientes SET" +
                         " nome = @nome, cpf = @cpf, dataNascimento = @dataNascimento, cep = @cep, tipoLogradouro = @tipoLogradouro," +
                         " logradouro = @logradouro, numero = @numero, bairro = @bairro, complemento = @complemento, idCidade = @idCidade," +
                         " uf = @uf, telefone = @telefone, celular = @celular, email = @email" +
                         " WHERE id = @id";

            List<SQLiteParameter> parameters = GetParameters(model);
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool SetById(Cliente model, int id, SQLiteTransaction transaction)
        {
            string sql = "SELECT *" +
                         " FROM clientes a" +
                         " WHERE a.`id` = @id AND a.`removido` = 0";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} clientes com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public List<Cliente> GetAll(List<ClienteFilter> filtros, List<ClienteOrdenation> ordenacoes, SQLiteTransaction transaction)
        {
            var parameters = new List<SQLiteParameter>();

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

        public bool ExistsByCpf(string cpf, int id, SQLiteTransaction transaction)
        {
            string sql = "SELECT COUNT(a.`id`) qtde" +
                         " FROM `clientes` a" +
                         " WHERE a.`id` <> @id AND a.`removido` = 0 AND a.`cpf` = @cpf";

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@cpf", DbType.String) { Value = cpf });
            parameters.Add(new SQLiteParameter("@id", DbType.Int32) { Value = id });

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
        }

        private List<SQLiteParameter> GetParameters(Cliente model)
        {
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@nome", DbType.String) { Value = model.Nome });
            parameters.Add(new SQLiteParameter("@cpf", DbType.String) { Value = model.Cpf });
            parameters.Add(new SQLiteParameter("@dataNascimento", DbType.Date) { Value = model.DataNascimento });
            parameters.Add(new SQLiteParameter("@cep", DbType.String) { Value = model.Cep });
            parameters.Add(new SQLiteParameter("@tipoLogradouro", DbType.String) { Value = model.TipoLogradouro });
            parameters.Add(new SQLiteParameter("@logradouro", DbType.String) { Value = model.Logradouro });
            parameters.Add(new SQLiteParameter("@numero", DbType.String) { Value = model.Numero });
            parameters.Add(new SQLiteParameter("@bairro", DbType.String) { Value = model.Bairro });
            parameters.Add(new SQLiteParameter("@complemento", DbType.String) { Value = model.Complemento });
            parameters.Add(new SQLiteParameter("@idCidade", DbType.Int32) { Value = model.IdCidade });
            parameters.Add(new SQLiteParameter("@uf", DbType.String) { Value = model.Uf });
            parameters.Add(new SQLiteParameter("@telefone", DbType.String) { Value = model.Telefone });
            parameters.Add(new SQLiteParameter("@celular", DbType.String) { Value = model.Celular });
            parameters.Add(new SQLiteParameter("@email", DbType.String) { Value = model.Email });

            return parameters;
        }

        private string Filtrar(List<ClienteFilter> filtros, string aliasCliente, List<SQLiteParameter> parameters)
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
                        parameters.Add(new SQLiteParameter("@nome", DbType.String) { Value = $"%{(string)filtros[i].Value}%" });
                        break;
                    case FieldFilterCliente.ffcCpf:
                        filtro += $"{aliasCliente}.`cpf` = @cpf";
                        parameters.Add(new SQLiteParameter("@cpf", DbType.String) { Value = $"{(string)filtros[i].Value}" });
                        break;
                    case FieldFilterCliente.ffcEmail:
                        filtro += $"{aliasCliente}.`email` LIKE @email";
                        parameters.Add(new SQLiteParameter("@email", DbType.String) { Value = $"%{(string)filtros[i].Value}%" });
                        break;
                    case FieldFilterCliente.ffcTelefone:
                        filtro += $"{aliasCliente}.`telefone` LIKE @telefone";
                        parameters.Add(new SQLiteParameter("@telefone", DbType.String) { Value = $"%{(string)filtros[i].Value}%" });
                        break;
                    case FieldFilterCliente.ffcCelular:
                        filtro += $"{aliasCliente}.`celular` LIKE @celular";
                        parameters.Add(new SQLiteParameter("@celular", DbType.String) { Value = $"%{(string)filtros[i].Value}%" });
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
