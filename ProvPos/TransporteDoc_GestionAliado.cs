using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.GestionAliados.ObtenerLista.Ficha> 
            TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.GestionAliados.ObtenerLista.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"SELECT 
                                        auto as idDoc,
                                        documento as numeroDoc,
                                        fecha as fechaDoc,
                                        razon_social as entidadDoc,
                                        ci_rif as ciRifEntidadDoc,
                                        auto_cliente as idEntidadDoc,
                                        monto_divisa as montoDivDoc,
                                        tipo as codigoTipoDoc,
                                        documento_nombre as nombreDoc,
                                        estatus_anulado as estatusDoc,
                                        serie as serieDoc
                                    from ventas
                                    where auto=@idDoc";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", id);
                    var _sql = _sql_1;
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.Documento.GestionAliados.ObtenerLista.Documento>(_sql, p1).FirstOrDefault();
                    if (_ent == null) 
                    {
                        throw new Exception("DOCUMENTO NO ENCONTRADO");
                    }
                    //
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", id);
                    _sql_1 = @"SELECT 
                                    aliado.id as aliadoId,
                                    aliado.codigo as aliadoCod,
                                    aliado.ciRif as aliadoCiRif,
                                    aliado.nombreRazonSocial as aliadoNombre,
                                    aliadoDoc.estatus_anulado as estatusAnuladoAliadoDoc,
                                    aliadoDoc.importe_divisa as importeDivAliadoDoc,
                                    aliadoDoc.acumulado_divisa as acumunuladoDivAliadoDoc,
                                    aliadoDoc.id as idRefAliadoDoc
                                FROM transp_aliado_doc as aliadoDoc
                                join transp_aliado as aliado on aliado.id=aliadoDoc.id_aliado
                                where aliadoDoc.id_doc_ref=@idDoc";
                    _sql = _sql_1;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Documento.GestionAliados.ObtenerLista.Item>(_sql, p1).ToList();
                    result.Entidad = new DtoTransporte.Documento.GestionAliados.ObtenerLista.Ficha()
                    {
                        documento=_ent,
                        items = _lst,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.Resultado 
            TransporteDocumento_GestionAliados_AnularAliado(DtoTransporte.Documento.GestionAliados.AnularAliado.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        //
                        var _ssql = @"select 1 
                                        from 
                                            ventas_transp_aliado 
                                        where 
                                            id_venta=@idVenta and id_aliado=@idAliado and estatus_anulado='0'";
                        var _tp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", ficha.idDocumento);
                        var _tp2 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", ficha.idAliado);
                        var _rtp = cn.Database.SqlQuery<int>(_ssql, _tp1, _tp2).FirstOrDefault();
                        if (_rtp != null) 
                        {
                            if (_rtp == 1) 
                            {
                                _ssql = @"update ventas_transp_aliado 
                                            set estatus_anulado='1'
                                        where 
                                            id_venta=@idVenta and id_aliado=@idAliado and estatus_anulado='0'";
                                _tp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", ficha.idDocumento);
                                _tp2 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", ficha.idAliado);
                                var _rtp_2 = cn.Database.ExecuteSqlCommand(_ssql, _tp1, _tp2);
                                if (_rtp_2 == 0) 
                                {
                                    throw new Exception("PROBLEMA AL ACTUALIZAR ESTATUS ALIADO EN EL DOCUMENTO");
                                }
                            }
                        }


                        //
                        //ALIADOS 
                        var _sql = @"update transp_aliado set 
                                        monto_debitos_mon_divisa=monto_debitos_mon_divisa+@monto
                                    where id=@idAliado";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", ficha.idAliado);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.montoAnular);
                        var r1 = cn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR SALDO ALIADO");
                        }
                        cn.SaveChanges();
                        //
                        //ALIADO DOCUMENTO
                        _sql = @"update transp_aliado_doc set 
                                    estatus_anulado='1' 
                                where id=@idRefAliadoDoc and 
                                        acumulado_divisa=0 and
                                        estatus_anulado='0'";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idRefAliadoDoc", ficha.idRefAliadoDoc);
                        r1 = cn.Database.ExecuteSqlCommand(_sql, p1);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR ESTATUS ALIADO-DOC");
                        }
                        cn.SaveChanges();
                        //
                        //ALIADO DOCUMENTO/SERVICIOS PRESTADOS
                        _sql = @"update transp_aliado_doc_servicio set 
                                    estatus_anulado='1' 
                                where id_aliado_doc=@idRefAliadoDoc and 
                                        monto_acumulado_div=0 and
                                        estatus_anulado='0'";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idRefAliadoDoc", ficha.idRefAliadoDoc);
                        r1 = cn.Database.ExecuteSqlCommand(_sql, p1);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR ESTATUS ALIADO-DOC-SERVICIOS");
                        }
                        cn.SaveChanges();
                        //
                        // INSERTAR MOVIMIENTO ANULADO DEL ALIADO EN EL DOCUMENTO
                        _sql = @"INSERT INTO transp_aliado_doc_anulado (
                                    id,
                                    fecha,
                                    motivo,
                                    id_aliado_doc,
                                    id_aliado,
                                    monto,
                                    auto_usuario,
                                    nombre_usuario
                                )
                                VALUES (
                                    NULL,
                                    CURRENT_TIMESTAMP,
                                    @motivo, 
                                    @idRefAliadoDoc,
                                    @idAliado, 
                                    @montoAnular, 
                                    @idUsuario, 
                                    @nombreUsuario)";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idRefAliadoDoc", ficha.idRefAliadoDoc);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", ficha.idAliado);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@montoAnular", ficha.montoAnular);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@idUsuario", ficha.idUsuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@nombreUsuario", ficha.nombreUsuario);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@motivo", ficha.motivo);
                        r1 = cn.Database.ExecuteSqlCommand(_sql, p1, p2, p3, p4, p5, p6);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR REGISTRO EN ALIADO-DOC-ANULADO");
                        }
                        cn.SaveChanges();
                        //
                        ts.Complete();
                    }
                };
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoEntidad<int> 
            TransporteDocumento_GestionAliados_InsertarAliado(DtoTransporte.Documento.GestionAliados.InsertarAliado.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<int>();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        //
                        //ALIADOS USADO
                        var _sql = @"update transp_aliado set 
                                        monto_debitos_mon_divisa=monto_debitos_mon_divisa+@monto
                                    where id=@idAliado";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.montoDiv);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", ficha.idAliado);
                        var r1= cn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR SALDO ALIADO");
                        }
                        cn.SaveChanges();
                        //
                        // INSERTAR ALIADO DOCUMENTO 
                        _sql = @"INSERT INTO transp_aliado_doc 
                                        (
                                            id, 
                                            id_aliado, 
                                            id_doc_ref, 
                                            id_cliente,
                                            doc_numero, 
                                            doc_fecha, 
                                            doc_codigo, 
                                            doc_nombre,
                                            importe_divisa, 
                                            acumulado_divisa,
                                            estatus_anulado
                                        )
                                    VALUES 
                                        (
                                            NULL,
                                            @idAliado, 
                                            @idDocRef, 
                                            @idCliente,
                                            @docNumero, 
                                            @docFecha, 
                                            @docCodigo, 
                                            @docNombre,
                                            @importeDivisa, 
                                            0,
                                            '0'
                                        )";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", ficha.idAliado);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", ficha.docId);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.docIdCliente);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@docNumero", ficha.docNumero);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@docFecha", ficha.docFecha);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@docCodigo", ficha.docCodigo);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@docNombre", ficha.docNombre);
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@importeDivisa", ficha.montoDiv);
                        r1 = cn.Database.ExecuteSqlCommand(_sql, p1, p2, p3, p4, p5, p6, p7, p8);
                        cn.SaveChanges();
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR ALIADO - DOCUMENTO");
                        }
                        //
                        //
                        var sql = "SELECT LAST_INSERT_ID()";
                        var idAliadoDoc = cn.Database.SqlQuery<int>(sql).FirstOrDefault();
                        //
                        //SERVICIOS PRESTADOS POR EL ALIADO
                        _sql = @"INSERT INTO transp_aliado_doc_servicio (
                                        id, 
                                        id_aliado_doc, 
                                        id_serv, 
                                        codigo_serv, 
                                        desc_serv, 
                                        detalle_serv, 
                                        importe_serv_div, 
                                        monto_acumulado_div, 
                                        estatus_anulado) 
                                    VALUES (
                                        NULL, 
                                        @id_aliado_doc, 
                                        @id_serv, 
                                        @codigo_serv, 
                                        @desc_serv, 
                                        @detalle_serv, 
                                        @importe_serv_div, 
                                        0, 
                                        '0')";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@id_aliado_doc", idAliadoDoc);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@id_serv", ficha.idServ);
                        p3 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_serv", ficha.codigoServ);
                        p4 = new MySql.Data.MySqlClient.MySqlParameter("@desc_serv", ficha.descServ);
                        p5 = new MySql.Data.MySqlClient.MySqlParameter("@detalle_serv", ficha.detalleServ);
                        p6 = new MySql.Data.MySqlClient.MySqlParameter("@importe_serv_div", ficha.montoDiv);
                        r1 = cn.Database.ExecuteSqlCommand(_sql, p1, p2, p3, p4, p5, p6);
                        cn.SaveChanges();
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR ALIADO - DOCUMENTO - SERVICIO");
                        }
                        //
                        ts.Complete();
                        //
                        result.Entidad = idAliadoDoc;
                    }
                };
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
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