using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.ListaCliente.Handler
{
    abstract public class baseVista: Vista.IVista
    {
        protected Utils.Control.Boton.Salir.ISalir _btSalida;
        protected Vista.IListaItem _listaItem;
        //
        public Utils.Control.Boton.Salir.ISalir BtSalida { get { return _btSalida; } }
        public Vista.IListaItem ListaItem { get { return _listaItem; } }
        //
        abstract public void Inicializa();
        abstract public void Inicia();
    }
}