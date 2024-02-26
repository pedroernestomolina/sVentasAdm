using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.ListaDoc.Handler
{
    abstract public class baseVista: Vista.IVista
    {
        protected Utils.Control.Boton.Salir.ISalir _btSalida;
        protected Vista.IListaDoc _listaDoc;
        //
        public Utils.Control.Boton.Salir.ISalir BtSalida { get { return _btSalida; } }
        public Vista.IListaDoc ListaDoc { get { return _listaDoc; } }
        //
        abstract public void Inicializa();
        abstract public void Inicia();
    }
}