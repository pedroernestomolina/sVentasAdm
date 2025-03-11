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
        IEnumerable<PanelPrincipal.Pago.IItemCtaPend> GetListaDocPagar { get; }
        //
        void Inicializa();
        void ListarCtasPagar();
        void setIdEntidad(object id);
        void setFechaServidor(DateTime fecha);
        void setMontoAbonadoASaldar(decimal monto);
        //
        void setClientePagar(string dat);
    }
}