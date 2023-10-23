using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Filtro.Vistas
{
    public interface IFiltro: Src.IGestion, Src.Gestion.IAbandonar, Src.Gestion.IProcesar 
    {
        IHndFiltro HndFiltro { get; }
        void Limpiar();
    }
}