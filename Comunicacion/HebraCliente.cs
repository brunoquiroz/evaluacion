using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecturaModelo;
using LecturaModelo.DAL;
using ServidorSocketUtils;
using MedidoresModelo;
using MedidoresModelo.DAL;


namespace evaluacion.Comunicacion
{
    public class HebraCliente
    {
        private ILecturasDAL lecturasDAL = LecturasDALArchivos.GetIntancia();
        private static IMedidorDAL medidorDAl = MedidorDAL.GetInstancia();
        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            bool verificador = false;
            clienteCom.Escribir("Ingrese nombre del medidor: ");
            string medidor = clienteCom.Leer();

            List<Medidores> medicion = null;
            lock (medidorDAl)
            {
                medicion = medidorDAl.ObtenerMedidores();
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
                    clienteCom.Escribir("El emedidor no se encuetra");
                    medidor = clienteCom.Leer();
                }
            }

            clienteCom.Escribir("Ingrese la fecha: ");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese el consumo: ");
            string consumo = clienteCom.Leer();
            Lectura lectura = new Lectura()
            {
                Medidor = medidor,
                Fecha = (DateTime)Convert.ChangeType(fecha, typeof(DateTime)),
                Consumo = consumo,
            };
            lock (lecturasDAL)
            {
                lecturasDAL.AgregarLectura(lectura);
            }
            clienteCom.Desconectar();
        }
    }
}
