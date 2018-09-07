using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Server.Exceptions;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class GestorComunicacion
    {
        public static readonly GestorComunicacion unicaInstancia = new GestorComunicacion();
        public static readonly ConcurrentDictionary<string, string> registroRemitentes = new ConcurrentDictionary<string, string>();
        public static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public static long instancia = 0;
        public const int cantidadClientes = 100;

        /// <summary>
        /// Método encargado de escuchar paquetes entrantes, en caso de encontrar un paquete instancia un nuevo hilo encargado de Leerlo y pasarselo
        /// al gestor de Mensajes.
        /// </summary>
        public void ListenIncomingSockets(string serverIp, string serverPort)
        {
            // Metodo encargado de recibir paquetes y crear un mensaje.
            // Agregando el identificador en el diccionario y relacionandolo con la direccion ip, para dar rta. 
            // El mensaje será enviado a la clase Gestora de Mensajes la cual lo agregará a la cola para su procesamiento.
            byte[] bytesRecibidos = new byte[255];
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            IPEndPoint host = new IPEndPoint(IPAddress.Parse(ProcesoPrincipal.instanciaUnica.direccionIp), int.Parse(ProcesoPrincipal.instanciaUnica.port));

            try
            {
                listenSocket.Bind(host);
                listenSocket.Listen(cantidadClientes);
                while (true)
                {
                    manualResetEvent.Reset();
                    Console.WriteLine("Esperando conexión.");
                    listenSocket.BeginAccept(new AsyncCallback(AceptarCallback), listenSocket);
                    // Aguarda que comience el proceso del método Asíncrono para volver a iterar.
                    manualResetEvent.WaitOne();
                }
            }
            catch (Exception e)
            {
                // LOGGEAR.
                throw e;
            }
        }

        public void AceptarCallback(IAsyncResult objeto)
        {
            manualResetEvent.Set();
            Socket listener = (Socket)objeto.AsyncState;
            // Acepta una conexión entrante del listener generando un nuevo Socket.
            Socket handler = listener.EndAccept(objeto);
            // Porqué requiere EndAccept que le pase el objeto?
        }

        /// <summary>
        /// Método encargado de enviar una respuesta.
        /// </summary>
        /// <param name="mensaje"></param>
        public void ResponseSocketToSender(Mensaje mensaje)
        {
            // Metodo encargado de Enviar un paquete.
        }
        
        public string ObtenerDireccion(Mensaje mensaje)
        {
            if(registroRemitentes.ContainsKey(mensaje.id))
            {
                string direccion;
                registroRemitentes.TryGetValue(mensaje.id, out direccion);
                return direccion;
            }else
            {
                throw new GestorComunicacionException("Error a la hora de obtener dirección, el diccionario no tiene registro del ID.");
            }
        }
    }
}
