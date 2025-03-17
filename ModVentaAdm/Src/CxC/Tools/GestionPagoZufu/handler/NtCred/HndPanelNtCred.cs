using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.NtCred
{
    public class HndPanelNtCred: baseHndPanel, PanelPrincipal.Pago.IPanelCtasNtCred
    {
        private decimal _montoNtCredDisponible;
        //
        public decimal GetMontoNtCredDisponible { get { return _montoNtCredDisponible; } }
        //
        public HndPanelNtCred()
            : base(new HndCtasPend())
        {
            _montoNtCredDisponible = 0m;
        }
        public void setMontoNtCredDisponible(decimal monto)
        {
            _montoNtCredDisponible = monto;
        }
    }
}