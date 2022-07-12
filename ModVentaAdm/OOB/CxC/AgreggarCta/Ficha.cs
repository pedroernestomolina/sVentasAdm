using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.AgregarCta
{
    
    public class Ficha
    {

        public string codSucursal { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string tipoDoc { get; set; }
        public int signoDoc { get; set; }
        public string numeroDoc { get; set; }
        public string serieDoc { get; set; }
        public int diasCreditoDoc { get; set; }
        public string notasDoc { get; set; }
        public decimal montoDivisaDoc { get; set; }
        public decimal tasaCambioDoc { get; set; }
        public string autoCliente { get; set; }
        public string codigoCliente { get; set; }
        public string nombreCliente { get; set; }
        public string ciRifCliente { get; set; }
        public string autoVendedor { get; set; }
        public DateTime fechaVencDoc { get; set; }
        public decimal montoDoc { get; set; }


        public Ficha()
        {
            codSucursal = "";
            fechaEmisionDoc = DateTime.Now.Date;
            tipoDoc = "";
            signoDoc = 1;
            numeroDoc = "";
            serieDoc = "";
            diasCreditoDoc = 0;
            notasDoc = "";
            montoDivisaDoc = 0m;
            tasaCambioDoc = 0m;
            autoCliente = "";
            codigoCliente = "";
            nombreCliente = "";
            ciRifCliente = "";
            autoVendedor = "";
            fechaVencDoc = DateTime.Now.Date;
            montoDoc = 0m;
        }

    }

}