using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.Exceptions;
using TatooReport.Models;
using TatooReport.Utils;

namespace TatooReport.DAO
{
    public class UserDAO : IDisposable
    {
        private readonly Connection connection;

        public UserDAO(Connection connection)
        {
            this.connection = connection;
        }

        public int Insert(User user, MySqlTransaction transaction = null)
        {
            if (user.Id != 0)
                throw new UserException("Não é possível inserir um registro que já possui um identificador!");

            string sql = "INSERT INTO user (";
            sql += " cpf, `name`, surname, email, phone, mobilePhone, normalizedName, passwordHash";
            sql += ") VALUES (";
            sql += " @cpf, @name, @surname, @email, @phone, @mobilePhone, @normalizedName, @passwordHash";
            sql += ")";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = user.Cpf });
            parameters.Add(new MySqlParameter("@name", MySqlDbType.String) { Value = user.Nome });
            parameters.Add(new MySqlParameter("@surname", MySqlDbType.String) { Value = user.Sobrenome });
            parameters.Add(new MySqlParameter("@email", MySqlDbType.String) { Value = user.Email });
            parameters.Add(new MySqlParameter("@phone", MySqlDbType.String) { Value = user.Telefone });
            parameters.Add(new MySqlParameter("@mobilePhone", MySqlDbType.String) { Value = user.Celular });
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = user.NormalizedUserName });
            parameters.Add(new MySqlParameter("@passwordHash", MySqlDbType.String) { Value = user.PasswordHash });

            int linhasAfetadas = connection.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            user.Id = connection.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Update(User user, MySqlTransaction transaction = null)
        {
            if (user.Id == 0)
                throw new UserException("Não é possível alterar um registro que não possui um identificador!");

            string sql = "UPDATE Usuario SET";
            sql += " cpf = @cpf, name = @name, surname = @surname, email = @email,";
            sql += " phone = @phone, mobilePhone = @mobilePhone,";
            sql += " normalizedName = @normalizedName, passwordHash = @passwordHash";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = user.Cpf });
            parameters.Add(new MySqlParameter("@name", MySqlDbType.String) { Value = user.Nome });
            parameters.Add(new MySqlParameter("@surname", MySqlDbType.String) { Value = user.Sobrenome });
            parameters.Add(new MySqlParameter("@email", MySqlDbType.String) { Value = user.Email });
            parameters.Add(new MySqlParameter("@phone", MySqlDbType.String) { Value = user.Telefone });
            parameters.Add(new MySqlParameter("@mobilePhone", MySqlDbType.String) { Value = user.Celular });
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = user.NormalizedUserName });
            parameters.Add(new MySqlParameter("@passwordHash", MySqlDbType.String) { Value = user.PasswordHash });

            return connection.Execute(sql, parameters, transaction);
        }

        public User FindById(int id, MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM user";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            var user = new User();
            DistributeData(user, dt.Rows[0]);

            return user;
        }

        public User FindByNormalizedName(string normalizedName, MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM user";
            sql += " WHERE normalizedName = @normalizedName";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = normalizedName });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            var user = new User();
            DistributeData(user, dt.Rows[0]);

            return user;
        }

        public List<User> FindInRole(int roleId, MySqlTransaction transaction = null)
        {
            string sql = "SELECT b.*";
            sql += " FROM user_role a";
            sql += " INNER JOIN user b ON a.userId = b.id";
            sql += " WHERE a.roleId = @roleId";
            sql += " AND a.removed = 0";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@roleId", MySqlDbType.String) { Value = roleId });

            using (DataTable dt = connection.ExecuteReader(sql, parameters, transaction))
            {
                var users = new List<User>();

                foreach (DataRow row in dt.Rows)
                {
                    var user = new User();
                    DistributeData(user, row);
                    users.Add(user);
                }

                return users;
            }
        }

        public List<User> FindByBranchNetworkId(int branchNetworkId, MySqlTransaction transaction)
        {
            string sql = "SELECT b.*";
            sql += " FROM `branch_network_user` a";
            sql += " INNER JOIN `user` b ON a.`userId` = b.id";
            sql += " WHERE a.`branchNetworkId` = @branchNetworkId";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@branchNetworkId", MySqlDbType.String) { Value = branchNetworkId });

            using (DataTable dt = connection.ExecuteReader(sql, parameters, transaction))
            {
                var users = new List<User>();

                foreach (DataRow row in dt.Rows)
                {
                    var user = new User();
                    DistributeData(user, row);
                    users.Add(user);
                }

                return users;
            }
        }

        private void DistributeData(User user, DataRow dr)
        {
            user.Id = int.Parse(dr["id"].ToString());
            user.Cpf = dr["cpf"].ToString();
            user.UserName = dr["cpf"].ToString();
            user.Nome = dr["name"].ToString();
            user.Sobrenome = dr["surname"].ToString();
            user.Email = dr["email"].ToString();
            user.Telefone = dr["phone"].ToString();
            user.Celular = dr["mobilePhone"].ToString();
            user.NormalizedUserName = dr["normalizedName"].ToString();
            user.PasswordHash = dr["passwordHash"].ToString();
        }

        public void Dispose() { }
    }
}
