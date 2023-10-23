using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Administrador.Vistas
{
    public interface IAdm: Utils.Componente.Administrador.Vistas.IAdmin
    {
        DateTime Get_Desde { get; }
        DateTime Get_Hasta { get; }
        bool Get_IsActivoDesde { get; }
        bool Get_IsActivoHasta { get; }
        void setDesde(DateTime fecha);
        void setHasta(DateTime fecha);
        void ActivarDesde(bool modo);
        void ActivarHasta(bool modo);

        IBusqDocAdm BusqDoc { get; }
        void FitrosBusqueda();
        void FiltrosLimpiar();
    }
}