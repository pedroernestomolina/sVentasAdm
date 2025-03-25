using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface IReportesCxc
    {
        DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorDoc.Ficha>
            ReportesCxc_DetalleCobranza_PorDocumentos(DtoLibPos.Reportes.Cxc.DetallePorDoc.Filtro filtro);

        DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.Resumen.Ficha>
            ReportesCxc_ResumenCobranza(DtoLibPos.Reportes.Cxc.Resumen.Filtro filtro);

        DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Ficha>
            ReportesCxc_DetalleCobranza_PorMedioPago(DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Filtro filtro);
    }
}
