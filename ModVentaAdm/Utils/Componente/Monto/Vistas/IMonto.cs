using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Monto.Vistas
{
    public interface IMonto: ModVentaAdm.Src.IGestion, ModVentaAdm.Src.Gestion.IProcesar, ModVentaAdm.Src.Gestion.IAbandonar 
    {
        decimal Get_Monto { get; }

        void setMonto(decimal _monto);
    }
}