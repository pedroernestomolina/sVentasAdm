using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes
{
    public interface IReporteConFiltroMasFecha: IReporteConFiltro
    {
        DateTime Desde { get; }
        //
        void setDesde(DateTime fecha);
    }
}