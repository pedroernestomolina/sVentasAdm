using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface ICtasPend: IGestion
    {
        object GetIdEntidad { get; }
        PanelPrincipal.Pago.IListaCtaPend ListaCtas { get; }
        IEnumerable<IItemCtaPend> GetListaDocPagar { get; }
        //
        decimal GetMontoPagar { get; }
        int GetCntCtasPagar { get; }
        //
        Object DataSource { get; }
        decimal GetTotalMontoPend { get; }
        string GetCntDocPend { get; }
        string GetCntDocAbon { get; }
        string GetTotalMontoAbon { get; }
        string GetNotas { get; }
        //
        void setIdEntidad(object id);
        void setFechaServidor(DateTime fecha);
        //
        void LimpiarAbonos();
        void AbonarCta();
        void setMontoAbonadoASaldar(decimal monto);
        //
        string GetCliente { get; }
        void setClientePagar(string dat);
    }
}