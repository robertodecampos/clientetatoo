using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.DAO;
using TatooReport.Utils;

namespace TatooReport.Models
{
    public class Branch : IDisposable
    {
        public int Id { get; set; }
        public int BranchNetworkId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public Branch() => Address = new Address();

        public void Save(Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new BranchDAO(connection))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public static List<Branch> FindAllByBranchNetwork(int branchNetworkId, Connection connection, MySqlTransaction transaction)
        {
            using (var dao = new BranchDAO(connection))
            {
                return dao.FindAllByBranchNetwork(branchNetworkId, transaction);
            }
        }

        public void Dispose() { }
    }
}
