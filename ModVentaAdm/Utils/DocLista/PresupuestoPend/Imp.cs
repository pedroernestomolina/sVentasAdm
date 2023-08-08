using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.DocLista.PresupuestoPend
{
    public class Imp: IPrespPend
    {
        private IPrespPendLista _items;
        private bool _itemSeleccionadoIsOk;


        public ILista Items { get { return _items; } }
        public object ItemSeleccionado { get { return _items.ItemActual; } }


        public Imp()
        {
            _itemSeleccionadoIsOk = false;
            _abandonarIsOK = false;
            _items = new ImpLista();
        }


        public void Inicializa()
        {
            _itemSeleccionadoIsOk = false;
            _items.Inicializa();
        }
        Frm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        private bool CargarData()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteDocumento_Presupuesto_Pendiente();
                var _lst = new List<data>();
                foreach (var rg in r01.ListaD)
                {
                    var nr = new data(rg);
                    _lst.Add(nr);
                }
                _items.setDataCargar(_lst);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }


        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionadoIsOk; } }
        public void SeleccionarItem()
        {
            _itemSeleccionadoIsOk = false;
            if (_items.ItemActual == null) 
            {
                return;
            }
            _itemSeleccionadoIsOk = true;
        }

        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = true;
        }
    }
}