using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Buscar
{
    public interface IItem
    {
        BindingSource Source_Get { get; }
        object ItemActual { get; }
        int CntItems_Get { get; }

        void Inicializa();
        void Limpiar();
        void setDataCargar(IEnumerable<object> list);
    }
}