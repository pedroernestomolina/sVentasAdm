using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.NotaCredito.Nueva
{
    public class Ficha
    {
        public string serieDocId { get; set; }
        public string serieDocDesc { get; set; }
        public string PrefijoSuc { get; set; }
        public string estacion { get; set; }
        public Documento Doc {get;set;}

    }
}