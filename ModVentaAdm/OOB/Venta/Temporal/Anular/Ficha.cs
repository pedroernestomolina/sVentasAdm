using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Venta.Temporal.Anular
{
    
    public class Ficha
    {

        public int IdEncabezado { get; set; }
        public List<Item> Items { get; set; }
        public List<ItemActDeposito> ItemsActDeposito { get; set; }


        public Ficha()
        {
            IdEncabezado = -1;
            Items = new List<Item>();
            ItemsActDeposito = new List<ItemActDeposito>();
        }

    }

}