using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoResumen> 
            TransporteReporte_AliadoResumen()
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoResumen>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"select 
                                    codigo as codigo,
                                    ciRif as ciRif,
                                    nombreRazonSocial as aliado,
                                    monto_debitos_mon_divisa as montoDebitoMonDivisa,
                                    monto_creditos_mon_divisa as montoCreditoMonDivisa,
                                    monto_anticipos_mon_divisa as montoAnticiposMonDivisa,
                                    monto_debitos_anulado_mon_divisa as montoDebitoAnuladoMonDivisa,
                                    monto_creditos_anulado_mon_divisa as montoCreditoAnuladoMonDivisa,
                                    monto_anticipos_anulado_mon_divisa as montoAnticiposAnuladoMonDivisa
                                from transp_aliado";
                    var _sql_2 = @"";
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.AliadoResumen>(_sql).ToList();
                    result.Lista = _lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleDoc> 
            TransporteReporte_AliadoDetalleDoc()
        {
            var sql = @"SELECT 
                        aliado.cirif as rifAliado, 
                        aliado.nombreRazonSocial as nombreAliado,
                        aliado.codigo as codigoAliado,
                        cli.razon_social as nombreCliente, 
                        cli.ci_rif as rifCliente,
                        aliadoDoc.doc_numero as numDoc,
                        aliadoDoc.doc_fecha as fechaDoc,
                        aliadoDoc.doc_nombre as nombreDoc,
                        aliadoDoc.importe_divisa as importe,
                        aliadoDoc.acumulado_divisa as acumulado
                    FROM transp_aliado_doc as aliadoDoc
                    join transp_aliado as aliado on aliado.id=aliadoDoc.id_aliado
                    join clientes as cli on aliadoDoc.id_cliente=cli.auto
                    where aliadoDoc.estatus_anulado<>'1'";
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleDoc>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = sql;
                    var _sql_2 = @"";
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.AliadoDetalleDoc>(_sql).ToList();
                    result.Lista = _lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleServ> 
            TransporteReporte_AliadoDetalleServ()
        {
            var sql = @"SELECT 
                            aliado.id as aliadoId,
                            aliado.ciRif as alidoCiRif,
                            aliado.nombreRazonSocial as aliadoNombre,
                            ventas.documento as docNumero,
                            ventas.fecha as docFecha,
                            ventas.razon_social as docCliente,
                            ventas.documento_nombre as docNombre,
                            vtaItemServ.servicio_id as servId,
                            vtaItemServ.servicio_codigo as servCodigo,
                            vtaItemServ.servicio_desc as servDesc, 
                            detServ.importe servImporte,
                            itemPresp.servicio_codigo as prespServCodigo,
                            itemPresp.servicio_id as prespServId,
                            itemPresp.servicio_desc as prespServDesc, 
                            detPresp.importe as prespServImporte

                        FROM `ventas_transp_aliado` as vtaAliado
                        join ventas as ventas on vtaAliado.id_venta=ventas.auto
                        join transp_aliado as aliado on aliado.id=vtaAliado.id_aliado
                        join ventas_transp_detalle as vtaDetalle on vtaDetalle.id_venta=ventas.auto

                        left join ventas_transp_item as itemPresp on itemPresp.id_venta=vtaDetalle.id_doc_ref 
                        left join ventas_transp_item_aliado as detPresp on detPresp.id_venta=vtaDetalle.id_doc_ref

                        left join ventas_transp_item as vtaItemServ on vtaItemServ.id_item=vtaDetalle.id_item_servicio 
                        and vtaItemServ.id_venta=vtaDetalle.id_venta 
                        left join ventas_transp_item_aliado as detServ on detServ.id_venta=vtaItemServ.id_venta 
                        and detServ.id_aliado= vtaAliado.id_aliado
                        and detServ.id_item=vtaItemServ.id_item

                        where vtaAliado.estatus_anulado<>'1'";
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleServ>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = sql;
                    var _sql_2 = @"";
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.AliadoDetalleServ>(_sql).ToList();
                    result.Lista = _lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
    }
}