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
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorDoc.Ficha> 
            ReportesCxc_DetalleCobranza_PorDocumentos(DtoLibPos.Reportes.Cxc.DetallePorDoc.Filtro filtro)
        {
            return ServiceProv.ReportesCxc_DetalleCobranza_PorDocumentos(filtro);
        }

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.Resumen.Ficha> 
            ReportesCxc_ResumenCobranza(DtoLibPos.Reportes.Cxc.Resumen.Filtro filtro)
        {
            return ServiceProv.ReportesCxc_ResumenCobranza(filtro);
        }

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Ficha> 
            ReportesCxc_DetalleCobranza_PorMedioPago(DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Filtro filtro)
        {
            return ServiceProv.ReportesCxc_DetalleCobranza_PorMedioPago(filtro);
        }
    }
}