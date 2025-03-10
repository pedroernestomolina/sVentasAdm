using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IItemCtaPend
    {
        DateTime fechaEmisionDoc { get; set; }
        string tipoDoc { get; set; }
        string numeroDoc { get; set; }
        DateTime fechaVencDoc { get; set; }
        string diasVencida { get; set; }
        decimal montoImporte { get; set; }
        decimal tasaCambioDoc { get; set; }
        decimal montoAcumulado{ get; set; }
        decimal montoResta{ get; set; }
        decimal montoAbonar{ get; set; }
        int SignoDoc { get; set; }
        string detalleAbono { get; set; }
    }
}