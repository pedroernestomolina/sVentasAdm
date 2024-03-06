using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaClientes.Vista
{
    public interface IVista: SrcComun.Documento.ListaCliente.Vista.IVista 
    {
        bool ItemSeleccionadoIsOk { get; }
        Idata Get_ItemSeleccionado { get; }
        //
        void setObjectoFiltrar(object data);
        void SeleccionarItem();
    }
}