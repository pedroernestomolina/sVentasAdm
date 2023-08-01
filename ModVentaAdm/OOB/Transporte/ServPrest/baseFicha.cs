using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ServPrest
{
    abstract public class baseFicha
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string detalle { get; set; }
    }
}