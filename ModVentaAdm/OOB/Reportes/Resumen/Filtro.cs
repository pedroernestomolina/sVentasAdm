using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Reportes.Resumen
{
    
    public class Filtro
    {

        public string codigoSucursal { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }


        public Filtro()
        {
            codigoSucursal = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
        }

    }

}