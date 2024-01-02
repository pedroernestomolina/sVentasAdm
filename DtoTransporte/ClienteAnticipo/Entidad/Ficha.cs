using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.ClienteAnticipo.Entidad
{
    public class Ficha
    {
        public Movimiento Mov { get; set; }
        public List<Caja> CajMov { get; set; }
    }
}