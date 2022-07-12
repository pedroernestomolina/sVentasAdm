using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CxC.GestionCobro
{
    
    public class FichaRecibo
    {

        public string AutoUsuario { get; set; }
        public string Usuario { get; set; }
        public string Cobrador { get; set; }
        public string AutoCliente { get; set; }
        public string Cliente { get; set; }
        public string CiRif { get; set; }
        public string Codigo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string AutoCobrador { get; set; }
        public string Nota { get; set; }
        public string CodigoCobrador { get; set; }
        public object ImporteDivisa { get; set; }
        public object MontoRecibidoDivisa { get; set; }
        public object CambioDivisa { get; set; }
        public decimal Importe { get; set; }
        public decimal MontoRecibido { get; set; }
        public decimal Cambio { get; set; }


        public FichaRecibo()
        {
            AutoUsuario = "";
            Usuario = "";
            Cobrador = "";
            AutoCliente = "";
            Cliente = "";
            CiRif = "";
            Codigo = "";
            Direccion = "";
            Telefono = "";
            AutoCobrador = "";
            Nota = "";
            CodigoCobrador = "";
            Importe = 0m;
            MontoRecibido = 0m;
            Cambio = 0m;
            ImporteDivisa = 0m;
            MontoRecibidoDivisa = 0m;
            CambioDivisa = 0m;
        }

    }

}