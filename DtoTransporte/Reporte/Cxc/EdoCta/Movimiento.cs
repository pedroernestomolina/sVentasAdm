using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Reporte.Cxc.EdoCta
{
    public class Movimiento
    {
        public DateTime fechaDoc { get; set; }
        public string nroDoc { get; set; }
        public string tipoDoc { get; set; }
        public DateTime fechaVencDoc { get; set; }
        public decimal importeDiv { get; set; }
        public int signoDoc { get; set; }
        public string notasDoc { get; set; }
    }
}
