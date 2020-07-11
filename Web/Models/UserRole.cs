using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.DAO;
using TatooReport.Utils;

namespace TatooReport.Models
{
    public class UserRole : IDisposable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public void Save(Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new UserRoleDAO(connection))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Delete(Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new UserRoleDAO(connection))
            {
                dao.Remove(this, transaction);
            }
        }

        public static UserRole FindByUserIdAndRoleId(int userId, int roleId, Connection connection, MySqlTransaction transaction = null)
        {

            using (var dao = new UserRoleDAO(connection))
            {
                return dao.FindByUserIdAndRoleId(userId, roleId);
            }
        }

        public static IList<string> FindRoleNameByUserId(int userId, Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new UserRoleDAO(connection))
            {
                return dao.FindRoleNameByUserId(userId, null);
            }
        }

        public static IList<string> FindRoleNormalizedNameByUserId(int userId, Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new UserRoleDAO(connection))
            {
                return dao.FindRoleNormalizedNameByUserId(userId, null);
            }
        }

        public static bool ExistsUserInRole(int usuarioId, int roleId, Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new UserRoleDAO(connection))
            {
                return dao.ExistsUserInRole(usuarioId, roleId, transaction);
            }
        }

        public static bool ExistsUserInRole(int usuarioId, string normalizedRoleName, Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new UserRoleDAO(connection))
            {
                return dao.ExistsUserInRole(usuarioId, normalizedRoleName, transaction);
            }
        }

        public void Dispose()
        {

        }
    }
}
