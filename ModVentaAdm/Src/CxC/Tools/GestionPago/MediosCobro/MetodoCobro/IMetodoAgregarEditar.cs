using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro
{
    
    public interface IMetodoAgregarEditar: IMetodo
    {

        string GetTituloFicha { get; }


        BindingSource GetMetCobroSource { get; }
        decimal GetMonto { get; }
        decimal GetFactor { get; }
        string GetBanco { get; }
        string GetNroCta { get; }
        string GetCheqRefTrans { get; }
        DateTime GetFechaOp { get; }
        string GetDetalleOp { get; }
        bool GetAplicaFactor { get; }
        decimal GetMontoResta { get; }
        string GetMetCobroID { get; }
        string GetReferencia { get; }
        string GetLote { get; }


        void setMetCobro(string id);
        void setMonto(decimal monto);
        void setFactor(decimal factor);
        void setBanco(string banco);
        void setCtaNro(string cta);
        void setChequeRefTranf(string cheqRefTranf);
        void setFechaOperacion(DateTime fecha);
        void setDetalleOperacion(string detalleOp);
        void setAplicaFactor(bool p);
        void setMontoResta(decimal GetMontoPend);
        void setLote(string lote);
        void setReferencia(string referenc);

    }

}