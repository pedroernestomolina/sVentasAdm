using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.Pendiente
{
    
    public class Gestion: IPendiente
    {

        private List<data> _list;
        private BindingSource _bs;
        private bool _seleccionarItemIsOk;
        private int _idItemSeleccionado;


        public BindingSource Source { get { return _bs; } }
        public int CntDoc { get { return _bs.Count; } }
        public bool ItemSeleccionadoIsOk { get { return _seleccionarItemIsOk; } }
        public int IdItemSeleccionado { get { return _idItemSeleccionado; } }


        public Gestion()
        {
            _idItemSeleccionado = -1;
            _seleccionarItemIsOk = false;
            _list = new List<data>();
            _bs = new BindingSource();
            _bs.DataSource = _list;
        }


        public void Inicializa() 
        {
            _idItemSeleccionado = -1;
            _seleccionarItemIsOk = false;
            _list.Clear();
            _bs.CurrencyManager.Refresh();
        }

        PendienteFrm _frm;
        public void Inicia() 
        {
            if (CargarData()) 
            {
                if (_frm == null) 
                {
                    _frm = new PendienteFrm();
                    _frm.setControlador(this);
                }
                _frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return true;
        }

        public void setData(List<OOB.Venta.Temporal.Pendiente.Lista.Ficha> list)
        {
            _list.Clear();
            foreach (var rg in list)
            {
                _list.Add(new data(rg));
            }
            _bs.CurrencyManager.Refresh();
        }

        public void SeleccionarItem()
        {
            _seleccionarItemIsOk = false;
            if (_bs.Current != null) 
            {
                var it= (data)_bs.Current ;
                var xmsg = "Deseas Abrir Este Documento ?";
                var rsp = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rsp == DialogResult.Yes) 
                {
                    _seleccionarItemIsOk = true;
                    _idItemSeleccionado = it.Id;
                }
            }
        }

    }

}