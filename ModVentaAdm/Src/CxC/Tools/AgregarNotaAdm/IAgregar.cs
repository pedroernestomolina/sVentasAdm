using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.AgregarNotaAdm
{
    
    public interface IAgregar: IGestion, Gestion.IAbandonar, Gestion.IProcesar
    {

        bool AgregarIsOk { get; }


        BindingSource VendGetSource { get; }
        string  VendGetId { get;  }
        void setVend(string id);


        void setMontoDoc(decimal monto);
        void setFactor(decimal tasa);
        void setNotas(string p);


        string NumeroConsecutivoDocGet { get; }
        DateTime FechaEmisionDocGet { get; }
        decimal MontoDocGet { get; }
        string NotasDocGet { get; }
        decimal TasaFactorDocGet { get; }


        void BuscarCliente();
        bool ClienteSeleccionadoIsOk { get; }
        string ClienteDataGet { get; }


        void setTipoNota(IAgregarTipoNotaAdm _gAgregarNotaCreditoAdm);
        string TipoNotaAdmGet { get; }

    }

}