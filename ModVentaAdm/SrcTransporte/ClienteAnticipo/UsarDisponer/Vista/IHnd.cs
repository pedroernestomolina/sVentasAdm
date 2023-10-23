using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.UsarDisponer.Vista
{
    public interface IHnd: Src.IGestion, Src.Gestion.IAbandonar, Src.Gestion.IProcesar
    {
        string Get_Cliente { get; }
        decimal Get_MontoADisponer { get; }
        decimal Get_Cliente_MontoAnticipo { get; }
        decimal Get_MontoDeudaVerificar { get; }

        void setCliente(string idCliente);
        void setMontoDeuda(decimal monto);
        void setMontoDisponer(decimal _monto);
    }
}