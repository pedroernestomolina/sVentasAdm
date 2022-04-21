﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Reportes.Utilidad
{
    
    public class Filtro
    {

        public string codSucursal { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }


        public Filtro()
        {
            codSucursal = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
        }

    }

}