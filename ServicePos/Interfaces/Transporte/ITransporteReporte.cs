using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITransporteReporte
    {
        DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.Resumen.Ficha>
            TransporteReporte_AliadoResumen(DtoTransporte.Reporte.Aliado.Resumen.Filtro filtro);
        DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleDoc.Ficha>
            TransporteReporte_AliadoDetalleDoc(DtoTransporte.Reporte.Aliado.DetalleDoc.Filtro filtro);
        DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleServ.Ficha>
            TransporteReporte_AliadoDetalleServ(DtoTransporte.Reporte.Aliado.DetalleServ.Filtro filtro);
        //
        DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.EdoCta.Ficha>
            TransporteReporte_Cxc_EdoCta(string idCliente);
        //
        DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.PlanillaCobro.Ficha>
            TransporteReporte_Cxc_CobroEmitido_Planilla(string idRec);
    }
}
