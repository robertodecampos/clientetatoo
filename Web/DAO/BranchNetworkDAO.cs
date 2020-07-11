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
    public class BranchNetworkDAO : IDisposable
    {
        private Connection connection { get; set; }

        public BranchNetworkDAO(Connection connection) => this.connection = connection;

        public int Insert(BranchNetwork branchNetwork, MySqlTransaction transaction)
        {
            if (branchNetwork.Id != 0)
                throw new BranchNetworkException("Não é possível inserir um registro que já possui um identificador!");

            string sql =
                "INSERT INTO branch_network ( " +
                "   `name`                    " +
                ") VALUES (                   " +
                "   @name                     " +
                ")                            ";

            List<MySqlParameter> parameters = GetParameters(branchNetwork);

            int linhasAfetadas = connection.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            branchNetwork.Id = connection.UltimoIdInserido();

            return linhasAfetadas;
        }

        public BranchNetwork FindById(int branchNetworkId, MySqlTransaction transaction)
        {
            string sql = "SELECT *";
            sql += " FROM `branch_network`";
            sql += " WHERE `id` = @branchNetworkId";

            var parameters = new List<MySqlParameter> {
                new MySqlParameter("@branchNetworkId", MySqlDbType.Int32) { Value = branchNetworkId }
            };

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            if ((dt.Rows.Count == 0) || (dt.Rows.Count > 1))
                return null;

            var branchNetwork = new BranchNetwork();
            DistributeData(branchNetwork, dt.Rows[0]);

            return branchNetwork;
        }

        public List<BranchNetwork> FindByUserId(int userId, MySqlTransaction transaction)
        {
            string sql =
                "SELECT                           " +
                "   *                             " +
                "FROM                             " +
                "   branch_network a              " +
                "WHERE                            " +
                "   id IN (                       " +
                "       SELECT                    " +
                "           b.branchNetworkId     " +
                "       FROM                      " +
                "           branch_network_user b " +
                "       WHERE                     " +
                "           b.userId = @userId    " +
                "   )                             ";

            var paramters = new List<MySqlParameter>();

            paramters.Add(new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId });

            DataTable dataTable = connection.ExecuteReader(sql, paramters, transaction);

            var branchNetworks = new List<BranchNetwork>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var branchNetwork = new BranchNetwork();
                DistributeData(branchNetwork, dataRow);
                branchNetworks.Add(branchNetwork);
            }

            return branchNetworks;
        }

        private void DistributeData(BranchNetwork branchNetwork, DataRow dataRow)
        {
            branchNetwork.Id = int.Parse(dataRow["id"].ToString());
            branchNetwork.Name = dataRow["name"].ToString();
        }

        private List<MySqlParameter> GetParameters(BranchNetwork branchNetwork)
        {
            return new List<MySqlParameter> {
                new MySqlParameter("@name", MySqlDbType.VarChar) { Value = branchNetwork.Name }
            };
        }

        public void Dispose() { }
    }
}
