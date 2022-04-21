using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Filtro
{
    
    public interface IFiltro
    {

        bool ActivarSucursal { get; }
        bool ActivarDesdeHasta { get; }
        bool ActivarEstatus { get; }
        bool ActivarMesAnoRelacion { get; }
        bool ActivarCliente { get; }
        bool ActivarTipoDocumento { get; }
        bool ValidarTipoDocumento { get; }
        bool ActivarProducto { get; }
        bool ActivarPalabreClave { get; }

    }

}