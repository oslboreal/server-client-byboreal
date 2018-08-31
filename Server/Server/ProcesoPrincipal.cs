using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{
    internal class ProcesoPrincipal
    {
        public static readonly ProcesoPrincipal instanciaUnica = new ProcesoPrincipal();
        public readonly string direccionIp = "127.0.0.1"; // Localhost.
        public readonly string port = "80"; // WebServer (HTTP - HyperTextTransportProtocol)
        public static readonly object objectLocker = new object();

        #region Metodos para el proceso de mensajes.

        /// <summary>
        /// Método encargado de iniciar el procesamiento de paquetes.
        /// </summary>
        public void StartServer()
        {
            GestorComunicacion.unicaInstancia.ListenIncomingSockets(direccionIp, port);
        }

        /// <summary>
        /// Método encargado de revisar la Cola de mensajes, en caso de que exista alguno le asigna su sub-proceso correspondiente.
        /// </summary>
        public void Procesar()
        {
            // Main processing Thread - This thread is polling continuisly the GestorMensajes collection.
            Task.Factory.StartNew(() =>
            {
                foreach (var mensaje in GestorMensajes.uniqueInstance.mensajes.GetConsumingEnumerable())
                {
                    Task.Factory.StartNew(() =>
                    {
                        InterpretarMensaje(mensaje);
                    }, TaskCreationOptions.LongRunning);
                }
            }, TaskCreationOptions.LongRunning);
        }
        
        /// <summary>
        /// Método encargado de interpretar el mensaje.
        /// </summary>
        /// <param name="msg"></param>
        public void InterpretarMensaje(Mensaje msg)
        {
            switch (msg.cuerpo)
            {
                case "1":
                    // Realizo operación.
                    // Modifico mensaje.
                    // Respondo mensaje.
                    GestorComunicacion.unicaInstancia.ResponseSocketToSender(msg);
                    break;
                case "2":
                    // Realizo operación.
                    // Modifico mensaje.
                    // Respondo mensaje.
                    GestorComunicacion.unicaInstancia.ResponseSocketToSender(msg);
                    break;
                case "3":
                    // Realizo operación.
                    // Modifico mensaje.
                    // Respondo mensaje.
                    GestorComunicacion.unicaInstancia.ResponseSocketToSender(msg);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Metodos para la vista.

        #endregion


        #region Getters de Parametros

        /// <summary>
        /// Retorna la Direccion de Ip del servidor.
        /// </summary>
        /// <returns></returns>
        public string GetDireccionIp()
        {
            lock (objectLocker)
            {
                return this.direccionIp;
            }
        }

        /// <summary>
        /// Retorna el puerto del servidor.
        /// </summary>
        /// <returns></returns>
        public string GetServerPort()
        {
            lock (objectLocker)
            {
                return this.port;
            }
        }

        #endregion

    }
}
