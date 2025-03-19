using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Ctas
{
    public class HndPanelCtas: baseHndPanel, PanelPrincipal.Pago.IPanelCtasDocPend
    {
        private decimal _saldoDocPendPagar;
        //
        public decimal GetSaldoDocPendPagar { get { return _saldoDocPendPagar; } }
        //
        public HndPanelCtas()
            : base(new HndCtasPend())
        {
            _saldoDocPendPagar = 0m;
        }
        public void setSaldoDocPendPagar(decimal monto)
        {
            _saldoDocPendPagar = monto;
        }
    }
}
