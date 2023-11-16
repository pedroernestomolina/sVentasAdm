using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.GetTurnos
{
    abstract public class baseFicha
    {
        public decimal importeMonDiv { get; set; }
        public string turnoDesc { get; set; }
        public string turnoRuta { get; set; }
    }
}