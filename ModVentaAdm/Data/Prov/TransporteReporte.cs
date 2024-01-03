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
        public OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.Resumen.Ficha>
            TransporteReporte_AliadoResumen(OOB.Transporte.Reporte.Aliado.Resumen.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.Resumen.Ficha>();
            var filtroDTO = new DtoTransporte.Reporte.Aliado.Resumen.Filtro()
            {
                Desde = filtro.Desde,
                Hasta = filtro.Hasta,
            };
            var r01 = MyData.TransporteReporte_AliadoResumen(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.Reporte.Aliado.Resumen.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Reporte.Aliado.Resumen.Ficha()
                        {
                            aliado = s.aliado,
                            ciRif = s.ciRif,
                            codigo = s.codigo,
                            importe = s.importe,
                            acumulado = s.acumulado,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = _lst;
            return rt;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.DetalleDoc.Ficha>
            TransporteReporte_AliadoDetalleDoc(OOB.Transporte.Reporte.Aliado.DetalleDoc.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.DetalleDoc.Ficha>();
            var filtroDTO = new DtoTransporte.Reporte.Aliado.DetalleDoc.Filtro()
            {
                Desde = filtro.Desde,
                Hasta = filtro.Hasta,
                IdAliado = filtro.IdAliado,
                IdCliente = filtro.IdCliente,
            };
            var r01 = MyData.TransporteReporte_AliadoDetalleDoc(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.Reporte.Aliado.DetalleDoc.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Reporte.Aliado.DetalleDoc.Ficha()
                        {
                            acumulado = s.acumulado,
                            codigoAliado = s.codigoAliado,
                            fechaDoc = s.fechaDoc,
                            importe = s.importe,
                            nombreAliado = s.nombreAliado,
                            nombreCliente = s.nombreCliente,
                            nombreDoc = s.nombreDoc,
                            numDoc = s.numDoc,
                            rifAliado = s.rifAliado,
                            rifCliente = s.rifCliente,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = _lst;
            return rt;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.DetalleServ.Ficha>
            TransporteReporte_AliadoDetalleServ(OOB.Transporte.Reporte.Aliado.DetalleServ.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Transporte.Reporte.Aliado.DetalleServ.Ficha>();
            var filtroDTO = new DtoTransporte.Reporte.Aliado.DetalleServ.Filtro()
            {
                Desde = filtro.Desde,
                Hasta = filtro.Hasta,
                IdAliado = filtro.IdAliado,
            };
            var r01 = MyData.TransporteReporte_AliadoDetalleServ(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.Reporte.Aliado.DetalleServ.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Reporte.Aliado.DetalleServ.Ficha()
                        {
                            aliadoCiRif = s.aliadoCiRif,
                            aliadoId = s.aliadoId,
                            aliadoNombre = s.aliadoNombre,
                            servId = s.servId,
                            servCodigo = s.servCodigo,
                            servDesc = s.servDesc,
                            aliadoCodigo = s.aliadoCodigo,
                            clienteCiRif = s.clienteCiRif,
                            clienteNombre = s.clienteNombre,
                            fechaDoc = s.fechaDoc,
                            importeServ = s.importeServ,
                            nombreDoc = s.nombreDoc,
                            numDoc = s.numDoc,
                            servDetalle = s.servDetalle,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = _lst;
            return rt;
        }
        //
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Reporte.Cxc.EdoCta.Ficha>
            TransporteReporte_Cxc_EdoCta(string idCliente)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Transporte.Reporte.Cxc.EdoCta.Ficha>();
            var r01 = MyData.TransporteReporte_Cxc_EdoCta(idCliente);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            if (r01.Entidad == null)
            {
                throw new Exception("PROBLEMA AL CARGAR DATA");
            }
            var _ent = new OOB.Transporte.Reporte.Cxc.EdoCta.Cliente()
            {
                ciRifCli = r01.Entidad.entidad.ciRifCli,
                codCli = r01.Entidad.entidad.codCli,
                dirCli = r01.Entidad.entidad.dirCli,
                nombreCli = r01.Entidad.entidad.nombreCli,
                telCli = r01.Entidad.entidad.telCli,
            };
            var _lst = new List<OOB.Transporte.Reporte.Cxc.EdoCta.Movimiento>();
            if (r01.Entidad.movimientos != null)
            {
                if (r01.Entidad.movimientos.Count > 0)
                {
                    _lst = r01.Entidad.movimientos.Select(s =>
                    {
                        var nr = new OOB.Transporte.Reporte.Cxc.EdoCta.Movimiento()
                        {
                            fechaDoc = s.fechaDoc,
                            fechaVencDoc = s.fechaVencDoc,
                            importeDiv = s.importeDiv,
                            notasDoc = s.notasDoc,
                            nroDoc = s.nroDoc,
                            signoDoc = s.signoDoc,
                            tipoDoc = s.tipoDoc,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.Entidad = new OOB.Transporte.Reporte.Cxc.EdoCta.Ficha()
            {
                entidad = _ent,
                movimientos = _lst,
            };
            return rt;
        }
        //
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Reporte.Cxc.PlanillaCobro.Ficha> 
            TransporteReporte_Cxc_CobroEmitido_Planilla(string idRec)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Reporte.Cxc.PlanillaCobro.Ficha>();
            var r01 = MyData.TransporteReporte_Cxc_CobroEmitido_Planilla(idRec);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var s = r01.Entidad;
            var nr = new OOB.Transporte.Reporte.Cxc.PlanillaCobro.Ficha()
            {
                ciRifProv = s.ciRifProv,
                dirProv = s.dirProv,
                estatusMov = s.estatusMov,
                fechaMov = s.fechaMov,
                importeDiv = s.importeDiv,
                montoRecDiv = s.montoRecDiv,
                nombreProv = s.nombreProv,
                notasMov = s.notasMov,
                reciboNro = s.reciboNro,
                tasaCambio = s.tasaCambio,
                montoPorAnticipo = s.montoPorAnticipo,
                doc = s.doc.Select(xd =>
                {
                    var tr = new OOB.Transporte.Reporte.Cxc.PlanillaCobro.Documento()
                    {
                        fechaEmisionDoc = xd.fechaEmisionDoc,
                        numeroDoc = xd.numeroDoc,
                        siglasDoc = xd.siglasDoc,
                        montoDiv = xd.montoDiv,
                        notas = xd.notas,
                    };
                    return tr;
                }).ToList(),
                metPago = s.metPago.Select(xm =>
                {
                    var tm = new OOB.Transporte.Reporte.Cxc.PlanillaCobro.MetodoPago()
                    {
                        codMet = xm.codMet,
                        descMet = xm.descMet,
                        opAplicaConversion = xm.opAplicaConversion,
                        opBanco = xm.opBanco,
                        opDetalle = xm.opDetalle,
                        opFecha = xm.opFecha,
                        opLote = xm.opLote,
                        opMonto = xm.opMonto,
                        opNroCta = xm.opNroCta,
                        opNroTransf = xm.opNroTransf,
                        opRef = xm.opNroRef,
                        opTasa = xm.opTasa,
                    };
                    return tm;
                }).ToList(),
                caja = s.caja.Select(xt =>
                {
                    var xxr = new OOB.Transporte.Reporte.Cxc.PlanillaCobro.Caja()
                    {
                        cjDesc = xt.cjDesc,
                        esDivisa = xt.esDivisa,
                        monto = xt.monto,
                        cjCod = xt.cjCod,
                    };
                    return xxr;
                }).ToList(),
            };
            result.Entidad = nr;
            return result;
        }
    }
}