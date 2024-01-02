using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes
{
    public interface IReporteConFiltro: Src.IReporte
    {
        void setFiltros(Filtro.Vista.IFiltro filtros);
        void setFiltros(SrcTransporte.Filtro.Vistas.IdataFiltrar dataFiltrar);
    }
}