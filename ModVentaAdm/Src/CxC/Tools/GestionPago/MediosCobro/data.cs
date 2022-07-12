using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro
{
    
    public class data
    {


        public int id { get; set; }
        public MetodoCobro.dataItem item { get; set; }


        public string Metodo { get { return item.GetMetodo.desc; } }
        public decimal Monto { get { return item.GetMonto; } }
        public decimal Tasa { get { return item.GetTasa; } }
        public decimal Importe { get { return item.Importe; } }


        public data() 
        {
            id = -1;
            item = null;
        }
        public data(int id, MetodoCobro.dataItem item)
        {
            this.id = id;
            this.item = item;
        }

    }

}