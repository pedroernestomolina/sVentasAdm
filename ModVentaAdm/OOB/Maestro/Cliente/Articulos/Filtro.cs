using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Maestro.Cliente.Articulos
{
    
    public class Filtro
    {

        public string autoCliente { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }


        public Filtro()
        {
            autoCliente = "";
            desde = new DateTime().Date;
            hasta = new DateTime().Date;
        }

    }

}