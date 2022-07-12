using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MontoAbonar
{

    public interface IMontoAbonar : IGestion, Gestion.IAbandonar, Gestion.IProcesar
    {

        bool MontoAbonarIsOk { get; }


        void setData(decimal montoResta, decimal montoAbonar, string detalle);


        decimal GetMontoPendiente { get; }
        decimal GetMontoAbonar { get; }
        string GetDetalle { get; }


        void setDetalle(string p);
        void setMontoAbonar(decimal rt);
           
    }

}