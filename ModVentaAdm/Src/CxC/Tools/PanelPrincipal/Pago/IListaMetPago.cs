using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IListaMetPago: IGestion 
    {
        object Source { get; }
        string GetCntMetRecibido { get; }
        decimal GetMontoRecibido { get; }
        string GetMetodoPagoOp { get; }
        decimal GetMontoOp { get; }
        DateTime GetFechaOp { get; }
        string GetDetalleOp { get; }
        string GetNroCtaOp { get; }
        string GetRefOp { get; }
        string GetBancoOp { get; }
        string GetAplicaFactorOp { get; }
        //
        void EliminarMetodoPago();
        void EditarMetodoPago();
        //
        void AgregarMet(IItemMetPago itemMetPago);
    }
}