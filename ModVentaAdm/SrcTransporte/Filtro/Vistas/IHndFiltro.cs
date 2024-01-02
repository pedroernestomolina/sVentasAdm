using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Filtro.Vistas
{
    public interface IHndFiltro
    {
        // 
        DateTime Get_Desde { get; }
        DateTime Get_Hasta { get; }
        bool Get_IsActivoDesde { get; }
        bool Get_IsActivoHasta { get; }
        void setDesde(DateTime fecha);
        void setHasta(DateTime fecha);
        void ActivarDesde(bool modo);
        void ActivarHasta(bool modo);

        //
        BindingSource Get_EstatusSource { get; }
        string Get_EstatusById { get; }
        void setEstatusById(string id);
        
        //
        BindingSource Get_CajaSource { get; }
        string Get_CajaById { get; }
        string GetCaja_TextoBuscar { get; }
        void setCajaById(string id);
        void setCajaBuscar(string desc);

        //
        BindingSource Get_ClienteSource { get; }
        string Get_ClienteById { get; }
        string GetCliente_TextoBuscar { get; }
        void setClienteById(string id);
        void setClienteBuscar(string desc);

        //
        Utils.FiltrosCB.ICtrlConBusqueda Cliente { get; }
        Utils.FiltrosCB.ICtrlConBusqueda Aliado { get; }
        Utils.FiltrosCB.ICtrlSinBusqueda EstatusDoc { get; }
        Utils.FiltroFecha.IFecha Desde { get; }
        Utils.FiltroFecha.IFecha Hasta { get; }

        //
        void Inicializa();
        void CargarData();
        void Limpiar();
        bool VerificarFiltros();
        IdataFiltrar Get_Filtros { get; }
    }
}