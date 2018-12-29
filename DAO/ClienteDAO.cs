using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

            if (!model.IsValid())
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

            if (model.IsValid())
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

        private List<MySqlParameter> GetParameters(Cliente model)
        {
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@nome", MySqlDbType.String) { Value = model.Nome });
            parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = model.Cpf });
            if (model.DataNascimento.Date == DateTime.Now.Date)
                parameters.Add(new MySqlParameter("@dataNascimento", MySqlDbType.Date) { Value = null });
            else
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

        public void Dispose() { }
    }
}
