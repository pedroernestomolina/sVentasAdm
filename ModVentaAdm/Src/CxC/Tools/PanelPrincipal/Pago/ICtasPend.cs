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
        //
        decimal GetMontoPagar { get; }
        int GetCntCtasPagar { get; }
        //
        Object DataSource { get; }
        string GetTotalMontoPend { get; }
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
    }
}