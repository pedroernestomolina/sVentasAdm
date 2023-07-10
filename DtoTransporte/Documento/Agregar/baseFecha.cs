using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar
{
    abstract public class baseFecha
    {
        public DateTime fecha  { get; set; }
        public string hora { get; set; }
        public string nota { get; set; }
        public baseFecha()
        {
            fecha = DateTime.Now.Date;
            hora = "";
            nota = "";
        }
    }
}
