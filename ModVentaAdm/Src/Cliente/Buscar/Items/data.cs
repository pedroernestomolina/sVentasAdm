using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Buscar.Items 
{
    
    public class data
    {

        private string _id;
        private string _codigo;
        private string _ciRif;
        private string _nombreRazonSocial;
        private bool _isActivo;
        private string _estatus;


        public string Id { get { return _id; } }
        public string Codigo { get { return _codigo; } }
        public string NombreRazonSocial { get { return _nombreRazonSocial; } }
        public string CiRif { get { return _ciRif; } }
        public bool IsActivo { get { return _isActivo; } }
        public string Estatus { get { return IsActivo ? "" : "INACTIVO"; } }


        public data() 
        {
            Limpiar();
        }

        public data(OOB.Maestro.Cliente.Entidad.Ficha it)
            :this()
        {
            SetActualizarFicha(it);
        }

        public void SetActualizarFicha(OOB.Maestro.Cliente.Entidad.Ficha it)
        {
            _id = it.id;
            _codigo = it.codigo;
            _nombreRazonSocial = it.razonSocial;
            _ciRif = it.ciRif;
            _isActivo = it.IsActivo;
        }
       
        public void Limpiar()
        {
            _id = "";
            _codigo = "";
            _ciRif = "";
            _nombreRazonSocial = "";
            _isActivo = true;
        }

    }

}