using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IPanelMetPago
    {
        decimal  GetMontoRecibido { get; }
        string GetCntMetRecibido { get; }
        //
        void Inicializa();
        void AgregarMetPago();
        void ListarMetPago();
        //
        void setFactorDivisa(decimal fact);
    }
}