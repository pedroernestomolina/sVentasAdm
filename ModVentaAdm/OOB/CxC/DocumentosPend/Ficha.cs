using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.DocumentosPend
{
    
    public class Ficha
    {

        public string autoDoc { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string tipoDoc { get; set; }
        public string numeroDoc { get; set; }
        public DateTime fechaVencDoc { get; set; }
        public string notasDoc { get; set; }
        public decimal importeDoc { get; set; }
        public decimal acumuladoDoc { get; set; }
        public string autoCliente { get; set; }
        public string codigoCliente { get; set; }
        public string nombreCliente { get; set; }
        public string ciRifCliente { get; set; }
        public int signoDoc { get; set; }
        public string serieDoc { get; set; }
        public int diasCreditoDoc { get; set; }
        public decimal tasaCambioDoc { get; set; }
        public string codSucursal { get; set; }
        public string autoVendedor { get; set; }
        public string nombreVendedor { get; set; }


        public Ficha()
        {
            autoDoc = "";
            fechaEmisionDoc = DateTime.Now.Date;
            tipoDoc = "";
            numeroDoc = "";
            fechaVencDoc = DateTime.Now.Date;
            notasDoc = "";
            importeDoc = 0m;
            acumuladoDoc = 0m;
            autoCliente = "";
            codigoCliente = "";
            nombreCliente = "";
            ciRifCliente = "";
            signoDoc = 1;
            serieDoc = "";
            diasCreditoDoc = 0;
            tasaCambioDoc = 0m;
            codSucursal = "";
            autoVendedor = "";
            nombreVendedor = "";
        }

    }

}