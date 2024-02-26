using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Comp.Vista
{
    public interface IVista: Utils.Componente.Filtro.Vista.IVista
    {
        Opcion.Vista.IPorCombo OpcPorGrupo { get; }
        Opcion.Vista.IPorCombo OpcPorEstado { get; }
        Opcion.Vista.IPorCombo OpcPorZona { get; }
        Opcion.Vista.IPorCombo OpcPorVendedor { get; }
        Opcion.Vista.IPorCombo OpcPorCobrador { get; }
        Opcion.Vista.IPorCombo OpcPorCategoria { get; }
        Opcion.Vista.IPorCombo OpcPorNivel { get; }
        Opcion.Vista.IPorCombo OpcPorEstatus { get; }
        Opcion.Vista.IPorCombo OpcPorCredito { get; }
        //
        void setOpcionesFiltrado(Opciones.IOpciones opciones);
    }
}