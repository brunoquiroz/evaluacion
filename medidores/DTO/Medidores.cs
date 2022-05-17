using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModelo
{
    public class Medidores
    {
        private string idmedidor;

        public string IDMedidor { get => idmedidor; set => idmedidor = value; }

        public override string ToString()
        {
            return idmedidor;
        }
    }
}
