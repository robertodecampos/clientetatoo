using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Exceptions
{
    public class UserRoleException : Exception
    {
        public UserRoleException(string message) : base(message) { }
    }
}
