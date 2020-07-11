using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatooReport.Models;

namespace TatooReport.Models.ViewModels.Management.BranchNetwork
{
    public class BranchNetworkDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Models.User> Users { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
