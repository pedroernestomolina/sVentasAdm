﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.CxcMovCobro.Anular
{
    public class Ficha
    {
        public string idCxcPago { get; set; }
        public string idCxcRecibo { get; set; }
        public List<Documento> docCobrado { get; set; }
    }
}