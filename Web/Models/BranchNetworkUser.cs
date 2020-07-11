using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.DAO;
using TatooReport.Utils;

namespace TatooReport.Models
{
    public class BranchNetworkUser : IDisposable
    {
        public int Id { get; set; }
        public int BranchNetworkId { get; set; }
        public int UserId { get; set; }

        public int Save(Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new BranchNetworkUserDAO(connection))
            {
                if (Id == 0)
                    return dao.Insert(this, transaction);
                else
                    throw new NotImplementedException("Alteração de branch_nwtwork_user não iplementada!");
            }
        }

        public static bool HasAccessInBranchNetwork(int branchNetworkId, int userId, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new BranchNetworkUserDAO(connection))
            {
                return dao.HasAccessInBranchNetwork(branchNetworkId, userId, transaction);
            }
        }

        public void Dispose() {}
    }
}
