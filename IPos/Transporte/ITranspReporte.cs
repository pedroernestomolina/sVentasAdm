using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos.Transporte
{
    public interface ITranspReporte
    {
        DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoResumen>
            TransporteReporte_AliadoResumen();
        DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleDoc>
            TransporteReporte_AliadoDetalleDoc();
        DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleServ>
            TransporteReporte_AliadoDetalleServ();
    }
}