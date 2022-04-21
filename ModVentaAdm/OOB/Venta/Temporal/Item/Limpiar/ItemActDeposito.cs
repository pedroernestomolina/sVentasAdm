﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Venta.Temporal.Item.Limpiar
{
    
    public class ItemActDeposito
    {

        public string prdDescripcion { get; set; }
        public decimal cntActualizar { get; set; }
        public string autoDeposito { get; set; }
        public string autoProducto { get; set; }


        public ItemActDeposito()
        {
            prdDescripcion = "";
            autoDeposito = "";
            autoProducto = "";
            cntActualizar = 0m;
        }

    }

}