using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IPago: IGestion
    {
        void setIdEntidadPagar(object id);
        bool AbandonarFichaIsOk { get; }
        bool IsPagoExitoso { get; }
        void AbandonarFicha();
        void ProcesarPago();

        //PANEL: MET PAGO
        decimal GetMontoRecibido { get; }
        string GetCntMetRecibido { get; }
        void AgregarMetPago();
        void ListarMetPago();

        //PANEL: CTAS 
        int GetCntCtasPagar { get; }
        decimal GetMontoCtasPagar { get; }
        void ListarCtasPagar();

        //PANEL: NOTAS/CREDITO
        int GetCntNtCred { get; }
        decimal GetMontoNtCred { get; }
        void ListarNtCred();

        //PANEL: RESUMEN
        decimal GetResumenMontoAnticipo { get; }
        decimal GetResumenMontoMetPago { get; }
        decimal GetResumenMontoNtCredito { get; }
        decimal GetResumenMontoAbono { get; }
        decimal GetResumenMontoCtasPend { get; }
        decimal GetResumenSaldo { get; }
        string GetResumenSaldoDesc { get; }

        //PANEL:ANTICIPO
        decimal GetMontoAnticipo { get; }
        void AgregarAnticipo();

        //
        string GetCliente { get; }
        void setClientePagar(string dat);

        //
        void LimpiarData();
    }
}