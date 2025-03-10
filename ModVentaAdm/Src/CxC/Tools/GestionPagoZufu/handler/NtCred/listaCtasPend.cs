using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.NtCred
{
    public class listaCtasPend: baseListaCtasPend, PanelPrincipal.Pago.IListaCtaPend
    {
        public listaCtasPend(): base()
        {
        }
        public override void ItemActualSetMontoAbonar(decimal monto)
        {
            if (ItemActual == null) return;
            ((itemCtaPend)ItemActual).setMontoAbonar(monto);
            ((BindingSource)Source).CurrencyManager.Refresh();
        }
        public override void ItemActualSetDetalleAbono(string p)
        {
            if (ItemActual == null) return;
            ((itemCtaPend)ItemActual).setDetalleAbono(p);
            ((BindingSource)Source).CurrencyManager.Refresh();
        }
    }
}