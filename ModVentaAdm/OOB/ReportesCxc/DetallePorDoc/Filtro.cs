using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.ReportesCxc.DetallePorDoc
{
    public class Filtro
    {
        public string codSucursal { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        public string idCliente { get; set; }
    }
}