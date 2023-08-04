using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.DocLista.Remision
{
    public interface IRemisionLista: ILista
    {
        void setDataCargar(List<Remision.data> lst);
    }
}