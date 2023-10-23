using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Administrador.Handler
{
    public class HndLista: Vistas.IListaAdm
    {
        private List<Vistas.IdataItem> _lst;
        private BindingList<Vistas.IdataItem> _bl;
        private BindingSource _bs;


        public BindingSource Get_Source { get { return _bs; } }
        public IEnumerable<object> Get_Items { get { return _bl.ToList(); } }
        public object ItemActual { get { return _bs.Current; } }
        public int Get_CntItem { get { return _bs.Count; } }


        public HndLista()
        {
            _lst = new List<Vistas.IdataItem>();
            _bl = new BindingList<Vistas.IdataItem>(_lst);
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
            _bl.Clear();
            foreach (var rg in lst)
            {
                var nr = (Vistas.IdataItem)rg;
                _lst.Add(nr);
            }
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }
        public void LimpiarData()
        {
            _lst.Clear();
            _bl.Clear();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }
        public void Refresca()
        {
            _bs.CurrencyManager.Refresh();
        }
    }
}