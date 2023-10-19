
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.AplicaRetencionIslr.Vista
{
    public interface IHnd
    {
        bool Get_AplicaRet { get; }
        decimal Get_TasaRetencion { get; }
        decimal Get_MontoSustraendo { get; }
        decimal Get_MontoRetencion { get; }
        decimal Get_TotalRetencionMonDiv { get; }
        decimal Get_TotalRetencionMonAct { get; }

        void setFactorCambio(decimal factor);
        void setMontoAplicarRetencionMonAct(decimal monto);
        void setTasaRet(decimal monto);
        void setMontoSustraendo(decimal monto);
        void setAplicaRet(bool aplica);

        void Inicializa();
        bool IsOk();
    }
}