using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Maestro
{
    public interface ILista
    {
        BindingSource DataSource_Get { get; }
        object ItemActual { get; }
        int CntItems_Get { get; }

        void Inicializa();
        void setDataCargar(IEnumerable<object> lst);
        void AgregarItem(object ficha);
        void RemoverItemBy(object ficha);
    }
}