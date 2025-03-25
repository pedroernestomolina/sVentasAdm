using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    public interface IReportesCxc
    {
        OOB.Resultado.Lista<OOB.ReportesCxc.DetallePorDoc.Ficha>
            ReportesCxc_DetalleCobranza_PorDocumentos(OOB.ReportesCxc.DetallePorDoc.Filtro filtro);

        OOB.Resultado.Lista<OOB.ReportesCxc.Resumen.Ficha>
            ReportesCxc_ResumenCobranza(OOB.ReportesCxc.Resumen.Filtro filtro);

        OOB.Resultado.Lista<OOB.ReportesCxc.DetallePorMedioPago.Ficha>
            ReportesCxc_DetalleCobranza_PorMedioPago(OOB.ReportesCxc.DetallePorMedioPago.Filtro filtro);
    }
}