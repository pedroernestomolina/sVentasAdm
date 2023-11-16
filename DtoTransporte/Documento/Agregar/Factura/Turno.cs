using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Factura
{
    public class Turno
    {
        public string detalle { get; set; }
        public decimal importe { get; set; }
        public string ruta { get; set; }
    }
}