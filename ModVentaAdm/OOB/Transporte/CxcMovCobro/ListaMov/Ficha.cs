using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.CxcMovCobro.ListaMov
{
    public class Ficha
    {
        public string idMov { get; set; }
        public string numRecibo { get; set; }
        public DateTime fechaEmision { get; set; }
        public string ciRifCliente { get; set; }
        public string nombreCliente { get; set; }
        public decimal importeDiv { get; set; }
        public decimal montoRecibidoDiv { get; set; }
        public decimal montoRetDiv { get; set; }
        public decimal montoAnticipoDiv { get; set; }
        public string estatusAnulado { get; set; }
    }
}