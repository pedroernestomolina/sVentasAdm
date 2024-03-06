using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaClientes.Handler
{
    public class ImpVista: SrcComun.Documento.ListaCliente.Handler.baseVista, Vista.IVista
    {
        private string _filtrarPor;
        private bool _itemSeleccionadoIsOk;
        private Vista.Idata _itemSeleccionado;
        //
        public Vista.Idata Get_ItemSeleccionado { get { return _itemSeleccionado; } }
        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionadoIsOk; } }
        //
        public ImpVista()
            :base()
        {
            _itemSeleccionadoIsOk = false;
            _itemSeleccionado = null;
            _btSalida = new Utils.Control.Boton.Salir.Imp();
            _listaItem = new ImpListItem();
        }
        public override void Inicializa()
        {
            _itemSeleccionadoIsOk = false;
            _itemSeleccionado = null;
            BtSalida.Inicializa();
            ListaItem.Inicializa();
        }
        Vista.Frm frm;
        public override void Inicia()
        {
            if (cargarDataIsOk()) 
            {
                if (frm == null)
                {
                    frm = new Vista.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setObjectoFiltrar(object data)
        {
            _filtrarPor = (string)data;
        }
        public void SeleccionarItem()
        {
            _itemSeleccionadoIsOk=false;
            _itemSeleccionado = (Vista.Idata)_listaItem.ItemActual;
            if (_listaItem.ItemActual == null) { return; }
            _itemSeleccionadoIsOk = true;
        }
        //
        private bool cargarDataIsOk()
        {
            try
            {
                if (_filtrarPor == null || _filtrarPor.Trim() == "") 
                {
                    throw new Exception("PROBLEMA AL CARGAR ITEMS");
                }
                var list = Sistema.Fabrica.DataDocumentos.ObtenerListaCliente_Resumen_FiltradoPor(_filtrarPor);
                _listaItem.setDataCargar(list);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
    }
}