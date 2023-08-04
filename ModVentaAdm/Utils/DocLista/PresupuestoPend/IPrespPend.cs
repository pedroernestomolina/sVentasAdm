using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.DocLista.PresupuestoPend
{
    public interface IPrespPend: IDocLista, Src.Gestion.IAbandonar
    {
        bool ItemSeleccionadoIsOk { get; }
        void SeleccionarItem();
    }
}
