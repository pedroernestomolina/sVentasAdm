using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Aliado
{
    abstract public class baseFicha
    {
        public string codigo { get; set; }
        public string ciRif { get; set; }
        public string nombreRazonSocial { get; set; }
        public string dirFiscal { get; set; }
        public string personaContacto { get; set; }
    }
}
