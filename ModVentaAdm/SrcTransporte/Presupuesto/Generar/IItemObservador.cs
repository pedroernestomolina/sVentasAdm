using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public interface IItemObservador
    {
        void OnItemAgregado(Item.IItem item);
        void OnItemEliminado(Item.IItem item);
    }
}
