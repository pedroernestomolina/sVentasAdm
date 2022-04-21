using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Administrador
{

    public class GestionLista
    {

        public event EventHandler ItemChanged;


        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;
        private data _item;


        public BindingSource Source { get { return _bs; } }
        public int Items { get { return _bs.Count; } }
        public data Item { get { return _item; } }
        public string Cliente 
        { 
            get 
            {
                var rt = "";
                if (Item != null) 
                {
                    rt = Item.CiRif + Environment.NewLine + Item.NombreRazonSocial;
                }
                return rt;
            } 
        }


        public GestionLista()
        {
            _item = null;
            _lst = new List<data>();
            _bl = new BindingList<data>(_lst);
            _bs = new BindingSource();
            _bs.CurrentChanged +=_bs_CurrentChanged;   
            _bs.DataSource = _bl;
        }

        private void _bs_CurrentChanged(object sender, EventArgs e)
        {
            if (_bs.Current!= null)
            {
                _item = (data)_bs.Current;
                EventHandler hnd = ItemChanged;
                if (hnd != null)
                {
                    hnd(this, null);
                }
            }
        }

        public void setLista(List<OOB.Maestro.Cliente.Entidad.Ficha> list)
        {
            _item = null;
            _lst.Clear();
            foreach (var it in list.OrderBy(o => o.razonSocial).ToList())
            {
                _lst.Add(new data(it));
            }
            _bs.CurrencyManager.Refresh();
        }

        public void LimpiarLista()
        {
            _item = null;
            _lst.Clear();
            _bs.CurrencyManager.Refresh();
        }

        public void AgregarFicha(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _lst.Add(new data(ficha));
            _bs.CurrencyManager.Refresh();
        }

        //public void EliminarItem(string autoId)
        //{
        //    //var it = _lst.FirstOrDefault(f => f.autoId == autoId);
        //    //if (it != null) 
        //    //{
        //    //    _lst.Remove(it);
        //    //    _bs.CurrencyManager.Refresh();
        //    //}
        //}

        public void ActualizarFicha(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            var it = _bl.FirstOrDefault(f => f.Id == ficha.id);
            if (it != null)
            {
                it.SetActualizarFicha(ficha);
                _bs.CurrencyManager.Refresh();
            }
        }

    }

}