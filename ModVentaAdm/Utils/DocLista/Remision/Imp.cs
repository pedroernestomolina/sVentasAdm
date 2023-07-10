using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.DocLista.Remision
{
    public class Imp: IRemision
    {
        private ILista _items;


        public ILista Items { get { return _items; } }


        public Imp()
        {
            _abandonarIsOK = false;
            _items = new ImpLista();
        }


        public void Inicializa()
        {
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


        public void setDataCargar(IEnumerable<object> lst)
        {
            var _lst = new List<data>();
            foreach (var rg in lst) 
            {
                var nr = new data();
                _lst.Add(nr);
            }
            _items.setDataCargar(_lst);
        }


        public bool ItemSeleccionadoIsOk { get { return false; } }
        public void SeleccionarItem()
        {
            if (_items.ItemActual == null) 
            {
                return;
            }
        }

        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = true;
        }
    }
}