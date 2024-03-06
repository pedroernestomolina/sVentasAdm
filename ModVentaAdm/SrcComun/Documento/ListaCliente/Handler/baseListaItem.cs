using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.ListaCliente.Handler
{
    abstract public class baseListaItem: Lista.baseLista, Vista.IListaItem
    {
        abstract public IEnumerable<object> Items { get; set; }
        //
        public baseListaItem()
            :base()
        {
        }
        abstract public override void setDataCargar(IEnumerable<object> list);
    }
}