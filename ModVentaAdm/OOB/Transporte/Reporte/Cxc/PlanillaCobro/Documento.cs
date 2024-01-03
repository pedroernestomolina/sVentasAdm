using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Reporte.Cxc.PlanillaCobro
{
    public class Documento
    {
        public string siglasDoc { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string numeroDoc { get; set; }
        public decimal montoDiv { get; set; }
        public string notas { get; set; }
    }
}