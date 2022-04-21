using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Documento.Lista
{
    
    public class Filtro
    {

        public string idArqueo { get; set; }
        public string idCliente { get; set; }
        public string idProducto { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        public string codSucursal { get; set; }
        public string codTipoDocumento { get; set; }
        public string estatus { get; set; }
        public string palabraClave { get; set; }


        public Filtro()
        {
            idArqueo = "";
            idCliente = "";
            idProducto = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
            codSucursal = "";
            codTipoDocumento = "";
            estatus = "";
            palabraClave = "";
        }

    }

}