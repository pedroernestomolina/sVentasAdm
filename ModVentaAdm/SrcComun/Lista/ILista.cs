using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcComun.Lista
{
    public interface ILista
    {
        BindingSource Get_Source { get; }
        object ItemActual { get; }
        int Cnt { get;  }
        //
        void Inicializa();
        void setDataCargar(IEnumerable<object> list);
    }
}