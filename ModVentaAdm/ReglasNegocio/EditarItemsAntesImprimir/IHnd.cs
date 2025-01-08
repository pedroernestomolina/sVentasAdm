using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir
{
    public interface IHnd: Src.IGestion
    {
        ModVentaAdm.Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get; }
        ModVentaAdm.Utils.Control.Boton.Procesar.IProcesar BtProcesar { get; }
        BindingSource DataSource { get; }
        void setItems(object items, int modo);
        void EditarItem();
    }
}