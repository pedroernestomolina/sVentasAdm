﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.ClienteAnticipo.Agregar
{
    public class Ficha
    {
        public Movimiento mov { get; set; }
        public List<Caja> caja { get; set; }
    }
}