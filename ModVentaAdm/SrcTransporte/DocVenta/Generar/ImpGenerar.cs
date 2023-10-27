using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar
{
    abstract public class ImpGenerar: IGenerar
    {
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;
        private data _dataGen;
        private List<OOB.Sistema.Fiscal.Entidad.Ficha> _tasasFiscal;
        private Remision.IRemision _remision;
        private decimal _factorDivisa;
        private decimal _tasaDivisa;
        private string _notasDelDoc;
        protected bool _tipoDocIsFactura;


        public BindingSource SourceItems_Get { get { return _dataGen.Items.Source_Get; } }
        public data Ficha { get { return _dataGen; } }
        public Remision.IRemision Remision { get { return _remision; } }


        public ImpGenerar()
        {
            _remision = new Remision.Imp();
            _limpiarDocumentoIsOK = false;
            _editarDocumentoIsOK = false;
            _notasObservaciones = "";
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _dataGen = new data();
            _tasasFiscal = null;
            _factorDivisa=0m;
            _tasaDivisa = 0m;
            _notasDelDoc = "";
        }


        public void Inicializa()
        {
            _limpiarDocumentoIsOK = false;
            _editarDocumentoIsOK = false;
            _notasObservaciones = "";
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _dataGen.Inicializa();
            _tasasFiscal = null;
            _remision.Inicializa();
            _tasaDivisa = _factorDivisa;
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
        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar() 
        {
            _procesarIsOK = false;
            if (_dataGen.DataIsOk())
            {
                var r = Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    GuardarDoc();
                    if (_procesarIsOK) 
                    {
                        Ficha.LimpiarTodo();
                        _remision.Limpiar();
                        _notasObservaciones = "";
                        _limpiarDocumentoIsOK = true;
                    }
                }
            }
        }

        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }

        private bool CargarData()
        {
            try
            {
                var r01 = Sistema.MyData.Configuracion_FactorDivisa();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                var r02 = Sistema.MyData.Sistema_TasaFiscal_GetLista();
                if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r02.Mensaje);
                }
                var r03 = Sistema.MyData.TransporteCnf_NotasFactura_Get ();
                //
                _notasDelDoc = r03.Entidad;
                setNotas(r03.Entidad);
                _tasasFiscal = r02.ListaD;
                _dataGen.setTasaDivisa(r01.Entidad);
                _factorDivisa = r01.Entidad;
                _tasaDivisa = r01.Entidad;
                _dataGen.Items.setTasaDivisa(r01.Entidad);
                _dataGen.Totales.setTasaDivisa(r01.Entidad);
                _dataGen.setTasaFiscal(r02.ListaD);
                _dataGen.Totales.setTasaFiscal(r02.ListaD);
                _remision.CargarData();
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }


        public bool RemisionIsOK { get { return _remision.RemisionIsOK; } }
        public void BuscarRemision()
        {
            if (!_dataGen.DocumentoIsOk)
            {
                Helpers.Msg.Alerta("DEBES PRIMERO CREAR UN NUEVO DOCUMENTO");
                return;
            }
            _remision.setClienteBuscar(_dataGen.DatosDoc.Cliente);
            _remision.setHabilitarCargarDocRemision(_dataGen.Items.GetItems.Count == 0);
            _remision.Buscar();
        }


        private Presupuesto.Generar.DatosDocumento.IDatosDoc _datosDoc;
        public void NuevoDocumento()
        {
            if (_datosDoc == null)
            {
                _datosDoc = new Presupuesto.Generar.DatosDocumento.Imp(null);
                _datosDoc.setEscucha(_dataGen.Items);
            }
            if (_dataGen.DatosDoc != null)
            {
                Helpers.Msg.Alerta("DOCUMENTO YA CREADO");
                return;
            }
            _datosDoc.Inicializa();
            _datosDoc.Inicia();
            if (_datosDoc.ProcesarIsOK)
            {
                _dataGen.setDatosDoc(_datosDoc.Data);
            }
        }


        private Item.Agregar.IAgregar _itemAgregar;
        public void AgregarItem()
        {
            if (!_dataGen.DocumentoIsOk)
            {
                Helpers.Msg.Alerta("DEBES PRIMERO CREAR UN NUEVO DOCUMENTO");
                return;
            }
            _itemAgregar = new Item.Agregar.Agregar();
            _itemAgregar.Inicializa();
            _itemAgregar.setTasaFiscal(_tasasFiscal);
            _itemAgregar.setCliente(_dataGen.DatosDoc.Cliente.id);
            _itemAgregar.setSolicitadoPor(_dataGen.DatosDoc.SolicitadoPor_Get);
            _itemAgregar.setModuloCargar(_dataGen.DatosDoc.ModuloCargar_Get);
            _itemAgregar.setTipoDocumentoIsFactura(_tipoDocIsFactura);
            _itemAgregar.Inicia();
            if (_itemAgregar.ProcesarIsOK)
            {
                if (_dataGen.Items.VerificarAlAgregarItem(_itemAgregar))
                {
                    _dataGen.Items.AgregarItem(_itemAgregar);
                    Helpers.Msg.AgregarOk();
                }
            }
        }
        public void EliminarItem()
        {
            if (_dataGen.Items.ItemActual != null)
            {
                var _msg = "Estas Seguro de querer eliminar este Item ?";
                var r = MessageBox.Show(_msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (r == DialogResult.Yes)
                {
                    var _item = _dataGen.Items.ItemActual;
                    _dataGen.Items.EliminarItem(_item);
                    Helpers.Msg.EliminarOk();
                }
            }
        }
        private Item.Editar.IEditar _itemEditar;
        public void EditarItem()
        {
            if (_dataGen.Items.ItemActual != null)
            {
                var _itemViejo = _dataGen.Items.ItemActual;
                _itemEditar = new Item.Editar.Editar();
                _itemEditar.Inicializa();
                _itemEditar.setTasaFiscal(_tasasFiscal);
                _itemEditar.setCliente(_dataGen.DatosDoc.Cliente.id);
                _itemEditar.setItemEditar(_dataGen.Items.ItemActual.Item);
                _itemEditar.setTipoDocumentoIsFactura(_tipoDocIsFactura);
                _itemEditar.Inicia();
                if (_itemEditar.ProcesarIsOK)
                {
                    _dataGen.Items.EliminarItem(_itemViejo);
                    _dataGen.Items.AgregarItem(_itemEditar);
                    Helpers.Msg.EditarOk();
                }
            }
        }

        private string _notasObservaciones;
        public string NotasObserv_Get { get { return _notasObservaciones; } }
        public void setNotas(string desc)
        {
            _notasObservaciones = desc;
        }


        private bool _limpiarDocumentoIsOK;
        public bool LimpiarDocumentoIsOK { get { return _limpiarDocumentoIsOK; } }
        public void LimpiarDocumento()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _limpiarDocumentoIsOK = false;
            var xmsg = "Quieres Limpiar todo el Documento Actual ?";
            var r = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                _remision.Limpiar();
                _dataGen.LimpiarTodo();
                _notasObservaciones = "";
                _limpiarDocumentoIsOK = true;
                _tasaDivisa = _factorDivisa;
                _dataGen.setTasaDivisa(_tasaDivisa);
                setNotas(_notasDelDoc);
            }
        }

        private bool _editarDocumentoIsOK;
        public bool EditarDocumentoIsOK { get { return _editarDocumentoIsOK; } }
        public void EditarDocumento()
        {
            _editarDocumentoIsOK = false;
            if (_dataGen.DatosDoc == null)
            {
                Helpers.Msg.Alerta("DEBES PRIMERO CREAR UN NUEVO DOCUMENTO");
                return;
            }
            _datosDoc.Inicia();
            if (_datosDoc.ProcesarIsOK)
            {
                _dataGen.setDatosDoc(_datosDoc.Data);
                _editarDocumentoIsOK = true;
            }
        }


        public void IniciarEnLimpio()
        {
            _dataGen.LimpiarTodo();
            _remision.Limpiar();
            _notasObservaciones = "";
            _abandonarIsOK = false;
            _procesarIsOK = false;
            _limpiarDocumentoIsOK = false;
            _editarDocumentoIsOK = false;
            _tasaDivisa = _factorDivisa;
            _dataGen.setTasaDivisa(_tasaDivisa);
            setNotas(_notasDelDoc);
        }


        abstract public string TipoDocumento_Get { get; }
        abstract protected void GuardarDoc();


        TasaDivisa.ITasa _gDivisa;
        public void EditarFactorDivisa()
        {
            if (_gDivisa == null) 
            {
                _gDivisa = new TasaDivisa.Imp();
            }
            _gDivisa.Inicializa();
            _gDivisa.setTasaDivisa(_tasaDivisa);
            _gDivisa.Inicia();
            if (_gDivisa.ProcesarIsOK) 
            {
                _dataGen.setTasaDivisa(_gDivisa.TasaActual_Get);
                _tasaDivisa=_gDivisa.TasaActual_Get;
            }
        }
    }
}