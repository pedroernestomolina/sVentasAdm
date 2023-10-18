using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ClienteAnticipo.Agregar
{
    public class CajaMov
    {
        public DateTime fechaMov { get; set; }
        public string descMov { get; set; }
        public decimal montoMovMonAct{ get; set; }
        public decimal montoMovMonDiv { get; set; }
        public decimal factorCambio { get; set; }
        public bool movFueDivisa { get; set; }
    }
}