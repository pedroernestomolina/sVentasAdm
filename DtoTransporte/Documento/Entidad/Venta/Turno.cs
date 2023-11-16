using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad.Venta
{
    public class Turno
    {
        public string detalle { get; set; }
        public string ruta { get; set; }
        public decimal importe { get; set; }
    }
}
