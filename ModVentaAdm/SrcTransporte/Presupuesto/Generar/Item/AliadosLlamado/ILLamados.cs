using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item.AliadosLlamado
{
    public interface ILLamados
    {
        BindingSource Get_Source { get; }
        List<data> GetLista { get; }
        data ItemActual { get; }
        decimal Get_Importe { get; }

        void Inicializa();
        void Agregar(data data);
        void setListaAliadosLlamados(List<data> lst);
        void Eliminar(data item);
    }
}