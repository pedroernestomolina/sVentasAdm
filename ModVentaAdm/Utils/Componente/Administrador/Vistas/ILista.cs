using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Componente.Administrador.Vistas
{
    public interface ILista
    {
        BindingSource Get_Source { get; }
        object ItemActual { get;  }
        int Get_CntItem { get; }
        IEnumerable<object> Get_Items { get; }

        void Inicializa();
        void setDataCargar(IEnumerable<object> lst);
        void LimpiarData();
        void Refresca();
    }
}