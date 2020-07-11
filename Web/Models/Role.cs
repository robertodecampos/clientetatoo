using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.DAO;
using TatooReport.Utils;

namespace TatooReport.Models
{
    public class Role : IDisposable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public void Save(Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new RoleDAO(connection))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public void Delete(Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new RoleDAO(connection))
            {
                dao.Remove(this, transaction);
            }
        }

        public static Role FindById(int id, Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new RoleDAO(connection))
            {
                return dao.FindById(id, transaction);
            }
        }

        public static Role FindByNormalizedName(string normalizedName, Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new RoleDAO(connection))
            {
                return dao.FindByNormalizedName(normalizedName.ToUpper(), transaction);
            }
        }

        public static Role FindByName(string name, Connection connection, MySqlTransaction transaction = null)
        {
            using (var dao = new RoleDAO(connection))
            {
                return dao.FindByName(name, transaction);
            }
        }

        public static List<Role> FindRoles(Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new RoleDAO(connection))
            {
                return dao.FindRoles(transaction);
            }
        }

        public void Dispose() { }
    }
}
