using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class dataItem
    {
        private List<Item.IItem> _lst;
        private BindingList<Item.IItem> _bl;
        private BindingSource _bs;


        public BindingSource Source_Get { get { return _bs; } }
        public Item.IItem ItemActual { get { return (Item.IItem)_bs.Current; } }
        public int Cnt_Get { get { return _bs.Count; } }


        public dataItem()
        {
            _lst = new List<Item.IItem>();
            _bl = new BindingList<Item.IItem>(_lst);
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
        public void AgregarItem(Item.IItem item)
        {
            var _id = 1;
            if (_lst.Count>0)
            {
                _id = _lst.Max(m => m.id) + 2;
            }
            item.setId(_id);
            _bl.Add(item);
            _bs.CurrencyManager.Refresh();
        }
        public void EliminarItem(Item.IItem item)
        {
            _bl.Remove(item);
            _bs.CurrencyManager.Refresh();
        }


        private decimal _tasaDivisa = 0m;
        public void setTasaDivisa(decimal tasa)
        {
            _tasaDivisa=tasa;
        }

        //PARA LOS TOTALES TODO EN BASE A DIVISA
        public decimal MontoNetoDivisa { get { return _bl.Sum(s => s.Item.Get_Importe); } }
        public decimal MontoIvaDivisa { get { return _bl.Sum(s => s.Item.Get_Iva); } }


        public void LimpiarTodo()
        {
            _lst.Clear();
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
    }
}