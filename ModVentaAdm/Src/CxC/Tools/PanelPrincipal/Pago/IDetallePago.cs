using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IDetallePago: IGestion 
    {
        bool DetalleIsOk { get; }
        string GetNotas { get; }
        string GetIdCobrador { get; }
        Object CobradorSource { get; }
        DateTime GetFechaProceso { get; }
        object GetCobrador { get; }
        //
        void setNotas(string p);
        void setCobrador(string p);
        void setFechaProceso(DateTime fechaProceso);
        //
        bool AbandonarIsOK { get; }
        void AbandonarFicha();
        //
        bool ProcesarIsOK { get; }
        void Procesar();
        //
        void LimpiarData();
    }
}