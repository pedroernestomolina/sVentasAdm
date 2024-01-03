using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{
    public partial class Service : IService
    {
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.Resumen.Ficha>
            TransporteReporte_AliadoResumen(DtoTransporte.Reporte.Aliado.Resumen.Filtro filtro)
        {
            return ServiceProv.TransporteReporte_AliadoResumen(filtro);
        }
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleDoc.Ficha > 
            TransporteReporte_AliadoDetalleDoc(DtoTransporte.Reporte.Aliado.DetalleDoc.Filtro filtro)
        {
            return ServiceProv.TransporteReporte_AliadoDetalleDoc(filtro);
        }
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleServ.Ficha> 
            TransporteReporte_AliadoDetalleServ(DtoTransporte.Reporte.Aliado.DetalleServ.Filtro filtro)
        {
            return ServiceProv.TransporteReporte_AliadoDetalleServ(filtro);
        }
        //
        public DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.EdoCta.Ficha> 
            TransporteReporte_Cxc_EdoCta(string idCliente)
        {
            return ServiceProv.TransporteReporte_Cxc_EdoCta(idCliente);
        }
        //
        public DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.PlanillaCobro.Ficha> 
            TransporteReporte_Cxc_CobroEmitido_Planilla(string idRec)
        {
            return ServiceProv.TransporteReporte_Cxc_CobroEmitido_Planilla(idRec);
        }
    }
}