using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecturaModelo;
using LecturaModelo.DAL;
using MedidoresModelo;
using MedidoresModelo.DAL;
using evaluacion.Comunicacion;
using System.Threading;

namespace evaluacion
{
    public class Program
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetIntancia();
        private static IMedidorDAL medidorDAL = MedidorDAL.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("¿Que quiere hacer?");
            Console.WriteLine(" 1. Ingresar \n 2. Mostrar \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }
        static void Main(string[] args)
        {

            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            while (Menu()) ;
        }

        static void Ingresar()
        {

            bool verificador = false;
            Console.WriteLine("Ingrese Medidor: ");

            string medidor = Console.ReadLine().Trim();

            List<Medidores> medicion = null;
            lock (medidorDAL)
            {
                medicion = medidorDAL.ObtenerMedidores();
            }
            while (!verificador)
            {
                for (int i = 0; i < medicion.Count; i++)
                {

                    verificador = medicion[i].ToString().Equals(medidor);
                    if (verificador == true)
                    {
                        break;
                    }
                    
                }

                if (verificador == true)
                {
                    break;

                }
                else
                {
                    Console.WriteLine("El emedidor no se encuetra");
                    medidor = Console.ReadLine().Trim();
                }
            }


            Console.WriteLine("Ingrese fecha :");
            string fecha = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese consumo :");
            string consumo = Console.ReadLine().Trim();
            Lectura lectura = new Lectura()
            {
                Medidor = medidor,
                Fecha = (DateTime)Convert.ChangeType(fecha, typeof(DateTime)),
                Consumo = consumo
            };
            lock (lecturasDAL)
            {
                lecturasDAL.AgregarLectura(lectura);
            }

        }

        static void Mostrar()
        {
            List<Lectura> lecturas = null;

            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();
            }

            foreach (Lectura lectura in lecturas)
            {
                Console.WriteLine(lectura);
            }
        }
    }
}
