using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Venta.Temporal.Anular
{
    
    public class Item
    {

        public int idItem { get; set; }
        public string prdDescripcion { get; set; }


        public Item()
        {
            idItem = -1;
            prdDescripcion = "";
        }
    }

}