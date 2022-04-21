using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Venta.Temporal.Anular
{
    
    public class ItemActDeposito
    {

        public int idItem { get; set; }
        public string prdDescripcion { get; set; }
        public decimal cntActualizar { get; set; }
        public string autoDeposito { get; set; }
        public string autoProducto { get; set; }


        public ItemActDeposito()
        {
            idItem = -1;
            prdDescripcion = "";
            autoDeposito = "";
            autoProducto = "";
            cntActualizar = 0m;
        }

    }

}