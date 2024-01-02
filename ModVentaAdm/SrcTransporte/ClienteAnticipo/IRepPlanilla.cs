using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo
{
    public interface IRepPlanilla: IReporte
    {
        void setItemCargar(object idMov);
    }
}
