using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.Factura
{
    public interface IFactura: Src.IReporte
    {
        void setIdDocVisualizar(string idDoc);
    }
}