using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.ListaGestionPago
{
    
    public class Lista: ILista
    {

        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource DocPendGetSource { get { return _bs; } }
        public List<data> ListaItems { get { return _bl.ToList(); } }
        public data ItemActual { get { return (data)_bs.Current; } }
        public decimal MontoPendientePorCobrar { get { return _bl.Sum(s => s.montoResta); } }
        public decimal MontoAbonar { get { return _bl.Sum(s => s.montoAbonar); } }
        public int CntItems { get { return _bl.Count; } }
        public int CantItemsSeleccionados { get { return _bl.Count(s => s.isPagarOk); } }
        public IEnumerable<data> ListaItemsSeleccionados { get { return _bl.Where(w => w.isPagarOk).ToList(); } }


        public Lista()
        {
            _lst = new List<data>();
            _bl = new BindingList<data>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
        }


        public void Inicializa()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void setListaDocPend(List<data> lst)
        {
            _bl.Clear();
            foreach (var rg in lst)
            {
                _bl.Add(rg);
            }
            _bs.CurrencyManager.Refresh();
        }
        public void Refrescar()
        {
            _bs.CurrencyManager.Refresh();
        }

    }

}