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
        private BindingList<Idata> _bl;
        private BindingSource _bs;
        private string _textoFiltrar;
        private bool _mostratCtasEnCero;
        //
        public BindingSource CtasPendGetSource { get { return _bs; } }
        public List<Idata> ListaItems { get { return _bl.ToList(); } }
        public Idata ItemActual { get { return (Idata)_bs.Current; } }
        public decimal MontoPendientePorCobrar { get { return _bl.Sum(s => s.montoResta); } }
        //
        public Lista() 
        {
            _mostratCtasEnCero = false;
            _textoFiltrar = "";
            _bl= new BindingList<Idata>();
            _bs= new BindingSource();
            _bs.DataSource = _bl;
        }
        public void Inicializa()
        {
            _mostratCtasEnCero = false;
            _textoFiltrar = "";
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void setFiltrarPor(string textoFiltrar)
        {
            _textoFiltrar = textoFiltrar;
        }
        public void setFiltrarMostrarCtasEnCero(bool mostratCtasEnCero)
        {
            _mostratCtasEnCero = mostratCtasEnCero;
        }
        public void setData(IEnumerable<object> lst)
        {
            List<Idata> _src = new List<Idata>();
            foreach (var rg in lst)
            {
                var nr = new ListaCtasPend.data(rg);
                _src.Add(nr);
            }
            _src=_src.Where(w => w.nombreRazonSocial.Contains(_textoFiltrar)).ToList();
            if (!_mostratCtasEnCero) 
            {
                _src = _src.Where(w => w.montoImporte>0m).ToList();
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