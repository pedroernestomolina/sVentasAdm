using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcComun.Lista
{
    abstract public class baseLista: ILista
    {
        private BindingSource _bs;
        //
        public BindingSource Get_Source { get { return _bs; } }
        public object ItemActual { get { return _bs.Current; } }
        public int Cnt { get { return _bs.Count; } }
        //
        public baseLista()
        {
            _bs = new BindingSource();
        }
        virtual public void Inicializa()
        {
            _bs.Clear();
        }
        abstract public void setDataCargar(IEnumerable<object> list);
    }
}