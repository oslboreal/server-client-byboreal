using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Server.Exceptions;

namespace Server
{
    /// <summary>
    /// Clase encargada de gestionar los mensajes.
    /// </summary>
    internal class GestorMensajes
    {
        public static readonly GestorMensajes uniqueInstance = new GestorMensajes();
        public BlockingCollection<Mensaje> mensajes = new BlockingCollection<Mensaje>();

        #region Metodos para la gestion de mensajes.

        /// <summary>
        /// Método encargado de agregar un mensaje a la colección, el cual luego será procesado por el procesador.
        /// </summary>
        /// <returns></returns>
        public bool AgregarMensaje(Mensaje msg)
        {
            try
            {
                bool ret = false;
                if (msg != null)
                {
                    mensajes.Add(msg);
                    ret = true;
                }
                return ret;
            }
            catch (ApplicationException e)
            {
                throw new GestorMensajeException("Error a la hora de Agregar un mensaje.", e);
            }
        }
        
        /// <summary>
        /// Método encargado de retornar el próximo mensaje para su procesamiento.
        /// </summary>
        /// <returns></returns>
        public Mensaje ProximoMensaje()
        {
            try
            {
                Mensaje ret = null;
                if (mensajes.Count > 0)
                {
                    ret = mensajes.Take();
                }
                return ret;
            }
            catch (ApplicationException e)
            {
                throw new GestorMensajeException("Error a la hora de devolver el proximo mensaje.", e);
            }
        }

        #endregion

        #region Metodos para la vista.

        #endregion


    }
}
