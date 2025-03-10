using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IPanelCtas
    {
        decimal GetMontoPagar { get; }
        string GetCntCtasPagar { get; }
        //
        void Inicializa();
        void ListarCtasPagar();
        void setIdEntidad(object id);
        void setFechaServidor(DateTime fecha);
    }
}