using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Aliado
{
    public interface IAliado: Src.IGestion
    {
        int CntItem_Get { get; }
        BindingSource Source_GetData { get; }
        Utils.Busqueda.IComp CompBusqueda { get; }
        bool ItemSeleccionadoIsOk { get; }
        int AliadoSeleccionado_GetId { get; }

        void BuscarAliados();
        void ItemSeleccionado();
    }
}