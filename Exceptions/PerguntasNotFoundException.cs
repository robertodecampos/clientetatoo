using System;

namespace ClienteTatoo.Exceptions
{
    class PerguntasNotFoundException : Exception
    {
        public PerguntasNotFoundException(string message) : base(message) { }

        public PerguntasNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
