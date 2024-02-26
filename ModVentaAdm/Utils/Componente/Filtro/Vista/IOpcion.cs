using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Filtro.Vista
{
    public interface IOpcion
    {
        bool GetHabilitarOpcion { get; }
        void setHabilitarOpcion(bool modo);
    }
}