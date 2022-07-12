using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro
{
    
    public interface IMedCobro: IGestion, Gestion.IAbandonar, Gestion.IProcesar
    {

        BindingSource Source { get;  }
        decimal GetMontoCobrar { get; }
        decimal GetMontoRecibido { get; }
        decimal GetMontoPend { get; }
        string GetRestaCambio { get; }


        void setMontoCobrar(decimal monto);


        void AgregarFicha();
        bool AgregarFichaIsOk { get; }


        void EliminarMetodoPago();
        bool EliminarMetodoPagoIsOk { get; }


        void EditarMetodoPago();
        bool EditarMetodoPagoIsOk { get; }


        string GetMetodoPagoOp { get; }
        decimal GetMontoOp { get; }
        DateTime GetFechaOp { get; }
        string GetDetalleOp { get; }
        string GetNroCtaOp { get; }
        string GetRefOp { get; }
        string GetBancoOp { get; }
        string GetAplicaFactorOp { get; }


        void setGenerarNotaCredito(bool p);


        decimal GetImporteMonedaLocal { get; }
        bool MedioCobroIsOk { get; }
        IEnumerable<data> GetListaMedCobro { get;  }

    }

}