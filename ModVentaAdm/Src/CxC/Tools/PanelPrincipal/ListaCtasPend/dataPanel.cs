using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.ListaCtasPend
{

    public class data
    {
        
        public string idCliente { get; set; }
        public string ciRif { get; set; }
        public string nombreRazonSocial { get; set; }
        public decimal montoImporte { get; set; }
        public decimal montoAcumulado { get; set; }
        public int cntDocPend { get; set; }
        public int limiteFactPend { get; set; }
        public decimal montoLimiteCredito { get; set; }
        public int cntFactPend { get; set; }
        public decimal montoResta { get { return montoImporte - montoAcumulado; } }


        public data() 
        {
            idCliente = "";
            ciRif = "";
            nombreRazonSocial = "";
            montoImporte = 0m;
            montoAcumulado = 0m;
            cntDocPend = 0;
            limiteFactPend = 0;
            montoLimiteCredito = 0m;
            cntFactPend = 0;
        }

    }

}