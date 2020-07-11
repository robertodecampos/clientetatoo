using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Exceptions
{
    public class BranchNetworkException : Exception
    {
        public BranchNetworkException(string message) : base(message) { }
    }
}
