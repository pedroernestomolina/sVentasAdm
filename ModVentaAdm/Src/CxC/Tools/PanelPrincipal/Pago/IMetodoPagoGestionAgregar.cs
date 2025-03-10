using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IMetodoPagoGestionAgregar: IMetodoPagoGestion
    {
        IItemMetPago ItemAgregar { get; }
    }
}