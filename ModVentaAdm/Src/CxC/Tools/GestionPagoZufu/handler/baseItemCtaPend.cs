using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler
{
    public class baseItemCtaPend: PanelPrincipal.Pago.IItemCtaPend
    {
        private object _ficha;
        //
        public DateTime fechaEmisionDoc { get; set; }
        public string tipoDoc { get; set; }
        public string numeroDoc { get; set; }
        public DateTime fechaVencDoc { get; set; }
        public string diasVencida { get; set; }
        public decimal montoImporte { get; set; }
        public decimal montoAcumulado { get; set; }
        public decimal montoResta { get; set; }
        public decimal montoAbonar { get; set; }
        public decimal tasaCambioDoc { get; set; }
        public int SignoDoc { get; set; }
        public string detalleAbono { get; set; }
        public object Ficha { get; set; }
    }
}