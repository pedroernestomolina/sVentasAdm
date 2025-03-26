using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.SinBusqueda.Sucursal
{
    public class data: Idata
    {
        private OOB.Sucursal.Entidad.Ficha rg;
        //
        public object Ficha { get; set; }
        public string codigo { get; set; }
        public string desc { get; set; }
        public string id { get; set; }
        //
        public data(OOB.Sucursal.Entidad.Ficha rg)
        {
            Ficha = rg;
            id = rg.auto;
            codigo = rg.codigo;
            desc = rg.nombre;
        }
    }
}