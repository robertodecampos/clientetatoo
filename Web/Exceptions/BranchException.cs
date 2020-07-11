using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Exceptions
{
    public class BranchException : Exception
    {
        public BranchException(String message) : base(message) { }
    }
}
