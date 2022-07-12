using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CxC.AgregarNotaAdm
{
    
    public class Ficha
    {

        public string codSucursal { get; set; }
        public string tipoDoc { get; set; }
        public int signoDoc { get; set; }
        public string notasDoc { get; set; }
        public decimal montoDivisaDoc { get; set; }
        public decimal tasaCambioDoc { get; set; }
        public string autoCliente { get; set; }
        public string codigoCliente { get; set; }
        public string nombreCliente { get; set; }
        public string ciRifCliente { get; set; }
        public string autoVendedor { get; set; }
        public decimal montoDoc { get; set; }

        
        public Ficha() 
        {
            codSucursal = "";
            tipoDoc = "";
            signoDoc = 1;
            notasDoc = "";
            montoDivisaDoc = 0m;
            tasaCambioDoc = 0m;
            autoCliente = "";
            codigoCliente = "";
            nombreCliente = "";
            ciRifCliente = "";
            autoVendedor = "";
            montoDoc = 0m;
        }

    }

}