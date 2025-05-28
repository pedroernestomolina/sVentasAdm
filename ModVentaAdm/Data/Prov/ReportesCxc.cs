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
        public OOB.Resultado.Lista<OOB.ReportesCxc.DetallePorDoc.Ficha> 
            ReportesCxc_DetalleCobranza_PorDocumentos(OOB.ReportesCxc.DetallePorDoc.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.ReportesCxc.DetallePorDoc.Ficha>();
            //
            try
            {
                var filtroDTO = new DtoLibPos.Reportes.Cxc.DetallePorDoc.Filtro()
                {
                    desde = filtro.desde,
                    hasta = filtro.hasta,
                    codSucursal = filtro.codSucursal,
                    idCliente = filtro.idCliente,
                };
                var r01 = MyData.ReportesCxc_DetalleCobranza_PorDocumentos(filtroDTO);
                if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                var list = new List<OOB.ReportesCxc.DetallePorDoc.Ficha>();
                if (r01.Lista != null)
                {
                    if (r01.Lista.Count > 0)
                    {
                        list = r01.Lista.Select(s =>
                        {
                            var nr = new OOB.ReportesCxc.DetallePorDoc.Ficha()
                            {
                                ciRifEntidad = s.ciRifEntidad,
                                codigoSuc = s.codigoSuc,
                                fechaDoc = s.fechaDoc,
                                fechaRecibo = s.fechaRecibo,
                                fechaVencDoc = s.fechaVencDoc,
                                importeDivisa = s.importeDivisa,
                                nombreEntidad = s.nombreEntidad,
                                notasDoc = s.notasDoc,
                                numeroDoc = s.numeroDoc,
                                numeroRecibo = s.numeroRecibo,
                                tipoDoc = s.tipoDoc,
                                importeRecibo = s.importeRecibo,
                                montoPorAnticiposPorCargar = s.montoPorAnticiposPorCargar,
                                montoPorAnticiposUsado = s.montoPorAnticiposUsado,
                                montoPorNtCreditoUsado = s.montoPorNtCreditoUsado,
                            };
                            return nr;
                        }).ToList();
                    }
                }
                rt.ListaD = list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //
            return rt;
        }

        public OOB.Resultado.Lista<OOB.ReportesCxc.Resumen.Ficha> 
            ReportesCxc_ResumenCobranza(OOB.ReportesCxc.Resumen.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.ReportesCxc.Resumen.Ficha>();
            //
            try
            {
                var filtroDTO = new DtoLibPos.Reportes.Cxc.Resumen.Filtro()
                {
                    desde = filtro.desde,
                    hasta = filtro.hasta,
                    codSucursal = filtro.codSucursal,
                    idCliente = filtro.idCliente,
                };
                var r01 = MyData.ReportesCxc_ResumenCobranza(filtroDTO);
                if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                var list = new List<OOB.ReportesCxc.Resumen.Ficha>();
                if (r01.Lista != null)
                {
                    if (r01.Lista.Count > 0)
                    {
                        list = r01.Lista.Select(s =>
                        {
                            var nr = new OOB.ReportesCxc.Resumen.Ficha()
                            {
                                ciRifEntidad = s.ciRifEntidad,
                                codigoSuc = s.codigoSuc,
                                fechaRec = s.fechaRec,
                                importeDivisa = s.importeDivisa,
                                montoPorAnticipoCargado = s.montoPorAnticipoCargado,
                                montoPorAnticipoUsado = s.montoPorAnticipoUsado,
                                montoPorNtCreditoUsado = s.montoPorNtCreditoUsado,
                                nombreEntidad = s.nombreEntidad,
                                notasRec = s.notasRec,
                                numeroRec = s.numeroRec,
                            };
                            return nr;
                        }).ToList();
                    }
                }
                rt.ListaD = list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //
            return rt;
        }

        public OOB.Resultado.Lista<OOB.ReportesCxc.DetallePorMedioPago.Ficha> 
            ReportesCxc_DetalleCobranza_PorMedioPago(OOB.ReportesCxc.DetallePorMedioPago.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.ReportesCxc.DetallePorMedioPago.Ficha>();
            //
            try
            {
                var filtroDTO = new DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Filtro()
                {
                    desde = filtro.desde,
                    hasta = filtro.hasta,
                    codSucursal = filtro.codSucursal,
                    idCliente = filtro.idCliente,
                };
                var r01 = MyData.ReportesCxc_DetalleCobranza_PorMedioPago(filtroDTO);
                if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                var list = new List<OOB.ReportesCxc.DetallePorMedioPago.Ficha>();
                if (r01.Lista != null)
                {
                    if (r01.Lista.Count > 0)
                    {
                        list = r01.Lista.Select(s =>
                        {
                            var nr = new OOB.ReportesCxc.DetallePorMedioPago.Ficha()
                            {
                                ciRifEntidad = s.ciRifEntidad,
                                codigoMedio = s.codigoMedio,
                                codigoSuc = s.codigoSuc,
                                fechaRec = s.fechaRec,
                                importeDivisa = s.importeDivisa,
                                montoPorAnticipoCargado = s.montoPorAnticipoCargado,
                                montoPorAnticipoUsado = s.montoPorAnticipoUsado,
                                montoPorNtCreditoUsado = s.montoPorNtCreditoUsado,
                                montoRecibido = s.montoRecibido,
                                nombreEntidad = s.nombreEntidad,
                                nombreMedio = s.nombreMedio,
                                notasRec = s.notasRec,
                                numeroRec = s.numeroRec,
                                opAplicaConversion = s.opAplicaConversion,
                                opBanco = s.opBanco,
                                opDetalle = s.opDetalle,
                                opFecha = s.opFecha,
                                opMonto = s.opMonto,
                                opNroCta = s.opNroCta,
                                opNroRef = s.opNroRef,
                                opTasa = s.opTasa,
                            };
                            return nr;
                        }).ToList();
                    }
                }
                rt.ListaD = list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //
            return rt;
        }
    }
}