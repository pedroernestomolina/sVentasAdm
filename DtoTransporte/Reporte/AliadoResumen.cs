using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Reporte
{
    public class AliadoResumen
    {
        public string codigo { get; set; }
        public string ciRif { get; set; }
        public string aliado { get; set; }
        public decimal montoDebitoMonDivisa { get; set; }
        public decimal montoCreditoMonDivisa { get; set; }
        public decimal montoAnticiposMonDivisa { get; set; }
        public decimal montoDebitoAnuladoMonDivisa { get; set; }
        public decimal montoCreditoAnuladoMonDivisa { get; set; }
        public decimal montoAnticiposAnuladoMonDivisa { get; set; }
        public AliadoResumen()
        {
            ciRif = "";
            codigo = "";
            aliado = "";
            montoAnticiposAnuladoMonDivisa = 0m;
            montoAnticiposMonDivisa = 0m;
            montoCreditoAnuladoMonDivisa = 0m;
            montoCreditoMonDivisa = 0m;
            montoDebitoAnuladoMonDivisa = 0m;
            montoDebitoMonDivisa = 0m;
        }
    }
}