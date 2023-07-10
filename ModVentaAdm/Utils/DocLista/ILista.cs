using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.DocLista
{
    public interface ILista
    {
        BindingSource Source_Get { get; }
        object ItemActual { get; }
        int Cnt { get;  }

        void Inicializa();
        void setDataCargar(List<Remision.data> _lst);
    }
}