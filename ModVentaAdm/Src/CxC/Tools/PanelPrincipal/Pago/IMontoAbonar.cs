using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IMontoAbonar : IGestion
    {
        decimal MontoAbonado { get; }
        //
        string GetMontoPendiente { get; }
        decimal GetMontoAbonar { get; }
        string GetDetalle { get; }
        //
        void setDetalle(string p);
        void setMontoAbonar(decimal monto);
        void setMontoPend(decimal monto);
        //
        bool AbandonarIsOK { get; }
        void AbandonarFicha();
        //
        bool ProcesarIsOK { get; }
        void ProcesarFicha();
    }
}