using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Reportes.LibroVenta
{
    
    public class Filtro
    {

        public string mesRelacion { get; set; }
        public string anoRelacion { get; set; }


        public Filtro()
        {
            mesRelacion = "";
            anoRelacion = "";
        }

    }

}