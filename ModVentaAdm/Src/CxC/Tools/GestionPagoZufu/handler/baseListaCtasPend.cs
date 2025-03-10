using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler
{
    abstract public class baseListaCtasPend: PanelPrincipal.Pago.IListaCtaPend
    {
        private BindingList<PanelPrincipal.Pago.IItemCtaPend> _bl;
        private BindingSource _bs;
        //
        public decimal GetTotalMontoAbonar { get { return _bl.Sum(s => s.montoAbonar); } }
        public decimal GetTotalMontoPend { get { return _bl.Sum(s => s.montoResta); } }
        public int GetCntDocAbon { get { return _bl.Count(c => c.montoAbonar > 0m); } }
        public decimal GetTotalMontoAbon { get { return _bl.Sum(s => s.montoAbonar); } }
        public int GetCntDocPend { get { return _bs.Count; } }
        //
        public object Source { get { return _bs; } }
        public PanelPrincipal.Pago.IItemCtaPend ItemActual { get { return (PanelPrincipal.Pago.IItemCtaPend)_bs.Current; } }
        //
        public baseListaCtasPend()
        {
            _bl = new BindingList<PanelPrincipal.Pago.IItemCtaPend>();
            _bs = new BindingSource();
            _bs.DataSource = _bl;
        }
        public void Inicializa()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void setData(IEnumerable<PanelPrincipal.Pago.IItemCtaPend> lst)
        {
            _bl.Clear();
            foreach(var it in lst)
            {
                _bl.Add(it);
            }
            _bs.CurrencyManager.Refresh();
        }
        public void LimpiarAbonos()
        {
            foreach (var it in _bl.ToList())
            {
                it.montoAbonar = 0m;
            }
            _bs.CurrencyManager.Refresh();
        }
        abstract public void ItemActualSetMontoAbonar(decimal monto);
        abstract public void ItemActualSetDetalleAbono(string p);
    }
}