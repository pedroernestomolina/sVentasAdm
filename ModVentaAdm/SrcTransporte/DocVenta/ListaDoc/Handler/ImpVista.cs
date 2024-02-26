using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaDoc.Handler
{
    public class ImpVista: SrcComun.Documento.ListaDoc.Handler.baseVista, Vista.IVista
    {
        private string _cliente;
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
            _listaDoc = new ImpListDoc();
        }
        public override void Inicializa()
        {
            _itemSeleccionadoIsOk = false;
            _itemSeleccionado = null;
            BtSalida.Inicializa();
            ListaDoc.Inicializa();
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
        public void setCargarDocFiltradoByCliente(object cliente)
        {
            _cliente=(string)cliente;
        }
        public void SeleccionarDoc()
        {
            _itemSeleccionadoIsOk=false;
            _itemSeleccionado = (Vista.Idata)_listaDoc.ItemActual;
            if (_listaDoc.ItemActual == null) { return; }
            _itemSeleccionadoIsOk = true;
        }
        //
        private bool cargarDataIsOk()
        {
            try
            {
                if (_cliente == null || _cliente.Trim() == "") 
                {
                    throw new Exception("PROBLEMA AL CARGAR DOCUMENTOS");
                }
                var list = Sistema.Fabrica.DataDocumentos.ObtenerLista_DocumentosVenta_AplicanNotaCredito_FiltradoByCliente(_cliente);
                _listaDoc.setDataCargar(list);
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