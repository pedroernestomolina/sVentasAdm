using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Reporte.Cxc.EdoCta
{
    public class Ficha
    {
        public Cliente entidad { get; set; }
        public List<Movimiento> movimientos { get; set; }
    }
}
