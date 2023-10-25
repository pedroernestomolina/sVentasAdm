using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.CxcMovCobro.Anular
{
    public class Ficha
    {
        public string idCxcPago { get; set; }
        public string idCxcRecibo { get; set; }
        public string idCliente { get; set; }
        public decimal importe { get; set; }
        public decimal anticipoRecibido { get; set; }
        public List<Documento> docCobrado { get; set; }
        public List<Caja> cajas { get; set; }
    }
}