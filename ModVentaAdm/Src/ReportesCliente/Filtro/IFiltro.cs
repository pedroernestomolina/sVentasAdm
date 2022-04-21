using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.ReportesCliente.Filtro
{
    
    public interface IFiltro
    {

        bool ActivarGrupo{ get; }
        bool ActivarEstado { get; }
        bool ActivarZona { get; }
        bool ActivarTarifa { get; }
        bool ActivarEstatus { get; }
        bool ActivarVendedor { get; }
        bool ActivarCobrador { get; }
        bool ActivarCategoria { get; }
        bool ActivarCredito { get; }
        bool ActivarNivel { get; }

    }

}