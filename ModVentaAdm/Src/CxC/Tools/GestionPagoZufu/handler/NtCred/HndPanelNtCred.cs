using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.NtCred
{
    public class HndPanelNtCred: baseHndPanel, PanelPrincipal.Pago.IPanelCtas
    {
        public HndPanelNtCred()
            : base(new HndCtasPend())
        {
        }
    }
}