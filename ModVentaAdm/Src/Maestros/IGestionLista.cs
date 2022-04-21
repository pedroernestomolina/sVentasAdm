using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Maestros
{
    
    public interface IGestionLista
    {

        int TotalItems { get; }
        System.Windows.Forms.BindingSource Source { get; }
        data ItemActual { get; }

        void setLista(List<data> lst);
        void Agregar(data data);
        void Actualizar(data data);

    }

}