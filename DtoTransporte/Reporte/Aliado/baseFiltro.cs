﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Reporte.Aliado
{
    public abstract class baseFiltro
    {
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
}