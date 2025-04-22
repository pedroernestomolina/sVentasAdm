using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.ListaCtasPend
{
    public interface ILista
    {
        BindingSource CtasPendGetSource { get;  }
        decimal MontoPendientePorCobrar { get; }
        //
        void Inicializa();
        List<Idata> ListaItems { get; }
        Idata ItemActual { get; }
        void setData(IEnumerable<object> lst);
        void setFiltrarPor(string textoFiltrar);
        void setFiltrarMostrarCtasEnCero(bool mostratCtasEnCero);
    }
}