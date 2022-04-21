using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Producto.Lista
{

    public class Gestion
    {

        private List<data> _lst;
        private BindingSource _bs;
        private data _itemSeleccionado;
        private string _aBuscar;


        public string ItemsEncontrados { get { return string.Format("{0}", _bs.Count); } }
        public BindingSource Source { get { return _bs; } }
        public data ItemSeleccionado { get { return _itemSeleccionado; } }
        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionado!=null; } }


        public Gestion()
        {
            _aBuscar = "";
            _itemSeleccionado = null;
            _lst = new List<data>();
            _bs = new BindingSource();
            _bs.DataSource = _lst;
        }

        private void setLista(List<OOB.Producto.ListaResumen.Ficha> list)
        {
            _lst.Clear();
            foreach (var rg in list.OrderBy(o => o.Nombre).ToList())
            {
                _lst.Add(new data(rg.Id, rg.Nombre, rg.Estatus));
            }
            _bs.CurrencyManager.Refresh();
        }

        ListaFrm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new ListaFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var rt= true;

            if (_aBuscar != "")
            {
                var filt = new OOB.Producto.ListaResumen.Filtro()
                {
                    Cadena = _aBuscar,
                    MetodoBusqueda = OOB.Producto.ListaResumen.Enumerados.EnumMetodoBusqueda.PorDescripcion,
                };
                var r01 = Sistema.MyData.Producto_GetListaResumen(filt);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return false;
                }
                setLista(r01.ListaD);
            }

            return rt;
        }

        public void SeleccionarItem()
        {
            var it = (data)_bs.Current;
            if (it != null)
            {
                if (!it.isActivo)
                    return;
                _itemSeleccionado = it;
            }
        }

        public void Cerrar()
        {
            frm.Close();
        }

        public void Inicializa()
        {
            _aBuscar = "";
            _itemSeleccionado = null;
            _lst.Clear();
            _bs.CurrencyManager.Refresh();
        }

        public void setBuscar(string aBuscar)
        {
            this._aBuscar= aBuscar;
        }

    }

}