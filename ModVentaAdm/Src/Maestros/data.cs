using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Maestros
{
    
    public class data
    {
        private OOB.Maestro.Zona.Entidad.Ficha rg;


        public string id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }


        public data()
        {
            id = "";
            codigo = "";
            descripcion = "";
        }


        public data(OOB.Maestro.Grupo.Entidad.Ficha it)
            :this()
        {
            id = it.auto;
            codigo = it.codigo;
            descripcion = it.nombre;
        }

        public data(OOB.Maestro.Zona.Entidad.Ficha it)
            :this()
        {
            id = it.auto;
            codigo = it.codigo;
            descripcion = it.nombre;
        }

    }

}