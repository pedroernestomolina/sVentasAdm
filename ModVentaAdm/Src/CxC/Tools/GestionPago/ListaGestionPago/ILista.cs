using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.ListaGestionPago
{
    
    public interface ILista
    {

        BindingSource DocPendGetSource { get; }
        decimal MontoPendientePorCobrar { get; }
        decimal MontoAbonar { get; }
        int CntItems { get; }
        int CantItemsSeleccionados { get; }


        void Inicializa();
        void setListaDocPend(List<data> lst);
        List<data> ListaItems { get; }
        data ItemActual { get; }


        void Refrescar();
        IEnumerable<data> ListaItemsSeleccionados { get; }

    }

}