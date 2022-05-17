using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecturaModelo.DAL
{
    public class LecturasDALArchivos : ILecturasDAL 
    {
            private LecturasDALArchivos()
            {

            }
            private static LecturasDALArchivos instancia;
            public static ILecturasDAL GetIntancia()
            {
                if (instancia == null)
                {
                    instancia = new LecturasDALArchivos();
                }
                return instancia;
            }

            private static string url = Directory.GetCurrentDirectory();

            private static string archivo = url + "/lecturas.txt";
            public void AgregarLectura(Lectura Lectura)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(archivo, true))
                    {
                        writer.WriteLine(Lectura.Medidor + "|" + Lectura.Fecha + "|" + Lectura.Consumo) ;
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            public List<Lectura> ObtenerLecturas()
            {
                List<Lectura> lista = new List<Lectura>();
                try
                {
                    using (StreamReader reader = new StreamReader(archivo))
                    {
                        string texto = "";
                        do
                        {
                            texto = reader.ReadLine();
                            if (texto != null)
                            {
                                string[] arr = texto.Trim().Split('|');
                                Lectura lectura = new Lectura()
                                {
                                    Medidor = arr[0],
                                    Fecha = (DateTime)Convert.ChangeType(arr[1],typeof(DateTime) ),
                                    Consumo = arr[2]
                                };
                                lista.Add(lectura);
                            }
                        } while (texto != null);
                    }
                }
                catch (Exception ex)
                {
                    lista = null;
                }
                return lista;
            }
        }
    }


