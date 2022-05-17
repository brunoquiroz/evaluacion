using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModelo.DAL
{
    public class MedidorDAL : IMedidorDAL
    {
        private MedidorDAL()
        {

        }
        private static MedidorDAL instancia;
        public static IMedidorDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDAL();
            }
            return instancia;
        }


        public List<Medidores> ObtenerMedidores()
        {
            List<Medidores> lista = new List<Medidores>();
            Medidores medidores1 = new Medidores();
            Medidores medidores2 = new Medidores();
            Medidores medidores3 = new Medidores();
            medidores1.IDMedidor = "1111";
            medidores2.IDMedidor = "2222";
            medidores3.IDMedidor = "3333";
            lista.Add(medidores1);
            lista.Add(medidores2);
            lista.Add(medidores3);


            return lista;
        }
    }
}
