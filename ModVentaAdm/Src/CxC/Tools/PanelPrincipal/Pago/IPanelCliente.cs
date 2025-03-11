using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface IPanelCliente
    {
        Object GetEntidadPagar { get; }
        decimal GetMontoPagar { get; }
        //
        void Inicializa();
        void setIdEntidad(object id);
        void setFechaServidor(DateTime fecha);
        void AgregarAnticipo();
    }
}