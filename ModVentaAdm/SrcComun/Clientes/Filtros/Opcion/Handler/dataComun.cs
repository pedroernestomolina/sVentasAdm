using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Opcion.Handler
{
    public class dataComun : LibUtilitis.Opcion.IData
    {
        public string codigo { get; set; }
        public string desc { get; set; }
        public string id { get; set; }
    }
}