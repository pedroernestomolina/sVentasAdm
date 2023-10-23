using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.GestionCobro
{
    public class CajaMov
    {
        public string descMov { get; set; }
        public decimal montoMovMonAct{ get; set; }
        public decimal montoMovMonDiv { get; set; }
        public decimal factorCambio { get; set; }
        public bool movFueDivisa { get; set; }
    }
}