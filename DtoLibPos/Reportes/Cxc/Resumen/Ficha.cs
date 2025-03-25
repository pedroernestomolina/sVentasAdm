using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.Cxc.Resumen
{
    public class Ficha
    {
        public string numeroRec { get; set; }
        public DateTime fechaRec { get; set; }
        public string ciRifEntidad { get; set; }
        public string nombreEntidad { get; set; }
        public string notasRec { get; set; }
        public decimal importeDivisa { get; set; }
        public string codigoSuc { get; set; }
        public decimal montoPorAnticipoCargado { get; set; }
        public decimal montoPorAnticipoUsado { get; set; }
        public decimal montoPorNtCreditoUsado { get; set; }
    }
}