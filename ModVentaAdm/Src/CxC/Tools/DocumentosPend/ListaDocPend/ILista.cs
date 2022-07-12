using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.DocumentosPend.ListaDocPend 
{

    public interface ILista
    {


        BindingSource DocPendGetSource { get;  }
        decimal MontoPendientePorCobrar { get; }
        decimal MontoImporte { get; }
        decimal MontoAcumulado { get; }
        int CntItems { get; }


        void Inicializa();
        void setListaDocPend(List<data> lst);
        List<data> ListaItems { get; }
        data ItemActual { get; }

    }

}