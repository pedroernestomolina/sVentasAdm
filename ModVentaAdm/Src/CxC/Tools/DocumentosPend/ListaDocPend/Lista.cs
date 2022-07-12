using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.DocumentosPend.ListaDocPend
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
        public decimal MontoImporte { get { return _bl.Sum(s => s.montoImporte); } }
        public decimal MontoAcumulado { get { return _bl.Sum(s => s.montoAcumulado); } }
        public int CntItems { get { return _bl.Count; } }


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
        public void setListaDocPend(List<data> lst)
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