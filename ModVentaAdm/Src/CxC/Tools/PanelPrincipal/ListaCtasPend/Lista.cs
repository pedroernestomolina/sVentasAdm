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

        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource CtasPendGetSource { get { return _bs; } }
        public decimal MontoPendientePorCobrar { get { return _bl.Sum(s => s.montoResta); } }


        public Lista() 
        {
            _lst= new List<data>();
            _bl= new BindingList<data>(_lst);
            _bs= new BindingSource();
            _bs.DataSource = _bl;
        }


        public void Inicializa()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void setListaCtasPend(List<data> lst)
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
