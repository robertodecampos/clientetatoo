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
    public class RoleDAO : IDisposable
    {
        private readonly Connection connection;

        public RoleDAO(Connection connection)
        {
            this.connection = connection;
        }

        public int Insert(Role role, MySqlTransaction transaction)
        {
            if (role.Id != 0)
                throw new RoleException("Não é possível inserir um registro que já possui um identificador!");

            string sql = "INSERT INTO role (";
            sql += " `name`, normalizedName";
            sql += ") VALUES (";
            sql += " @name, @normalizedName";
            sql += ")";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@name", MySqlDbType.String) { Value = role.Name });
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = role.NormalizedName });

            int linhasAfetadas = connection.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            role.Id = connection.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Update(Role role, MySqlTransaction transaction)
        {
            if (role.Id == 0)
                throw new RoleException("Não é possível alterar um registro que não possui um identificador!");

            string sql = "UPDATE role SET";
            sql += " `name` = @name, normalizedName = @normalizedName";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = role.Id });
            parameters.Add(new MySqlParameter("@name", MySqlDbType.String) { Value = role.Name });
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = role.NormalizedName });

            return connection.Execute(sql, parameters, transaction);
        }

        public int Remove(Role role, MySqlTransaction transaction)
        {
            if (role.Id == 0)
                throw new RoleException("Não é possível remover um registro que não possui um identificador!");

            string sql = "UPDATE role SET";
            sql += " removed = 1";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = role.Id });

            return connection.Execute(sql, parameters, transaction);
        }

        public Role FindById(int id, MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM role";
            sql += " WHERE id = @id";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            var role = new Role();
            DistributeData(role, dt.Rows[0]);

            return role;
        }

        public Role FindByNormalizedName(string normalizedName, MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM role";
            sql += " WHERE normalizedName = @normalizedName";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@normalizedName", MySqlDbType.String) { Value = normalizedName });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            var role = new Role();
            DistributeData(role, dt.Rows[0]);

            return role;
        }

        public Role FindByName(string nome, MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM role";
            sql += " WHERE `name` = @name";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@name", MySqlDbType.String) { Value = nome });

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            var role = new Role();
            DistributeData(role, dt.Rows[0]);

            return role;
        }

        public List<Role> FindRoles(MySqlTransaction transaction = null)
        {
            string sql = "SELECT *";
            sql += " FROM role";
            sql += " WHERE removed = 0";

            DataTable dt = connection.ExecuteReader(sql, null, transaction);

            var roles = new List<Role>();

            foreach (DataRow row in dt.Rows)
            {
                var role = new Role();

                DistributeData(role, row);

                roles.Add(role);
            }

            return roles;
        }

        private void DistributeData(Role role, DataRow dr)
        {
            role.Id = int.Parse(dr["id"].ToString());
            role.Name = dr["name"].ToString();
            role.NormalizedName = dr["normalizedName"].ToString();
        }

        public void Dispose() { }
    }
}
