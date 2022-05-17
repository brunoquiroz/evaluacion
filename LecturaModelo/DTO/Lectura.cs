﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecturaModelo
{
    public class Lectura
    {
        private string medidor;
        private DateTime fecha;
        private string consumo;

        public string Medidor { get => medidor; set => medidor = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Consumo { get => consumo; set => consumo = value; }

        public override string ToString()
        {
            return medidor + "" + fecha + "" + consumo;
        }
    }
}
