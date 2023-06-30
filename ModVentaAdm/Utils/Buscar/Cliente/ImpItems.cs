using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Buscar.Cliente
{
    public class ImpItems: IItem
    {
        private List<dataCli> _lst;
        private BindingList<dataCli> _bl;
        private BindingSource _bs;


        public BindingSource Source_Get { get { return _bs; } }
        public object ItemActual { get { return _bs.Current; } }
        public int CntItems_Get { get { return _bs.Count; } }


        public ImpItems()
        {
            _lst = new List<dataCli>();
            _bl = new BindingList<dataCli>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }


        public void Inicializa()
        {
            _lst.Clear();
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void Limpiar()
        {
            _lst.Clear();
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void setDataCargar(IEnumerable<object> list)
        {
            _bl.Clear();
            foreach (dataCli rg in list)
            {
                _bl.Add(rg);
            }
            _bs.CurrencyManager.Refresh();
        }
    }
}