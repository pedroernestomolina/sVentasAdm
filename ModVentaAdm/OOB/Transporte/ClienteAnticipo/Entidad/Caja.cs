using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ClienteAnticipo.Entidad
{
    public class Caja
    {
        public string cjCodigo { get; set; }
        public string cjDesc { get; set; }
        public decimal monto { get; set; }
        public string esDivisa { get; set; }
    }
}