using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Agregar.Vistas
{
    public interface IMainData
    {
        Utils.FiltrosCB.ICtrlConBusqueda Aliado { get; }
        Utils.FiltrosCB.ICtrlConBusqueda Servicio { get; }
        //
        void Inicializa();
        void setMonto(decimal monto);
        void setServicio(string desc);
        bool IsOk();
        object DataExportar();
    }
}