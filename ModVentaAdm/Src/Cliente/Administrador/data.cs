using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Administrador
{
    
    public class data
    {

        private OOB.Maestro.Cliente.Entidad.Ficha _it;


        public string Id { get { return _it.id; } }
        public string Codigo { get { return _it.codigo; } }
        public string NombreRazonSocial { get { return _it.razonSocial; } }
        public string CiRif { get { return _it.ciRif; } }
        public bool IsActivo { get { return _it.IsActivo; } }
        public string Estatus { get { return IsActivo ? "" : _it.estatus; } }


        public data(OOB.Maestro.Cliente.Entidad.Ficha it)
        {
            this._it = it;
        }

        public void SetActualizarFicha(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            this._it = ficha;
        }

    }

}