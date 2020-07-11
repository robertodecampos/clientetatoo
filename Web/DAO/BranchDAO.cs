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
    public class BranchDAO : IDisposable
    {
        private Connection connection { get; set; }

        public BranchDAO(Connection connection)
        {
            this.connection = connection;
        }

        public int Insert(Branch branch, MySqlTransaction transaction)
        {
            if (branch.Id != 0)
                throw new BranchException("Não é possível inserir um registro que já possui um identificador!");

            string sql =
                "INSERT INTO branch (" +
                "   `branchNetworkId`, `name`, `addressPostalCode`, `addressStreetType`, `addressStreet`, `addressNumber`, `addressComplement`, `addressDistrict`, `addressCity`, `addressState`" +
                ") VALUES (" +
                "   @branchNetworkId, @name, @addressPostalCode, @addressStreetType, @addressStreet, @addressNumber, @addressComplement, @addressDistrict, @addressCity, @addressState" +
                ")                              ";

            List<MySqlParameter> parameters = GetParameters(branch);

            int linhasAfetadas = connection.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            branch.Id = connection.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Update(Branch branch, MySqlTransaction transaction = null)
        {
            if (branch.Id == 0)
                throw new BranchException("Não é possível alterar um registro que não possui um identificador!");

            string sql = 
                "UPDATE branch SET" +
                "   `branchNetworkId` = @branchNetworkId, `name` = @name, `addressPostalCode` = @addressPostalCode, `addressStreetType` = @addressStreetType, `addressNumber = @addressNumber`," +
                "   `addressComplement` = @addressComplement, addressDistrict = @addressDistrict, `addressCity` = @addressCity, `addressState` = @addressState" +
                " WHERE `id` = @id";

            var parameters = GetParameters(branch);
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = branch.Id });

            return connection.Execute(sql, parameters, transaction);
        }

        public List<Branch> FindAllByBranchNetwork(int branchNetworkId, MySqlTransaction transaction)
        {
            string sql = "SELECT *";
            sql += " FROM `branch`";
            sql += " WHERE `branchNetworkId` = @branchNetworkId";

            var parameters = new List<MySqlParameter> {
                new MySqlParameter("@branchNetworkId", MySqlDbType.Int32) { Value = branchNetworkId }
            };

            DataTable dt = connection.ExecuteReader(sql, parameters, transaction);

            var branches = new List<Branch>();

            foreach (DataRow row in dt.Rows)
            {
                var branch = new Branch();
                DistributeData(branch, row);
                branches.Add(branch);
            }

            return branches;
        }

        private void DistributeData(Branch branch, DataRow dataRow)
        {
            branch.Id = int.Parse(dataRow["id"].ToString());
            branch.BranchNetworkId = int.Parse(dataRow["branchNetworkId"].ToString());
            branch.Name = dataRow["name"].ToString();
            branch.Address.PostalCode = dataRow["addressPostalCode"].ToString();
            branch.Address.StreetType = dataRow["addressStreetType"].ToString();
            branch.Address.Street = dataRow["addressStreet"].ToString();
            branch.Address.Number = dataRow["addressNumber"].ToString();
            branch.Address.Complement = dataRow["addressComplement"].ToString();
            branch.Address.District = dataRow["addressDistrict"].ToString();
            branch.Address.City = dataRow["addressCity"].ToString();
            branch.Address.State = dataRow["addressState"].ToString();
        }

        private List<MySqlParameter> GetParameters(Branch branch)
        {
            return new List<MySqlParameter> {
                new MySqlParameter("@branchNetworkId", MySqlDbType.VarChar) { Value = branch.BranchNetworkId },
                new MySqlParameter("@name", MySqlDbType.VarChar) { Value = branch.Name },
                new MySqlParameter("@addressPostalCode", MySqlDbType.VarChar) { Value = branch.Address.PostalCode },
                new MySqlParameter("@addressStreetType", MySqlDbType.VarChar) { Value = branch.Address.StreetType },
                new MySqlParameter("@addressStreet", MySqlDbType.VarChar) { Value = branch.Address.Street },
                new MySqlParameter("@addressNumber", MySqlDbType.VarChar) { Value = branch.Address.Number },
                new MySqlParameter("@addressComplement", MySqlDbType.VarChar) { Value = branch.Address.Complement },
                new MySqlParameter("@addressDistrict", MySqlDbType.VarChar) { Value = branch.Address.District },
                new MySqlParameter("@addressCity", MySqlDbType.VarChar) { Value = branch.Address.City },
                new MySqlParameter("@addressState", MySqlDbType.VarChar) { Value = branch.Address.State },
            };
        }

        public void Dispose() { }
    }
}
