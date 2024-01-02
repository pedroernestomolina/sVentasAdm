using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Administrador.Vistas
{
    public interface IAdm: Utils.Componente.Administrador.Vistas.IAdmin
    {
        SrcTransporte.Filtro.Vistas.IFiltro CtrlFiltro { get; }
        IBusqDocAdm BusqDoc { get; }
        void FitrosBusqueda();
        void FiltrosLimpiar();
    }
}