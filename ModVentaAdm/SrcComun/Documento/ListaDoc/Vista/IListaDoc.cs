using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.ListaDoc.Vista
{
    public interface IListaDoc: Lista.ILista
    {
        IEnumerable<Object> Items { get; set; }
    }
}