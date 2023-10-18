using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Agregar.Vistas
{
    public interface IHnd: ModVentaAdm.Src.IGestion, ModVentaAdm.Src.Gestion.IProcesar, ModVentaAdm.Src.Gestion.IAbandonar
    {
        Idata data { get;  }
        IHndCaja caja { get; }

        void setClienteCargar(string id);
        void ActualizarSaldoCaja();
    }
}