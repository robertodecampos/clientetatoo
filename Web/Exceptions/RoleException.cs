using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Exceptions
{
    public class RoleException : Exception
    {
        public RoleException(string message) : base(message) { }
    }
}
