using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.ReportesCliente
{
    
    public interface IGestion
    {

        Filtro.IFiltro Filtros { get; }
        void Generar(Filtro.data data);

    }

}