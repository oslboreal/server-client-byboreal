using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Mensaje
    {
        /// <summary>
        /// Cuerpo en Texto plano del mensaje.
        /// </summary>
        public string cuerpo { get; set; }
        /// <summary>
        /// Identificacion del mensaje, útil para identificar el paquete en la co,unicación.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Constructor publico de la clase Mensaje.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cuerpo"></param>
        public Mensaje(string id, string cuerpo)
        {
            this.cuerpo = cuerpo;
            this.id = id;
        }
        
    }
}
