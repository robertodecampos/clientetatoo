using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.DAO;
using TatooReport.Utils;

namespace TatooReport.Models
{
    public class BranchNetwork : IDisposable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Save(Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new BranchNetworkDAO(connection))
            {
                if (Id == 0)
                    return dao.Insert(this, transaction);
                else
                    return 0;
            }
        }

        public static BranchNetwork FindById(int branchNetworkId, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new BranchNetworkDAO(connection))
            {
                return dao.FindById(branchNetworkId, transaction);
            }
        }

        public static List<BranchNetwork> FindByUserId(int userId, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new BranchNetworkDAO(connection))
            {
                return dao.FindByUserId(userId, transaction);
            }
        }

        public void Dispose() { }
    }
}
