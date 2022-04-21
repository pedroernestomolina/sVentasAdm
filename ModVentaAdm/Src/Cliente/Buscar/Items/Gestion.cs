using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Buscar.Items
{
    
    public class Gestion
    {

        //public event EventHandler ItemChanged;

        
        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource ItemSource { get { return _bs; } }
        public int Cnt { get { return _bs.Count; } }
        public data Item 
        {
            get 
            {
                if (_bs.Current == null) 
                {
                    return new data();
                }
                return (data)_bs.Current; 
            } 
        }


        public Gestion()
        {
            _lst = new List<data>();
            _bl = new BindingList<data>(_lst);
            _bs = new BindingSource();
            //_bs.CurrentChanged +=_bs_CurrentChanged;   
            _bs.DataSource = _bl;
        }


        public void Inicializa()
        {
            LimpiarLista();
        }

        //private void _bs_CurrentChanged(object sender, EventArgs e)
        //{
        //    if (_bs.Current!= null)
        //    {
        //        _item = (data)_bs.Current;
        //        EventHandler hnd = ItemChanged;
        //        if (hnd != null)
        //        {
        //            hnd(this, null);
        //        }
        //    }
        //}

        public void setLista(List<OOB.Maestro.Cliente.Entidad.Ficha> list)
        {
            _lst.Clear();
            foreach (var it in list.OrderBy(o => o.razonSocial).ToList())
            {
                _lst.Add(new data(it));
            }
            _bs.CurrencyManager.Refresh();
        }

        public void LimpiarLista()
        {
            _lst.Clear();
            _bs.CurrencyManager.Refresh();
        }

        public void AgregarFicha(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _lst.Add(new data(ficha));
            _bs.CurrencyManager.Refresh();
        }

        public void ActualizarFicha(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            var it = _bl.FirstOrDefault(f => f.Id == ficha.id);
            if (it != null)
            {
                it.SetActualizarFicha(ficha);
                _bs.CurrencyManager.Refresh();
            }
        }

        public bool CargarData()
        {
            return true;
        }

    }

}