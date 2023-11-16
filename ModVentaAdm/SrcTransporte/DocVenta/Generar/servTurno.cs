using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar
{
    public class servTurno
    {
        public int turnoId { get; set; }
        public string turnoDesc { get; set; }
        public string detalleRuta { get; set; }
        public decimal importeMonDiv { get; set; }
    }
}