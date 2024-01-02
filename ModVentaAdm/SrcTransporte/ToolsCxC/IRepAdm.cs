using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ToolsCxC
{
    public interface IRepAdm: IReporte
    {
        void setDataCargar(IEnumerable<object> lista);
        void setFiltrosBusq(string filtros);
    }
}