using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.DetalleCobro
{
    public interface IDetalle: IGestion, Gestion.IAbandonar , Gestion.IProcesar
    {
        bool DetalleIsOk { get; }
        string GetNotas { get; }
        string GetIdCobrador { get; }
        BindingSource CobradorSource { get; }
        DateTime Get_FechaProceso { get; }
        //
        void setNotas(string p);
        void setCobrador(string p);
        void setFechaProceso(DateTime fechaProceso);
    }
}