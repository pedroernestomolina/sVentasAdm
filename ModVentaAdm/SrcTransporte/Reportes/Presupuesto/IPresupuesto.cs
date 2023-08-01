using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.Presupuesto
{
    public interface IPresupuesto: Src.IReporte
    {
        void setIdDocVisualizar(string idDoc);
    }
}