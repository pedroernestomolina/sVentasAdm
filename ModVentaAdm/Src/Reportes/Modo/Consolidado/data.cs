using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.Consolidado
{
    
    public class data
    {

        public DateTime fecha { get; set; }
        public string codigoSuc { get; set; }
        public string nombreSuc { get; set; }
        public int caja { get; set; }
        public string docNombre { get; set; }
        public string aplica { get; set; }
        public string tipo { get; set; }
        public string inicio { get; set; }
        public string fin { get; set; }
        public decimal total { get; set; }
        public decimal totalDivisa { get; set; }

    }

}
