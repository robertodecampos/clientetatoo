using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Exceptions
{
    public class BranchNetworkExceptionUser : Exception
    {
        public BranchNetworkExceptionUser(string message) : base(message) { }
    }
}
