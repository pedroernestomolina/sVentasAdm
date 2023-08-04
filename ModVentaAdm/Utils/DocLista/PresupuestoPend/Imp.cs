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
        private ILista _items;
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
            var rt = true;
            return rt;
        }


        //public void setDataCargar(IEnumerable<object> lst)
        //{
        //    //var _lst = new List<data>();
        //    //foreach (var rg in lst) 
        //    //{
        //    //    var nr = new data((OOB.Transporte.Documento.Remision.Lista.Ficha)rg);
        //    //    _lst.Add(nr);
        //    //}
        //    //_items.setDataCargar(_lst);
        //}


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