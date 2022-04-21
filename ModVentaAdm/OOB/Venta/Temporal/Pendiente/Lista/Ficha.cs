using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Venta.Temporal.Pendiente.Lista
{
    
    public class Ficha
    {

        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string nombreCliente { get; set; }
        public string ciRifCliente { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public int renglones { get; set; }
        public string sucursalNombre { get; set; }
        public string depositoNombre { get; set; }


        public Ficha()
        {
            id = -1;
            fecha = DateTime.Now.Date;
            hora = "";
            nombreCliente = "";
            ciRifCliente = "";
            monto = 0m;
            montoDivisa = 0m;
            renglones = 0;
            sucursalNombre = "";
            depositoNombre = "";
        }

    }

}