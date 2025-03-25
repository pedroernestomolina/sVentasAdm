using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.ReportesCxc.DetallePorMedioPago
{
    public class Ficha
    {
        public string numeroRec { get; set; }
        public DateTime fechaRec { get; set; }
        public string ciRifEntidad { get; set; }
        public string nombreEntidad { get; set; }
        public string notasRec { get; set; }
        public decimal importeDivisa { get; set; }
        public decimal montoPorAnticipoCargado { get; set; }
        public string codigoSuc { get; set; }
        public decimal montoPorAnticipoUsado { get; set; }
        public decimal montoPorNtCreditoUsado { get; set; }
        public string nombreMedio { get; set; }
        public string codigoMedio { get; set; }
        public decimal montoRecibido { get; set; }
        public string opBanco { get; set; }
        public string opNroCta { get; set; }
        public string opNroRef { get; set; }
        public DateTime opFecha { get; set; }
        public string opDetalle { get; set; }
        public decimal opMonto { get; set; }
        public decimal opTasa { get; set; }
        public string opAplicaConversion { get; set; }
    }
}
