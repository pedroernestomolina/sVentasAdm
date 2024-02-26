using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaDoc.Vista
{
    public interface IVista: SrcComun.Documento.ListaDoc.Vista.IVista
    {
        bool ItemSeleccionadoIsOk { get; }
        Idata Get_ItemSeleccionado { get; }
        //
        void setCargarDocFiltradoByCliente(object cliente);
        void SeleccionarDoc();
    }
}