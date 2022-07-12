using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.AgregarCta
{
    
    public interface IAgregar: IGestion, Gestion.IAbandonar, Gestion.IProcesar
    {

        bool AgregarIsOk { get; }


        BindingSource VendGetSource { get; }
        string  VendGetId { get;  }
        void setVend(string id);

        BindingSource TipoDocGetSource { get; }
        string TipoDocGetId { get; }
        void setTipoDoc(string id);


        void setSerieDoc(string p);
        void setNumDoc(string p);
        void setFechaEmisionDoc(DateTime dateTime);
        void setDiasCreditoDoc(int cnt);
        void setMontoDoc(decimal monto);
        void setFactor(decimal tasa);
        void setNotas(string p);


        string SerieGet { get; }
        DateTime FechaVencDocGet { get; }
        string NumeroDocGet { get; }
        DateTime FechaEmisionDocGet { get; }
        int DiasCreditoDocGet { get; }
        decimal MontoDocGet { get; }
        string NotasDocGet { get; }
        decimal TasaFactorDocGet { get; }


        void BuscarCliente();
        bool ClienteSeleccionadoIsOk { get; }
        string ClienteDataGet { get; }


    }

}