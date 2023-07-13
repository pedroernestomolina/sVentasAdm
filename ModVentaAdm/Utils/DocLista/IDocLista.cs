using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.DocLista
{
    public interface IDocLista: Src.IGestion
    {
         ILista Items { get;  }
         bool ItemSeleccionadoIsOk { get; }
         object ItemSeleccionado { get; }
         void setDataCargar(IEnumerable<object> lst);
    }
}