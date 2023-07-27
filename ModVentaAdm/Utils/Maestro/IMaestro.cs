using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Maestro
{
    public interface IMaestro: Src.IGestion
    {
        ILista Lista { get; }
        string TituloMaestro_Get { get; }
        BindingSource DataSource_Get { get; }
        int CntItems_Get { get; }

        void AgregarItem();
        void EditarItem();
    }
}