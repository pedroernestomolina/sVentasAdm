using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaClientes.Handler
{
    public class data: Vista.Idata
    {
        private OOB.Maestro.Cliente.Lista.Ficha _ficha;
        //
        public OOB.Maestro.Cliente.Lista.Ficha Get_Ficha { get { return _ficha; } }
        public string CiRif { get; set; }
        public string Nombre { get; set; }
        //
        public data(object rg)
        {
            _ficha = (OOB.Maestro.Cliente.Lista.Ficha)rg;
            CiRif = _ficha.ciRif;
            Nombre = _ficha.nombre;
        }
    }
}