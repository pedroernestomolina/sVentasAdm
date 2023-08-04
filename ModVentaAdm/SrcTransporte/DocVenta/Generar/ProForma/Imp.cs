using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.ProForma
{
    public class Imp: ImpGenerar, IProForma
    {
        public override string TipoDocumento_Get { get { return "PRO - FORMA"; } }


        public Imp()
            :base()
        {
        }

        protected override void GuardarDoc()
        {
            try
            {
                var _idCliente = Ficha.DatosDoc.Cliente.id;
                var _cirif = Ficha.DatosDoc.Cliente.ciRif;
                var _codCliente = Ficha.DatosDoc.Cliente.codigo;
                var _dirFiscalCliente = Ficha.DatosDoc.Cliente.dirFiscal;
                var _razonSocial = Ficha.DatosDoc.Cliente.razonSocial;
                var _telefonoCliente = Ficha.DatosDoc.Cliente.telefono1;
                var _docModuloCargar = Ficha.DatosDoc.ModuloCargar_Get;
                var _docSolicitadoPor = Ficha.DatosDoc.SolicitadoPor_Get;
                //
                var _idVendedor = "0000000001";
                var s01 = Sistema.MyData.Sistema_Vendedor_Entidad_GetById(_idVendedor);
                var _codVendedor = s01.Entidad.codigo;
                var _nombreVendedor = s01.Entidad.nombre;
                //
                var _idSitemaDocumento = Sistema.Id_SistDocumento_NotaEntrega;
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
                var _nombreUsuario = Sistema.Usuario.nombre;
                //
                var _cntRenglones = Ficha.Items.Cnt_Get;
                var _diasValidez = Ficha.DatosDoc.DiasValidez_Get;
                var _diasCredito = Ficha.DatosDoc.DiasCredito_Get;
                var _factorCambio = (decimal)Ficha.TasaDivisa_Get;
                var _condPago = Ficha.DatosDoc.CondPago_Get.Trim().ToUpper();
                var _estacion = Sistema.EquipoEstacion;
                //
                var _tasa1 = 0m;
                var _tasa2 = 0m;
                var _tasa3 = 0m;
                _tasa1 = Ficha.TasasFiscal_1.tasa;
                _tasa2 = Ficha.TasasFiscal_2.tasa;
                _tasa3 = Ficha.TasasFiscal_3.tasa;
                //
                var _montoExento = Ficha.Totales.MontoExento_Get;
                var _montoBase1 = Ficha.Totales.MontoBase1_Get;
                var _montoBase2 = Ficha.Totales.MontoBase2_Get;
                var _montoBase3 = Ficha.Totales.MontoBase3_Get;
                var _montoIva1 = Ficha.Totales.MontoIva1_Get;
                var _montoIva2 = Ficha.Totales.MontoIva2_Get;
                var _montoIva3 = Ficha.Totales.MontoIva3_Get;
                var _neto = _montoExento + _montoBase1 + _montoBase2 + _montoBase3;
                var _montoBase = _montoBase1 + _montoBase2 + _montoBase3;
                var _montoIva = _montoIva1 + _montoIva2 + _montoIva3;
                var _montoDivisa = Ficha.Totales.MontoTotal_MonedaDivisa_Get;
                var _montoTotal = Ficha.Totales.MontoTotal_MonedaActual_Get;
                //
                var _subTotalNeto = Ficha.Totales.SubTotalNeto_Get; //ANTES DE LOS CARGOS /DESCEUNTOS
                var _subTotal = _montoTotal - _montoIva;
                var _subTotalImpuesto = _montoIva;
                //
                var _idDocRemision = Remision.DocRemision.docId;
                var _numDocRemision = Remision.DocRemision.docNumero;
                var _tipoDocRemision = Remision.DocRemision.docTipo;
                //
                var _notasObs = NotasObserv_Get;
                //
                var _docRef = Ficha.Items.GetItems.Where(w=>w.Item.IsItemPresupuesto).Select(s => 
                {
                    var nr = new OOB.Transporte.Documento.Agregar.Factura.FichaDocRef()
                    {
                        codigoDoc = s.Item.Get_ItemPresupuesto.Ficha.docCodigo,
                        fechaDoc = s.Item.Get_ItemPresupuesto.DocFecha,
                        idDoc = s.Item.Get_ItemPresupuesto.DocId,
                        montoDivisaDoc = s.Item.Get_ItemPresupuesto.Monto,
                        numDoc = s.Item.Get_ItemPresupuesto.DocNumero,
                        tipoDoc = s.Item.Get_ItemPresupuesto.DocTipo,
                    };
                    return nr;
                }).ToList();
                //
                var _serieDoc = "NEN";
                var s05 = Sistema.MyData.Sistema_Serie_GetIdByNombre(_serieDoc);
                var _serieId = s05.Entidad;
                //
                var _aliados = new List<OOB.Transporte.Documento.Agregar.Factura.FichaAliadoResumen>();
                //
                var itemsDet = Ficha.Items.GetItems.Select(s =>
                {
                    var _idDocRef = "";
                    var _numDocRef = "";
                    var _codDocRef = "";
                    var _montoDocRef = 0m;
                    var _fecDocRef = DateTime.Now.Date;
                    var _tipoItemProcedencia = "";
                    OOB.Transporte.Documento.Agregar.Presupuesto.FichaDetalle _servdetalle=null;
                    if (s.Item.IsItemPresupuesto)
                    {
                        _idDocRef = s.Item.Get_ItemPresupuesto.Ficha.docId;
                        _numDocRef = s.Item.Get_ItemPresupuesto.Ficha.docNumero;
                        _codDocRef = s.Item.Get_ItemPresupuesto.Ficha.docCodigo;
                        _montoDocRef = s.Item.Get_ItemPresupuesto.Ficha.docMontoMonedaDiv;
                        _fecDocRef = s.Item.Get_ItemPresupuesto.Ficha.docFechaEmision;
                        _tipoItemProcedencia = "P";
                    }
                    if (s.Item.Get_ItemServicio!=null) 
                    {
                        _tipoItemProcedencia = "S";
                        _servdetalle = new OOB.Transporte.Documento.Agregar.Presupuesto.FichaDetalle()
                        {
                            alicuotaDesc = s.Item.Get_ItemServicio.Item.Get_Alicuota.desc,
                            alicuotaId = s.Item.Get_ItemServicio.Item.Get_Alicuota.id,
                            alicuotaTasa = s.Item.Get_ItemServicio.Item.Get_Alicuota.tasa,
                            cntDias = s.Item.Get_ItemServicio.Item.Get_CntDias,
                            cntUnidades = s.Item.Get_ItemServicio.Item.Get_CntUnidades,
                            dscto = s.Item.Get_ItemServicio.Item.Get_Dscto,
                            estatusAnulado = "0",
                            notas = s.Item.Get_ItemServicio.Item.Get_DescripcionFull,
                            precioNetoDivisa = s.Item.Get_ItemServicio.Item.Get_PrecioDivisa,
                            servicioDesc = s.Item.Get_ItemServicio.Item.Get_TipoServicio.descripcion,
                            signoDoc = _signoDocumento,
                            tipoDoc = _tipoDcumento,
                            importe = s.Item.Get_ItemServicio.Item.Get_Importe,
                            servicioCodigo = s.Item.Get_ItemServicio.Item.Get_TipoServicio.codigo,
                            servicioDetalle = s.Item.Get_ItemServicio.Item.Get_Descripcion,
                            servicioId = s.Item.Get_ItemServicio.Item.Get_TipoServicio.id,
                            unidadesDesc = s.Item.Get_ItemServicio.Item.Get_UnidadesDetall,
                            fechas = s.Item.Get_ItemServicio.Item.Get_Fechas.Select(ss =>
                            {
                                var nr2 = new OOB.Transporte.Documento.Agregar.Presupuesto.Fecha()
                                {
                                    fecha = ss.fechaServ.Date,
                                    hora = ss.horaServ.ToShortTimeString(),
                                    nota = "",
                                };
                                return nr2;
                            }).ToList(),
                            aliados = s.Item.Get_ItemServicio.Item.Get_ListaAliadosLLamados.Select(xx =>
                            {
                                var _aliadoRes = new OOB.Transporte.Documento.Agregar.Factura.FichaAliadoResumen()
                                {
                                    idAliado = xx.aliado.id,
                                    montoDivisa = xx.Importe,
                                };
                                _aliados.Add(_aliadoRes);
                                //
                                var nr3 = new OOB.Transporte.Documento.Agregar.Presupuesto.Aliado()
                                {
                                    ciRif = xx.aliado.ciRif,
                                    cntDias = xx.cnt,
                                    codigo = xx.aliado.codigo,
                                    desc = xx.aliado.nombreRazonSocial,
                                    id = xx.aliado.id,
                                    importe = xx.Importe,
                                    precioUnitDivisa = xx.precio,
                                };
                                return nr3;
                            }).ToList(),
                        };
                    }
                    var ni = new OOB.Transporte.Documento.Agregar.Factura.FichaItem()
                    {
                        alicuotaDesc = s.Item.Get_Alicuota.desc,
                        alicuotaId = s.Item.Get_Alicuota.id,
                        alicuotaTasa = s.Item.Get_Alicuota.tasa,
                        cntDias = s.Item.Get_Cnt,
                        detalle = s.Item.Get_Descripcion,
                        precioNetoMonDivisa = s.Item.Get_PrecioDivisa,
                        dsctoMontoMonDivisa = s.Item.Get_DsctoMontoDivisa,
                        dsctoPorc = s.Item.Get_Dscto,
                        precioItemMonDivisa = s.Item.Get_PrecioItemDivisa,
                        importeNetoMonDivisa = s.Item.Get_Cnt * s.Item.Get_PrecioDivisa,
                        impuestoMonDivisa = s.Item.Get_Iva,
                        importeTotalMonDivisa = s.Item.Get_Importe,
                        totalMonDivisa = s.Item.Get_Importe + s.Item.Get_Iva,
                        idDocRef = _idDocRef,
                        codigoDocRef = _codDocRef,
                        fechaDocRef = _fecDocRef,
                        montoDocRef = _montoDocRef,
                        numDocRef = _numDocRef,
                    };
                    ni.precioNetoMonLocal = ni.precioNetoMonDivisa * _factorCambio;
                    ni.importeNetoMonLocal = ni.importeNetoMonDivisa * _factorCambio;
                    ni.dsctoMontoMonLocal = ni.dsctoMontoMonDivisa * _factorCambio;
                    ni.precioItemMonLocal = ni.precioItemMonDivisa * _factorCambio;
                    ni.impuestoMonLocal = ni.impuestoMonDivisa * _factorCambio;
                    ni.importeTotalMonLocal = ni.importeTotalMonDivisa * _factorCambio;
                    ni.totalMonLocal = ni.totalMonDivisa * _factorCambio;
                    ni.precioFinalMonDivisa = ni.precioItemMonDivisa;
                    ni.precioFinalMonLocal = ni.precioFinalMonDivisa * _factorCambio;
                    ni.servDetalle = _servdetalle;
                    ni.tipoItemProcedencia = _tipoItemProcedencia;
                    return ni;
                }).ToList();
                //
                foreach (var rg in _docRef)
                {
                    var s04 = Sistema.MyData.TransporteDocumento_EntidadPresupuesto_GetAliadosById(rg.idDoc);
                    foreach (var xr in s04.ListaD)
                    {
                        var nr = new OOB.Transporte.Documento.Agregar.Factura.FichaAliadoResumen()
                        {
                            idAliado = xr.idAliado,
                            montoDivisa = xr.importe,
                        };
                        _aliados.Add(nr);
                    }
                }
                var _grupoAliados = _aliados.GroupBy(g => g.idAliado).Select(s => new { key = s.Key, lst = s.ToList() }).ToList();
                //
                var fichaOOB = new OOB.Transporte.Documento.Agregar.Factura.Ficha()
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
                    docRemision = _numDocRemision,
                    estacion = _estacion,
                    factorCambio = _factorCambio,
                    idCliente = _idCliente,
                    idRemision = _idDocRemision,
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
                    tipoRemision = _tipoDocRemision,
                    Total = _montoTotal,
                    usuario = _nombreUsuario,
                    vendedor = _nombreVendedor,
                    nota = _notasObs,
                    docModuloCargar = _docModuloCargar,
                    docSolicitadoPor = _docSolicitadoPor,
                    serieDocDesc = _serieDoc,
                    serieDocId = _serieId,
                    subTotalMonDivisa = 0m,
                    items=itemsDet,
                    tipoDocSiglas="PRF",
                    aliadosResumen = _grupoAliados.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.Agregar.Factura.FichaAliadoResumen()
                        {
                            idAliado = s.key,
                            montoDivisa = s.lst.Sum(tt => tt.montoDivisa),
                        };
                        return nr;
                    }).ToList(),
                    docRef = _docRef,
                };
                var r01 = Sistema.MyData.TransporteDocumento_AgregarFactura(fichaOOB);
                _procesarIsOK = true;
                Helpers.Msg.AgregarOk();
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}