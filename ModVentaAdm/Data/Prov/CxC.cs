using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{
    public partial class DataPrv : IData
    {
        public OOB.Resultado.Lista<OOB.CxC.Tools.CtasPendiente.Lista.Ficha> 
            CxC_Tool_CtasPendiente_GetLista(OOB.CxC.Tools.CtasPendiente.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.CxC.Tools.CtasPendiente.Lista.Ficha>();

            var filtroDTO = new DtoLibPos.CxC.Tools.CtasPendiente.Lista.Filtro()
            {
                codSucursal=filtro.codSucursal,
            };
            var r01 = MyData.CxC_Tool_CtasPendiente_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var lst = new List<OOB.CxC.Tools.CtasPendiente.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var _cntFactPend = 0;
                        if (s.cntFactPend.HasValue) 
                        {
                            _cntFactPend = s.cntFactPend.Value;
                        }
                        var nr = new OOB.CxC.Tools.CtasPendiente.Lista.Ficha()
                        {
                            idCliente = s.idCliente,
                            acumulado = s.acumulado,
                            ciRif = s.ciRif,
                            cntDocPend = s.cntDocPend,
                            cntFactPend = _cntFactPend,
                            importe = s.importe,
                            limiteFactPend = s.limiteFactPend,
                            limiteMontoCredito = s.limiteMontoCredito,
                            nombreRazonSocial = s.nombreRazonSocial,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.Ficha 
            CxC_Agregar(OOB.CxC.AgregarCta.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDto = new DtoLibPos.CxC.Agregar.Ficha()
            {
                autoCliente = ficha.autoCliente,
                autoVendedor = ficha.autoVendedor,
                ciRifCliente = ficha.ciRifCliente,
                codigoCliente = ficha.codigoCliente,
                codSucursal = ficha.codSucursal,
                diasCreditoDoc = ficha.diasCreditoDoc,
                fechaEmisionDoc = ficha.fechaEmisionDoc,
                montoDivisaDoc = ficha.montoDivisaDoc,
                nombreCliente = ficha.nombreCliente,
                notasDoc = ficha.notasDoc,
                numeroDoc = ficha.numeroDoc,
                serieDoc = ficha.serieDoc,
                signoDoc = ficha.signoDoc,
                tasaCambioDoc = ficha.tasaCambioDoc,
                tipoDoc = ficha.tipoDoc,
                fechaVencDoc = ficha.fechaVencDoc,
                montoDoc=ficha.montoDoc,
            };
            var r01 = MyData.CxC_Agregar(fichaDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Ficha
            CxC_AgregarNotaCreditoAdm(OOB.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDto = new DtoLibPos.CxC.AgregarNotaAdm.Ficha()
            {
                autoCliente = ficha.autoCliente,
                autoVendedor = ficha.autoVendedor,
                ciRifCliente = ficha.ciRifCliente,
                codigoCliente = ficha.codigoCliente,
                codSucursal = ficha.codSucursal,
                montoDivisaDoc = ficha.montoDivisaDoc,
                nombreCliente = ficha.nombreCliente,
                notasDoc = ficha.notasDoc,
                signoDoc = ficha.signoDoc,
                tasaCambioDoc = ficha.tasaCambioDoc,
                tipoDoc = ficha.tipoDoc,
                montoDoc = ficha.montoDoc,
            };
            var r01 = MyData.CxC_AgregarNotaCreditoAdm(fichaDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Ficha
            CxC_AgregarNotaDebitoAdm(OOB.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDto = new DtoLibPos.CxC.AgregarNotaAdm.Ficha()
            {
                autoCliente = ficha.autoCliente,
                autoVendedor = ficha.autoVendedor,
                ciRifCliente = ficha.ciRifCliente,
                codigoCliente = ficha.codigoCliente,
                codSucursal = ficha.codSucursal,
                montoDivisaDoc = ficha.montoDivisaDoc,
                nombreCliente = ficha.nombreCliente,
                notasDoc = ficha.notasDoc,
                signoDoc = ficha.signoDoc,
                tasaCambioDoc = ficha.tasaCambioDoc,
                tipoDoc = ficha.tipoDoc,
                montoDoc = ficha.montoDoc,
            };
            var r01 = MyData.CxC_AgregarNotaDebitoAdm(fichaDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Lista<OOB.CxC.DocumentosPend.Ficha> 
            CxC_DocumentosPend_GetLista(OOB.CxC.DocumentosPend.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.CxC.DocumentosPend.Ficha>();

            var filtroDTO = new DtoLibPos.CxC.DocumentosPend.Filtro()
            {
                idCliente = filtro.idCliente,
            };
            var r01 = MyData.CxC_DocumentosPend_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var lst = new List<OOB.CxC.DocumentosPend.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.CxC.DocumentosPend.Ficha()
                        {
                            acumuladoDoc = s.acumuladoDoc,
                            autoCliente = s.autoCliente,
                            autoDoc = s.autoDoc,
                            autoVendedor = s.autoVendedor,
                            ciRifCliente = s.ciRifCliente,
                            codigoCliente = s.codigoCliente,
                            codSucursal = s.codSucursal,
                            diasCreditoDoc = s.diasCreditoDoc,
                            fechaEmisionDoc = s.fechaEmisionDoc,
                            fechaVencDoc = s.fechaVencDoc,
                            importeDoc = s.importeDoc,
                            nombreCliente = s.nombreCliente,
                            nombreVendedor = s.nombreVendedor,
                            notasDoc = s.notasDoc,
                            numeroDoc = s.numeroDoc,
                            serieDoc = s.serieDoc,
                            signoDoc = s.signoDoc,
                            tasaCambioDoc = s.tasaCambioDoc,
                            tipoDoc = s.tipoDoc,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }


        public OOB.Resultado.FichaEntidad<int> 
            CxC_Get_ContadorNotaCreditoAdm()
        {
            var result = new OOB.Resultado.FichaEntidad<int>();

            var r01 = MyData.CxC_Get_ContadorNotaCreditoAdm();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Entidad = r01.Entidad;

            return result;
        }
        public OOB.Resultado.FichaEntidad<int> 
            CxC_Get_ContadorNotaDebitoAdm()
        {
            var result = new OOB.Resultado.FichaEntidad<int>();

            var r01 = MyData.CxC_Get_ContadorNotaDebitoAdm();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Entidad = r01.Entidad;

            return result;
        }


        public OOB.Resultado.Ficha 
            CxC_GestionCobro_Agregar(OOB.CxC.GestionCobro.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();
            var fichaDto = new DtoLibPos.CxC.GestionCobro.Ficha()
            {
                autoCliente = ficha.autoCliente,
                montoAnticipo = ficha.montoAnticipo,
                factorCambio =ficha.factorCambio,
                montoRecibido = ficha.montoRecibido,
                SucPrefijo = ficha.SucPrefijo,
                Cobro = new DtoLibPos.CxC.GestionCobro.FichaCobro()
                {
                    AutoCliente = ficha.Cobro.AutoCliente,
                    AutoVendedor = ficha.Cobro.AutoVendedor,
                    CiRif = ficha.Cobro.CiRif,
                    Cliente = ficha.Cobro.Cliente,
                    CodigoCliente = ficha.Cobro.CodigoCliente,
                    Importe = ficha.Cobro.Importe,
                    MontoDivisa = ficha.Cobro.MontoDivisa,
                    Nota = ficha.Cobro.Nota,
                    TasaDivisa = ficha.Cobro.TasaDivisa,
                },
                Recibo = new DtoLibPos.CxC.GestionCobro.FichaRecibo()
                {
                    AutoCliente = ficha.Recibo.AutoCliente,
                    AutoCobrador = ficha.Recibo.AutoCobrador,
                    AutoUsuario = ficha.Recibo.AutoUsuario,
                    Cambio = ficha.Recibo.Cambio,
                    CambioDivisa = ficha.Recibo.CambioDivisa,
                    CiRif = ficha.Recibo.CiRif,
                    Cliente = ficha.Recibo.Cliente,
                    Cobrador = ficha.Recibo.Cobrador,
                    Codigo = ficha.Recibo.Codigo,
                    CodigoCobrador = ficha.Recibo.CodigoCobrador,
                    Direccion = ficha.Recibo.Direccion,
                    Importe = ficha.Recibo.Importe,
                    ImporteDivisa = ficha.Recibo.ImporteDivisa,
                    MontoRecibido = ficha.Recibo.MontoRecibido,
                    MontoRecibidoDivisa = ficha.Recibo.MontoRecibidoDivisa,
                    Nota = ficha.Recibo.Nota,
                    Telefono = ficha.Recibo.Telefono,
                    Usuario = ficha.Recibo.Usuario,
                },
                Documentos = ficha.Documentos.Select(s =>
                {
                    var nr = new DtoLibPos.CxC.GestionCobro.FichaDocumento()
                    {
                        AutoCxC = s.AutoCxC,
                        DocumentoNro = s.DocumentoNro,
                        EstatusDocCancelado = s.EstatusDocCancelado,
                        Id = s.Id,
                        Importe = s.Importe,
                        ImporteDivisa = s.ImporteDivisa,
                        TipoDocumento = s.TipoDocumento,
                        Notas = s.Notas,
                    };
                    return nr;
                }).ToList(),
                MetodosPago = ficha.MetodosPago.Select(s =>
                {
                    var nr = new DtoLibPos.CxC.GestionCobro.FichaMetodoPago()
                    {
                        AutoCobrador = s.AutoCobrador,
                        AutoMedioPago = s.AutoMedioPago,
                        AutoUsuario = s.AutoUsuario,
                        Cierre = s.Cierre,
                        Codigo = s.Codigo,
                        Lote = s.Lote,
                        Medio = s.Medio,
                        MontoRecibido = s.MontoRecibido,
                        OpAplicaConversion = s.OpAplicaConversion,
                        OpBanco = s.OpBanco,
                        OpDetalle = s.OpDetalle,
                        OpFecha = s.OpFecha,
                        OpMonto = s.OpMonto,
                        OpNroCta = s.OpNroCta,
                        OpNroRef = s.OpNroRef,
                        OpTasa = s.OpTasa,
                        Referencia = s.Referencia,
                    };
                    return nr;
                }).ToList(),
            };
            if (ficha.notaAdm != null) 
            {
                fichaDto.notaAdm = new DtoLibPos.CxC.GestionCobro.FichaNotaAdm()
                {
                    autoCliente = ficha.notaAdm.autoCliente,
                    autoVendedor = ficha.notaAdm.autoVendedor,
                    ciRifCliente = ficha.notaAdm.ciRifCliente,
                    codigoCliente = ficha.notaAdm.codigoCliente,
                    codSucursal = ficha.notaAdm.codSucursal,
                    montoDivisaDoc = ficha.notaAdm.montoDivisaDoc,
                    nombreCliente = ficha.notaAdm.nombreCliente,
                    notasDoc = ficha.notaAdm.notasDoc,
                    signoDoc = ficha.notaAdm.signoDoc,
                    tasaCambioDoc = ficha.notaAdm.tasaCambioDoc,
                    tipoDoc = ficha.notaAdm.tipoDoc,
                    montoDoc = ficha.notaAdm.montoDoc,
                };
            }
            if (ficha.retencion != null) 
            {
                var fr= ficha.retencion;
                fichaDto.retencion = new DtoLibPos.CxC.GestionCobro.Retencion()
                {
                    factorCambio = fr.factorCambio,
                    montoAplicarRetMonAct = fr.montoAplicarRetMonAct,
                    retencionMonAct = fr.retencionMonAct,
                    sustraendoMonAct = fr.sustraendoMonAct,
                    tasaRet = fr.tasaRet,
                    totalRetMonAct = fr.totalRetMonAct,
                };
            }
            if (ficha.cajas != null) 
            {
                var lt = new List<DtoLibPos.CxC.GestionCobro.Caja>();
                foreach (var rg in ficha.cajas) 
                {
                    var mv = new DtoLibPos.CxC.GestionCobro.Caja()
                    {
                        idCaja = rg.idCaja,
                        codCaja = rg.codCaja,
                        descCaja = rg.descCaja,
                        monto = rg.monto,
                        cajaMov = new DtoLibPos.CxC.GestionCobro.CajaMov()
                        {
                            descMov = rg.cajaMov.descMov,
                            factorCambio = rg.cajaMov.factorCambio,
                            montoMovMonAct = rg.cajaMov.montoMovMonAct,
                            montoMovMonDiv = rg.cajaMov.montoMovMonDiv,
                            movFueDivisa = rg.cajaMov.movFueDivisa,
                        },
                    };
                    lt.Add(mv);
                }
                fichaDto.cajas = lt;
            }
            var r01 = MyData.CxC_GestionCobro_Agregar(fichaDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            return result;
        }
    }
}