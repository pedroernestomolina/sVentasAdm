using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes
{
    
    public interface IGestion
    {

        Reportes.Filtro.IFiltro Filtros { get; }
        void Generar(Filtro.data data);

    }

}