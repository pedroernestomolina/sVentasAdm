using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaClientes.Handler
{
    public class ImpListItem : SrcComun.Documento.ListaCliente.Handler.baseListaItem 
    {
        public override IEnumerable<object> Items { get; set; }
        //
        public ImpListItem()
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
                var nr = new data((OOB.Maestro.Cliente.Lista.Ficha)rg);
                _items.Add(nr);
            }
            Get_Source.DataSource = _items;
            Get_Source.CurrencyManager.Refresh();
        }
    }
}