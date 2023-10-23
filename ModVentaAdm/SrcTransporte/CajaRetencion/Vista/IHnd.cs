using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.CajaRetencion.Vista
{
    public interface IHnd : ModVentaAdm.Src.IGestion, ModVentaAdm.Src.Gestion.IProcesar, ModVentaAdm.Src.Gestion.IAbandonar
    {
        decimal Get_FactorCambio { get; }
        Utils.Componente.AplicaRetencionIslr.Vista.IHnd Retencion { get; }
        Utils.Componente.CajasUtilizar.Vista.IHnd Caja { get; }

        void setFactorCambio(decimal factor);
        void setMontoCajaProcesarMonDiv(decimal montoCaja);
        void ActualizarSaldoCaja();
    }
}