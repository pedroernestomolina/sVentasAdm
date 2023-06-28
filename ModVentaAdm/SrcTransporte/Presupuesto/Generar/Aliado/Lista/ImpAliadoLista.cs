using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Aliado.Lista
{
    public class ImpAliadoLista: IAliadoLista
    {
        private List<data> _lst;
        private BindingSource _bs;


        public int CntItem_Get { get { return _bs.Count; } }
        public BindingSource Source_GetData { get { return _bs; } }
        public data ItemActual { get { return (data)_bs.Current; } }


        public ImpAliadoLista()
        {
            _lst = new List<data>();
            _bs = new BindingSource();
            _bs.DataSource = _lst;
        }
        public void Inicializa()
        {
            _lst.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void setData(List<data> lst)
        {
            _lst.Clear();
            _lst.AddRange(lst);
            _bs.CurrencyManager.Refresh();
        }
    }
}