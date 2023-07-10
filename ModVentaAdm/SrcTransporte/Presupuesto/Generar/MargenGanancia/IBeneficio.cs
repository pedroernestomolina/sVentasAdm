using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.MargenGanancia
{
    public interface IBeneficio: Src.IGestion, Src.Gestion.IAbandonar
    {
        data Data { get; }

        void setMontoDoc(decimal monto);
        void setPagoAliado(decimal monto);
    }
}
