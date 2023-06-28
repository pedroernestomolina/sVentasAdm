using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Aliado.Lista
{
    public interface IAliadoLista
    {
        BindingSource Source_GetData { get; }
        int CntItem_Get { get; }
        data ItemActual { get; }

        void Inicializa();
        void setData(List<data> lst);
    }
}