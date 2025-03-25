using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.Cxc.DetallePorDoc
{
    public class Ficha
    {
        public string numeroDoc { get; set; }
        public string tipoDoc { get; set; }
        public string numeroRecibo { get; set; }
        public decimal importeDivisa { get; set; }
        public string codigoSuc { get; set; } 
        public string notasDoc { get; set; }
        public DateTime fechaRecibo { get; set; }
        public string nombreEntidad { get; set; }
        public string ciRifEntidad { get; set; }
        public DateTime fechaDoc { get; set; }
        public DateTime fechaVencDoc { get; set; }
        public decimal importeRecibo { get; set; }
        public decimal montoPorAnticiposUsado { get; set; }
        public decimal montoPorAnticiposPorCargar { get; set; }
        public decimal montoPorNtCreditoUsado { get; set; }
    }
}
