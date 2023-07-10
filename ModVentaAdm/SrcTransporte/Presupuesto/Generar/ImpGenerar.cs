﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class ImpGenerar: IGenerar 
    {
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;
        private data _dataGen;
        private List<OOB.Sistema.Fiscal.Entidad.Ficha> _tasasFiscal;
        private MargenGanancia.IBeneficio _margBeneficio;
        private Remision.IRemision _remision;


        public BindingSource SourceItems_Get { get { return _dataGen.Items.Source_Get; } }
        public data Ficha { get { return _dataGen; } }
        public Remision.IRemision Remision { get { return _remision; } }


        public ImpGenerar()
        {
            _limpiarDocumentoIsOK = false;
            _editarDocumentoIsOK = false;
            _notasObservaciones = "";
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _dataGen = new data();
            _tasasFiscal = null;
            _margBeneficio = new MargenGanancia.Imp(_dataGen.Items, _dataGen.Totales);
            _remision = new Remision.Imp();
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
                var r= Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    GuardarDoc();
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
                _tasasFiscal = r02.ListaD;
                _dataGen.setTasaDivisa(r01.Entidad);
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


        private DatosDocumento.IDatosDoc _datosDoc;
        public void NuevoDocumento()
        {
            if (_datosDoc==null)
            {
                _datosDoc = new DatosDocumento.Imp();
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
            _itemAgregar.Inicia();
            if (_itemAgregar.ProcesarIsOK) 
            {
                _dataGen.Items.AgregarItem(_itemAgregar);
                Helpers.Msg.AgregarOk();
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
                _itemEditar.setItemEditar(_dataGen.Items.ItemActual.Item);
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
            _limpiarDocumentoIsOK = false; 
            var xmsg = "Quieres Limpiar todo el Documento Actual ?";
            var r = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes) 
            {
                _remision.Limpiar();
                _dataGen.LimpiarTodo();
                _notasObservaciones = "";
                _limpiarDocumentoIsOK = true; 
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
        public void MostrarBeneficio()
        {
            if (_dataGen.DatosDoc == null)
            {
                Helpers.Msg.Alerta("DEBES PRIMERO CREAR UN NUEVO DOCUMENTO");
                return;
            }
            _margBeneficio.Inicializa();
            _margBeneficio.Inicia();
        }


        private void GuardarDoc()
        {
            try
            {
                var _idCliente = _dataGen.DatosDoc.Cliente.id;
                var _cirif= _dataGen.DatosDoc.Cliente.ciRif;
                var _codCliente = _dataGen.DatosDoc.Cliente.codigo;
                var _dirFiscalCliente = _dataGen.DatosDoc.Cliente.dirFiscal;
                var _razonSocial = _dataGen.DatosDoc.Cliente.razonSocial;
                var _telefonoCliente = _dataGen.DatosDoc.Cliente.telefono1;
                //
                var _idVendedor = "0000000001";
                var s01 = Sistema.MyData.Sistema_Vendedor_Entidad_GetById(_idVendedor);
                var _codVendedor = s01.Entidad.codigo;
                var _nombreVendedor = s01.Entidad.nombre;
                //
                var _idSitemaDocumento = "0000000005";
                var s02 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById(_idSitemaDocumento);
                if (s02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(s02.Mensaje);
                }
                var _moduloDocumento = s02.Entidad.descripcion;
                var _nombreDocumento = s02.Entidad.tipo;
                var _tipoDcumento = s02.Entidad.codigo;
                var _signoDocumento = s02.Entidad.signo;
                //
                var s03 = Sistema.MyData.Sistema_GetCodigoSucursal();
                if (s03.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(s03.Mensaje);
                }
                var _codSucursal = s03.Entidad;
                //
                var _idUsuario = Sistema.Usuario.id;
                var _codUsuario = Sistema.Usuario.codigo;
                var _nombreUsuario= Sistema.Usuario.nombre;
                //
                var _cntRenglones = _dataGen.Items.Cnt_Get;
                var _diasValidez= _dataGen.DatosDoc.DiasValidez_Get;
                var _diasCredito=_dataGen.DatosDoc.DiasCredito_Get;
                var _factorCambio = (decimal)_dataGen.TasaDivisa_Get;
                var _condPago = _dataGen.DatosDoc.CondPago_Get.Trim().ToUpper();
                var _estacion = Sistema.EquipoEstacion;
                //
                var _tasa1 = 0m;
                var _tasa2 = 0m;
                var _tasa3 = 0m;
                _tasa1 = _dataGen.TasasFiscal_1.tasa;
                _tasa2 = _dataGen.TasasFiscal_2.tasa;
                _tasa3 = _dataGen.TasasFiscal_3.tasa;
                //
                var _montoExento = _dataGen.Totales.MontoExento_Get;
                var _montoBase1 = _dataGen.Totales.MontoBase1_Get;
                var _montoBase2 = _dataGen.Totales.MontoBase2_Get;
                var _montoBase3 = _dataGen.Totales.MontoBase3_Get;
                var _montoIva1 = _dataGen.Totales.MontoIva1_Get;
                var _montoIva2 = _dataGen.Totales.MontoIva2_Get;
                var _montoIva3 = _dataGen.Totales.MontoIva3_Get;
                var _neto = _montoExento+_montoBase1+_montoBase2+_montoBase3;
                var _montoBase=_montoBase1+_montoBase2+_montoBase3;
                var _montoIva=_montoIva1+_montoIva2+_montoIva3;
                var _montoDivisa = _dataGen.Totales.MontoTotal_MonedaDivisa_Get;
                var _montoTotal = _dataGen.Totales.MontoTotal_MonedaActual_Get;
                //
                var _subTotalNeto = _dataGen.Totales.SubTotalNeto_Get; //ANTES DE LOS CARGOS /DESCEUNTOS
                var _subTotal = _montoTotal - _montoIva;
                var _subTotalImpuesto = _montoIva;
                //
                var fichaOOB = new OOB.Transporte.Documento.Agregar.Presupuesto.Ficha()
                {
                    cargos = 0m,
                    cargosp = 0m,
                    CiRif = _cirif,
                    cntRenglones = _cntRenglones,
                    codCliente = _codCliente,
                    codSucursal = _codSucursal,
                    codUsuario = _codUsuario,
                    codVendedor = _codVendedor,
                    condPago = _condPago,
                    control = "",
                    descuento1 = 0m,
                    descuento1p = 0m,
                    descuento2 = 0m,
                    descuento2p = 0m,
                    diasCredito = _diasCredito,
                    diasValidez = _diasValidez,
                    DirFiscal = _dirFiscalCliente,
                    docCodigo = _nombreDocumento,
                    docNombre = _moduloDocumento,
                    docRemision = "",
                    estacion = _estacion,
                    factorCambio = _factorCambio,
                    idCliente = _idCliente,
                    idRemision = "",
                    idUsuario = _idUsuario,
                    idVendedor = _idVendedor,
                    montoBase = _montoBase,
                    montoBase1 = _montoBase1,
                    montoBase2 = _montoBase2,
                    montoBase3 = _montoBase3,
                    montoDivisa = _montoDivisa,
                    montoExento = _montoExento,
                    montoImpuesto = _montoIva,
                    montoImpuesto1 = _montoIva1,
                    montoImpuesto2 = _montoIva2,
                    montoImpuesto3 = _montoIva3,
                    neto = _neto,
                    RazonSocial = _razonSocial,
                    signo = _signoDocumento,
                    subTotal = _subTotal,
                    subTotalImpuesto = _subTotalImpuesto,
                    subTotalNeto = _subTotalNeto,
                    Tasa1 = _tasa1,
                    Tasa2 = _tasa2,
                    Tasa3 = _tasa3,
                    telefono = _telefonoCliente,
                    TipoDoc = _tipoDcumento,
                    tipoRemision = "",
                    Total = _montoTotal,
                    usuario = _nombreUsuario,
                    vendedor = _nombreVendedor,
                    nota= _notasObservaciones,
                    items = _dataGen.Items.GetItems.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.Agregar.Presupuesto.FichaDetalle()
                        {
                            aliadoCirif = s.Item.Get_Aliado.ciRif,
                            aliadoCodigo = s.Item.Get_Aliado.codigo,
                            aliadoDesc = s.Item.Get_Aliado.nombreRazonSocial,
                            aliadoId = s.Item.Get_Aliado.id,
                            aliadoPrecioDivisa = s.Item.Get_Aliado_PrecioPautado,
                            alicuotaDesc = s.Item.Get_Alicuota.desc,
                            alicuotaId = s.Item.Get_Alicuota.id,
                            alicuotaTasa = s.Item.Get_Alicuota.tasa,
                            cntDias = s.Item.Get_CntDias,
                            cntUnidades = s.Item.Get_CntUnidades,
                            dscto = s.Item.Get_Dscto,
                            estatusAnulado = "0",
                            moduloCargar = s.Item.Get_ModuloCargar,
                            notas = s.Item.Get_DescripcionFull,
                            precioNetoDivisa = s.Item.Get_PrecioDivisa,
                            servicioDesc = s.Item.Get_Descripcion,
                            signoDoc = _signoDocumento,
                            solicitadorPor = s.Item.Get_SolicitadoPor,
                            tipoDoc = _tipoDcumento,
                            importe = s.Item.Get_Importe,
                            fechas = s.Item.Get_Fechas.Select(ss => 
                            {
                                var nr2 = new OOB.Transporte.Documento.Agregar.Presupuesto.Fecha()
                                {
                                    fecha = ss.fechaServ.Date,
                                    hora = ss.horaServ.ToShortTimeString(),
                                    nota = "",
                                };
                                return nr2;
                            }).ToList(),
                        };
                        return nr;
                    }).ToList(),
                };
                var r01 = Sistema.MyData.TransporteDocumento_AgregarPresupuesto(fichaOOB);
                _procesarIsOK = true;
                Helpers.Msg.AgregarOk();
                _dataGen.LimpiarTodo();
                _notasObservaciones = "";
                _limpiarDocumentoIsOK = true; 
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}