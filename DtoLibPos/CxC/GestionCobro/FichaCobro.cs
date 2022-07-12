using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CxC.GestionCobro
{
    
    public class FichaCobro
    {
        
        public string Nota { get; set; }
        public string AutoCliente { get; set; }
        public string Cliente { get; set; }
        public string CiRif { get; set; }
        public string CodigoCliente { get; set; }
        public string AutoVendedor { get; set; }
        public decimal MontoDivisa { get; set; }
        public decimal TasaDivisa { get; set; }
        public decimal Importe { get; set; }


        public FichaCobro()
        {
            Nota = "";
            AutoCliente="";
            Cliente = "";
            CiRif = "";
            CodigoCliente = "";
            AutoVendedor = "";
            MontoDivisa = 0m;
            TasaDivisa = 0m;
            Importe = 0m;
        }

    }

}