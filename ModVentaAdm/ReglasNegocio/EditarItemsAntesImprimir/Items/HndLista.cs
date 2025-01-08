using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir.Items
{
    public class HndLista: ILista
    {
        private List<Idata> _data;
        private BindingList<Idata> _bl;
        private BindingSource _bs;
        //
        public BindingSource DataSource { get { return _bs; } }
        public object ItemActual { get { return _bs.Current; } }
        //
        public HndLista() 
        {
            _data = new List<Idata>();
            _bl = new BindingList<Idata>(_data);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
        }
        public void Inicializa()
        {
            _bl.Clear();
        }
        public void setItems(List<OOB.Transporte.Documento.Entidad.Venta.FichaDetalle> _items)
        {
            _bl.Clear();
            foreach (OOB.Transporte.Documento.Entidad.Venta.FichaDetalle it in _items) 
            {
                _bl.Add(new data(it));
            }
        }
        public void setItemsDoc(List<OOB.Transporte.Documento.Entidad.Venta.DetDoc> _itemsDoc)
        {
            _bl.Clear();
            foreach (OOB.Transporte.Documento.Entidad.Venta.DetDoc it in _itemsDoc)
            {
                _bl.Add(new data(it));
            }
        }
    }
}