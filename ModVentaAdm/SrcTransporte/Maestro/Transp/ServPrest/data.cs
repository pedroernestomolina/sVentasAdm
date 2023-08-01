using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Maestro.Transp.ServPrest
{
    public class data
    {
        private OOB.Transporte.Aliado.Entidad.Ficha _ficha;


        public OOB.Transporte.Aliado.Entidad.Ficha Ficha { get { return _ficha; } }
        public string Descripcion { get { return _ficha.nombreRazonSocial; } }
        public string Codigo { get { return _ficha.codigo; } }


        public data(OOB.Transporte.Aliado.Entidad.Ficha ficha)
        {
            _ficha = ficha;
        }
    }
}