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
    public class UserRoleDAO : IDisposable
    {
        private readonly Connection connection;

        public UserRoleDAO(Connection connection)
        {
            this.connection = connection;
        }

        public int Insert(UserRole model, MySqlTransaction transaction)
        {
            if (model.Id != 0)
                throw new UserRoleException("Não é possível inserir um registro que já possui um identificador!");

            UserRole userRole = FindByUserIdAndRoleIdAndDeleted(model.UserId, model.RoleId, transaction);
            if (userRole != null)
                return Active(userRole, transaction);

            string sql = "INSERT INTO user_role (";
            sql += " userId, roleId";
            sql += ") VALUES (";
            sql += " @userId, @roleId";
            sql += ")";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@userId", MySqlDbType.Int32) { Value = model.UserId });
            parameters.Add(new MySqlParameter("@roleId", MySqlDbType.Int32) { Value = model.RoleId });

            int linhasAfetadas = connection.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = connection.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(UserRole model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new UserRoleException("Não é possível remover um registro que não possui um identificador!");

            string sql = "UPDATE user_role SET";
            sql += " removed = 1";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            return connection.Execute(sql, parameters, transaction);
        }

        public int Active(UserRole model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new UserRoleException("Não é possível remover um registro que não possui um identificador!");

            string sql = "UPDATE user_role SET";
            sql += " removed = 0";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });

            return connection.Execute(sql, parameters, transaction);
        }

        public int Update(UserRole model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new UserRoleException("Não é possível alterar um registro que não possui um identificador!");

            string sql = "UPDATE user_role SET";
            sql += " userId = @userId, roleId = @roleId";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = model.Id });
            parameters.Add(new MySqlParameter("@userId", MySqlDbType.String) { Value = model.UserId });
            parameters.Add(new MySqlParameter("@roleId", MySqlDbType.String) { Value = model.RoleId });

            return connection.Execute(sql, parameters, transaction);
        }

        public UserRole FindByUserIdAndRoleId(int userId, int roleId, MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM user_role";
            sql += " WHERE userId = @userId AND roleId = @roleId";
            sql += " AND removed = 0";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId });
            parameters.Add(new MySqlParameter("@roleId", MySqlDbType.Int32) { Value = roleId });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            using (var userRole = new UserRole())
            {
                DistributeData(userRole, dt.Rows[0]);

                return userRole;
            }
        }

        public UserRole FindByUserIdAndNormalizedRole(string normalizedRole, MySqlTransaction transaction = null)
        {
            string sql = "SELECT b.*";
            sql += " FROM role a";
            sql += " INNER JOIN user_role b ON a.id = b.roleId";
            sql += " WHERE a.normalizedName = @normalizedName";
            sql += " AND removed = 0";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = normalizedRole });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            using (var userRole = new UserRole())
            {
                DistributeData(userRole, dt.Rows[0]);

                return userRole;
            }
        }

        public bool ExistsUserInRole(int userId, int roleId, MySqlTransaction transaction = null)
        {
            string sql = "SELECT a.id";
            sql += " FROM user_role a";
            sql += " WHERE a.userId = @userId";
            sql += " AND a.roleId = @roleId";
            sql += " AND a.removed = 0";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@userId", MySqlDbType.String) { Value = userId });
            parameters.Add(new MySqlParameter("@roleId", MySqlDbType.String) { Value = roleId });

            using (DataTable dt = connection.ExecuteReader(sql, parameters, null))
            {
                return (dt.Rows.Count > 0);
            }
        }

        public bool ExistsUserInRole(int userId, string normalizedRoleName, MySqlTransaction transaction = null)
        {
            string sql = "SELECT b.id";
            sql += " FROM role a";
            sql += " INNER JOIN user_role b ON a.id = b.roleId AND b.userId = @userId AND b.removed = 0";
            sql += " WHERE a.normalizedName = @normalizedName";
            sql += " AND a.removed = 0";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId });
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = normalizedRoleName });

            using (DataTable dt = connection.ExecuteReader(sql, parameters, null))
            {
                return (dt.Rows.Count > 0);
            }
        }

        public IList<string> FindRoleNameByUserId(int userId, MySqlTransaction transaction = null)
        {
            string sql = "SELECT b.name";
            sql += " FROM user_role a";
            sql += " INNER JOIN role b ON a.roleId = b.id";
            sql += " WHERE a.userId = @userId";
            sql += " AND a.removed = 0";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId });

            using (DataTable rows = connection.ExecuteReader(sql, parameters, null))
            {
                var names = new List<string>();
                foreach (DataRow row in rows.Rows)
                    names.Add(row["name"].ToString());
                return names;
            }
        }

        public IList<string> FindRoleNormalizedNameByUserId(int userId, MySqlTransaction transaction = null)
        {
            string sql = "SELECT b.normalizedName";
            sql += " FROM user_role a";
            sql += " INNER JOIN role b ON a.roleId = b.id";
            sql += " WHERE a.userId = @userId";
            sql += " AND a.removed = 0";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId });

            using (DataTable rows = connection.ExecuteReader(sql, parameters, null))
            {
                var names = new List<string>();
                foreach (DataRow row in rows.Rows)
                    names.Add(row["narmalizedNome"].ToString());
                return names;
            }
        }

        private void DistributeData(UserRole userRole, DataRow dr)
        {
            userRole.Id = int.Parse(dr["id"].ToString());
            userRole.UserId = int.Parse(dr["userId"].ToString());
            userRole.RoleId = int.Parse(dr["roleId"].ToString());
        }

        private UserRole FindByUserIdAndRoleIdAndDeleted(int usuarioId, int roleId, MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM user_role";
            sql += " WHERE userId = @userId AND roleId = @roleId";
            sql += " AND removed = 1";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@usuarioId", MySqlDbType.Int32) { Value = usuarioId });
            parameters.Add(new MySqlParameter("@roleId", MySqlDbType.Int32) { Value = roleId });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            using (var userRole = new UserRole())
            {
                DistributeData(userRole, dt.Rows[0]);

                return userRole;
            }
        }

        public void Dispose() { }
    }
}
