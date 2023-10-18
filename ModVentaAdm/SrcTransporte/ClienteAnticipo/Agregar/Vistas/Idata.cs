using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Agregar.Vistas
{
    public interface Idata
    {
        OOB.Maestro.Cliente.Entidad.Ficha Get_Cliente { get; }
        string Get_ClienteInfo { get; }
        decimal Get_MontoAnticipoMonDiv { get; }
        decimal Get_MontoAnticipoMonAct { get; }
        decimal Get_TasaFactorCambio { get; }
        string Get_Motivo { get; }
        DateTime Get_FechaAnticipo { get; }
        decimal Get_TasaRetencion { get; }
        decimal Get_MontoSustraendo { get; }
        decimal Get_MontoRetencion { get; }
        bool Get_AplicaRet { get; }
        decimal Get_MontoAbonoMonAct { get; }
        decimal Get_MontoAbonoMonDiv { get; }
        DateTime Get_FechaServidor { get; }
        decimal Get_TotalRetencionMonDiv { get; }
        decimal Get_TotalRetencionMonAct { get; }

        
        void Inicializa();
        void CargarData();
        void setFechaAnticipo(DateTime fecha);
        void setTasaFactorCambio(decimal monto);
        void setMontoAnticipoMonDiv(decimal monto);
        void setMotivo(string desc);
        void setCliente(OOB.Maestro.Cliente.Entidad.Ficha ficha);
        void setTasaRet(decimal monto);
        void setMontoSustraendo(decimal monto);
        void setAplicaRet(bool aplica);
        void setFechaServidor(DateTime fecha);
        bool VerificarData();
        bool IsOk();
    }
}