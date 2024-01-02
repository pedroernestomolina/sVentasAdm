﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.ConBusqueda.Aliado
{
    public class data: Idata
    {
        private OOB.Transporte.Aliado.Entidad.Ficha rg;

        public object Ficha { get; set; }
        public string codigo { get; set; }
        public string desc { get; set; }
        public string id { get; set; }
        public data(OOB.Transporte.Aliado.Entidad.Ficha rg)
        {
            Ficha = rg;
            id = rg.id.ToString();
            codigo = rg.codigo;
            desc = rg.nombreRazonSocial;
        }
    }
}