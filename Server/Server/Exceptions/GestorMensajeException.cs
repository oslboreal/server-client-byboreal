using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class GestorMensajeException : Exception
    {
        public GestorMensajeException(string message) : base(message)
        {
        }

        public GestorMensajeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
