using LecturaModelo.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace evaluacion.Comunicacion
{
    public class HebraServidor
    {
        private ILecturasDAL lecturasDAL = LecturasDALArchivos.GetIntancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket serverSocket = new ServerSocket(puerto);
            Console.WriteLine("Iniciando servidor en puerto {0}", puerto);
            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando Cliente...");
                    Socket cliente = serverSocket.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido");

                    ClienteCom clienteCom = new ClienteCom(cliente);
                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("no se puedu levantar servervidor en {0}", puerto);
            }
        }
    }
}
