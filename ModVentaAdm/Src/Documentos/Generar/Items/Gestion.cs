using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.Items
{
    
    public class Gestion: IItems
    {

        private decimal _mDivisa;
        private List<data> _ldata;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public int CntItem { get { return _bl.Count; } }
        public decimal MontoNeto { get { return _bl.Sum(s => s.Importe); } }
        public decimal MontoIva { get { return _bl.Sum(s => s.MIva); } }
        public decimal MontoTotal { get { return _bl.Sum(s => s.mTotal); } }
        public decimal MontoTotalDivisa { get { return MontoTotal / _mDivisa; } }
        public BindingSource ItemsSource { get { return _bs; } }
        public bool HayItemsEnBandeja { get { return CntItem > 0; } }
        public data ItemActual { get { return (data) _bs.Current; } }
        public List<data> ListaItems { get { return _bl.ToList(); } }


        public Gestion() 
        {
            _mDivisa = 0m;
            _ldata = new List<data>();
            _bl = new BindingList<data>(_ldata);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
        }

        public void Inicializa()
        {
            _mDivisa = 0m;
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }

        public void setDivisa(decimal mont) 
        {
            _mDivisa = mont;
        }

        public void LimpiarItems()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }

        public void EliminarItem(data it)
        {
            _bl.Remove(it);
            _bs.CurrencyManager.Refresh();
        }

        public void AgregarItem(OOB.Venta.Temporal.Item.Entidad.Ficha ficha, decimal TasaDivisa)
        {
            var rg = new data(ficha);
            rg.setTasaDivisa(TasaDivisa);
            _bl.Insert(0,rg);
        }

        public void EliminarLista(int id)
        {
            var it = _bl.FirstOrDefault(f => f.Id == id);
            if (it != null)
            {
                _bl.Remove(it);
            }
        }

        public void EliminarListaItems()
        {
            _bl.Clear();
        }

        public void AgregarLista(List<OOB.Venta.Temporal.Item.Entidad.Ficha> list, decimal factorDivisa)
        {
            foreach(var item in list)
            {
                var rg = new data(item);
                rg.setTasaDivisa(factorDivisa);
                _bl.Insert(0, rg);
            }
        }

        public void setCambioTasaDivisa(decimal tasa)
        {
            _mDivisa = tasa;
            foreach (var it in _ldata)
            {
                it.setTasaDivisa(tasa);
            }
        }

    }

}