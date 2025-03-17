using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler
{
    public class HndPago : PanelPrincipal.Pago.IPago
    {
        private string _idClientePagar;
        private DateTime _fechaServidor;
        private decimal _factorDivActual;
        private bool _pagoExitoso;
        private Utils.Control.Boton.Abandonar.IAbandonar _abandonar;
        private Utils.Control.Boton.Procesar.IProcesar _procesar;
        private PanelPrincipal.Pago.IPanelMetPago _panMetPago;
        private PanelPrincipal.Pago.IPanelCtas _panCtas;
        private PanelPrincipal.Pago.IPanelCtasNtCred _panNtCred;
        private PanelPrincipal.Pago.IPanelResumen _panResumen;
        private PanelPrincipal.Pago.IPanelCliente _panCliente;
        private OOB.CxC.CargarData.Cliente.Ficha _entidadFichaPagar;
        private PanelPrincipal.Pago.IDetallePago _detallePago;
        private string _autoReciboGenerar;
        //
        public string GetAutoReciboGenerar { get { return _autoReciboGenerar; } }
        public Object GetEntidadPagar { get { return _panCliente.GetEntidadPagar; } }
        //
        public HndPago()
        {
            _autoReciboGenerar="";
            _clientPag = "";
            _entidadFichaPagar = null;
            _abandonar = new Utils.Control.Boton.Abandonar.Imp();
            _procesar = new Utils.Control.Boton.Procesar.Imp();
            _panMetPago = new Met.HndPanelMetPago();
            _panCtas = new Ctas.HndPanelCtas();
            _panNtCred = new NtCred.HndPanelNtCred();
            _panResumen = new Resumen.HndPanelResumen();
            _panCliente = new Cliente.HndPanelCliente();
            _detallePago = new DetallePago.HndDetallePago();
        }
        public void Inicializa()
        {
            _autoReciboGenerar = "";
            _clientPag = "";
            _entidadFichaPagar = null;
            _pagoExitoso = false;
            _fechaServidor = DateTime.Now.Date;
            _factorDivActual = 0m;
            _abandonar.Inicializa();
            _procesar.Inicializa();
            _panMetPago.Inicializa();
            _panCtas.Inicializa();
            _panNtCred.Inicializa();
            _panResumen.Inicializa();
            _panCliente.Inicializa();
            _detallePago.Inicializa();
        }
        private PanelPrincipal.Pago.vistas.vPago frm;
        public void Inicia()
        {
            if (cargarData())
            {
                if (frm == null)
                {
                    frm = new PanelPrincipal.Pago.vistas.vPago();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setIdEntidadPagar(object id)
        {
            _idClientePagar = (string)id;
            _panCtas.setIdEntidad(id);
            _panNtCred.setIdEntidad(id);
            _panCliente.setIdEntidad(id);
        }
        //
        private bool cargarData()
        {
            try
            {
                var r01 = Sistema.MyData.Configuracion_FactorDivisa();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                setFactorDivisa(r01.Entidad);
                var r02 = Sistema.MyData.FechaServidor();
                if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r02.Mensaje);
                }
                setFechaServidor(r02.Entidad);
                if (_idClientePagar == "") throw new Exception("CLIENTE NO SELECCIONADO");
                var r03 = Sistema.MyData.CxC_CapturarData_Cliente_ById(_idClientePagar);
                if (r03.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r03.Mensaje);
                }
                _entidadFichaPagar = r03.Entidad;
                //
                _panMetPago.setFactorDivisa(r01.Entidad);
                _panCtas.setFechaServidor(r02.Entidad);
                _panNtCred.setFechaServidor(r02.Entidad);
                _panCliente.setFechaServidor(r02.Entidad);
                _panCliente.setFichaEntidadPagar(r03.Entidad);
                _panNtCred.setMontoNtCredDisponible(r03.Entidad.montoNtCreditoDisponible);
                _panCliente.setMontoAnticiposDisponible(r03.Entidad.montoAnticiposClient);
                //
                setClientePagar(_entidadFichaPagar.ciRifClient + System.Environment.NewLine + _entidadFichaPagar.nombreRazonSocialClient);
                //
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private void setFechaServidor(DateTime fech)
        {
            _fechaServidor = fech;
        }
        private void setFactorDivisa(decimal factor)
        {
            _factorDivActual = factor;
        }
        //
        public bool AbandonarFichaIsOk { get { return _abandonar.OpcionIsOK; } }
        public void AbandonarFicha()
        {
            _abandonar.Opcion();
        }
        //
        public bool IsPagoExitoso { get { return _pagoExitoso; } }
        public void ProcesarPago()
        {
            if (_panResumen.GetResumenMontoAbono == 0m) 
            {
                Helpers.Msg.Alerta("NO HAY MOVIMIENTOS QUE REGISTRAR");
                return;
            }
            if (_panResumen.GetResumenSaldo < 0)
            {
                Helpers.Msg.Alerta("MONTO DE PAGO INSUFICIENTE");
                return;
            }
            if (((_panResumen.GetResumenMontoAnticipo+_panResumen.GetResumenMontoNtCredito)>0m) && (_panResumen.GetResumenMontoCtasPend==0m))
            {
                Helpers.Msg.Alerta("ANTICIPOS Y/O NOTAS DE CREDITO SOLO SE USAN COMO METODOS DE PAGO");
                return;
            }
            _pagoExitoso = false;
            _detallePago.Inicializa();
            _detallePago.setFechaProceso(_fechaServidor);
            _detallePago.Inicia();
            if (_detallePago.ProcesarIsOK)
            {
                var _seg = true;
                if (_panResumen.GetResumenSaldo > 0)
                {
                    _seg = Helpers.Msg.ProcesarGuardar("SE VA HA GENERAR UN ANTICIPO AL CLIENTE, ESTAS DE ACUERDO ?");
                }
                if (_seg)
                {
                    guardarPago();
                }
            }

        }
        //
        private void guardarPago()
        {
            try
            {
                _autoReciboGenerar = "";
                var fichaOOB = (OOB.CxC.GestionCobro.Ficha)FichaCobro();
                var rt1 = Sistema.MyData.CxC_GestionCobro_Agregar(fichaOOB);
                if (rt1.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(rt1.Mensaje);
                }
                _pagoExitoso = true;
                _autoReciboGenerar = rt1.Entidad;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
            return;
        }

        //PANEL: MET
        public string GetCntMetRecibido { get { return _panMetPago.GetCntMetRecibido; } }
        public decimal GetMontoRecibido { get { return _panMetPago.GetMontoRecibido; } }
        public void AgregarMetPago()
        {
            _panMetPago.AgregarMetPago();
            _panResumen.setMontoMetPago(_panMetPago.GetMontoRecibido);
        }
        public void ListarMetPago()
        {
            _panMetPago.ListarMetPago();
            _panResumen.setMontoMetPago(_panMetPago.GetMontoRecibido);
        }

        //PANEL: CTAS
        public int GetCntCtasPagar { get { return _panCtas.GetCntCtasPagar; } }
        public decimal GetMontoCtasPagar { get { return _panCtas.GetMontoPagar; } }
        public void ListarCtasPagar()
        {
            _panCtas.setMontoAbonadoASaldar(_panResumen.GetResumenMontoAbono);
            _panCtas.ListarCtasPagar();
            _panResumen.setMontoCtasPend(_panCtas.GetMontoPagar);
        }

        //PANEL: NOTAS_CREDITO
        public int GetCntNtCred { get { return _panNtCred.GetCntCtasPagar; } }
        public decimal GetMontoNtCred { get { return _panNtCred.GetMontoPagar; } }
        public decimal GetMontoNtCredDisponible { get { return _panNtCred.GetMontoNtCredDisponible; } }
        public void ListarNtCred()
        {
            _panNtCred.ListarCtasPagar();
            _panResumen.setMontoNtCredito(_panNtCred.GetMontoPagar);
        }

        //PANEL: RESUMEN
        public decimal GetResumenMontoAnticipo { get { return _panResumen.GetResumenMontoAnticipo; } }
        public decimal GetResumenMontoMetPago { get { return _panResumen.GetResumenMontoMetPago; } }
        public decimal GetResumenMontoNtCredito { get { return _panResumen.GetResumenMontoNtCredito; } }
        public decimal GetResumenMontoAbono { get { return _panResumen.GetResumenMontoAbono; } }
        public decimal GetResumenMontoCtasPend { get { return _panResumen.GetResumenMontoCtasPend; } }
        public decimal GetResumenSaldo { get { return _panResumen.GetResumenSaldo; } }
        public string GetResumenSaldoDesc { get { return _panResumen.GetResumenSaldoDesc; } }

        //PANEL: ANTICIPO
        public decimal GetMontoAnticipo { get { return _panCliente.GetMontoPagar; } }
        public decimal GetMontoAnticipoDisponible { get { return _panCliente.GetMontoAnticipoDisponible; } }
        public void AgregarAnticipo()
        {
            _panCliente.AgregarAnticipo();
            _panResumen.setMontoAnticipo(_panCliente.GetMontoPagar);
        }


        //
        private string _clientPag;
        public string GetCliente { get { return _clientPag; } }
        public void setClientePagar(string dat)
        {
            _clientPag = dat;
            _panCtas.setClientePagar(dat);
            _panNtCred.setClientePagar(dat);
        }

        //
        private object FichaCobro()
        {
            var _cobrador = (LibUtilitis.Opcion.IData)_detallePago.GetCobrador;
            var _notaRecibo = _detallePago.GetNotas;
            var _fechaRecibo = _detallePago.GetFechaProceso;
            var _sucPrefijo = Sistema.Sucursal.codigo;
            var _entidadPagar = (OOB.CxC.CargarData.Cliente.Ficha)GetEntidadPagar;
            var _idUsu = Sistema.Usuario.id;
            var _metPag = _panMetPago.GetListaMetPago.Select(s =>
            {
                var _met = new OOB.CxC.GestionCobro.FichaMetodoPago()
                {
                    AutoCobrador = _cobrador.id,
                    AutoMedioPago = s.MetCobro.id,
                    AutoUsuario = _idUsu,
                    Cierre = "",
                    Codigo = s.MetCobro.codigo,
                    Lote = s.Lote,
                    Medio = s.MetCobro.desc,
                    MontoRecibido = s.Monto,
                    OpAplicaConversion = s.AplicaFactor ? "1" : "0",
                    OpBanco = s.Banco,
                    OpDetalle = s.DetalleOp,
                    OpFecha = s.FechaOp,
                    OpMonto = s.ImporteMonDiv,
                    OpNroCta = s.NroCta,
                    OpNroRef = s.CheqRefTranf,
                    OpTasa = s.FactorCambio,
                    Referencia = s.Referencia,
                };
                return _met;
            }).ToList();
            var _id = 1;
            var _docPend = _panCtas.GetListaDocPagar.Select(s =>
            {
                var ss = (Ctas.itemCtaPend)s;
                var sss = (OOB.CxC.DocumentosPend.Ficha)ss.Ficha;
                var _doc = new OOB.CxC.GestionCobro.FichaDocumento()
                {
                    AutoCxC = sss.autoDoc,
                    DocumentoNro = sss.numeroDoc,
                    EstatusDocCancelado = s.montoAbonar == s.montoResta ? "1" : "0",
                    Id = _id,
                    Importe = Math.Round(s.montoAbonar * s.tasaCambioDoc, 2, MidpointRounding.AwayFromZero),
                    ImporteDivisa = s.montoAbonar,
                    Notas = s.detalleAbono,
                    TipoDocumento = sss.tipoDoc,
                };
                _id += 1;
                return _doc;
            }).ToList();
            var _docNtCred = _panNtCred.GetListaDocPagar.Select(s =>
            {
                var ss = (NtCred.itemCtaPend)s;
                var sss = (OOB.CxC.DocumentosPend.Ficha)ss.Ficha;
                var _doc = new OOB.CxC.GestionCobro.FichaDocumento()
                {
                    AutoCxC = sss.autoDoc,
                    DocumentoNro = sss.numeroDoc,
                    EstatusDocCancelado = s.montoAbonar == s.montoResta ? "1" : "0",
                    Id = _id,
                    Importe = Math.Round(s.montoAbonar * s.tasaCambioDoc, 2, MidpointRounding.AwayFromZero),
                    ImporteDivisa = s.montoAbonar,
                    Notas = s.detalleAbono,
                    TipoDocumento = sss.tipoDoc,
                };
                _id += 1;
                return _doc;
            }).ToList();
            foreach (var doc in _docNtCred)
            {
                _docPend.Add(doc);
            }
            var _recibo = new OOB.CxC.GestionCobro.FichaRecibo()
            {
                AutoCliente = _entidadFichaPagar.idClient,
                AutoCobrador = _cobrador.id,
                AutoUsuario = Sistema.Usuario.id,
                Cambio = 0m,
                CambioDivisa = 0m,
                CiRif = _entidadFichaPagar.ciRifClient,
                Cliente = _entidadFichaPagar.nombreRazonSocialClient,
                Cobrador = _cobrador.desc,
                Codigo = _entidadFichaPagar.codigoClient,
                CodigoCobrador = _cobrador.codigo,
                Direccion = _entidadFichaPagar.dirFiscalClient,
                Importe = _panCtas.GetMontoPagar,
                ImporteDivisa = _panCtas.GetMontoPagar,
                MontoRecibido = _panResumen.GetResumenMontoAbono,
                MontoRecibidoDivisa = _panResumen.GetResumenMontoAbono,
                Nota = _notaRecibo,
                Telefono = _entidadFichaPagar.telefonoClient,
                Usuario = Sistema.Usuario.nombre,
                MontoRetencionDiv = 0m,
            };
            var _cobro = new OOB.CxC.GestionCobro.FichaCobro()
            {
                AutoCliente = _entidadFichaPagar.idClient,
                AutoVendedor = _entidadFichaPagar.idVendedor,
                CiRif = _entidadFichaPagar.ciRifClient,
                Cliente = _entidadFichaPagar.nombreRazonSocialClient,
                CodigoCliente = _entidadFichaPagar.codigoClient,
                Importe = Math.Round(_panResumen.GetResumenMontoAbono * _factorDivActual, 2, MidpointRounding.AwayFromZero),
                MontoDivisa = _panResumen.GetResumenMontoAbono,
                Nota = "",
                TasaDivisa = _factorDivActual,
            };
            var oob = new OOB.CxC.GestionCobro.Ficha()
            {
                SucPrefijo = _sucPrefijo,
                Cobro = _cobro,
                Recibo = _recibo,
                Documentos = _docPend,
                MetodosPago = _metPag,
                notaAdm = null,
                retencion = null,
                cajas = null,
                autoCliente = _idClientePagar,
                montoAnticipoDescargar = _panResumen.GetResumenMontoAnticipo,
                factorCambio = _factorDivActual,
                montoRecibido = _panResumen.GetResumenMontoAbono,
                fechaProceso = _fechaRecibo,
                montoAnticipoCargar = _panResumen.GetResumenSaldo,
            };
            return oob;
        }

        //
        public void LimpiarData()
        {
            _autoReciboGenerar = "";
            _idClientePagar = "";
            _panCtas.LimpiarData();
            _panNtCred.LimpiarData();
            _detallePago.LimpiarData();
        }

        //
        public void GenerarRecibo(string autoRecibo)
        {
            ModVentaAdm.SrcTransporte.ToolsCxC.IRepPlanilla _rep = new ModVentaAdm.SrcTransporte.ToolsCxC.Reportes.Planilla.Imp();
            _rep.setItemCargar(autoRecibo);
            _rep.Generar();
        }
    }
}