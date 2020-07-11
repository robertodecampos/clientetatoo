using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.Models;
using TatooReport.Utils;

namespace TatooReport.DAO
{
    public class BranchNetworkUserDAO : IDisposable
    {
        private Connection connection { get; set; }

        public BranchNetworkUserDAO(Connection connection)
        {
            this.connection = connection;
        }

        public int Insert(BranchNetworkUser branchNetwork, MySqlTransaction transaction)
        {
            if (branchNetwork.Id != 0)
                throw new BranchNetworkUserException("Não é possível inserir um registro que já possui um identificador!");

            string sql =
                "INSERT INTO branch_network (   " +
                "   `branchNetworkId`, `userId` " +
                ") VALUES (                     " +
                "   @branchNetworkId, @userId   " +
                ")                              ";

            List<MySqlParameter> parameters = GetParameters(branchNetwork);

            int linhasAfetadas = connection.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            branchNetwork.Id = connection.UltimoIdInserido();

            return linhasAfetadas;
        }

        public bool HasAccessInBranchNetwork(int branchNetworkId, int userId, MySqlTransaction transaction)
        {
            string sql =
                " SELECT                                      " +
                "    COUNT(*) `qtde`                          " +
                " FROM                                        " +
                "    branch_network_user                      " +
                " WHERE                                       " +
                "    `branchNetworkId` = @branchNetworkId AND " +
                "    `userId` = @userId                       ";

            var paramters = new List<MySqlParameter>();

            paramters.Add(new MySqlParameter("@branchNetworkId", MySqlDbType.Int32) { Value = branchNetworkId });
            paramters.Add(new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId });

            DataTable dataTable = connection.ExecuteReader(sql, paramters, transaction);

            return (int.Parse(dataTable.Rows[0]["qtde"].ToString()) == 1);
        }

        private void DistributeData(BranchNetworkUser branchNetworkUser, DataRow dataRow)
        {
            branchNetworkUser.Id = int.Parse(dataRow["id"].ToString());
            branchNetworkUser.BranchNetworkId = int.Parse(dataRow["branchNetworkId"].ToString());
            branchNetworkUser.UserId = int.Parse(dataRow["userId"].ToString());
        }

        private List<MySqlParameter> GetParameters(BranchNetworkUser branchNetworkUser)
        {
            return new List<MySqlParameter> {
                new MySqlParameter("@branchNetworkId", MySqlDbType.VarChar) { Value = branchNetworkUser.BranchNetworkId },
                new MySqlParameter("@UserId", MySqlDbType.VarChar) { Value = branchNetworkUser.UserId }
            };
        }

        public void Dispose(){}
    }
}
