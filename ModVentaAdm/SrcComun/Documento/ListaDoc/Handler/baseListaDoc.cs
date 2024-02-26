using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.ListaDoc.Handler
{
    abstract public class baseListaDoc: Lista.baseLista, Vista.IListaDoc
    {
        abstract public IEnumerable<object> Items { get; set; }
        //
        public baseListaDoc()
            :base()
        {
        }
        abstract public override void setDataCargar(IEnumerable<object> list);
    }
}