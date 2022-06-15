using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.Tools.CtasPendiente
{
    
    public class Ficha
    {

        public string idCliente { get; set; }
        public string ciRif { get; set; }
        public string nombreRazonSocial { get; set; }
        public decimal importe { get; set; }
        public decimal acumulado { get; set; }
        public int cntDocPend { get; set; }
        public int limiteFactPend { get; set; }
        public decimal limiteMontoCredito { get; set; }
        public int cntFactPend { get; set; }


        public Ficha() 
        {
            idCliente = "";
            ciRif = "";
            nombreRazonSocial = "";
            importe = 0m;
            acumulado = 0m;
            cntDocPend = 0;
            limiteFactPend = 0;
            limiteMontoCredito = 0m;
            cntFactPend = 0;
        }

    }

}