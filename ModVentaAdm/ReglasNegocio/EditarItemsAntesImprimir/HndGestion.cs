using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir
{
    public class HndGestion: IHnd
    {
        private Utils.Control.Boton.Abandonar.IAbandonar _btAbandonar;
        private Utils.Control.Boton.Procesar.IProcesar _btProcesar;
        private List<OOB.Transporte.Documento.Entidad.Venta.FichaDetalle> _items;
        private List<OOB.Transporte.Documento.Entidad.Venta.DetDoc> _itemsDoc;
        private Items.ILista _hndLista;
        //
        public Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get { return _btAbandonar; } }
        public Utils.Control.Boton.Procesar.IProcesar BtProcesar { get { return _btProcesar; } }
        public BindingSource DataSource { get { return _hndLista.DataSource; } }
        //
        public HndGestion() 
        {
            _items = new List<OOB.Transporte.Documento.Entidad.Venta.FichaDetalle>();
            _itemsDoc = new List<OOB.Transporte.Documento.Entidad.Venta.DetDoc>();
            _btAbandonar = new Utils.Control.Boton.Abandonar.Imp();
            _btProcesar = new Utils.Control.Boton.Procesar.Imp();
            _hndLista = new Items.HndLista();
        }
        public void Inicializa()
        {
            _btAbandonar.Inicializa();
            _btProcesar.Inicializa();
            _hndLista.Inicializa();
            _items.Clear();
            _itemsDoc.Clear();
        }
        Frm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        //
        private bool CargarData()
        {
            return true;
        }
        private int _modo=-1;
        public void setItems(object items, int modo)
        {
            _modo = modo;
            if (modo == 1) 
            {
                _items = (List<OOB.Transporte.Documento.Entidad.Venta.FichaDetalle>)items;
                _hndLista.setItems(_items);
            }
            else if (modo == 2)
            {
                _itemsDoc = (List<OOB.Transporte.Documento.Entidad.Venta.DetDoc>)items;
                _hndLista.setItemsDoc(_itemsDoc);
            }
        }

        private Editar.IEditar _hndEditar;
        public void EditarItem()
        {
            if (_hndLista.ItemActual != null) 
            {
                var item = (Items.data)_hndLista.ItemActual;
                if (_hndEditar==null)
                {
                    _hndEditar = new Editar.HndEditar();
                }
                _hndEditar.Inicializa();
                _hndEditar.setTitulo("EDITAR: ( Item Documento )");
                _hndEditar.setItemDescripcion(item.Descripcion);
                _hndEditar.Inicia();
                if (_hndEditar.EdicionIsOk) 
                {
                    ((Items.data)_hndLista.ItemActual).setActualizaDescripcion(_hndEditar.GetDetalle, _modo);
                }
            }
        }
    }
}