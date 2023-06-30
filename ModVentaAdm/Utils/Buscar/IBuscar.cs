using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Buscar
{
    public interface IBuscar: Src.IGestion 
    {
        Busqueda.IComp CompBusqueda { get; }
        IItem Items { get; }
        bool ItemSeleccionadoIsOk { get; }
        object ItemSeleccionadoGetId { get; }

        void Buscar();
        void Limpiar();
        void ItemSeleccionado();
    }
}