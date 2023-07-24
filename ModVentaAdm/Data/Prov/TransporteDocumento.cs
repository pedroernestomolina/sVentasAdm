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
            var r01 = MyData.TransporteDocumento_EntidadPresupuesto_GetById (idDoc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var e = r01.Entidad.encabezado;
            var d= r01.Entidad.items;
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
                    docModuloCargar= e.docModuloCargar,
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
            if (r01.Lista !=null)
            {
                if (r01.Lista.Count>0)
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
            };
            var r01 = MyData.TransporteDocumento_Remision_ListaBy(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.Remision.Lista.Ficha>();
            if (r01.Lista!= null) 
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
                            estatusAnulado=s.estatusAnulado,
                            docSolicitadoPor= s.docSolicitadoPor,
                            docModuloCargar=s.docModuloCargar,
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