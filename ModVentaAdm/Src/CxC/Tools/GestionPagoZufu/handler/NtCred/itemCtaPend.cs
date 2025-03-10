using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.NtCred
{
    public class itemCtaPend: baseItemCtaPend,  PanelPrincipal.Pago.IItemCtaPend
    {
        public itemCtaPend(OOB.CxC.DocumentosPend.Ficha s)
        {
            Ficha = s;
            fechaEmisionDoc = s.fechaEmisionDoc;
            tipoDoc = s.tipoDoc;
            numeroDoc = s.numeroDoc;
            fechaVencDoc = s.fechaVencDoc;
            diasVencida = ""; 
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