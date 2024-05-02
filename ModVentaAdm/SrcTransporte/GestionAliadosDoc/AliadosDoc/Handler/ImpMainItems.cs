using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Handler
{
    public class ImpMainItems: Vistas.ImainItems
    {
        private Vistas.Iitems _items;
        //
        public BindingSource Get_Source { get { return _items.Get_Source; } }
        public object Get_ItemActual { get { return _items.Get_ItemActual; } }
        public int Get_CntItems { get { return _items.Get_CntItems; } }
        //
        public ImpMainItems()
        {
            _items = new ImpItems();
        }
        public void Inicializa()
        {
            _items.Inicializa();
        }
        public void setAliadosInv(object aliadosInv)
        {
            _items.setAliadosInv(aliadosInv);
        }
        public void EliminarAliadoInv(object it)
        {
            if (it != null)
            {
                _items.EliminarAliadoInv(it);
            }
        }
    }
}