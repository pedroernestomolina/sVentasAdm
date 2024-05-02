using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Handler
{
    public class ImpItems: Vistas.Iitems
    {
        private List<Vistas.Idata> _lst;
        private BindingSource _bs;
        //
        public BindingSource Get_Source { get { return _bs; } }
        public int Get_CntItems { get { return _bs.Count; } }
        public object Get_ItemActual { get { return _bs.Current; } }
        //
        public ImpItems()
        {
            _lst = new List<Vistas.Idata>();
            _bs = new BindingSource();
            _bs.DataSource = _lst;
            _bs.CurrencyManager.Refresh();
        }
        public void Inicializa()
        {
            _lst.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void setAliadosInv(object aliadosInv)
        {
            if (aliadosInv is List<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item>)
            { 
                var _lst = (List<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item>) aliadosInv;
                cargarListaAliados(_lst);
            }
        }
        //
        private void cargarListaAliados(List<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item> lst)
        {
            _lst.Clear();
            foreach (var r in lst)
            {
                var it = new Impdata(r);
                _lst.Add(it);
            }
            _bs.CurrencyManager.Refresh();
        }
        public void EliminarAliadoInv(object it)
        {
            if (it != null)
            {
                var _it = (Vistas.Idata)it;
                _lst.Remove(_it);
                _bs.CurrencyManager.Refresh();
            }
        }
    }
}