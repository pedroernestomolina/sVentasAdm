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
    }
}