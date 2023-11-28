using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteReporte
    {
        OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoResumen>
            TransporteReporte_AliadoResumen();
        OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoDetalleDoc>
            TransporteReporte_AliadoDetalleDoc();
        OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoDetalleServ>
            TransporteReporte_AliadoDetalleServ();
        //
        OOB.Resultado.FichaEntidad<OOB.Transporte.Reporte.Cxc.EdoCta.Ficha>
            TransporteReporte_Cxc_EdoCta(string idCliente);
    }
}