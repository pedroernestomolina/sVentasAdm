using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir.Items
{
    public interface ILista
    {
        BindingSource DataSource { get; }
        object ItemActual { get; }
        void Inicializa();
        void setItems(List<OOB.Transporte.Documento.Entidad.Venta.FichaDetalle> _items);
        void setItemsDoc(List<OOB.Transporte.Documento.Entidad.Venta.DetDoc> _itemsDoc);
    }
}