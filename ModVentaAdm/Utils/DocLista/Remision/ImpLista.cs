using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.DocLista.Remision
{
    public class ImpLista: ILista
    {
        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource Source_Get { get { return _bs; } }
        public object ItemActual { get { return _bs.Current; } }
        public int Cnt { get { return _bs.Count; } }


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

        public void setDataCargar(List<data> lst)
        {
            _lst.Clear();
            _lst.AddRange(lst);
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }
    }
}