using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.GestionCobro
{
    
    public class FichaCliente
    {

        public string idCliente { get; set; }
        public decimal monto { get; set; }


        public FichaCliente() 
        {
            idCliente = "";
            monto = 0m;
        }

    }

}