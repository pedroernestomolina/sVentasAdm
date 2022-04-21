using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar
{

    public class Gestion 
    {

        private enumerados.BusqPrd _prefBusqPrd;
        private int _idVentaTemporal;
        private bool _rupturaPorExistencia;
        private bool _remisionIsOk;
        private bool _docPendienteIsOk;
        private bool _recuperarDocumentoIsOk;
        private bool _abandonarDocIsOk;
        private bool _documentoProcesadoIsOk;
        private bool _abrirDocPendienteIsOk;
        private bool _cambioTasaDivisaIsOk;
        private bool _limpiezaGeneralIsOk;
        //
        private Cliente.Visualizar.Gestion _visualCliente;
        private Cliente.Documentos.Gestion _visualClienteDoc;
        private Cliente.Articulos.Gestion _visualClienteArticulos;
        private AgregarEditarItem.Gestion _agregarEditarItem;
        private IItems _gItems;
        private ICambioTasa _gCambioTasa;
        private IPendiente _gPendiente;
        private IDsctoCargoFinal _gDsctoCargoFinal;
        private IRemision _gRemision;
        private IDatosDoc _gDatosDoc;
        private IDocGestion _docGestion;
        private IBuscarProducto _gBuscarProd;


        public string TipoDocumento { get { return _docGestion.TipoDocumento; } }
        public string CntItem { get { return _gItems.CntItem.ToString(); } }
        public decimal TasaDivisa { get { return _docGestion.TasaDivisa; } }
        public decimal Monto { get { return _gItems.MontoTotal; } }
        public decimal MontoDivisa { get { return _gItems.MontoTotalDivisa; } }
        public decimal MontoNeto { get { return _gItems.MontoNeto; } }
        public decimal MontoIva { get { return _gItems.MontoIva; } }
        public string RifCliente { get { return _gDatosDoc.GetData.ClienteRif; } }
        public string CodigoCliente { get { return _gDatosDoc.GetData.ClienteCodigo ; } }
        public string Cliente { get { return _gDatosDoc.GetData.ClienteRazonSocialDireccion ; } }
        public BindingSource ItemsSource { get { return _gItems.ItemsSource; } }
        public enumerados.BusqPrd PrefBusqProducto { get { return _prefBusqPrd; } }
        public DateTime DatosDoc_Fecha { get { return _gDatosDoc.GetData.Fecha; } }
        public string DatosDoc_CondPago { get { return _gDatosDoc.GetData.CondicionPago; } }
        public string DatosDoc_Deposito { get { return _gDatosDoc.GetData.EntidadDeposito.desc; } }
        public DateTime DatosDoc_FechaVence { get { return _gDatosDoc.GetData.FechaVence; } }
        public string DatosDoc_OrdenCompra { get { return _gDatosDoc.GetData.OrdenCompra; } }
        public string DatosDoc_Pedido { get { return _gDatosDoc.GetData.Pedido; } }
        public string DatosDoc_Serie { get { return ""; } }
        public string DatosDoc_Sucursal { get { return _gDatosDoc.EntidadSucursal.desc; } }
        public string DatosDoc_Notas { get { return _gDatosDoc.GetData.NotasDoc; } }
        public OOB.Sistema.TipoDocumento.Entidad.Ficha SistTipoDocumento { get { return _docGestion.SistTipoDocumento; } }
        public bool AbandonarDocIsOk { get { return _abandonarDocIsOk; } }
        public int CantDocPend { get { return _docGestion.CantDocPend; } }
        public int CantDocRecuperar { get { return _docGestion.CantDocRecuperar; } }
        //
        public bool DocumentoProcesadoIsOk { get { return _documentoProcesadoIsOk; } }
        //
        public BindingSource RemisionSource { get { return _gRemision.RemisionSource; } }
        public string DatosDoc_FechaDocRemision { get { return _gDatosDoc.FechaDocRemision; } }
        public string DatosDoc_NumeroDocRemision { get { return _gDatosDoc.GetData.RemisionNumDoc; } }
        public string DatosDoc_NombreDocRemision { get { return _gDatosDoc.GetData.RemisionNombreDoc; } }
        //
        public bool RemisionIsOk { get { return _remisionIsOk; } }
        public bool DocPendienteIsOk { get { return _docPendienteIsOk; } }
        public bool RecuperarDocumentoIsOk { get { return _recuperarDocumentoIsOk; } }
        public bool AbrirDocPendienteIsOk { get { return _abrirDocPendienteIsOk; } }
        public bool CambioTasaDivisaIsOk { get { return _cambioTasaDivisaIsOk; } }
        public bool LimpiezaGeneralIsOk { get { return _limpiezaGeneralIsOk; } }
        

        public Gestion()
        {
            _idVentaTemporal = -1;
            _abandonarDocIsOk = false;
            _rupturaPorExistencia = false;
            _prefBusqPrd = enumerados.BusqPrd.SnDefinir;
            _visualCliente = new Cliente.Visualizar.Gestion();
            _visualClienteDoc = new Cliente.Documentos.Gestion();
            _visualClienteArticulos = new Cliente.Articulos.Gestion();
            _agregarEditarItem = new AgregarEditarItem.Gestion();
        }


        public void setGestionDsctoCargoFinal(IDsctoCargoFinal ctr)
        {
            _gDsctoCargoFinal = ctr;
        }

        public void setGestionRemision(IRemision  ctr)
        {
            _gRemision= ctr;
        }
        public void setGestionPendiente(IPendiente ctr)
        {
            _gPendiente = ctr;
        }
        public void setGestionCambioTasa(ICambioTasa ctr)
        {
            _gCambioTasa = ctr;
        }
        public void setGestionDatosDoc(IDatosDoc ctr)
        {
            _gDatosDoc= ctr;
        }
        public void setGestionBuscarProducto(IBuscarProducto ctr)
        {
            _gBuscarProd= ctr;
        }
        public void setGestionItems(IItems ctr)
        {
            _gItems= ctr;
        }


        public void Inicializa()
        {
            _prefBusqPrd = enumerados.BusqPrd.SnDefinir;
            _docGestion.Inicializa();
            _gDatosDoc.Inicializa();
            _gItems.Inicializa();
            _gBuscarProd.Inicializa();
            _idVentaTemporal = -1;
            _abandonarDocIsOk = false;
            _rupturaPorExistencia = false;
            _documentoProcesadoIsOk = false;
            _remisionIsOk = false;
            _docPendienteIsOk = false;
            _recuperarDocumentoIsOk = false;
            _abrirDocPendienteIsOk = false;
            _cambioTasaDivisaIsOk = false;
            _limpiezaGeneralIsOk = false;
        }

        private DocGenerarFrm _frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (_docGestion.CargarData())
                {
                    _gRemision.setTipoDocRemision(_docGestion.TipoDocRemision);
                    _gItems.setDivisa(TasaDivisa);
                    if (_frm == null)
                    {
                        _frm = new DocGenerarFrm();
                        _frm.setControlador(this);
                    }
                    _frm.ShowDialog();
                }
            }
        }

        private bool CargarData()
        {
            var r01 = Sistema.MyData.Configuracion_BusquedaPreferenciaProducto();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }

            var r02 = Sistema.MyData.Configuracion_RupturaPorExistencia();
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _rupturaPorExistencia = r02.Entidad;

            _prefBusqPrd = (enumerados.BusqPrd)r01.Entidad;
            switch (_prefBusqPrd)
            {
                case enumerados.BusqPrd.Codigo:
                    ActivarBusPorCodigo();
                    break;
                case enumerados.BusqPrd.Nombre:
                    ActivarBusPorDescripcion();
                    break;
                case enumerados.BusqPrd.Referencia:
                    ActivarBusPorReferencia();
                    break;
            }

            return true;
        }

        public void setDocGestion(IDocGestion doc)
        {
            _docGestion = doc;
        }

        public void NuevoDocumento()
        {
            if (_gDatosDoc.AceptarDatosIsOK)
            {
                return;
            }
            _documentoProcesadoIsOk = false;
            _gDatosDoc.Inicializa();
            _gDatosDoc.setHabilitarSucursal(!_gItems.HayItemsEnBandeja);
            _gDatosDoc.setHabilitarDeposito(!_gItems.HayItemsEnBandeja);
            _gDatosDoc.setHabilitarBusquedaCliente(!_gItems.HayItemsEnBandeja);
            _gDatosDoc.setHabilitarDatosDoc(_docGestion.HabilitarDatosDoc);
            _gDatosDoc.setTipoDocumento(SistTipoDocumento);
            _gDatosDoc.setIsModoRegistrar(true);
            _gDatosDoc.setFactorDivisa(TasaDivisa);
            _gDatosDoc.setIdEquipo(Sistema.IdEquipo);
            _gDatosDoc.Inicia();
            if (_gDatosDoc.AceptarDatosIsOK)
            {
                _idVentaTemporal = _gDatosDoc.IdRegDocTemporal;
            }
        }

        public void EditarDatosDocumento()
        {
            if (!_gDatosDoc.AceptarDatosIsOK)
            {
                return;
            }
            _gDatosDoc.setHabilitarSucursal(!_gItems.HayItemsEnBandeja);
            _gDatosDoc.setHabilitarDeposito(!_gItems.HayItemsEnBandeja);
            _gDatosDoc.setHabilitarBusquedaCliente(!_gItems.HayItemsEnBandeja);
            _gDatosDoc.setHabilitarDatosDoc(_docGestion.HabilitarDatosDoc);
            _gDatosDoc.setIsModoRegistrar(false);
            _gDatosDoc.Inicia();
        }

        public void LimpiarDatosDocumento()
        {
            if (_idVentaTemporal == -1) { return; }
            if (_gItems.HayItemsEnBandeja)
            {
                Helpers.Msg.Error("DATOS DEL DOCUMENTO EN CUESTION NO PUEDEN SER LIMPIADOS");
                return;
            }

            var msg = "Estas Seguro De Limpiar Datos de Encabezado Del Documento ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                var r01 = Sistema.MyData.Venta_Temporal_Encabezado_Eliminar(_idVentaTemporal);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _idVentaTemporal = -1;
                _gDatosDoc.Limpiar();
            }
        }

        public void VisualizarCliente()
        {
            if (_gDatosDoc.AceptarDatosIsOK)
            {
                _visualCliente.Inicializa();
                _visualCliente.setFicha(_gDatosDoc.EntidadCliente.id);
                _visualCliente.Inicia();
            }
        }

        public void VisualizarClenteDoc()
        {
            if (_gDatosDoc.AceptarDatosIsOK)
            {
                var fecha = DateTime.Now.Date;
                var filtroOOB = new OOB.Maestro.Cliente.Documento.Filtro()
                {
                    desde = fecha.AddDays(-120),
                    hasta = fecha,
                    autoCliente = _gDatosDoc.EntidadCliente.id,
                };
                var r01 = Sistema.MyData.Cliente_Documentos_GetLista(filtroOOB);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }

                _visualClienteDoc.Inicializa();
                _visualClienteDoc.setCliente(_gDatosDoc.EntidadCliente);
                _visualClienteDoc.setLista(r01.ListaD);
                _visualClienteDoc.Inicia();
            }
        }

        public void VisualizarClienteArticulos()
        {
            if (_gDatosDoc.AceptarDatosIsOK)
            {
                var fecha = DateTime.Now.Date;
                var filtroOOB = new OOB.Maestro.Cliente.Articulos.Filtro()
                {
                    desde = fecha.AddDays(-120),
                    hasta = fecha,
                    autoCliente = _gDatosDoc.EntidadCliente.id,
                };
                var r01 = Sistema.MyData.Cliente_ArticulosVenta_GetLista(filtroOOB);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _visualClienteArticulos.Inicializa();
                _visualClienteArticulos.setCliente(_gDatosDoc.EntidadCliente);
                _visualClienteArticulos.setLista(r01.ListaD);
                _visualClienteArticulos.Inicia();
            }
        }

        public void EliminarItem()
        {
            if (_gItems.ItemActual != null)
            {
                var r00 = Sistema.MyData.Permiso_GenerarDoc_AnularItem(Sistema.Usuario.idGrupo);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return;
                }

                if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
                {
                    var msg = "Eliminar Item En Cuestión ?";
                    var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (r != DialogResult.Yes)
                    {
                        return;
                    }

                    var _itActual = _gItems.ItemActual;
                    var ficha = new OOB.Venta.Temporal.Item.Eliminar.Ficha()
                    {
                        itemEncabezado = new OOB.Venta.Temporal.Item.Eliminar.ItemEncabezado()
                        {
                            id = _idVentaTemporal,
                            monto = _itActual.mTotal,
                            montoDivisa = _itActual.mTotalDivisa,
                            renglones = 1,
                        },
                        itemDetalle = new OOB.Venta.Temporal.Item.Eliminar.ItemDetalle()
                        {
                            id = _itActual.Id,
                        },
                        itemActDeposito = null,
                    };
                    if (_itActual.MercanciaIsEnReserva)
                    {
                        var xficha = new OOB.Venta.Temporal.Item.Eliminar.ItemActDeposito()
                        {
                            autoDeposito = _gDatosDoc.EntidadDeposito.id,
                            autoProducto = _itActual.IdProducto,
                            cntActualizar = _itActual.CantUnd,
                            prdDescripcion = _itActual.DescripcionPrd,
                        };
                        ficha.itemActDeposito = xficha;
                    }
                    var r01 = Sistema.MyData.Venta_Temporal_Item_Eliminar(ficha);
                    if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                    {
                        Helpers.Msg.Error(r01.Mensaje);
                        return;
                    }
                    _gItems.EliminarItem(_itActual);
                }
            }
        }

        public void setCadenaBusqProducto(string cad)
        {
            _gBuscarProd.setCadenaBusq(cad);
        }

        public void BusqProducto()
        {
            if (!_gDatosDoc.AceptarDatosIsOK)
            {
                Helpers.Msg.Alerta("DEBES PRIMERO HACER CLICK EN NUEVO DOCUMENTO");
                return;
            }
            _gBuscarProd.setDepositoBuscar(_gDatosDoc.EntidadDeposito.id);
            _gBuscarProd.setActivarSeleccionItem(true);
            _gBuscarProd.setFactorCambio(TasaDivisa);
            _gBuscarProd.Buscar();
            if (_gBuscarProd.ItemSeleccionadoIsOk)
            {
                CapturarDataProducto(_gBuscarProd.IdItemSeleccionado);
            }
        }

        private void CapturarDataProducto(string id)
        {
            var r00 = Sistema.MyData.Configuracion_RupturaPorExistencia();
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            var r01 = Sistema.MyData.Producto_GetFichaById(id);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            var tarifaPrecioManejar = _gDatosDoc.EntidadCliente.tarifa;
            _rupturaPorExistencia = r00.Entidad;

            _agregarEditarItem.Inicializa();
            _agregarEditarItem.setItemDocGestion(_docGestion.ItemGestion);
            _agregarEditarItem.setTarifaPrecio(tarifaPrecioManejar);
            _agregarEditarItem.setTasaDivisa(TasaDivisa);
            _agregarEditarItem.setEntidadDeposito(_gDatosDoc.EntidadDeposito);
            _agregarEditarItem.setRupturaPorExistencia(_rupturaPorExistencia);
            _agregarEditarItem.setAgregar(r01.Entidad, _idVentaTemporal);
            _agregarEditarItem.Inicia();
            if (_agregarEditarItem.ProcesarItemIsOk)
            {
                var idItemAgregado = _agregarEditarItem.IdItemAgregado;
                var r02 = Sistema.MyData.Venta_Temporal_Item_GetFichaById(idItemAgregado);
                if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r02.Mensaje);
                    return;
                }
                _gItems.AgregarItem(r02.Entidad, TasaDivisa);
            }
        }

        public void ActivarBusPorCodigo()
        {
            _gBuscarProd.setActivarBusPorCodigo();
        }

        public void ActivarBusPorDescripcion()
        {
            _gBuscarProd.setActivarBusPorDescripcion();
        }

        public void ActivarBusPorReferencia()
        {
            _gBuscarProd.setActivarBusPorReferencia();
        }

        public void setNotasDoc(string p)
        {
            if (_idVentaTemporal != -1)
            {
                var ficha = new OOB.Venta.Temporal.Cambios.Notas.Ficha()
                {
                    id = _idVentaTemporal,
                    notas = p,
                };
                var r01 = Sistema.MyData.Venta_Temporal_SetNotas(ficha);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _gDatosDoc.setNotasDoc(p);
            }
        }

        public void LimpiarItems()
        {
            if (_gItems.HayItemsEnBandeja)
            {
                var msg = "Eliminar Items En Cuestión ?";
                var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (r != DialogResult.Yes)
                {
                    return;
                }

                var ficha = new OOB.Venta.Temporal.Item.Limpiar.Ficha()
                {
                    itemEncabezado = new OOB.Venta.Temporal.Item.Limpiar.ItemEncabezado()
                    {
                        id = _idVentaTemporal,
                    },
                    itemDetalle = _gItems.ListaItems.Select(s =>
                    {
                        var rg = new OOB.Venta.Temporal.Item.Limpiar.ItemDetalle()
                        {
                            id = s.Id,
                        };
                        return rg;
                    }).ToList(),
                    itemActDeposito = null,
                };
                ficha.itemActDeposito = _gItems.ListaItems.Where(w => w.MercanciaIsEnReserva).Select(s =>
                {
                    var rg = new OOB.Venta.Temporal.Item.Limpiar.ItemActDeposito()
                    {
                        autoDeposito = _gDatosDoc.EntidadDeposito.id,
                        autoProducto = s.IdProducto,
                        cntActualizar = s.CantUnd,
                        prdDescripcion = s.DescripcionPrd,
                    };
                    return rg;
                }).ToList();

                var r01 = Sistema.MyData.Venta_Temporal_Item_Limpiar(ficha);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _gItems.LimpiarItems();
            }
            else
            {
                Helpers.Msg.Alerta("NOHAY ITEMS REGISTRADOS");
            }
        }

        public void AbandonarDoc()
        {
            if (_idVentaTemporal == -1)
            {
                _abandonarDocIsOk = true;
                return;
            }

            _abandonarDocIsOk = false;
            var msg = "Abandonar Documento En Cuestión ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                if (_idVentaTemporal != -1)
                {
                    var ficha = new OOB.Venta.Temporal.Anular.Ficha()
                    {
                        IdEncabezado = _idVentaTemporal,
                        Items = _gItems.ListaItems.Select(s =>
                        {
                            var rg = new OOB.Venta.Temporal.Anular.Item()
                            {
                                idItem = s.Id,
                            };
                            return rg;
                        }).ToList(),
                        ItemsActDeposito = _gItems.ListaItems.Where(w => w.MercanciaIsEnReserva).Select(s =>
                        {
                            var rg = new OOB.Venta.Temporal.Anular.ItemActDeposito()
                            {
                                autoDeposito = _gDatosDoc.EntidadDeposito.id,
                                autoProducto = s.IdProducto,
                                cntActualizar = s.CantUnd,
                                prdDescripcion = s.DescripcionPrd,
                            };
                            return rg;
                        }).ToList(),
                    };
                    var r01 = Sistema.MyData.Venta_Temporal_Anular(ficha);
                    if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                    {
                        Helpers.Msg.Error(r01.Mensaje);
                        return;
                    }
                    _abandonarDocIsOk = true;
                }
                else
                {
                    _abandonarDocIsOk = true;
                }
            }
        }

        public void EditarItem()
        {
            if (_gItems.ItemActual != null)
            {
                var _itActual = _gItems.ItemActual;
                var _idEditar = _itActual.Id;

                var r00 = Sistema.MyData.Configuracion_RupturaPorExistencia() ;
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return;
                }
                var r01 = Sistema.MyData.Producto_GetFichaById(_itActual.IdProducto);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _rupturaPorExistencia = r00.Entidad;
                var tarifaPrecioManejar = _gDatosDoc.EntidadCliente.tarifa;

                _agregarEditarItem.Inicializa();
                _agregarEditarItem.setItemDocGestion(_docGestion.ItemGestion);
                _agregarEditarItem.setTarifaPrecio(tarifaPrecioManejar);
                _agregarEditarItem.setTasaDivisa(TasaDivisa);
                _agregarEditarItem.setEntidadDeposito(_gDatosDoc.EntidadDeposito);
                _agregarEditarItem.setRupturaPorExistencia(_rupturaPorExistencia);
                _agregarEditarItem.setEditar(_itActual, _idVentaTemporal, r01.Entidad);
                _agregarEditarItem.setCargarPrecioNeto(_itActual.PNeto);
                _agregarEditarItem.Inicia();
                if (_agregarEditarItem.ProcesarItemIsOk)
                {
                    var idItemAgregado = _agregarEditarItem.IdItemAgregado;
                    var r02 = Sistema.MyData.Venta_Temporal_Item_GetFichaById(idItemAgregado);
                    if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                    {
                        Helpers.Msg.Error(r02.Mensaje);
                        return;
                    }
                    _gItems.EliminarLista(_idEditar);
                    _gItems.AgregarItem(r02.Entidad, TasaDivisa);
                }

            }
        }

        public void DocPendiente()
        {
            _docPendienteIsOk = false;
            if (_idVentaTemporal == -1)
            {
                return;
            }
            if (!_gItems.HayItemsEnBandeja)
            {
                return;
            }

            var msg = "Pasar Documento En Cuestión A Espera/Pendiente ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                var fichaOOB = new OOB.Venta.Temporal.Pendiente.Dejar.Ficha()
                {
                    idTemporal = _idVentaTemporal,
                };
                var r01 = Sistema.MyData.VentaAdm_Temporal_Pendiente_Dejar(fichaOOB);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _gItems.EliminarListaItems();
                _idVentaTemporal = -1;
                _gDatosDoc.Limpiar();
                ActualizarTasaDivisa();
                _docPendienteIsOk = true;
            }
        }

        public void RecuperarDocumento()
        {
            _recuperarDocumentoIsOk = false;
            if (_idVentaTemporal != -1)
            {
                Helpers.Msg.Alerta("NO DEBE HABER NINGUN DOCUMENTO EN PROCESO PARA ACTIVAR ESTA OPCION");
                return;
            }
            if (_gItems.HayItemsEnBandeja)
            {
                Helpers.Msg.Alerta("NO DEBE HABER NINGUN DOCUMENTO EN PROCESO PARA ACTIVAR ESTA OPCION");
                return;
            }

            var msg = "Recuperar Documentos Dejados Por Corte/Fallas Electricas ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                var fichaOOB = new OOB.Venta.Temporal.Recuperar.Ficha()
                {
                    autoSistDocumento = _docGestion.SistTipoDocumento.id,
                    autoUsuario = Sistema.Usuario.id,
                    idEquipo = Sistema.IdEquipo,
                };
                var r01 = Sistema.MyData.VentaAdm_Temporal_Recuperar(fichaOOB);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _recuperarDocumentoIsOk = true;
                Helpers.Msg.OK("SISTEMA RECUPERO ( " + r01.Entidad.ToString() + " ) DOCUMENTO(S)");
            }
        }

        public void AbrirDocPendiente()
        {
            _abrirDocPendienteIsOk = false;
            if (_idVentaTemporal != -1)
            {
                Helpers.Msg.Alerta("NO DEBE HABER NINGUN DOCUMENTO EN PROCESO PARA ACTIVAR ESTA OPCION");
                return;
            }
            if (_gItems.HayItemsEnBandeja)
            {
                Helpers.Msg.Alerta("NO DEBE HABER NINGUN DOCUMENTO EN PROCESO PARA ACTIVAR ESTA OPCION");
                return;
            }

            var filtroOOB = new OOB.Venta.Temporal.Pendiente.Lista.Filtro()
            {
                autoSistDocumento = SistTipoDocumento.id,
                autoUsuario = Sistema.Usuario.id,
                idEquipo = Sistema.IdEquipo,
            };
            var r01 = Sistema.MyData.VentaAdm_Temporal_Pendiente_GetLista(filtroOOB);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _gPendiente.Inicializa();
            _gPendiente.setData(r01.ListaD);
            _gPendiente.Inicia();
            if (_gPendiente.ItemSeleccionadoIsOk)
            {
                AbrirPendiente(_gPendiente.IdItemSeleccionado);
            }
        }

        private void AbrirPendiente(int idPendiente)
        {
            var r01 = Sistema.MyData.VentaAdm_Temporal_Pendiente_Abrir(idPendiente);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            //
            _gDatosDoc.setIdEquipo(Sistema.IdEquipo);
            _gDatosDoc.setTipoDocumento(SistTipoDocumento);
            _gDatosDoc.setCargarData(r01.Entidad.Encabezado);
            _gItems.AgregarLista(r01.Entidad.Items, r01.Entidad.Encabezado.factorDivisa);
            _idVentaTemporal = r01.Entidad.Encabezado.id;
            //
            _docGestion.setCambioTasaDivisa(_gDatosDoc.GetData.FactorDivisa );
            _gItems.setCambioTasaDivisa(_gDatosDoc.GetData.FactorDivisa);
            //
            _abrirDocPendienteIsOk = true;
        }

        public void RemisionDoc()
        {
            if (!_gDatosDoc.AceptarDatosIsOK)
            {
                return;
            }
            if (_gItems.HayItemsEnBandeja)
            {
                Helpers.Msg.Alerta("OPCION NO PERMITIDA CUANDO HAY ITEMS EN PROCESO");
                return;
            }
            if (_gDatosDoc.GetData.RemisionIsOk)
            {
                Helpers.Msg.Alerta("EXISTE UN DOCUMENTO DE REMISION YA CARGADO");
                return;
            }

            _gRemision.Inicializa();
            _gRemision.setIdCliente(_gDatosDoc.EntidadCliente.id);
            _gRemision.Inicia();
            if (_gRemision.ItemSeleccionadoIsOk) 
            {
                CargarDocumentoRemision(_gRemision.IdItemSeleccionado);
            }
        }

        private void CargarDocumentoRemision(string autoDoc)
        {
            var r01 = Sistema.MyData.Documento_GetById(autoDoc);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            var ficha = _docGestion.CargaRemision(r01.Entidad, _idVentaTemporal);
            foreach (var it in ficha.items)
            {
                //ASIGNO EL DEPOSITO ACTUAL
                it.autoDeposito = _gDatosDoc.EntidadDeposito.id;
                it.nombreDeposito = _gDatosDoc.EntidadDeposito.desc;
            }
            if (ficha != null)
            {
                var r02 = Sistema.MyData.VentaAdm_Temporal_Remision_Registrar(ficha);
                if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r02.Mensaje);
                    return;
                }
                var r03 = Sistema.MyData.Venta_Temporal_Item_GetLista(_idVentaTemporal);
                if (r03.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r03.Mensaje);
                    return;
                }
                _gItems.AgregarLista(r03.ListaD, TasaDivisa);
                _gDatosDoc.setRemision(r01.Entidad);
                _remisionIsOk = true;
            }
        }

        public void CambioTasaDivisa()
        {
            _cambioTasaDivisaIsOk = false;
            if (_idVentaTemporal != -1)
            {
                var _tasaAnterior = TasaDivisa;

                _gCambioTasa.setTasa(TasaDivisa);
                _gCambioTasa.Inicializa();
                _gCambioTasa.Inicia();
                if (_gCambioTasa.CambioTasaIsOk) 
                {
                    var _tasa = _gCambioTasa.TasaCambiar;
                    _docGestion.setCambioTasaDivisa(_tasa);
                    _gItems.setCambioTasaDivisa(_tasa);
                    var ficha = new OOB.Venta.Temporal.Cambios.TasaDivisa.Ficha()
                    {
                        id = _idVentaTemporal,
                        tasaDivisa = _tasa,
                        montoDivisa = MontoDivisa,
                        items = _gItems.ListaItems.Select(s =>
                        {
                            var nr = new OOB.Venta.Temporal.Cambios.TasaDivisa.Item()
                            {
                                id = s.Id,
                                descProducto = s.DescripcionPrd,
                                totalDivisa = s.mTotalDivisa,
                            };
                            return nr;
                        }).ToList(),
                    };
                    var r01 = Sistema.MyData.Venta_Temporal_SetTasaDivisa(ficha);
                    if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                    {
                        _docGestion.setCambioTasaDivisa(_tasaAnterior);
                        _gItems.setCambioTasaDivisa(_tasaAnterior);
                        Helpers.Msg.Error(r01.Mensaje);
                        return;
                    }
                    _cambioTasaDivisaIsOk = true;
                }
            }
        }

        private void ActualizarTasaDivisa()
        {
            _docGestion.ActualizarTasaDivisaSistema();
        }

        public void LimpiezaGeneral()
        {
            _limpiezaGeneralIsOk = false;

            if (_idVentaTemporal == -1) { return; }
            var msg = "Limpiar Plantilla General ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r != DialogResult.Yes)
            {
                return;
            }

            var fichaAnular = new OOB.Venta.Temporal.Anular.Ficha()
            {
                IdEncabezado = _idVentaTemporal,
                Items = _gItems.ListaItems.Select(s =>
                {
                    var rg = new OOB.Venta.Temporal.Anular.Item()
                    {
                        idItem = s.Id,
                        prdDescripcion = s.DescripcionPrd,
                    };
                    return rg;
                }).ToList(),
                ItemsActDeposito = _gItems.ListaItems.Where(w => w.MercanciaIsEnReserva).Select(s =>
                {
                    var rg = new OOB.Venta.Temporal.Anular.ItemActDeposito()
                    {
                        autoDeposito = _gDatosDoc.EntidadDeposito.id,
                        autoProducto = s.IdProducto,
                        cntActualizar = s.CantUnd,
                        prdDescripcion = s.DescripcionPrd,
                        idItem = s.Id,
                    };
                    return rg;
                }).ToList(),
            };
            var r01 = Sistema.MyData.Venta_Temporal_Anular(fichaAnular);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            IniciarDocumentoNuevo();
            _limpiezaGeneralIsOk = true;
        }

        public void ProcesarDoc()
        {
            _documentoProcesadoIsOk = false;
            if (_idVentaTemporal == -1)
            {
                return;
            }
            if (!_gItems.HayItemsEnBandeja)
            {
                return;
            }

            _gDsctoCargoFinal.Inicializa();
            _gDsctoCargoFinal.setData(Monto, TasaDivisa);
            _gDsctoCargoFinal.Inicia();
            if (_gDsctoCargoFinal.IsOk)
            {
                switch (_gDatosDoc.EntidadTipoDoc.codigo)
                {
                    case "05":
                        ProcesarPresupuesto();
                        break;
                    case "06":
                        ProcesarPedido();
                        break;
                }
            }

        }

        private void ProcesarPresupuesto()
        {
            var r01 = Sistema.MyData.Sistema_TasaFiscal_GetLista();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return ;
            }
            var _tf1 = r01.ListaD.FirstOrDefault(f => f.codigo == "01");
            var _tf2 = r01.ListaD.FirstOrDefault(f => f.codigo == "02");
            var _tf3 = r01.ListaD.FirstOrDefault(f => f.codigo == "03");


            var _monto = Monto;
            var _montoNeto = MontoNeto;
            var _montoImpuesto = MontoIva;
            var subTotalNeto = _montoNeto;  

            var montoCambio = 0m;
            var MontoBase = _gItems.ListaItems.Sum(s => s.MontoBase);
            var MontoImpuesto = _gItems.ListaItems.Sum(s => s.MontoImpuesto);
            var BaseExenta = _gItems.ListaItems.Sum(s => s.MontoExento);
            var MontoBase1 = _gItems.ListaItems.Where(w => w.idTasaIva == _tf1.id).Sum(s => s.MontoBase);
            var MontoBase2 = _gItems.ListaItems.Where(w => w.idTasaIva == _tf2.id).Sum(s => s.MontoBase);
            var MontoBase3 = _gItems.ListaItems.Where(w => w.idTasaIva == _tf3.id).Sum(s => s.MontoBase);


            var importeDocumento  = (BaseExenta + MontoBase + MontoImpuesto);
            var dsctoMonto = importeDocumento * _gDsctoCargoFinal.DsctoFinal / 100;
            importeDocumento -= dsctoMonto;
            var cargoMonto = importeDocumento * _gDsctoCargoFinal.CargoFinal / 100;
            importeDocumento += cargoMonto;
            importeDocumento = Math.Round(importeDocumento, 2, MidpointRounding.AwayFromZero);
            dsctoMonto = Math.Round(dsctoMonto, 2, MidpointRounding.AwayFromZero);
            cargoMonto = Math.Round(cargoMonto, 2, MidpointRounding.AwayFromZero);

            BaseExenta -= (BaseExenta * _gDsctoCargoFinal.DsctoFinal / 100);
            BaseExenta += (BaseExenta * _gDsctoCargoFinal.CargoFinal / 100);

            MontoBase1 -= (MontoBase1 * _gDsctoCargoFinal.DsctoFinal / 100);
            MontoBase1 += (MontoBase1 * _gDsctoCargoFinal.CargoFinal / 100);
            MontoBase1 = Math.Round(MontoBase1, 2, MidpointRounding.AwayFromZero);
            var MontoImpuesto1 = MontoBase1 * (_tf1.tasa / 100);
            MontoImpuesto1 = Math.Round(MontoImpuesto1, 2, MidpointRounding.AwayFromZero);

            MontoBase2 -= (MontoBase2 * _gDsctoCargoFinal.DsctoFinal / 100);
            MontoBase2 += (MontoBase2 * _gDsctoCargoFinal.CargoFinal / 100);
            MontoBase2 = Math.Round(MontoBase2, 2, MidpointRounding.AwayFromZero);
            var MontoImpuesto2 = MontoBase2 * (_tf2.tasa / 100);
            MontoImpuesto2 = Math.Round(MontoImpuesto2, 2, MidpointRounding.AwayFromZero);

            MontoBase3 -= (MontoBase3 * _gDsctoCargoFinal.DsctoFinal / 100);
            MontoBase3 += (MontoBase3 * _gDsctoCargoFinal.CargoFinal / 100);
            MontoBase3 = Math.Round(MontoBase3, 2, MidpointRounding.AwayFromZero);
            var MontoImpuesto3 = MontoBase3 * (_tf3.tasa / 100);
            MontoImpuesto3 = Math.Round(MontoImpuesto3, 2, MidpointRounding.AwayFromZero);

            var costoMonto = _gItems.ListaItems.Sum(s => s.CostoVenta); 
            var netoMonto = _gItems.ListaItems.Sum(s => s.NetoVenta); 
            var utilidadMonto = netoMonto - costoMonto;
            var importeDocumentoDivisa = importeDocumento/TasaDivisa;
            importeDocumentoDivisa = Math.Round(importeDocumentoDivisa, 2, MidpointRounding.AwayFromZero);
            var factorCambio = TasaDivisa;
            var subTotal = importeDocumento;
            var saldoPendiente = 0.0m;

            var fichaOOB = new OOB.Documento.Agregar.Presupuesto.Ficha()
            {
                RazonSocial = _gDatosDoc.EntidadCliente.razonSocial,
                DirFiscal = _gDatosDoc.EntidadCliente.dirFiscal,
                CiRif = _gDatosDoc.EntidadCliente.ciRif,
                CodigoTipoDoc = _gDatosDoc.EntidadTipoDoc.codigo,
                Exento = BaseExenta,
                Base1 = MontoBase1,
                Base2 = MontoBase2,
                Base3 = MontoBase3,
                Impuesto1 = MontoImpuesto1,
                Impuesto2 = MontoImpuesto2,
                Impuesto3 = MontoImpuesto3,
                MBase = (MontoBase1+MontoBase2+MontoBase3),
                Impuesto = (MontoImpuesto1+MontoImpuesto2+MontoImpuesto3),
                Total = importeDocumento,
                Tasa1 = _tf1.tasa,
                Tasa2 = _tf2.tasa,
                Tasa3 = _tf3.tasa,
                Nota = _gDatosDoc.GetData.NotasDoc,
                TasaRetencionIva = 0.0m,
                TasaRetencionIslr = 0.0m,
                RetencionIva = 0.0m,
                RetencionIslr = 0.0m,
                AutoCliente = _gDatosDoc.EntidadCliente.id,
                CodigoCliente = _gDatosDoc.EntidadCliente.codigo,
                Control = "",
                OrdenCompra = "",
                Dias = _gDatosDoc.GetData.DiasCredito,
                Descuento1 = dsctoMonto,
                Descuento2 = 0.0m,
                Cargos = cargoMonto,
                Descuento1p = _gDsctoCargoFinal.DsctoFinal,
                Descuento2p = 0.0m,
                Cargosp = _gDsctoCargoFinal.CargoFinal,
                Columna = "1",
                EstatusAnulado = "0",
                Aplica = "",
                ComprobanteRetencion = "",
                SubTotalNeto = subTotalNeto,
                Telefono = _gDatosDoc.EntidadCliente.telefono1,
                FactorCambio = factorCambio,
                CodigoVendedor = _gDatosDoc.EntidadVendedor.cod,
                Vendedor = _gDatosDoc.EntidadVendedor.desc,
                AutoVendedor = _gDatosDoc.EntidadVendedor.id,
                FechaPedido = DateTime.Now.Date,
                Pedido = "",
                CondicionPago = _gDatosDoc.GetData.CondicionPagoIsCredito ? "CREDITO" : "CONTADO",
                Usuario = Sistema.Usuario.nombre,
                CodigoUsuario = Sistema.Usuario.codigo,
                CodigoSucursal = _gDatosDoc.EntidadSucursal.cod,
                Transporte = _gDatosDoc.EntidadTransporte.desc,
                CodigoTransporte = _gDatosDoc.EntidadTransporte.cod,
                MontoDivisa = importeDocumentoDivisa,
                Despachado = "",
                DirDespacho = _gDatosDoc.GetData.DirDespacho,
                Estacion = Sistema.EquipoEstacion,
                Renglones = _gItems.ListaItems.Count(),
                SaldoPendiente = saldoPendiente,
                ComprobanteRetencionIslr = "",
                DiasValidez = _gDatosDoc.GetData.DiasValidez,
                AutoUsuario = Sistema.Usuario.id,
                AutoTransporte = _gDatosDoc.EntidadTransporte.id,
                Situacion = "Procesado",
                SignoTipoDoc = _gDatosDoc.EntidadTipoDoc.signo,
                SiglasTipoDoc = _gDatosDoc.EntidadTipoDoc.siglas,
                Tarifa = _gDatosDoc.EntidadCliente.tarifa,
                TipoRemision = _gDatosDoc.GetData.RemisionCodTipoDoc,
                DocumentoRemision = _gDatosDoc.GetData.RemisionNumDoc,
                AutoRemision = _gDatosDoc.GetData.RemisionAutoDoc,
                NombreTipoDoc = _gDatosDoc.EntidadTipoDoc.descripcion,
                SubTotalImpuesto = MontoImpuesto,
                SubTotal = subTotal,
                TipoCliente = "",
                Planilla = "",
                Expendiente = "",
                AnticipoIva = 0.0m,
                TercerosIva = 0.0m,
                Neto = netoMonto,
                Costo = costoMonto,
                Utilidad = utilidadMonto,
                Utilidadp = 100 - (costoMonto / netoMonto * 100),
                TipoTipoDoc = _gDatosDoc.EntidadTipoDoc.tipo,
                CiTitular = "",
                NombreTitular = "",
                CiBeneficiario = "",
                NombreBeneficiario = "",
                Clave = "",
                DenominacionFiscal = "No Contribuyente",
                Cambio = montoCambio,
                EstatusValidado = "0",
                Cierre = "",
                EstatusCierreContable = "0",
                CierreFtp = "",
                Prefijo = _gDatosDoc.EntidadSucursal.cod,
            };

            var detalles = _gItems.ListaItems.Select(s =>
            {
                var nr = new OOB.Documento.Agregar.Presupuesto.FichaDetalle()
                {
                    AutoProducto = s.DataItem.autoProducto,
                    Codigo = s.DataItem.codigoProducto,
                    Nombre = s.DataItem.nombreProducto,
                    AutoDepartamento = s.DataItem.autoDepartamento,
                    AutoGrupo = s.DataItem.autoGrupo,
                    AutoSubGrupo = s.DataItem.autoSubGrupo,
                    AutoDeposito = s.DataItem.autoDeposito,
                    Cantidad = s.DataItem.cantidad,
                    Empaque = s.DataItem.empaqueDesc,
                    PrecioNeto = s.DataItem.precioNeto,
                    Descuento1p = s.DataItem.dsctoPorct,
                    Descuento2p = 0.0m,
                    Descuento3p = 0.0m,
                    Descuento1 = s.DsctoMonto,
                    Descuento2 = 0.0m,
                    Descuento3 = 0.0m,
                    CostoVenta = s.CostoVenta,
                    TotalNeto = s.Importe ,
                    Tasa = s.DataItem.tasaIva,
                    Impuesto = s.MIva,
                    Total = s.mTotal, 
                    EstatusAnulado = "0",
                    Tipo = _gDatosDoc.EntidadTipoDoc.codigo,
                    Deposito = _gDatosDoc.EntidadDeposito.desc,
                    Signo = _gDatosDoc.EntidadTipoDoc.signo,
                    PrecioFinal = s.PrecioFinal,
                    AutoCliente = _gDatosDoc.EntidadCliente.id,
                    Decimales = s.DataItem.decimalesProducto,
                    ContenidoEmpaque = s.DataItem.empaqueCont,
                    CantidadUnd = s.DataItem.cantidadUnd,
                    PrecioUnd = s.PrecioFinal,
                    CostoUnd = s.DataItem.costoUnd,
                    Utilidad = s.Utilidad, 
                    Utilidadp = s.UtilidadP,
                    PrecioItem = s.PrecioItem,
                    EstatusGarantia = "0",
                    EstatusSerial = "0",
                    CodigoDeposito = _gDatosDoc.EntidadDeposito.cod,
                    DiasGarantia = 0,
                    Detalle = s.Notas,
                    PrecioSugerido = 0.0m,
                    AutoTasa = s.DataItem.autoTasaIva,
                    EstatusCorte = "0",
                    X = 0m,
                    Y = 0m,
                    Z = 0m,
                    Corte = "",
                    Categoria = s.DataItem.categroiaProducto,
                    Cobranzap = 0.0m,
                    Ventasp = 0.0m,
                    CobranzapVendedor = 0.0m,
                    VentaspVendedor = 0.0m,
                    Cobranza = 0.0m,
                    Ventas = 0.0m,
                    CobranzaVendedor = 0.0m,
                    VentasVendedor = 0.0m,
                    CostoPromedioUnd = s.DataItem.costoPromdUnd,
                    CostoCompra = s.DataItem.costoPromd,
                    EstatusChecked = "1",
                    Tarifa = s.DataItem.tarifaPrecio,
                    TotalDescuento = s.DsctoMontoTotal,
                    CodigoVendedor = _gDatosDoc.EntidadVendedor.cod,
                    AutoVendedor = _gDatosDoc.EntidadVendedor.id,
                    CierreFtp = "",
                };
                return nr;
            }).ToList();
            fichaOOB.Detalles = detalles;
            fichaOOB.VentaTemporal = new OOB.Documento.Agregar.Presupuesto.FichaTemporalVenta() { id = _idVentaTemporal };

            var r02 = Sistema.MyData.Documento_Agregar_Presupuesto(fichaOOB);
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r02.Mensaje);
                return;
            }
            _documentoProcesadoIsOk = true;
            IniciarDocumentoNuevo();
        }

        private void ProcesarPedido()
        {
            var r00 = Sistema.MyData.Configuracion_RupturaPorExistencia();
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            var r01 = Sistema.MyData.Sistema_TasaFiscal_GetLista();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _rupturaPorExistencia = r00.Entidad;
            var _tf1 = r01.ListaD.FirstOrDefault(f => f.codigo == "01");
            var _tf2 = r01.ListaD.FirstOrDefault(f => f.codigo == "02");
            var _tf3 = r01.ListaD.FirstOrDefault(f => f.codigo == "03");


            var _monto = Monto;
            var _montoNeto = MontoNeto;
            var _montoImpuesto = MontoIva;
            var subTotalNeto = _montoNeto;

            var montoCambio = 0m;
            var MontoBase = _gItems.ListaItems.Sum(s => s.MontoBase);
            var MontoImpuesto = _gItems.ListaItems.Sum(s => s.MontoImpuesto);
            var BaseExenta = _gItems.ListaItems.Sum(s => s.MontoExento);
            var MontoBase1 = _gItems.ListaItems.Where(w => w.idTasaIva == _tf1.id).Sum(s => s.MontoBase);
            var MontoBase2 = _gItems.ListaItems.Where(w => w.idTasaIva == _tf2.id).Sum(s => s.MontoBase);
            var MontoBase3 = _gItems.ListaItems.Where(w => w.idTasaIva == _tf3.id).Sum(s => s.MontoBase);

            var importeDocumento = (BaseExenta + MontoBase + MontoImpuesto);
            var dsctoMonto = importeDocumento * _gDsctoCargoFinal.DsctoFinal / 100;
            importeDocumento -= dsctoMonto;
            var cargoMonto = importeDocumento * _gDsctoCargoFinal.CargoFinal / 100;
            importeDocumento += cargoMonto;
            importeDocumento = Math.Round(importeDocumento, 2, MidpointRounding.AwayFromZero);
            dsctoMonto = Math.Round(dsctoMonto, 2, MidpointRounding.AwayFromZero);
            cargoMonto = Math.Round(cargoMonto, 2, MidpointRounding.AwayFromZero);

            BaseExenta -= (BaseExenta * _gDsctoCargoFinal.DsctoFinal / 100);
            BaseExenta += (BaseExenta * _gDsctoCargoFinal.CargoFinal / 100);

            MontoBase1 -= (MontoBase1 * _gDsctoCargoFinal.DsctoFinal / 100);
            MontoBase1 += (MontoBase1 * _gDsctoCargoFinal.CargoFinal / 100);
            MontoBase1 = Math.Round(MontoBase1, 2, MidpointRounding.AwayFromZero);
            var MontoImpuesto1 = MontoBase1 * (_tf1.tasa / 100);
            MontoImpuesto1 = Math.Round(MontoImpuesto1, 2, MidpointRounding.AwayFromZero);

            MontoBase2 -= (MontoBase2 * _gDsctoCargoFinal.DsctoFinal / 100);
            MontoBase2 += (MontoBase2 * _gDsctoCargoFinal.CargoFinal / 100);
            MontoBase2 = Math.Round(MontoBase2, 2, MidpointRounding.AwayFromZero);
            var MontoImpuesto2 = MontoBase2 * (_tf2.tasa / 100);
            MontoImpuesto2 = Math.Round(MontoImpuesto2, 2, MidpointRounding.AwayFromZero);

            MontoBase3 -= (MontoBase3 * _gDsctoCargoFinal.DsctoFinal / 100);
            MontoBase3 += (MontoBase3 * _gDsctoCargoFinal.CargoFinal / 100);
            MontoBase3 = Math.Round(MontoBase3, 2, MidpointRounding.AwayFromZero);
            var MontoImpuesto3 = MontoBase3 * (_tf3.tasa / 100);
            MontoImpuesto3 = Math.Round(MontoImpuesto3, 2, MidpointRounding.AwayFromZero);

            var costoMonto = _gItems.ListaItems.Sum(s => s.CostoVenta);
            var netoMonto = _gItems.ListaItems.Sum(s => s.NetoVenta);
            var utilidadMonto = netoMonto - costoMonto;
            var importeDocumentoDivisa = importeDocumento / TasaDivisa;
            importeDocumentoDivisa = Math.Round(importeDocumentoDivisa, 2, MidpointRounding.AwayFromZero);
            var factorCambio = TasaDivisa;
            var subTotal = importeDocumento;
            var saldoPendiente = 0.0m;

            var fichaOOB = new OOB.Documento.Agregar.Pedido.Ficha();
            var encabezado= new OOB.Documento.Agregar.Pedido.FichaEncabezado()
            {
                RazonSocial = _gDatosDoc.EntidadCliente.razonSocial,
                DirFiscal = _gDatosDoc.EntidadCliente.dirFiscal,
                CiRif = _gDatosDoc.EntidadCliente.ciRif,
                CodigoTipoDoc = _gDatosDoc.EntidadTipoDoc.codigo,
                Exento = BaseExenta,
                Base1 = MontoBase1,
                Base2 = MontoBase2,
                Base3 = MontoBase3,
                Impuesto1 = MontoImpuesto1,
                Impuesto2 = MontoImpuesto2,
                Impuesto3 = MontoImpuesto3,
                MBase = (MontoBase1 + MontoBase2 + MontoBase3),
                Impuesto = (MontoImpuesto1 + MontoImpuesto2 + MontoImpuesto3),
                Total = importeDocumento,
                Tasa1 = _tf1.tasa,
                Tasa2 = _tf2.tasa,
                Tasa3 = _tf3.tasa,
                Nota = _gDatosDoc.GetData.NotasDoc,
                TasaRetencionIva = 0.0m,
                TasaRetencionIslr = 0.0m,
                RetencionIva = 0.0m,
                RetencionIslr = 0.0m,
                AutoCliente = _gDatosDoc.EntidadCliente.id,
                CodigoCliente = _gDatosDoc.EntidadCliente.codigo,
                Control = "",
                OrdenCompra = _gDatosDoc.GetData.OrdenCompra,
                Dias = _gDatosDoc.GetData.DiasCredito,
                Descuento1 = dsctoMonto,
                Descuento2 = 0.0m,
                Cargos = cargoMonto,
                Descuento1p = _gDsctoCargoFinal.DsctoFinal,
                Descuento2p = 0.0m,
                Cargosp = _gDsctoCargoFinal.CargoFinal,
                Columna = "1",
                EstatusAnulado = "0",
                Aplica = "",
                ComprobanteRetencion = "",
                SubTotalNeto = subTotalNeto,
                Telefono = _gDatosDoc.EntidadCliente.telefono1,
                FactorCambio = factorCambio,
                CodigoVendedor = _gDatosDoc.EntidadVendedor.cod,
                Vendedor = _gDatosDoc.EntidadVendedor.desc,
                AutoVendedor = _gDatosDoc.EntidadVendedor.id,
                FechaPedido = DateTime.Now.Date,
                Pedido = "",
                CondicionPago = _gDatosDoc.GetData.CondicionPagoIsCredito ? "CREDITO" : "CONTADO",
                Usuario = Sistema.Usuario.nombre,
                CodigoUsuario = Sistema.Usuario.codigo,
                CodigoSucursal = _gDatosDoc.EntidadSucursal.cod,
                Transporte = _gDatosDoc.EntidadTransporte.desc,
                CodigoTransporte = _gDatosDoc.EntidadTransporte.cod,
                MontoDivisa = importeDocumentoDivisa,
                Despachado = "",
                DirDespacho = _gDatosDoc.GetData.DirDespacho,
                Estacion = Sistema.EquipoEstacion,
                Renglones = _gItems.ListaItems.Count(),
                SaldoPendiente = saldoPendiente,
                ComprobanteRetencionIslr = "",
                DiasValidez = _gDatosDoc.GetData.DiasValidez,
                AutoUsuario = Sistema.Usuario.id,
                AutoTransporte = _gDatosDoc.EntidadTransporte.id,
                Situacion = "Transito",
                SignoTipoDoc = _gDatosDoc.EntidadTipoDoc.signo,
                SiglasTipoDoc = _gDatosDoc.EntidadTipoDoc.siglas,
                Tarifa = _gDatosDoc.EntidadCliente.tarifa,
                TipoRemision = _gDatosDoc.GetData.RemisionCodTipoDoc,
                DocumentoRemision = _gDatosDoc.GetData.RemisionNumDoc,
                AutoRemision = _gDatosDoc.GetData.RemisionAutoDoc,
                NombreTipoDoc = _gDatosDoc.EntidadTipoDoc.descripcion,
                SubTotalImpuesto = MontoImpuesto,
                SubTotal = subTotal,
                TipoCliente = "",
                Planilla = "",
                Expendiente = "",
                AnticipoIva = 0.0m,
                TercerosIva = 0.0m,
                Neto = netoMonto,
                Costo = costoMonto,
                Utilidad = utilidadMonto,
                Utilidadp = 100 - (costoMonto / netoMonto * 100),
                TipoTipoDoc = _gDatosDoc.EntidadTipoDoc.tipo,
                CiTitular = "",
                NombreTitular = "",
                CiBeneficiario = "",
                NombreBeneficiario = "",
                Clave = "",
                DenominacionFiscal = "No Contribuyente",
                Cambio = montoCambio,
                EstatusValidado = "0",
                Cierre = "",
                EstatusCierreContable = "0",
                CierreFtp = "",
                Prefijo = _gDatosDoc.EntidadSucursal.cod,
            };
            fichaOOB.Encabezado = encabezado;
            var detalles = _gItems.ListaItems.Select(s =>
            {
                var nr = new OOB.Documento.Agregar.Pedido.FichaDetalle()
                {
                    AutoProducto = s.DataItem.autoProducto,
                    Codigo = s.DataItem.codigoProducto,
                    Nombre = s.DataItem.nombreProducto,
                    AutoDepartamento = s.DataItem.autoDepartamento,
                    AutoGrupo = s.DataItem.autoGrupo,
                    AutoSubGrupo = s.DataItem.autoSubGrupo,
                    AutoDeposito = s.DataItem.autoDeposito,
                    Cantidad = s.DataItem.cantidad,
                    Empaque = s.DataItem.empaqueDesc,
                    PrecioNeto = s.DataItem.precioNeto,
                    Descuento1p = s.DataItem.dsctoPorct,
                    Descuento2p = 0.0m,
                    Descuento3p = 0.0m,
                    Descuento1 = s.DsctoMonto,
                    Descuento2 = 0.0m,
                    Descuento3 = 0.0m,
                    CostoVenta = s.CostoVenta,
                    TotalNeto = s.Importe,
                    Tasa = s.DataItem.tasaIva,
                    Impuesto = s.MIva,
                    Total = s.mTotal,
                    EstatusAnulado = "0",
                    Tipo = _gDatosDoc.EntidadTipoDoc.codigo,
                    Deposito = _gDatosDoc.EntidadDeposito.desc,
                    Signo = _gDatosDoc.EntidadTipoDoc.signo,
                    PrecioFinal = s.PrecioFinal,
                    AutoCliente = _gDatosDoc.EntidadCliente.id,
                    Decimales = s.DataItem.decimalesProducto,
                    ContenidoEmpaque = s.DataItem.empaqueCont,
                    CantidadUnd = s.DataItem.cantidadUnd,
                    PrecioUnd = s.PrecioFinal,
                    CostoUnd = s.DataItem.costoUnd,
                    Utilidad = s.Utilidad,
                    Utilidadp = s.UtilidadP,
                    PrecioItem = s.PrecioItem,
                    EstatusGarantia = "0",
                    EstatusSerial = "0",
                    CodigoDeposito = _gDatosDoc.EntidadDeposito.cod,
                    DiasGarantia = 0,
                    Detalle = s.Notas,
                    PrecioSugerido = 0.0m,
                    AutoTasa = s.DataItem.autoTasaIva,
                    EstatusCorte = "0",
                    X = 0m,
                    Y = 0m,
                    Z = 0m,
                    Corte = "",
                    Categoria = s.DataItem.categroiaProducto,
                    Cobranzap = 0.0m,
                    Ventasp = 0.0m,
                    CobranzapVendedor = 0.0m,
                    VentaspVendedor = 0.0m,
                    Cobranza = 0.0m,
                    Ventas = 0.0m,
                    CobranzaVendedor = 0.0m,
                    VentasVendedor = 0.0m,
                    CostoPromedioUnd = s.DataItem.costoPromdUnd,
                    CostoCompra = s.DataItem.costoPromd,
                    EstatusChecked = "1",
                    Tarifa = s.DataItem.tarifaPrecio,
                    TotalDescuento = s.DsctoMontoTotal,
                    CodigoVendedor = _gDatosDoc.EntidadVendedor.cod,
                    AutoVendedor = _gDatosDoc.EntidadVendedor.id,
                    CierreFtp = "",
                };
                return nr;
            }).ToList();
            fichaOOB.Detalles = detalles;
            fichaOOB.VentaTemporal = new OOB.Documento.Agregar.Pedido.FichaTemporalVenta() { id = _idVentaTemporal };
            fichaOOB.ItemDepositoBloquear = _gItems.ListaItems.Where(w=>w.DataItem.estatusRemision=="1" && w.DataItem.estatusReservaMerc=="").Select(s =>
            {
                var nr = new OOB.Documento.Agregar.Pedido.FichaItemDepositoBloquear()
                {
                    autoDeposito = s.DataItem.autoDeposito ,
                    autoProducto = s.IdProducto,
                    cntUnd = s.CantUnd,
                    depDescripcion = s.DataItem.nombreDeposito,
                    prdDescripcion = s.DescripcionPrd,
                };
                return nr;
            }).ToList();
            fichaOOB.ValidarRupturaPorExistencia = _rupturaPorExistencia;

            var r02 = Sistema.MyData.Documento_Agregar_Pedido(fichaOOB);
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r02.Mensaje);
                return;
            }
            _documentoProcesadoIsOk = true;
            IniciarDocumentoNuevo();
        }

        private void IniciarDocumentoNuevo()
        {
            _idVentaTemporal = -1;
            _gDatosDoc.Limpiar();
            _gItems.LimpiarItems();
            ActualizarTasaDivisa();
        }

    }

}