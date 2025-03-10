using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IListaCtaPend
    {
        decimal GetTotalMontoAbonar { get; }
        decimal GetTotalMontoPend { get; }
        int GetCntDocAbon { get; }
        decimal GetTotalMontoAbon { get; }
        int GetCntDocPend { get; }
        object Source { get;}
        PanelPrincipal.Pago.IItemCtaPend ItemActual { get;}
        //
        void Inicializa();
        void setData(IEnumerable<IItemCtaPend> lst);
        void LimpiarAbonos();
        void ItemActualSetMontoAbonar(decimal monto);
        void ItemActualSetDetalleAbono(string p);
    }
}
