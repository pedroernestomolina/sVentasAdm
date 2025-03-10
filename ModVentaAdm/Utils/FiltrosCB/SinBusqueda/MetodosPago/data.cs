using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.SinBusqueda.MetodosPago
{
    public class data: Idata
    {
        private OOB.Sistema.MedioCobro.Entidad.Ficha rg;
        //
        public object Ficha { get; set; }
        public string codigo { get; set; }
        public string desc { get; set; }
        public string id { get; set; }
        //
        public data(OOB.Sistema.MedioCobro.Entidad.Ficha rg)
        {
            Ficha = rg;
            id = rg.id;
            codigo = rg.codigo;
            desc = rg.nombre;
        }
    }
}