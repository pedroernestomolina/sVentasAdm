using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.ClienteAnticipo.Anular
{
    public class Ficha
    {
        public int idMov { get; set; }
        public string idCliente { get; set; }
        public decimal montoAnticipoDiv { get; set; }
        public List<Caja> cajas { get; set; }
    }
}