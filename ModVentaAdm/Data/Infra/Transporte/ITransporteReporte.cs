using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteReporte
    {
        OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.Resumen.Ficha>
            TransporteReporte_AliadoResumen(OOB.Transporte.Reporte.Aliado.Resumen.Filtro filtro);
        OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.DetalleDoc.Ficha>
            TransporteReporte_AliadoDetalleDoc(OOB.Transporte.Reporte.Aliado.DetalleDoc.Filtro filtro);
        OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.DetalleServ.Ficha>
            TransporteReporte_AliadoDetalleServ(OOB.Transporte.Reporte.Aliado.DetalleServ.Filtro filtro);
        //
        OOB.Resultado.FichaEntidad<OOB.Transporte.Reporte.Cxc.EdoCta.Ficha>
            TransporteReporte_Cxc_EdoCta(string idCliente);
        //
        OOB.Resultado.FichaEntidad<OOB.Transporte.Reporte.Cxc.PlanillaCobro.Ficha>
            TransporteReporte_Cxc_CobroEmitido_Planilla(string idRec);
    }
}