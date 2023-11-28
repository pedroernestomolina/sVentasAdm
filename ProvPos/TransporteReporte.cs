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
                            aliado.ciRif as aliadoCiRif,
                            aliado.nombreRazonSocial as aliadoNombre,
                            aliado.codigo as aliadoCodigo,
                            vta.ci_rif as clienteCiRif,
                            vta.razon_social as clienteNombre,
                            aliadoDoc.doc_fecha as fechaDoc,
                            aliadoDoc.doc_numero as numDoc,
                            aliadoDoc.doc_nombre as nombreDoc,
                            aliadoServ.importe_serv_div as importeServ,
                            aliadoServ.id_serv as servId,
                            aliadoServ.codigo_serv as servCodigo,
                            aliadoServ.desc_serv as servDesc,
                            aliadoServ.detalle_serv as servDetalle
                        from transp_aliado_doc as aliadoDoc 
                        join transp_aliado_doc_servicio as aliadoServ on aliadoServ.id_aliado_doc=aliadoDoc.id
                        join transp_aliado as aliado on aliado.id=aliadoDoc.id_aliado
                        join ventas as vta on vta.auto=aliadoDoc.id_doc_ref
                        where aliadoDoc.estatus_anulado='0'";
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
        //
        public DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.EdoCta.Ficha> 
            TransporteReporte_Cxc_EdoCta(string idCliente)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.EdoCta.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"SELECT 
                                    codigo as codigoCli,
                                    razon_social as nombreCli,
                                    ci_rif as ciRifCli,
                                    dir_fiscal as dirCli,
                                    telefono as telCli
                                FROM clientes
                                where auto=@idCliente";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", idCliente);
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.Reporte.Cxc.EdoCta.Cliente>(_sql, p1).FirstOrDefault();
                    if (_ent == null) 
                    {
                        throw new Exception("FICHA CLENTE NO ENCONTRADA");
                    }
                    _sql = @"SELECT 
                                    fecha as fechaDoc,
                                    documento as nroDoc,
                                    tipo_documento as tipoDoc,
                                    fecha_vencimiento as fechaVencDoc,
                                    monto_divisa as importeDiv,
                                    signo as signoDoc,
                                    nota as notasDoc
                                FROM cxc 
                                where auto_cliente=@idCliente
                                    and estatus_anulado='0'";
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", idCliente);
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.Cxc.EdoCta.Movimiento>(_sql, p1).ToList();
                    result.Entidad = new DtoTransporte.Reporte.Cxc.EdoCta.Ficha()
                    {
                        entidad = _ent,
                        movimientos = _lst,
                    };
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