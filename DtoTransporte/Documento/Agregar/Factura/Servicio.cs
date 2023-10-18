using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Factura
{
    public class Servicio
    {
        public int  id { get; set; }
        public string codigo { get; set; }
        public string desc { get; set; }
        public string detalle { get; set; }
        public decimal importe { get; set; }
    }
}