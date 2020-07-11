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
    public class User : IDisposable
    {
        public int Id { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public void Save(Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new UserDAO(connection))
            {
                if (Id == 0)
                {
                    dao.Insert(this, transaction);
                }
                else
                {
                    dao.Update(this, transaction);
                }
            }
        }

        public void Delete(Connection connection, MySqlTransaction transaction)
        {
            throw new NotImplementedException("Função não implementada");
        }

        public static User FindById(int id, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new UserDAO(connection))
            {
                return dao.FindById(id, transaction);
            }
        }

        public static User FindByNormalizedName(string normalizedName, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new UserDAO(connection))
            {
                return dao.FindByNormalizedName(normalizedName.ToUpper(), transaction);
            }
        }

        public static IList<User> FindInRole(int roleId, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new UserDAO(connection))
            {
                return dao.FindInRole(roleId, transaction);
            }
        }

        public static List<User> FindByBranchNetworkId(int branchNetworkId, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new UserDAO(connection))
            {
                return dao.FindByBranchNetworkId(branchNetworkId, transaction);
            }
        }

        public void Dispose() { }
    }
}
