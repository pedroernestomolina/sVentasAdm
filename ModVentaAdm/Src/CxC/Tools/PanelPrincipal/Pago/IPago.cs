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
        string GetMontoRecibido { get; }
        string GetCntMetRecibido { get; }
        void AgregarMetPago();
        void ListarMetPago();

        //PANEL: CTAS 
        string GetCntCtasPagar { get; }
        string GetMontoCtasPagar { get; }
        void ListarCtasPagar();
    }
}