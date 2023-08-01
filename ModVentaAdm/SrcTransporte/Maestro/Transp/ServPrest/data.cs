using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Maestro.Transp.ServPrest
{
    public class data
    {
        private OOB.Transporte.ServPrest.Entidad.Ficha _ficha;


        public OOB.Transporte.ServPrest.Entidad.Ficha Ficha { get { return _ficha; } }
        public string Descripcion { get { return _ficha.descripcion; } }
        public string Codigo { get { return _ficha.codigo; } }


        public data(OOB.Transporte.ServPrest.Entidad.Ficha ficha)
        {
            _ficha = ficha;
        }
    }
}