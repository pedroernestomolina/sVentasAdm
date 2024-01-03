using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Reporte.Cxc.PlanillaCobro
{
    public class Caja
    {
        public string cjDesc { get; set; }
        public string cjCod { get; set; }
        public decimal monto { get; set; }
        public string esDivisa { get; set; }
    }
}