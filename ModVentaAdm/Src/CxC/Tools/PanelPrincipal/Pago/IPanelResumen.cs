using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IPanelResumen
    {
        decimal GetResumenMontoAnticipo { get; }
        decimal GetResumenMontoMetPago { get; }
        decimal GetResumenMontoNtCredito { get; }
        decimal GetResumenMontoAbono { get; }
        decimal GetResumenMontoCtasPend { get; }
        decimal GetResumenSaldo { get; }
        string GetResumenSaldoDesc { get; }
        //
        void Inicializa();
        void setMontoMetPago(decimal p);
        void setMontoAnticipo(decimal p);
        void setMontoNtCredito(decimal p);
        void setMontoCtasPend(decimal p);
    }
}
