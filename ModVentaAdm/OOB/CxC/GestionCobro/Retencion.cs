using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.GestionCobro
{
    public class Retencion
    {
        public decimal montoAplicarRetMonAct { get; set; }
        public decimal tasaRet { get; set; }
        public decimal retencionMonAct { get; set; }
        public decimal sustraendoMonAct { get; set; }
        public decimal totalRetMonAct { get; set; }
        public decimal factorCambio { get; set; }
    }
}