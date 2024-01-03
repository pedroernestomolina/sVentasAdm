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
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.Resumen.Ficha> 
            TransporteReporte_AliadoResumen(DtoTransporte.Reporte.Aliado.Resumen.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.Resumen.Ficha>();
            var sql_1 = @"SELECT 
                        aliado.cirif as ciRif, 
                        aliado.nombreRazonSocial as aliado,
                        aliado.codigo as codigo,
                        sum(aliadoDoc.importe_divisa) as importe,
                        sum(aliadoDoc.acumulado_divisa) as acumulado
                    FROM transp_aliado_doc as aliadoDoc
                    join transp_aliado as aliado on aliado.id=aliadoDoc.id_aliado ";
            var sql_2 = @" where aliadoDoc.estatus_anulado<>'1' ";
            var sql_3 = @" group by aliado.cirif, aliado.nombreRazonSocial, aliado.codigo ";
            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
            var p3 = new MySql.Data.MySqlClient.MySqlParameter();
            if (filtro != null)
            {
                if (filtro.Desde.HasValue)
                {
                    p2.Value = filtro.Desde.Value;
                    p2.ParameterName = "@desde";
                    sql_2 += "and aliadoDoc.doc_fecha>=@desde ";
                }
                if (filtro.Hasta.HasValue)
                {
                    p3.Value = filtro.Hasta.Value;
                    p3.ParameterName = "@hasta";
                    sql_2 += "and aliadoDoc.doc_fecha<=@hasta ";
                }
            }
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = sql_1 + sql_2 + sql_3;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.Aliado.Resumen.Ficha>(sql, p1, p2, p3).ToList();
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
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleDoc.Ficha> 
            TransporteReporte_AliadoDetalleDoc(DtoTransporte.Reporte.Aliado.DetalleDoc.Filtro filtro)
        {
            var sql_1 = @"SELECT 
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
                    join clientes as cli on aliadoDoc.id_cliente=cli.auto ";
            var sql_2=@" where aliadoDoc.estatus_anulado<>'1' ";
            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
            var p3 = new MySql.Data.MySqlClient.MySqlParameter();
            var p4 = new MySql.Data.MySqlClient.MySqlParameter();
            if (filtro != null) 
            {
                if (filtro.IdAliado != -1) 
                {
                    p1.Value = filtro.IdAliado;
                    p1.ParameterName = "@idAliado";
                    sql_2 += "and aliado.id=@idAliado ";
                }
                if (filtro.IdCliente!="")
                {
                    p4.Value = filtro.IdCliente;
                    p4.ParameterName = "@idCliente";
                    sql_2 += "and cli.auto=@idCliente ";
                }
                if (filtro.Desde.HasValue)
                {
                    p2.Value = filtro.Desde.Value;
                    p2.ParameterName = "@desde";
                    sql_2 += "and aliadoDoc.doc_fecha>=@desde ";
                }
                if (filtro.Hasta.HasValue)
                {
                    p3.Value = filtro.Hasta.Value;
                    p3.ParameterName = "@hasta";
                    sql_2 += "and aliadoDoc.doc_fecha<=@hasta ";
                }
            }
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleDoc.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = sql_1 + sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.Aliado.DetalleDoc.Ficha>(sql, p1, p2, p3, p4).ToList();
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
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleServ.Ficha> 
            TransporteReporte_AliadoDetalleServ(DtoTransporte.Reporte.Aliado.DetalleServ.Filtro filtro)
        {
            var sql_1 = @"SELECT 
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
                        join ventas as vta on vta.auto=aliadoDoc.id_doc_ref ";
            var sql_2=@" where aliadoDoc.estatus_anulado<>'1' ";
            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
            var p3 = new MySql.Data.MySqlClient.MySqlParameter();
            if (filtro != null)
            {
                if (filtro.IdAliado != -1)
                {
                    p1.Value = filtro.IdAliado;
                    p1.ParameterName = "@idAliado";
                    sql_2 += "and aliado.id=@idAliado ";
                }
                if (filtro.Desde.HasValue)
                {
                    p2.Value = filtro.Desde.Value;
                    p2.ParameterName = "@desde";
                    sql_2 += "and aliadoDoc.doc_fecha>=@desde ";
                }
                if (filtro.Hasta.HasValue)
                {
                    p3.Value = filtro.Hasta.Value;
                    p3.ParameterName = "@hasta";
                    sql_2 += "and aliadoDoc.doc_fecha<=@hasta ";
                }
            }
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.Aliado.DetalleServ.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = sql_1 + sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.Aliado.DetalleServ.Ficha>(_sql, p1, p2, p3).ToList();
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
        //
        public DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.PlanillaCobro.Ficha> 
            TransporteReporte_Cxc_CobroEmitido_Planilla(string idRec)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Reporte.Cxc.PlanillaCobro.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"SELECT 
                                    rec.documento as reciboNro,
                                    rec.fecha as fechaMov,
                                    rec.importe_divisa as importeDiv,
                                    rec.monto_recibido_divisa as montoRecDiv,
                                    rec.tasa_cambio as tasaCambio,
                                    rec.nota as notasMov,
                                    rec.cliente as nombreProv,
                                    rec.ci_rif as ciRifProv,
                                    rec.direccion as dirProv,
                                    rec.estatus_anulado as estatusMov,
                                    rec.anticipos as montoPorAnticipo 
                                FROM cxc_recibos as rec
                                where auto=@idMov";
                    var p0 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idRec);
                    var ent = cnn.Database.SqlQuery<DtoTransporte.Reporte.Cxc.PlanillaCobro.Ficha>(sql, p0).FirstOrDefault();
                    if (ent == null)
                    {
                        throw new Exception("MOVIMIENTO NO ENCONTRADO");
                    }
                    //
                    sql = @"SELECT 
                                doc.tipo_documento as siglasDoc,
                                doc.fecha as fechaEmisionDoc,
                                doc.documento as numeroDoc,
                                doc.importe_divisa as montoDiv,
                                doc.notas as notas
                            FROM cxc_documentos as doc
                            WHERE auto_cxc_recibo=@idMov";
                    p0 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idRec);
                    var _doc = cnn.Database.SqlQuery<DtoTransporte.Reporte.Cxc.PlanillaCobro.Documento>(sql, p0).ToList();
                    //
                    sql = @"SELECT 
                                met.medio as descMet,
                                met.codigo as codMet,
                                '' as opLote,
                                '' as opNroTransf,
                                met.opBanco,
                                met.opNroCta,
                                met.opNroRef,
                                met.opFecha,
                                met.opDetalle,
                                met.opMonto,
                                met.opTasa,
                                met.opAplicaConversion
                            FROM cxc_medio_pago as met
                            where auto_recibo=@idMov";
                    p0 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idRec);
                    var _met = cnn.Database.SqlQuery<DtoTransporte.Reporte.Cxc.PlanillaCobro.MetodoPago>(sql, p0).ToList();
                    //
                    sql = @"SELECT 
                                cj.cod_caja as cjCod,
                                cj.desc_caja as cjDesc,
                                cj.monto as monto,
                                cj.es_divisa as esDivisa
                            FROM cxc_recibos_caj as cj
                            where auto_recibo=@idMov";
                    p0 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idRec);
                    var _cjs = cnn.Database.SqlQuery<DtoTransporte.Reporte.Cxc.PlanillaCobro.Caja>(sql, p0).ToList();
                    //
                    ent.doc = _doc;
                    ent.metPago = _met;
                    ent.caja = _cjs;
                    result.Entidad = ent;
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