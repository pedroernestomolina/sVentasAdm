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
        public OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoResumen> 
            TransporteReporte_AliadoResumen()
        {
            var rt = new OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoResumen>();
            var r01 = MyData.TransporteReporte_AliadoResumen();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.Reporte.AliadoResumen>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Reporte.AliadoResumen()
                        {
                            aliado = s.aliado,
                            ciRif = s.ciRif,
                            codigo = s.codigo,
                            montoAnticiposAnuladoMonDivisa = s.montoAnticiposAnuladoMonDivisa,
                            montoAnticiposMonDivisa = s.montoAnticiposMonDivisa,
                            montoCreditoAnuladoMonDivisa = s.montoCreditoAnuladoMonDivisa,
                            montoCreditoMonDivisa = s.montoCreditoMonDivisa,
                            montoDebitoAnuladoMonDivisa = s.montoDebitoAnuladoMonDivisa,
                            montoDebitoMonDivisa = s.montoDebitoMonDivisa,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = _lst;
            return rt;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoDetalleDoc> 
            TransporteReporte_AliadoDetalleDoc()
        {
            var rt = new OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoDetalleDoc>();
            var r01 = MyData.TransporteReporte_AliadoDetalleDoc();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.Reporte.AliadoDetalleDoc>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Reporte.AliadoDetalleDoc()
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
        public OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoDetalleServ> 
            TransporteReporte_AliadoDetalleServ()
        {
            var rt = new OOB.Resultado.Lista<OOB.Transporte.Reporte.AliadoDetalleServ>();
            var r01 = MyData.TransporteReporte_AliadoDetalleServ();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.Reporte.AliadoDetalleServ>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Reporte.AliadoDetalleServ()
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
    }
}