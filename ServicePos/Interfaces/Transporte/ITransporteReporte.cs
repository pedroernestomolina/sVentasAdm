using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITransporteReporte
    {
        DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoResumen>
            TransporteReporte_AliadoResumen();
        DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleDoc>
            TransporteReporte_AliadoDetalleDoc();
    }
}
