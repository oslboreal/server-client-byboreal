using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class GestorComunicacionException : Exception
    {
        public GestorComunicacionException(string mensaje) : base(mensaje)
        {

        }

        public GestorComunicacionException(string mensaje, Exception inner) : base(mensaje, inner)
        {

        }
    }
}
