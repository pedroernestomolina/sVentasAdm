using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CxC.GestionCobro
{

    public class FichaMetodoPago
    {

        public string AutoMedioPago { get; set; }
        public string Medio { get; set; }
        public string Codigo { get; set; }
        public decimal MontoRecibido { get; set; }
        public string AutoUsuario { get; set; }
        public string Lote { get; set; }
        public string Referencia { get; set; }
        public string AutoCobrador { get; set; }
        public string Cierre { get; set; }
        //
        public string OpBanco { get; set; }
        public string OpNroCta { get; set; }
        public string OpNroRef { get; set; }
        public DateTime OpFecha { get; set; }
        public string OpDetalle { get; set; }
        public decimal OpMonto { get; set; }
        public decimal OpTasa { get; set; }
        public string OpAplicaConversion { get; set; }


        public FichaMetodoPago()
        {
            AutoMedioPago = "";
            Medio = "";
            Codigo = "";
            MontoRecibido = 0.0m;
            AutoUsuario = "";
            Lote = "";
            Referencia = "";
            AutoCobrador = "";
            Cierre = "";
            //
            OpBanco = "";
            OpNroCta = "";
            OpNroRef = "";
            OpFecha = DateTime.Now.Date;
            OpDetalle = "";
            OpMonto = 0m;
            OpTasa = 0m;
            OpAplicaConversion = "";
        }

    }

}