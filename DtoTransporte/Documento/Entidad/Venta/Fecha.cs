using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad.Venta
{
    public class Fecha
    {
        public int  idItem  { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string nota { get; set; }
    }
}