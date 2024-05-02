using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar
{
    abstract public class ImpGenerar : IGenerar
    {
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;
        private data _dataGen;
        private List<OOB.Sistema.Fiscal.Entidad.Ficha> _tasasFiscal;
        private Remision.IRemision _remision;
        private decimal _factorDivisa;
        private string _notasDelDoc;
        protected bool _tipoDocIsFactura;
        private string _notasPeriodoLapso;


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
            _factorDivisa = 0m;
            _notasDelDoc = "";
            _notasPeriodoLapso = "";
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
            _notasPeriodoLapso = "";
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
                        //
                        setNotas(_notasDelDoc);
                        _dataGen.setTasaDivisa(_factorDivisa);
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
                var r03 = Sistema.MyData.TransporteCnf_NotasFactura_Get();
                //
                _notasDelDoc = r03.Entidad;
                setNotas(r03.Entidad);
                _tasasFiscal = r02.ListaD;
                _dataGen.setTasaDivisa(r01.Entidad);
                _factorDivisa = r01.Entidad;
                _dataGen.Items.setTasaDivisa(r01.Entidad);
                _dataGen.Totales.setTasaDivisa(r01.Entidad);
                _dataGen.setTasaFiscal(r02.ListaD);
                _dataGen.Totales.setTasaFiscal(r02.ListaD);
                cargarDataRemision();
                //_remision.CargarData();
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        abstract public void cargarDataRemision();


        public bool RemisionIsOK { get { return _remision.RemisionIsOK; } }
        public void BuscarRemision()
        {
            if (!_dataGen.DocumentoIsOk)
            {
                Helpers.Msg.Alerta("DEBES PRIMERO CREAR UN NUEVO DOCUMENTO");
                return;
            }
            if (_dataGen.Items.Cnt_Get > 0) 
            {
                Helpers.Msg.Alerta("NO DEBEN HABER ITEMS CARGADOS");
                return;
            }
            _remision.setClienteBuscar(_dataGen.DatosDoc.Cliente);
            _remision.setHabilitarCargarDocRemision(_dataGen.Items.GetItems.Count == 0);
            _remision.Buscar();
            if (_remision.RemisionIsOK)
            {
                try
                {
                    var idDoc = _remision.Get_IdDocSeleccionado;
                    var r01 = Sistema.MyData.TransporteDocumento_EntidadVenta_GetById(idDoc);
                    InyectarItemsLista(r01.Entidad);
                }
                catch (Exception e)
                {
                    Helpers.Msg.Error(e.Message);
                }
            }
        }

        private void InyectarItemsLista(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha)
        {
            var tipoProcedencia = "";
            foreach (var it in ficha.detalles)
            {
                tipoProcedencia = it.tipoProcedenciaItem;
                _itemAgregar = new Item.Agregar.Agregar();
                _itemAgregar.Inicializa();
                _itemAgregar.setTasaFiscal(_tasasFiscal);
                _itemAgregar.setCliente(_dataGen.DatosDoc.Cliente.id);
                _itemAgregar.setSolicitadoPor(_dataGen.DatosDoc.SolicitadoPor_Get);
                _itemAgregar.setModuloCargar(_dataGen.DatosDoc.ModuloCargar_Get);
                _itemAgregar.setTipoDocumentoIsFactura(_tipoDocIsFactura);
                //
                _itemAgregar.Item.setDescripcion(it.detalle);
                _itemAgregar.Item.setCnt(it.cntDias);
                _itemAgregar.Item.setPrecioDivisa(it.precioNetoMonDivisa);
                _itemAgregar.Item.setDscto(it.dsctoPorc);
                _itemAgregar.AlicuotaSetFichaById(it.alicuotaId);
                if (tipoProcedencia == "")
                {
                    _itemAgregar.Item.setItemPresupuesto(null);
                    _itemAgregar.Item.setItemServicio(null);
                    _itemAgregar.Item.setItemHojaServicio(null);
                    _dataGen.Items.AgregarItem(_itemAgregar);
                }
                else if (tipoProcedencia == "P")
                {
                    _itemAgregar.Item.setItemServicio(null);
                    _itemAgregar.Item.setItemHojaServicio(null);
                    var filtro = new OOB.Transporte.Documento.Remision.Lista.Filtro()
                    {
                        codTipoDoc = "",
                        esPorRemision = false,
                        idCliente = "",
                        idDocumento = it.idDocRef,
                    };
                    var r01 = Sistema.MyData.TransporteDocumento_Remision_ListaBy(filtro);
                    if (r01.cntRegistro == 1)
                    {
                        var _doc = new Utils.DocLista.Remision.data(r01.ListaD[0]);
                        var _docNumero = _doc.DocNumero;
                        var _desc = "PRESUPUESTO #" + _docNumero + Environment.NewLine + _doc.SolicitadoPor + Environment.NewLine + _doc.ModuloCargar;
                        var _precio = _doc.Monto;
                        _itemAgregar.Item.setItemPresupuesto(_doc);
                    }
                    _dataGen.Items.AgregarItem(_itemAgregar);
                }
            }
            foreach (var it in ficha.detalles)
            {
                if (it.tipoProcedenciaItem == "S")
                {
                    _itemAgregar = new Item.Agregar.Agregar();
                    _itemAgregar.Inicializa();
                    _itemAgregar.setTasaFiscal(_tasasFiscal);
                    _itemAgregar.setCliente(_dataGen.DatosDoc.Cliente.id);
                    _itemAgregar.setSolicitadoPor(_dataGen.DatosDoc.SolicitadoPor_Get);
                    _itemAgregar.setModuloCargar(_dataGen.DatosDoc.ModuloCargar_Get);
                    _itemAgregar.setTipoDocumentoIsFactura(_tipoDocIsFactura);
                    //
                    _itemAgregar.Item.setDescripcion(it.detalle);
                    _itemAgregar.Item.setCnt(it.cntDias);
                    _itemAgregar.Item.setPrecioDivisa(it.precioNetoMonDivisa);
                    _itemAgregar.Item.setDscto(it.dsctoPorc);
                    _itemAgregar.AlicuotaSetFichaById(it.alicuotaId);
                    //
                    var serv = ficha.detTurnos.FirstOrDefault(f => f.idItem == it.idItemServicio);
                    if (serv != null)
                    {
                        var _itServ = new Presupuesto.Generar.Item.Agregar.Agregar();
                        _itServ.Inicializa();
                        _itServ.setTasaFiscal(_tasasFiscal);
                        _itServ.setValidarDatosCompletos(false);
                        //
                        var _alicuota = new Presupuesto.Generar.alicuota()
                        {
                            codigo = "",
                            desc = it.alicuotaDesc,
                            id = it.alicuotaId,
                            tasa = it.alicuotaTasa,
                        };
                        var _tipoServicio = new OOB.Transporte.ServPrest.Entidad.Ficha()
                        {
                            detalle = serv.servDet,
                            descripcion = serv.servDesc,
                            codigo = serv.servCod,
                            id = serv.servId,
                        };

                        _itServ.Item.setSolicitadoPor(_dataGen.DatosDoc.SolicitadoPor_Get);
                        _itServ.Item.setModuloaCargar(_dataGen.DatosDoc.ModuloCargar_Get);
                        _itServ.Item.setCntDias(serv.cntDias);
                        _itServ.Item.setCntUnidades(serv.cntVehic);
                        _itServ.Item.setPrecioDivisa(serv.pnetoDiv);
                        _itServ.Item.setDscto(it.dsctoPorc);
                        _itServ.Item.setAlicuota(_alicuota);
                        _itServ.Item.setUnidadesDetalle(serv.descVehic);
                        _itServ.Item.setTipoServicio(_tipoServicio);
                        _itServ.Item.setDescripcion(serv.servDet);
                        _itServ.Item.setDescripcionFull(serv.notas);

                        foreach (var xr in ficha.aliados)
                        {
                            if (serv.idItem == xr.idItem)
                            {
                                var _aliado = new OOB.Transporte.Aliado.Entidad.Ficha()
                                {
                                    id = xr.idAliado,
                                    ciRif = xr.ciRifAliado,
                                    codigo = xr.codigoAliado,
                                    nombreRazonSocial = xr.nombreAliado,
                                };
                                _itServ.Item.setAliado(_aliado);
                                _itServ.Item.setPrecioAliadoPautado(xr.pNeto);
                                _itServ.Item.setCntAliadoPautado((int)xr.cantDias);
                                _itServ.Item.GuardarAliado();
                            }
                        }

                        foreach (var xr in ficha.fechas)
                        {
                            if (xr.idItem == serv.idItem)
                            {
                                _itServ.Item.setFecha(xr.fecha);
                                _itServ.Item.setHora(xr.fecha);
                                _itServ.Item.AgregarFecha();
                            }
                        }
                        _itemAgregar.Item.setItemPresupuesto(null);
                        _itemAgregar.Item.setItemServicio(_itServ);
                        _itemAgregar.Item.setItemHojaServicio(null);
                    }
                    _dataGen.Items.AgregarItem(_itemAgregar);
                }
            }
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
                _notasPeriodoLapso = "";
                //
                setNotas(_notasDelDoc);
                _dataGen.setTasaDivisa(_factorDivisa);
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
            //
            _dataGen.setTasaDivisa(_factorDivisa);
            setNotas(_notasDelDoc);
        }


        TasaDivisa.ITasa _gDivisa;
        public void EditarFactorDivisa()
        {
            var _tasaDivisa = _dataGen.TasaDivisa_Get;
            if (_gDivisa == null)
            {
                _gDivisa = new TasaDivisa.Imp();
            }
            _gDivisa.Inicializa();
            _gDivisa.setTexto("Tasa Divisa Actual ?");
            _gDivisa.setTasaDivisa(_tasaDivisa);
            _gDivisa.Inicia();
            if (_gDivisa.ProcesarIsOK)
            {
                _dataGen.setTasaDivisa(_gDivisa.TasaActual_Get);
            }
        }


        //CLASES ABSTRACTAS
        abstract public string TipoDocumento_Get { get; }
        abstract protected void GuardarDoc();
        abstract public void ActivarIGTF();
        abstract public void ActivarISLR();
        abstract public void LimpiarTasa_ISLR();
        abstract public void LimpiarTasa_IGTF();
        abstract public void DocumentoNumeroGenerar();
        abstract public bool DocumentoNumeroGenerarIsOk { get; }


        private NotasPeriodo.Vista.INotas _notasPeriodo;
        public NotasPeriodo.Vista.INotas NotasPeriodo { get { return _notasPeriodo; } }
        public void PeriodoLapso()
        {
            if (_notasPeriodo == null)
            {
                _notasPeriodo = new NotasPeriodo.Handler.Imp();
            }
            _notasPeriodo.Inicializa();
            _notasPeriodo.setNotas(_notasPeriodoLapso);
            _notasPeriodo.Inicia();
            if (_notasPeriodo.ProcesarIsOK)
            {
                _notasPeriodoLapso = _notasPeriodo.Notas_Get;
            }
        }
    }
}