using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Venta.Temporal.Pendiente.Entidad
{
    
    public class Ficha
    {

        public Encabezado.Entidad.Ficha Encabezado { get; set; }
        public List<Item.Entidad.Ficha> Items{ get; set; }


        public Ficha() 
        {
            Encabezado = new Encabezado.Entidad.Ficha();
            Items = new List<Item.Entidad.Ficha>();
        }

    }

}