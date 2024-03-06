using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.ListaCliente.Vista
{
    public interface IListaItem: Lista.ILista
    {
        IEnumerable<Object> Items { get; set; }
    }
}