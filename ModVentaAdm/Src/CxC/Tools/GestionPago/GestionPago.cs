using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago
{
    
    public class GestionPago: IGestionPago 
    {


        private bool _abandonarIsOK;
        private bool _procesarPago; 
        private string _idCliente;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;
        private ListaGestionPago.ILista _gListaDoc;
        private Cliente.Visualizar.IVisualizar _gVistaCliente;
        private Reportes.ListaDocPend.IRepDocPend _gRepDocPend;
        private MontoAbonar.IMontoAbonar _gMontoAbonar;
        private MediosCobro.IMedCobro _gMedCobro;
        private DetalleCobro.IDetalle _gDetalleCobro;
        private decimal _factorDivisa;


        public BindingSource DocPendGetSource { get { return _gListaDoc.DocPendGetSource; } }
        public ListaGestionPago.data ItemActual { get { return _gListaDoc.ItemActual; } }
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public int GetCantItemsSeleccionados { get { return _gListaDoc.CantItemsSeleccionados; } }
        public decimal GetMontoResta { get { return _gListaDoc.MontoPendientePorCobrar; } }
        public decimal GetMontoAbonar { get { return _gListaDoc.MontoAbonar; } }
        public decimal GetMontoPend { get { return GetMontoResta - GetMontoAbonar; } }
        public int GetCantDoc { get { return _gListaDoc.CntItems; } }
        public string GetNotas { get { return ItemActual != null ? ItemActual.notasDoc : ""; } }
        public string GetClienteData
        {
            get 
            {
                var rt = "";
                if (_cliente != null) 
                {
                    rt += _cliente.ciRif + Environment.NewLine;
                    rt += _cliente.razonSocial + Environment.NewLine;
                    rt += _cliente.dirFiscal;
                }
                return rt;
            }
        }
        public bool ProcesarPagoIsOk { get { return _procesarPago; } }


        public GestionPago() 
        {
            _factorDivisa = 0m;
            _abandonarIsOK = false;
            _procesarPago = false;
            _idCliente="";
            _cliente = null;
            _gListaDoc = new ListaGestionPago.Lista();
            _gVistaCliente = new Cliente.Visualizar.Gestion();
            _gRepDocPend = new Reportes.ListaDocPend.RepDocPend();
            _gMontoAbonar = new MontoAbonar.MontoAbonar();
            _gMedCobro = new MediosCobro.MedCobro();
            _gDetalleCobro = new DetalleCobro.Detalle();
        }


        public void Inicializa()
        {
            _factorDivisa = 0m;
            _abandonarIsOK = false;
            _procesarPago = false;
            _idCliente = "";
            _cliente = null;
            _gListaDoc.Inicializa();
        }
        GestionPagoFrm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new GestionPagoFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        public void AbandonarFicha()
        {
            _abandonarIsOK = true;
        }
        public void setIdCliente(string id)
        {
            _idCliente = id;
        }



        private bool CargarData()
        {
            var rt = true;

            var filtroOOb = new OOB.CxC.DocumentosPend.Filtro()
            {
                idCliente = _idCliente,
            };
            var r00 = Sistema.MyData.Cliente_GetFicha(_idCliente);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r00.Mensaje);
                return false;
            }
            _cliente = r00.Entidad;
            var r01 = Sistema.MyData.CxC_DocumentosPend_GetLista(filtroOOb);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            var r02 = Sistema.MyData.Configuracion_FactorDivisa();
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r02.Mensaje);
                return false;
            }
            _factorDivisa = r02.Entidad;
            var lst= r01.ListaD.Select(s =>
            {
                var nr = new ListaGestionPago.data()
                {
                    acumuladoDoc = s.acumuladoDoc,
                    autoDoc = s.autoDoc,
                    diasCreditoDoc = s.diasCreditoDoc,
                    fechaEmisionDoc = s.fechaEmisionDoc,
                    fechaVencDoc = s.fechaVencDoc,
                    importeDoc = s.importeDoc,
                    notasDoc = s.notasDoc,
                    numeroDoc = s.numeroDoc,
                    serieDoc = s.serieDoc,
                    signoDoc = s.signoDoc,
                    tasaCambioDoc = s.tasaCambioDoc,
                    tipoDoc = s.tipoDoc,
                };
                return nr;
            }).ToList();
            _gListaDoc.setListaDocPend(lst.OrderBy(o => o.fechaEmisionDoc).ToList());

            return rt;
        }

        public void VerFichaCliente()
        {
            if (_cliente != null)
            {
                _gVistaCliente.Inicializa();
                _gVistaCliente.setFicha(_cliente);
                _gVistaCliente.Inicia();
            }
        }

        public void MarcarDesmarcarDoc()
        {
            MarcarItemPagar();
        }
        private void MarcarItemPagar()
        {
            if (ItemActual != null)
            {
                _gMontoAbonar.Inicializa();
                _gMontoAbonar.setData(Math.Abs(ItemActual.montoResta), Math.Abs(ItemActual.montoAbonar), ItemActual.DetalleAbono);
                _gMontoAbonar.Inicia();
                if (_gMontoAbonar.MontoAbonarIsOk)
                {
                    ItemActual.setActivarAbono(_gMontoAbonar.GetMontoAbonar * ItemActual.signoDoc, _gMontoAbonar.GetDetalle);
                    _gListaDoc.Refrescar();
                }
            }
        }
        public void LimpiarAbonoMarcado()
        {
            if (ItemActual != null)
            {
                if (ItemActual.isPagarOk)
                {
                    var xmsg = "Estas Seguro de Eliminar/Limpiar Abono ?";
                    var msg = MessageBox.Show(xmsg, "*** Alerta ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (msg == DialogResult.Yes)
                    {
                        ItemActual.setEliminarAbono();
                        _gListaDoc.Refrescar();
                    }
                }
            }
        }
        public void AplicarPagos()
        {
            _procesarPago = false;
            if (GetMontoAbonar < 0) 
            {
                Helpers.Msg.Error("MONTO A ABONAR ESTA INCORRECTO, VERIFIQUE POR FAVOR");
                return;
            }
            if (GetMontoAbonar == 0m && GetCantItemsSeleccionados > 0) // PAGO CON NOTA DE CREDITO
            {
                _gDetalleCobro.Inicializa();
                _gDetalleCobro.Inicia();
                if (_gDetalleCobro.DetalleIsOk)
                {
                    ProcesarCobro();
                    if (_procesarPago)
                    {
                        CargarData();
                    }
                }
            }
            if (GetMontoAbonar > 0m && GetCantItemsSeleccionados > 0) 
            {
                _gMedCobro.Inicializa();
                _gMedCobro.setMontoCobrar(GetMontoAbonar);
                _gMedCobro.Inicia();
                if (_gMedCobro.MedioCobroIsOk) 
                {
                    _gDetalleCobro.Inicializa();
                    _gDetalleCobro.Inicia();
                    if (_gDetalleCobro.DetalleIsOk)
                    {
                        ProcesarCobro();
                        if (_procesarPago) 
                        {
                            CargarData();
                        }
                    }
                }
            }
        }

        private void ProcesarCobro()
        {
            var _montoAbonarDivisa = GetMontoAbonar;
            var _montoAbonar = _gMedCobro.GetImporteMonedaLocal;
            var _tasaCambio = _factorDivisa;
            if (_montoAbonarDivisa > 0)
            {
                _tasaCambio = Math.Round(_montoAbonar / _montoAbonarDivisa, 2, MidpointRounding.AwayFromZero);
            }
            var _montoRecibidoDivisa= _gMedCobro.GetMontoRecibido;
            var _montoRecibido= Math.Round( _montoRecibidoDivisa*_tasaCambio,2, MidpointRounding.AwayFromZero);
            var _cambioDivisa= 0m;
            var _cambio= 0m;

            var rt1= Sistema.MyData.Sistema_Cobrador_GetFicha_ById(_gDetalleCobro.GetIdCobrador);
            if (rt1 .Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(rt1.Mensaje);
                return;
            }
            var _cobrador=rt1.Entidad;

            var cobroOOb = new OOB.CxC.GestionCobro.FichaCobro()
            {
                AutoCliente = _cliente.id,
                AutoVendedor = _cliente.idVendedor,
                CiRif = _cliente.ciRif,
                Cliente = _cliente.razonSocial,
                CodigoCliente = _cliente.codigo,
                Importe = _montoAbonar,
                MontoDivisa = _montoAbonarDivisa,
                Nota = "",
                TasaDivisa = _tasaCambio,
            };
            var reciboOOb = new OOB.CxC.GestionCobro.FichaRecibo()
            {
                AutoCliente = _cliente.id,
                AutoCobrador = _cobrador.id,
                AutoUsuario = Sistema.Usuario.id,
                Cambio = _cambio,
                CambioDivisa = _cambioDivisa,
                CiRif = _cliente.ciRif,
                Cliente = _cliente.razonSocial,
                Cobrador = _cobrador.nombre,
                Codigo = _cliente.codigo,
                CodigoCobrador = _cobrador.codigo,
                Direccion = _cliente.dirFiscal,
                Importe = _montoAbonar,
                ImporteDivisa = _montoAbonarDivisa,
                MontoRecibido = _montoRecibido,
                MontoRecibidoDivisa = _montoRecibidoDivisa,
                Nota = _gDetalleCobro.GetNotas,
                Telefono = _cliente.telefono1,
                Usuario = Sistema.Usuario.nombre,
            };
            var id=0;
            var documentosOOb = new List<OOB.CxC.GestionCobro.FichaDocumento>();
            foreach (var rg in _gListaDoc.ListaItemsSeleccionados)
            {
                id+=1;
                var nr = new OOB.CxC.GestionCobro.FichaDocumento()
                {
                    AutoCxC = rg.autoDoc,
                    DocumentoNro = rg.numeroDoc,
                    EstatusDocCancelado = rg.EstatusIsCancelado ? "1" : "0",
                    Id = id,
                    Importe = rg.montoAbonar * _tasaCambio,
                    ImporteDivisa = rg.montoAbonar,
                    TipoDocumento = rg.tipoDoc,
                    Notas= rg.DetalleAbono,
                };
                documentosOOb.Add(nr);
            }
            var _metodosCobOOb = new List<OOB.CxC.GestionCobro.FichaMetodoPago>();
            foreach (var rg in _gMedCobro.GetListaMedCobro) 
            {
                var nr = new OOB.CxC.GestionCobro.FichaMetodoPago()
                {
                    AutoCobrador = _cobrador.id,
                    AutoMedioPago = rg.item.GetMetodo.id,
                    AutoUsuario = Sistema.Usuario.id,
                    Cierre = "",
                    Codigo = rg.item.GetMetodo.codigo,
                    Lote = rg.item.GetLote,
                    Medio = rg.item.GetMetodo.desc,
                    MontoRecibido = rg.item.GetMonto,
                    OpAplicaConversion = rg.item.GetAplicaFactor ? "1" : "0",
                    OpBanco = rg.item.GetBanco,
                    OpDetalle = rg.item.GetDetalleOp,
                    OpFecha = rg.item.GetFechaOp,
                    OpMonto = rg.item.Importe,
                    OpNroCta = rg.item.GetNroCta,
                    OpNroRef = rg.item.GetCheqRefTranf,
                    OpTasa = rg.item.GetTasa,
                    Referencia = rg.item.GetReferencia,
                };
                _metodosCobOOb.Add(nr);
            };
            var saldoClienteOOb = new OOB.CxC.GestionCobro.FichaCliente()
            {
                idCliente = _cliente.id,
                monto = _montoAbonarDivisa,
            };

            OOB.CxC.GestionCobro.FichaNotaAdm _notaCrAdm = null;
            var _montoCambioDivisa = Math.Abs(_gMedCobro.GetMontoPend);
            var _montoCambio = Math.Round(_montoCambioDivisa * _tasaCambio, 2, MidpointRounding.AwayFromZero);
            if (_montoCambioDivisa >0m)
            {
                _notaCrAdm = new OOB.CxC.GestionCobro.FichaNotaAdm()
                {
                    autoCliente = _cliente.id,
                    autoVendedor = _cliente.idVendedor,
                    ciRifCliente = _cliente.ciRif,
                    codigoCliente = _cliente.codigo,
                    codSucursal = Sistema.Sucursal.codigo,
                    montoDivisaDoc = _montoCambioDivisa,
                    montoDoc = _montoCambio,
                    nombreCliente = _cliente.razonSocial,
                    notasDoc = "NOTA DE CREDITO A FAVOR DEL CLIENTE",
                    signoDoc = -1,
                    tasaCambioDoc = _tasaCambio,
                    tipoDoc = "NCR",
                };
            }
            var fichaOOb = new OOB.CxC.GestionCobro.Ficha()
            {
                SucPrefijo = Sistema.Sucursal.codigo,
                Cobro = cobroOOb,
                Documentos = documentosOOb,
                Recibo = reciboOOb,
                MetodosPago= _metodosCobOOb,
                saldoCliente = saldoClienteOOb,
                notaAdm=_notaCrAdm,
            };
            var rt2 = Sistema.MyData.CxC_GestionCobro_Agregar(fichaOOb);
            if (rt2.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Alerta(rt2.Mensaje);
                return;
            }
            _procesarPago = true;
        }

    }

}