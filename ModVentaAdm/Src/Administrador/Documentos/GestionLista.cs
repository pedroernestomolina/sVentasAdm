using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Administrador.Documentos
{
    
    public class GestionLista: IGestionListaDetalle
    {


        private List<data> _list;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource ItemsSource { get { return _bs; } }
        public string ItemsEncontrados { get { return "Items Encontrados: "+_bl.Count.ToString("n0").Trim(); } }
        public List<data> GetListaDoc { get { return _bl.ToList(); } }
        public data GetItemActual { get { return (data)_bs.Current; } }


        public GestionLista()
        {
            _list = new List<data>();
            _bl = new BindingList<data>(_list);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
        }

        public void Inicializa()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();

        }

        public void LimpiarData()
        {
            if (_bl.Count > 0)
            {
                var msg = MessageBox.Show("Desechar Vista Actual ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (msg == DialogResult.Yes)
                {
                    _bl.Clear();
                    _bs.CurrencyManager.Refresh();
                }
            }
        }

        public void setLista(List<OOB.Documento.Lista.Ficha> list)
        {
            _bl.Clear();
            foreach (var doc in list.OrderByDescending(o=>o.FechaEmision).ThenByDescending(o=>o.DocNombre).ThenByDescending(o=>o.DocNumero).ToList())
            {
                _bl.Add(new data(doc));
            }
        }

    }

}