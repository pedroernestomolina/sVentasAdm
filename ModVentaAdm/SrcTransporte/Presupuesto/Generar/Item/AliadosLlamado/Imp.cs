using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item.AliadosLlamado
{
    public class Imp: ILLamados
    {
        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource Get_Source { get { return _bs; } }
        public List<data> GetLista { get { return _bl.ToList(); } }
        public data ItemActual { get { return (data)_bs.Current; } }
        public decimal Get_Importe { get { return _bl.Sum(s => s.Importe); } }


        public Imp()
        {
            _lst = new List<data>();
            _bl = new BindingList<data>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }

        public void Inicializa()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void Agregar(data data)
        {
            _bl.Add(data);
            _bs.CurrencyManager.Refresh();
        }
        public void setListaAliadosLlamados(List<data> lst)
        {
            _bl.Clear();
            foreach (var rg in lst)
            {
                _bl.Add(rg);
            }
        }
        public void Eliminar(data item)
        {
            _bl.Remove(item);
            _bs.CurrencyManager.Refresh();
        }
    }
}