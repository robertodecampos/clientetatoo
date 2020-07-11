using System;
using System.Runtime.Serialization;

namespace TatooReport.DAO
{
    [Serializable]
    internal class BranchNetworkUserException : Exception
    {
        public BranchNetworkUserException()
        {
        }

        public BranchNetworkUserException(string message) : base(message)
        {
        }

        public BranchNetworkUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BranchNetworkUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}