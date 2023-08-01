using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Maestro.Transp.ServPrest
{
    public class ImpLista: IServLista
    {
        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource DataSource_Get { get { return _bs; } }
        public object ItemActual { get { return _bs.Current; } }
        public int CntItems_Get { get { return _bs.Count; } }


        public ImpLista()
        {
            _lst = new List<data>();
            _bl = new BindingList<data>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }
        public void Inicializa()
        {
            _lst.Clear();
            _bl.Clear();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }

        public void setDataCargar(IEnumerable<object> lst)
        {
            _lst.Clear();
            foreach (var rg in lst)
            {
                var nr = new data((OOB.Transporte.ServPrest.Entidad.Ficha)rg);
                _lst.Add(nr);
            }
            _bs.CurrencyManager.Refresh();
        }
        public void AgregarItem(object ficha)
        {
            var nr = new data(((OOB.Transporte.ServPrest.Entidad.Ficha)ficha));
            _lst.Add(nr);
            _bs.CurrencyManager.Refresh();
        }
        public void RemoverItemBy(object id)
        {
            var _idItem= ((int)id);
            var _item= _lst.FirstOrDefault(b => b.Ficha.id == _idItem);
            if (_item != null)
            {
                _bl.Remove(_item);
            }
            _bs.CurrencyManager.Refresh();
        }
    }
}