using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.ClienteAnticipo.ListaMov
{
    public class Filtro
    {
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public string Estatus { get; set; }
        public string IdCliente { get; set; }
    }
}