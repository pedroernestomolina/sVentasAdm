using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.Cxc.DetallePorDoc
{
    public class Filtro
    {
        public string codSucursal { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
    }
}