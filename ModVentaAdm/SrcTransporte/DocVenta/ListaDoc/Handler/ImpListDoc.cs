using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaDoc.Handler
{
    public class ImpListDoc : SrcComun.Documento.ListaDoc.Handler.baseListaDoc
    {
        public override IEnumerable<object> Items { get; set; }
        //
        public ImpListDoc()
            : base()
        {
            Items = new List<Vista.Idata>();
            Get_Source.DataSource = Items;
            Get_Source.CurrencyManager.Refresh();
        }
        public override void setDataCargar(IEnumerable<object> list)
        {
            var _items= (List<Vista.Idata>)Items;
            _items.Clear();
            foreach (var rg in list)
            {
                var nr = new data((OOB.Documento.Lista.Ficha)rg);
                _items.Add(nr);
            }
            Get_Source.DataSource = _items;
            Get_Source.CurrencyManager.Refresh();
        }
    }
}