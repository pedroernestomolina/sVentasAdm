using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.ListaCtasPend
{
    public class Lista: ILista
    {
        private List<Idata> _src;
        private BindingList<Idata> _bl;
        private BindingSource _bs;
        //
        public BindingSource CtasPendGetSource { get { return _bs; } }
        public List<Idata> ListaItems { get { return _bl.ToList(); } }
        public Idata ItemActual { get { return (Idata)_bs.Current; } }
        public decimal MontoPendientePorCobrar { get { return _bl.Sum(s => s.montoResta); } }
        //
        public Lista() 
        {
            _src = new List<Idata>();
            _bl= new BindingList<Idata>();
            _bs= new BindingSource();
            _bs.DataSource = _bl;
        }
        public void Inicializa()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void FiltrarPor(string txt)
        {
            var _lst = _src.Where(w => w.nombreRazonSocial.Contains(txt)).ToList();
            agregarData(_lst);
        }
        public void setData(IEnumerable<object> lst)
        {
            _src.Clear();
            foreach (var rg in lst)
            {
                var nr = new ListaCtasPend.data(rg);
                _src.Add(nr);
            }
            agregarData(_src);
        }
        private void agregarData(IEnumerable<Idata> lst)
        {
            _bl.Clear();
            foreach (var rg in lst)
            {
                _bl.Add(rg);

            }
            _bs.CurrencyManager.Refresh();
        }
    }
}