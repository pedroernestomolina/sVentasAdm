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
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarFactura(OOB.Transporte.Documento.Agregar.Factura.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>();
            var fichaDTO = new DtoTransporte.Documento.Agregar.Factura.Ficha()
            {
                cargos = ficha.cargos,
                cargosp = ficha.cargosp,
                cntRenglones = ficha.cntRenglones,
                codCliente = ficha.codCliente,
                codSucursal = ficha.codSucursal,
                codUsuario = ficha.codUsuario,
                codVendedor = ficha.codVendedor,
                condPago = ficha.condPago,
                control = ficha.control,
                descuento1 = ficha.descuento1,
                descuento1p = ficha.descuento1p,
                descuento2 = ficha.descuento2,
                descuento2p = ficha.descuento2p,
                diasCredito = ficha.diasCredito,
                diasValidez = ficha.diasValidez,
                docCodigo = ficha.docCodigo,
                docNombre = ficha.docNombre,
                docRemision = ficha.docRemision,
                estacion = ficha.estacion,
                factorCambio = ficha.factorCambio,
                idCliente = ficha.idCliente,
                idRemision = ficha.idRemision,
                idUsuario = ficha.idUsuario,
                idVendedor = ficha.idVendedor,
                montoBase = ficha.montoBase,
                montoBase1 = ficha.montoBase1,
                montoBase2 = ficha.montoBase2,
                montoBase3 = ficha.montoBase3,
                montoDivisa = ficha.montoDivisa,
                montoExento = ficha.montoExento,
                montoImpuesto = ficha.montoImpuesto,
                montoImpuesto1 = ficha.montoImpuesto1,
                montoImpuesto2 = ficha.montoImpuesto2,
                montoImpuesto3 = ficha.montoImpuesto3,
                neto = ficha.neto,
                signo = ficha.signo,
                subTotal = ficha.subTotal,
                subTotalImpuesto = ficha.subTotalImpuesto,
                subTotalNeto = ficha.subTotalNeto,
                telefono = ficha.telefono,
                TipoDoc = ficha.TipoDoc,
                tipoRemision = ficha.tipoRemision,
                usuario = ficha.usuario,
                vendedor = ficha.vendedor,
                CiRif = ficha.CiRif,
                DirFiscal = ficha.DirFiscal,
                RazonSocial = ficha.RazonSocial,
                Tasa1 = ficha.Tasa1,
                Tasa2 = ficha.Tasa2,
                Tasa3 = ficha.Tasa3,
                Total = ficha.Total,
                nota = ficha.nota,
                docModuloCargar = ficha.docModuloCargar,
                docSolicitadoPor = ficha.docSolicitadoPor,
                serieDocDesc = ficha.serieDocDesc,
                serieDocId = ficha.serieDocId,
                subTotalMonDivisa = ficha.subTotalMonDivisa,
                tipoDocSiglas = ficha.tipoDocSiglas,
                fechaEmision = ficha.fechaEmision,
                fechaVencimiento = ficha.fechaVencimiento,
                items = ficha.items.Select(z =>
                {
                    DtoTransporte.Documento.Agregar.Presupuesto.FichaDetalle _servicioDetalle = null;
                    if (z.servDetalle != null)
                    {
                        var s = z.servDetalle;
                        var nr = new DtoTransporte.Documento.Agregar.Presupuesto.FichaDetalle()
                        {
                            alicuotaDesc = s.alicuotaDesc,
                            alicuotaId = s.alicuotaId,
                            alicuotaTasa = s.alicuotaTasa,
                            cntDias = s.cntDias,
                            cntUnidades = s.cntUnidades,
                            dscto = s.dscto,
                            estatusAnulado = s.estatusAnulado,
                            notas = s.notas,
                            precioNetoDivisa = s.precioNetoDivisa,
                            servicioDesc = s.servicioDesc,
                            signoDoc = s.signoDoc,
                            tipoDoc = s.tipoDoc,
                            importe = s.importe,
                            servicioCodigo = s.servicioCodigo,
                            servicioDetalle = s.servicioDetalle,
                            servicioId = s.servicioId,
                            unidadesDesc = s.unidadesDesc,
                            fechas = s.fechas.Select(ss =>
                            {
                                var nr2 = new DtoTransporte.Documento.Agregar.Presupuesto.Fecha()
                                {
                                    fecha = ss.fecha,
                                    hora = ss.hora,
                                    nota = ss.nota,
                                };
                                return nr2;
                            }).ToList(),
                            alidos = s.aliados.Select(xx =>
                            {
                                var nr3 = new DtoTransporte.Documento.Agregar.Presupuesto.Aliado()
                                {
                                    ciRif = xx.ciRif,
                                    cntDias = xx.cntDias,
                                    codigo = xx.codigo,
                                    desc = xx.desc,
                                    id = xx.id,
                                    importe = xx.importe,
                                    precioUnitDivisa = xx.precioUnitDivisa,
                                };
                                return nr3;
                            }).ToList(),
                        };
                        _servicioDetalle = nr;
                    }
                    var zr = new DtoTransporte.Documento.Agregar.Factura.FichaItem()
                    {
                        alicuotaDesc = z.alicuotaDesc,
                        alicuotaId = z.alicuotaId,
                        alicuotaTasa = z.alicuotaTasa,
                        cntDias = z.cntDias,
                        codigoDocRef = z.codigoDocRef,
                        detalle = z.detalle,
                        dsctoMontoMonDivisa = z.dsctoMontoMonDivisa,
                        dsctoMontoMonLocal = z.dsctoMontoMonLocal,
                        dsctoPorc = z.dsctoPorc,
                        fechaDocRef = z.fechaDocRef,
                        idDocRef = z.idDocRef,
                        importeNetoMonDivisa = z.importeNetoMonDivisa,
                        importeNetoMonLocal = z.importeNetoMonLocal,
                        importeTotalMonDivisa = z.importeTotalMonDivisa,
                        importeTotalMonLocal = z.importeTotalMonLocal,
                        impuestoMonDivisa = z.impuestoMonDivisa,
                        impuestoMonLocal = z.impuestoMonLocal,
                        montoDocRef = z.montoDocRef,
                        numDocRef = z.numDocRef,
                        precioFinalMonDivisa = z.precioFinalMonDivisa,
                        precioFinalMonLocal = z.precioFinalMonLocal,
                        precioItemMonDivisa = z.precioItemMonDivisa,
                        precioItemMonLocal = z.precioItemMonLocal,
                        precioNetoMonDivisa = z.precioNetoMonDivisa,
                        precioNetoMonLocal = z.precioNetoMonLocal,
                        totalMonDivisa = z.totalMonDivisa,
                        totalMonLocal = z.totalMonLocal,
                        tipoProcedenciaItem = z.tipoItemProcedencia,
                        servicioDetalle = _servicioDetalle,
                        mostrarItemDocFinal= z.mostrarItemDocFinal,
                    };
                    return zr;
                }).ToList(),
                docRef = ficha.docRef.Select(s =>
                {
                    var nr = new DtoTransporte.Documento.Agregar.Factura.FichaDocRef()
                    {
                        codigoDoc = s.codigoDoc,
                        fechaDoc = s.fechaDoc,
                        idDoc = s.idDoc,
                        montoDivisaDoc = s.montoDivisaDoc,
                        numDoc = s.numDoc,
                        tipoDoc = s.tipoDoc,
                    };
                    return nr;
                }).ToList(),
                aliadosResumen = ficha.aliadosResumen.Select(ss =>
                {
                    var nr = new DtoTransporte.Documento.Agregar.Factura.FichaAliadoResumen()
                    {
                        idAliado = ss.idAliado,
                        montoDivisa = ss.montoDivisa,
                        servicios = ss.servicios.Select(svc =>
                        {
                            var xnr = new DtoTransporte.Documento.Agregar.Factura.Servicio()
                            {
                                codigo = svc.codigo,
                                desc = svc.desc,
                                detalle = svc.detalle,
                                id = svc.id,
                                importe = svc.importe,
                            };
                            return xnr;
                        }).ToList(),
                    };
                    return nr;
                }).ToList(),
                aliadosDocRef = ficha.aliadosDocRef.Select(xx =>
                {
                    var nr = new DtoTransporte.Documento.Agregar.Factura.FichaAliadoDocRef()
                    {
                        idAliado = xx.idAliado,
                        idDocRef = xx.idDocRef,
                    };
                    return nr;
                }).ToList(),
                turnos = ficha.turnos.Select(tr =>
                {
                    var tnr = new DtoTransporte.Documento.Agregar.Factura.Turno()
                    {
                        detalle = tr.detalle,
                        importe = tr.importe,
                        ruta = tr.ruta,
                    };
                    return tnr;
                }).ToList(),
            };
            var r01 = MyData.TransporteDocumento_AgregarFactura(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Entidad = new OOB.Transporte.Documento.Agregar.Resultado()
            {
                autoDoc = r01.Entidad.autoDoc,
                numDoc = r01.Entidad.numDoc,
            };
            return result;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado> 
            TransporteDocumento_AgregarFactura_From_HojaServ(OOB.Transporte.Documento.Agregar.FacturaFromHojaServ.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>();
            var fichaDTO = new DtoTransporte.Documento.Agregar.FacturaFromHojaServ.Ficha()
            {
                cargos = ficha.cargos,
                cargosp = ficha.cargosp,
                cntRenglones = ficha.cntRenglones,
                codCliente = ficha.codCliente,
                codSucursal = ficha.codSucursal,
                codUsuario = ficha.codUsuario,
                codVendedor = ficha.codVendedor,
                condPago = ficha.condPago,
                control = ficha.control,
                descuento1 = ficha.descuento1,
                descuento1p = ficha.descuento1p,
                descuento2 = ficha.descuento2,
                descuento2p = ficha.descuento2p,
                diasCredito = ficha.diasCredito,
                diasValidez = ficha.diasValidez,
                docCodigo = ficha.docCodigo,
                docNombre = ficha.docNombre,
                docRemision = ficha.docRemision,
                estacion = ficha.estacion,
                factorCambio = ficha.factorCambio,
                idCliente = ficha.idCliente,
                idRemision = ficha.idRemision,
                idUsuario = ficha.idUsuario,
                idVendedor = ficha.idVendedor,
                montoBase = ficha.montoBase,
                montoBase1 = ficha.montoBase1,
                montoBase2 = ficha.montoBase2,
                montoBase3 = ficha.montoBase3,
                montoDivisa = ficha.montoDivisa,
                montoExento = ficha.montoExento,
                montoImpuesto = ficha.montoImpuesto,
                montoImpuesto1 = ficha.montoImpuesto1,
                montoImpuesto2 = ficha.montoImpuesto2,
                montoImpuesto3 = ficha.montoImpuesto3,
                neto = ficha.neto,
                signo = ficha.signo,
                subTotal = ficha.subTotal,
                subTotalImpuesto = ficha.subTotalImpuesto,
                subTotalNeto = ficha.subTotalNeto,
                telefono = ficha.telefono,
                TipoDoc = ficha.TipoDoc,
                tipoRemision = ficha.tipoRemision,
                usuario = ficha.usuario,
                vendedor = ficha.vendedor,
                CiRif = ficha.CiRif,
                DirFiscal = ficha.DirFiscal,
                RazonSocial = ficha.RazonSocial,
                Tasa1 = ficha.Tasa1,
                Tasa2 = ficha.Tasa2,
                Tasa3 = ficha.Tasa3,
                Total = ficha.Total,
                nota = ficha.nota,
                docModuloCargar = ficha.docModuloCargar,
                docSolicitadoPor = ficha.docSolicitadoPor,
                serieDocDesc = ficha.serieDocDesc,
                serieDocId = ficha.serieDocId,
                subTotalMonDivisa = ficha.subTotalMonDivisa,
                tipoDocSiglas = ficha.tipoDocSiglas,
                fechaEmision = ficha.fechaEmision,
                fechaVencimiento = ficha.fechaVencimiento,
                items = ficha.items.Select(z =>
                {
                    var zr = new DtoTransporte.Documento.Agregar.FacturaFromHojaServ.FichaItem()
                    {
                        alicuotaDesc = z.alicuotaDesc,
                        alicuotaId = z.alicuotaId,
                        alicuotaTasa = z.alicuotaTasa,
                        cntDias = z.cntDias,
                        codigoDocRef = z.codigoDocRef,
                        detalle = z.detalle,
                        dsctoMontoMonDivisa = z.dsctoMontoMonDivisa,
                        dsctoMontoMonLocal = z.dsctoMontoMonLocal,
                        dsctoPorc = z.dsctoPorc,
                        fechaDocRef = z.fechaDocRef,
                        idDocRef = z.idDocRef,
                        importeNetoMonDivisa = z.importeNetoMonDivisa,
                        importeNetoMonLocal = z.importeNetoMonLocal,
                        importeTotalMonDivisa = z.importeTotalMonDivisa,
                        importeTotalMonLocal = z.importeTotalMonLocal,
                        impuestoMonDivisa = z.impuestoMonDivisa,
                        impuestoMonLocal = z.impuestoMonLocal,
                        montoDocRef = z.montoDocRef,
                        numDocRef = z.numDocRef,
                        precioFinalMonDivisa = z.precioFinalMonDivisa,
                        precioFinalMonLocal = z.precioFinalMonLocal,
                        precioItemMonDivisa = z.precioItemMonDivisa,
                        precioItemMonLocal = z.precioItemMonLocal,
                        precioNetoMonDivisa = z.precioNetoMonDivisa,
                        precioNetoMonLocal = z.precioNetoMonLocal,
                        totalMonDivisa = z.totalMonDivisa,
                        totalMonLocal = z.totalMonLocal,
                        tipoProcedenciaItem = z.tipoItemProcedencia,
                    };
                    return zr;
                }).ToList(),
                docRef = ficha.docRef.Select(s =>
                {
                    var nr = new DtoTransporte.Documento.Agregar.FacturaFromHojaServ.FichaDocRef()
                    {
                        codigoDoc = s.codigoDoc,
                        fechaDoc = s.fechaDoc,
                        idDoc = s.idDoc,
                        montoDivisaDoc = s.montoDivisaDoc,
                        numDoc = s.numDoc,
                        tipoDoc = s.tipoDoc,
                    };
                    return nr;
                }).ToList(),
                turnos = ficha.turnos.Select(tr =>
                {
                    var tnr = new DtoTransporte.Documento.Agregar.FacturaFromHojaServ.Turno()
                    {
                        detalle = tr.detalle,
                        importe = tr.importe,
                        ruta = tr.ruta,
                    };
                    return tnr;
                }).ToList(),
            };
            var r01 = MyData.TransporteDocumento_AgregarFactura_From_HojasServicio(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Entidad = new OOB.Transporte.Documento.Agregar.Resultado()
            {
                autoDoc = r01.Entidad.autoDoc,
                numDoc = r01.Entidad.numDoc,
            };
            return result;
        }
    }
}