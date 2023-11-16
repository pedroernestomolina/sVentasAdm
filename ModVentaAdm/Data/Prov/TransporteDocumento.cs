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
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Entidad.Presupuesto.Ficha>
            TransporteDocumento_EntidadPresupuesto_GetById(string idDoc)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Entidad.Presupuesto.Ficha>();
            var r01 = MyData.TransporteDocumento_EntidadPresupuesto_GetById(idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var e = r01.Entidad.encabezado;
            var d = r01.Entidad.items;
            result.Entidad = new OOB.Transporte.Documento.Entidad.Presupuesto.Ficha()
            {
                encabezado = new OOB.Transporte.Documento.Entidad.Presupuesto.FichaEncabezado()
                {
                    anoRelacion = e.anoRelacion,
                    cargos = e.cargos,
                    cargosp = e.cargosp,
                    clienteCiRif = e.clienteCiRif,
                    clienteCodigo = e.clienteCodigo,
                    clienteDirFiscal = e.clienteDirFiscal,
                    clienteId = e.clienteId,
                    clienteNombre = e.clienteNombre,
                    clienteTelefono = e.clienteTelefono,
                    cntRenglones = e.cntRenglones,
                    codSucursal = e.codSucursal,
                    condPago = e.condPago,
                    descuento1 = e.descuento1,
                    descuento1p = e.descuento1p,
                    descuento2 = e.descuento2,
                    descuento2p = e.descuento2p,
                    diasCredito = e.diasCredito,
                    diasValidez = e.diasValidez,
                    docCodigoTipo = e.docCodigoTipo,
                    docFechaEmision = e.docFechaEmision,
                    docFechaVence = e.docFechaVence,
                    docModulo = e.docModulo,
                    docNombre = e.docNombre,
                    docNumero = e.docNumero,
                    docSigno = e.docSigno,
                    docTotal = e.docTotal,
                    estacion = e.estacion,
                    estatusAnulado = e.estatusAnulado,
                    factorCambio = e.factorCambio,
                    fechaRegistro = e.fechaRegistro,
                    horaRegistro = e.horaRegistro,
                    idDoc = e.idDoc,
                    mesRelacion = e.mesRelacion,
                    montoBase = e.montoBase,
                    montoBase1 = e.montoBase1,
                    montoBase2 = e.montoBase2,
                    montoBase3 = e.montoBase3,
                    montoDivisa = e.montoDivisa,
                    montoExento = e.montoExento,
                    montoImpuesto = e.montoImpuesto,
                    montoImpuesto1 = e.montoImpuesto1,
                    montoImpuesto2 = e.montoImpuesto2,
                    montoImpuesto3 = e.montoImpuesto3,
                    montoNeto = e.montoNeto,
                    notasObs = e.notasObs,
                    subTotal = e.subTotal,
                    subTotalImpuesto = e.subTotalImpuesto,
                    subTotalNeto = e.subTotalNeto,
                    tasa1 = e.tasa1,
                    tasa2 = e.tasa2,
                    tasa3 = e.tasa3,
                    usuarioId = e.usuarioId,
                    usuarioNombre = e.usuarioNombre,
                    uUsuarioCodigo = e.uUsuarioCodigo,
                    vendedorCodigo = e.vendedorCodigo,
                    vendedorId = e.vendedorId,
                    vendedorNombre = e.vendedorNombre,
                    docSolicitadoPor = e.docSolicitadoPor,
                    docModuloCargar = e.docModuloCargar,
                },
                items = d.Select(s =>
                {
                    var nr = new OOB.Transporte.Documento.Entidad.Presupuesto.FichaDetalle()
                    {
                        alicuotaDesc = s.alicuotaDesc,
                        alicuotaId = s.alicuotaId,
                        alicuotaTasa = s.alicuotaTasa,
                        cntDias = s.cntDias,
                        cntUnidades = s.cntUnidades,
                        dscto = s.dscto,
                        id = s.id,
                        importe = s.importe,
                        notas = s.notas,
                        precioNetoDivisa = s.precioNetoDivisa,
                        servicioDesc = s.servicioDesc,
                        servicioCodigo = s.servicioCodigo,
                        servicioDetalle = s.servicioDetalle,
                        servicioId = s.servicioId,
                        unidadesDesc = s.unidadesDesc,
                        turnoEstatus = s.turnoEstatus,
                        turnoId = s.turnoId,
                        turnoDesc = s.turnoDesc,
                        turnoCntDias = s.turnoCntDias,
                        fechaServ = s.fechaServ.Select(ss =>
                        {
                            var xr = new OOB.Transporte.Documento.Entidad.Presupuesto.FichaFechaServ()
                            {
                                fecha = ss.fecha,
                                hora = ss.hora,
                                nota = ss.nota,
                            };
                            return xr;
                        }).ToList(),
                        aliados = s.aliados.Select(xx =>
                        {
                            var xz = new OOB.Transporte.Documento.Entidad.Presupuesto.FichaAliado()
                            {
                                ciRif = xx.ciRif,
                                cntDias = xx.cntDias,
                                codigo = xx.codigo,
                                descripcion = xx.descripcion,
                                idAliado = xx.idAliado,
                                importe = xx.importe,
                                precioUnitDivisa = xx.precioUnitDivisa,
                            };
                            return xz;
                        }).ToList(),
                    };
                    return nr;
                }).ToList(),
            };
            return result;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Documento.Entidad.Presupuesto.FichaAliado>
            TransporteDocumento_EntidadPresupuesto_GetAliadosById(string idDoc)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.Entidad.Presupuesto.FichaAliado>();
            var r01 = MyData.TransporteDocumento_EntidadPresupuesto_GetAliadosById(idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.Entidad.Presupuesto.FichaAliado>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(xx =>
                        {
                            var xz = new OOB.Transporte.Documento.Entidad.Presupuesto.FichaAliado()
                            {
                                ciRif = xx.ciRif,
                                cntDias = xx.cntDias,
                                codigo = xx.codigo,
                                descripcion = xx.descripcion,
                                idAliado = xx.idAliado,
                                importe = xx.importe,
                                precioUnitDivisa = xx.precioUnitDivisa,
                            };
                            return xz;
                        }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Documento.Remision.Lista.Ficha>
            TransporteDocumento_Remision_ListaBy(OOB.Transporte.Documento.Remision.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.Remision.Lista.Ficha>();
            var filtroDTO = new DtoTransporte.Documento.Remision.Lista.Filtro()
            {
                codTipoDoc = filtro.codTipoDoc,
                idCliente = filtro.idCliente,
                esPorRemision=filtro.esPorRemision,
            };
            var r01 = MyData.TransporteDocumento_Remision_ListaBy(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.Remision.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.Remision.Lista.Ficha()
                        {
                            clienteCiRif = s.clienteCiRif,
                            clienteNombre = s.clienteNombre,
                            docCntRenglones = s.docCntRenglones,
                            docCodigo = s.docCodigo,
                            docFechaEmision = s.docFechaEmision,
                            docHoraEmision = s.docHoraEmision,
                            docId = s.docId,
                            docMontoMonedaAct = s.docMontoMonedaAct,
                            docMontoMonedaDiv = s.docMontoMonedaDiv,
                            docNombre = s.docNombre,
                            docNumero = s.docNumero,
                            docSigno = s.docSigno,
                            factorCambio = s.factorCambio,
                            estatusAnulado = s.estatusAnulado,
                            docSolicitadoPor = s.docSolicitadoPor,
                            docModuloCargar = s.docModuloCargar,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Entidad.Venta.Ficha>
            TransporteDocumento_EntidadVenta_GetById(string idDoc)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Entidad.Venta.Ficha>();
            var r01 = MyData.TransporteDocumento_EntidadVenta_GetById(idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var e = r01.Entidad.encabezado;
            var d = r01.Entidad.detalles;
            result.Entidad = new OOB.Transporte.Documento.Entidad.Venta.Ficha()
            {
                encabezado = new OOB.Transporte.Documento.Entidad.Venta.FichaEncabezado()
                {
                    anoRelacion = e.anoRelacion,
                    cargos = e.cargos,
                    cargosp = e.cargosp,
                    clienteCiRif = e.clienteCiRif,
                    clienteCodigo = e.clienteCodigo,
                    clienteDirFiscal = e.clienteDirFiscal,
                    clienteId = e.clienteId,
                    clienteNombre = e.clienteNombre,
                    clienteTelefono = e.clienteTelefono,
                    cntRenglones = e.cntRenglones,
                    codSucursal = e.codSucursal,
                    condPago = e.condPago,
                    descuento1 = e.descuento1,
                    descuento1p = e.descuento1p,
                    descuento2 = e.descuento2,
                    descuento2p = e.descuento2p,
                    diasCredito = e.diasCredito,
                    diasValidez = e.diasValidez,
                    docCodigoTipo = e.docCodigoTipo,
                    docFechaEmision = e.docFechaEmision,
                    docFechaVence = e.docFechaVence,
                    docModulo = e.docModulo,
                    docNombre = e.docNombre,
                    docNumero = e.docNumero,
                    docSigno = e.docSigno,
                    docTotal = e.docTotal,
                    estacion = e.estacion,
                    estatusAnulado = e.estatusAnulado,
                    factorCambio = e.factorCambio,
                    fechaRegistro = e.fechaRegistro,
                    horaRegistro = e.horaRegistro,
                    idDoc = e.idDoc,
                    mesRelacion = e.mesRelacion,
                    montoBase = e.montoBase,
                    montoBase1 = e.montoBase1,
                    montoBase2 = e.montoBase2,
                    montoBase3 = e.montoBase3,
                    montoDivisa = e.montoDivisa,
                    montoExento = e.montoExento,
                    montoImpuesto = e.montoImpuesto,
                    montoImpuesto1 = e.montoImpuesto1,
                    montoImpuesto2 = e.montoImpuesto2,
                    montoImpuesto3 = e.montoImpuesto3,
                    montoNeto = e.montoNeto,
                    notasObs = e.notasObs,
                    subTotal = e.subTotal,
                    subTotalImpuesto = e.subTotalImpuesto,
                    subTotalNeto = e.subTotalNeto,
                    tasa1 = e.tasa1,
                    tasa2 = e.tasa2,
                    tasa3 = e.tasa3,
                    usuarioId = e.usuarioId,
                    usuarioNombre = e.usuarioNombre,
                    uUsuarioCodigo = e.uUsuarioCodigo,
                    vendedorCodigo = e.vendedorCodigo,
                    vendedorId = e.vendedorId,
                    vendedorNombre = e.vendedorNombre,
                    docSolicitadoPor = e.docSolicitadoPor,
                    docModuloCargar = e.docModuloCargar,
                    igtfMontoMonAct = e.igtfMontoMonAct,
                    igtfTasa = e.igtfTasa,
                },
                detalles = d.Select(s =>
                {
                    var nr = new OOB.Transporte.Documento.Entidad.Venta.FichaDetalle()
                    {
                        alicuotaDesc = s.alicuotaDesc,
                        alicuotaId = s.alicuotaId,
                        alicuotaTasa = s.alicuotaTasa,
                        cntDias = s.cntDias,
                        codigoDocRef = s.codigoDocRef,
                        detalle = s.detalle,
                        dsctoMontoMonDivisa = s.dsctoMontoMonDivisa,
                        dsctoMontoMonLocal = s.dsctoMontoMonLocal,
                        dsctoPorc = s.dsctoPorc,
                        fechaDocRef = s.fechaDocRef,
                        idDocRef = s.idDocRef,
                        idItemServicio = s.idItemServicio,
                        importeNetoMonDivisa = s.importeNetoMonDivisa,
                        importeNetoMonLocal = s.importeNetoMonLocal,
                        importeTotalMonDivisa = s.importeTotalMonDivisa,
                        importeTotalMonLocal = s.importeTotalMonLocal,
                        impuestoMonDivisa = s.impuestoMonDivisa,
                        impuestoMonLocal = s.impuestoMonLocal,
                        montoDocRef = s.montoDocRef,
                        numDocRef = s.numDocRef,
                        precioFinalMonDivisa = s.precioFinalMonDivisa,
                        precioFinalMonLocal = s.precioFinalMonLocal,
                        precioItemMonDivisa = s.precioItemMonDivisa,
                        precioItemMonLocal = s.precioItemMonLocal,
                        precioNetoMonDivisa = s.precioNetoMonDivisa,
                        precioNetoMonLocal = s.precioNetoMonLocal,
                        tipoProcedenciaItem = s.tipoProcedenciaItem,
                        totalMonDivisa = s.totalMonDivisa,
                        totalMonLocal = s.totalMonLocal,
                        mostrarItemDocFinal= s.mostrarItemDocFinal,
                    };
                    return nr;
                }).ToList(),
                turnos = r01.Entidad.turnos.Select(s => 
                {
                    var _turno = new OOB.Transporte.Documento.Entidad.Venta.Turno()
                    {
                        detalle = s.detalle,
                        importe = s.importe,
                        ruta = s.ruta,
                    };
                    return _turno;
                }).ToList(),
            };
            return result;
        }
        public OOB.Resultado.FichaEntidad<int>
            TransporteDocumento_Presupuesto_Pendiente_Cnt()
        {
            var result = new OOB.Resultado.FichaEntidad<int>();
            var r01 = MyData.TransporteDocumento_Presupuesto_Pendiente_Cnt();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Entidad = r01.Entidad;
            return result;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Documento.Lista.Pendiente.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_Pendiente()
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.Lista.Pendiente.Presupuesto.Ficha>();
            var r01 = MyData.TransporteDocumento_Presupuesto_Pendiente();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.Lista.Pendiente.Presupuesto.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.Lista.Pendiente.Presupuesto.Ficha()
                        {
                            clienteCiRif = s.clienteCiRif,
                            clienteNombre = s.clienteNombre,
                            docCntRenglones = s.docCntRenglones,
                            docCodigo = s.docCodigo,
                            docFechaEmision = s.docFechaEmision,
                            docHoraEmision = s.docHoraEmision,
                            docId = s.docId,
                            docMontoMonedaAct = s.docMontoMonedaAct,
                            docMontoMonedaDiv = s.docMontoMonedaDiv,
                            docNombre = s.docNombre,
                            docNumero = s.docNumero,
                            docSigno = s.docSigno,
                            factorCambio = s.factorCambio,
                            estatusAnulado = s.estatusAnulado,
                            docSolicitadoPor = s.docSolicitadoPor,
                            docModuloCargar = s.docModuloCargar,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
        public OOB.Resultado.Lista<OOB.Documento.Lista.Ficha>
            TransporteDocumento_GetLista(OOB.Documento.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Documento.Lista.Ficha>();
            var xestatus = DtoLibPos.Documento.Lista.Filtro.enumEstatus.SinDefinir;
            switch (filtro.estatus.Trim().ToUpper())
            {
                case "ACTIVO":
                    xestatus = DtoLibPos.Documento.Lista.Filtro.enumEstatus.Activo;
                    break;
                case "ANULADO":
                    xestatus = DtoLibPos.Documento.Lista.Filtro.enumEstatus.Anulado;
                    break;
            }
            var filtroDTO = new DtoLibPos.Documento.Lista.Filtro()
            {
                idArqueo = filtro.idArqueo,
                idCliente = filtro.idCliente,
                idProducto = filtro.idProducto,
                codSucursal = filtro.codSucursal,
                codTipoDocumento = filtro.codTipoDocumento,
                fecha = new DtoLibPos.Documento.Lista.Filtro.Fecha() { desde = filtro.desde, hasta = filtro.hasta },
                estatus = xestatus,
                palabraClave = filtro.palabraClave,
            };
            var r01 = MyData.TransporteDocumento_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var lst = new List<OOB.Documento.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Documento.Lista.Ficha()
                        {
                            CiRif = s.CiRif,
                            Control = s.Control,
                            DocCodigo = s.DocCodigo,
                            DocNombre = s.DocNombre,
                            DocNumero = s.DocNumero,
                            DocSigno = s.DocSigno,
                            Estatus = s.Estatus,
                            FechaEmision = s.FechaEmision,
                            HoraEmision = s.HoraEmision,
                            Id = s.Id,
                            Monto = s.Monto,
                            NombreRazonSocial = s.NombreRazonSocial,
                            Renglones = s.Renglones,
                            Serie = s.Serie,
                            MontoDivisa = s.MontoDivisa,
                            DocAplica = s.DocAplica,
                            DocSituacion = s.DocSituacion,
                            SucursalCod = s.SucursalCod,
                            SucursalDesc = s.SucursalDesc,
                            ClaveSistema = s.ClaveSistema,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }

        public OOB.Resultado.Lista<OOB.Transporte.Documento.GetAliados.Presupuesto.Ficha>
            TransporteDocumento_GetAliados_Presupuesto(string idDoc)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.GetAliados.Presupuesto.Ficha>();
            var r01 = MyData.TransporteDocumento_Presupuesto_GetAliados(idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.GetAliados.Presupuesto.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.GetAliados.Presupuesto.Ficha()
                        {
                            ciRif = s.ciRif,
                            idAliado = s.idAliado,
                            importe = s.importe,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }

        public OOB.Resultado.Lista<OOB.Transporte.Documento.GetServicios.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_GetServicios(string idDoc)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.GetServicios.Presupuesto.Ficha>();
            var r01 = MyData.TransporteDocumento_Presupuesto_GetServicios(idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.GetServicios.Presupuesto.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.GetServicios.Presupuesto.Ficha()
                        {
                            codServ = s.codServ,
                            descServ = s.descServ,
                            detServ = s.detServ,
                            idAliado = s.idAliado,
                            idServ = s.idServ,
                            importeServ = s.importeServ,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }

        public OOB.Resultado.Lista<OOB.Transporte.Documento.GetTurnos.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_GetTurnos(string idDoc)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.GetTurnos.Presupuesto.Ficha>();
            var r01 = MyData.TransporteDocumento_Presupuesto_GetTurnos(idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.GetTurnos.Presupuesto.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.GetTurnos.Presupuesto.Ficha()
                        {
                            importeMonDiv = s.importeMonDiv,
                            turnoDesc = s.turnoDesc,
                            turnoId = s.turnoId,
                            turnoRuta = s.turnoRuta,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }

        //
        public OOB.Resultado.Lista<OOB.Transporte.Documento.GetTurnos.Documento.Ficha> 
            TransporteDocumento_Documento_GetTurnos(string idDoc)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.GetTurnos.Documento.Ficha>();
            var r01 = MyData.TransporteDocumento_Documento_GetTurnos(idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.GetTurnos.Documento.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.GetTurnos.Documento.Ficha()
                        {
                            importeMonDiv = s.importeMonDiv,
                            turnoDesc = s.turnoDesc,
                            turnoRuta = s.turnoRuta,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
    }
}