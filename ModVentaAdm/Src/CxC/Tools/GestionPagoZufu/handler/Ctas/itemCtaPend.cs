using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Ctas
{
    public class itemCtaPend: PanelPrincipal.Pago.IItemCtaPend
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
        public itemCtaPend()
        {
        }
        public itemCtaPend(OOB.CxC.DocumentosPend.Ficha s)
        {
            this._ficha = s;
            var d= DateTime.Now.Date.Subtract(s.fechaVencDoc).Days; 
            fechaEmisionDoc = s.fechaEmisionDoc;
            tipoDoc = s.tipoDoc;
            numeroDoc = s.numeroDoc;
            fechaVencDoc = s.fechaVencDoc;
            diasVencida = d <= 0 ? "Por Vencer": d.ToString()+"Dia(s)"; 
            montoImporte = s.importeDoc;
            montoAcumulado = s.acumuladoDoc;
            montoResta = s.montoPend;
            montoAbonar = 0m;
            tasaCambioDoc = s.tasaCambioDoc;
            SignoDoc = s.signoDoc;
            montoAbonar = 0m;
            detalleAbono = "";
        }
        public void setMontoAbonar(decimal monto)
        {
            montoAbonar = monto;
        }
        public void setDetalleAbono(string p)
        {
            detalleAbono = p;
        }
    }
}