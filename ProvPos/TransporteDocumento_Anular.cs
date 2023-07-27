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
        private class docVtaVerifAnular
        {
            public string estatus { get; set; }
            public string autoRemision { get; set; }
            public decimal monto { get; set; }
            public docVtaVerifAnular()
            {
                estatus = "";
                autoRemision = "";
                monto = 0m;
            }
        }
        public DtoLib.Resultado
            TransporteDocumento_AnularPresupuesto(DtoTransporte.Documento.Anular.Presupuesto.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO auditoria_documentos 
                                        (
                                            `auto_documento`, 
                                            `auto_sistema_documentos`, 
                                            `auto_usuario`, 
                                            `usuario`, 
                                            `codigo`, 
                                            `fecha`, 
                                            `hora`, 
                                            `memo`, 
                                            `estacion`, 
                                            `ip`
                                        ) 
                                    VALUES 
                                        (
                                            @p1, 
                                            @p2, 
                                            @p3, 
                                            @p4, 
                                            @p5, 
                                            @p6, 
                                            @p7, 
                                            @p8, 
                                            @p9, 
                                            ''
                                        )";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.idDoc);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.idSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.idUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL INSERTAR AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        //DOCUMENTO
                        sql = @"update ventas set estatus_anulado='1' 
                                    where auto=@p1 and 
                                        estatus_anulado<>'1' and
                                        auto_remision=''";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        //
                        sql = "update ventas_transp_item set estatus_anulado='1' where id_venta=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();
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
            return result;
        }

        public DtoLib.Resultado
            TransporteDocumento_AnularNotaEntrega(DtoTransporte.Documento.Anular.NotaEntrega.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO auditoria_documentos 
                                        (
                                            `auto_documento`, 
                                            `auto_sistema_documentos`, 
                                            `auto_usuario`, 
                                            `usuario`, 
                                            `codigo`, 
                                            `fecha`, 
                                            `hora`, 
                                            `memo`, 
                                            `estacion`, 
                                            `ip`
                                        ) 
                                    VALUES 
                                        (
                                            @p1, 
                                            @p2, 
                                            @p3, 
                                            @p4, 
                                            @p5, 
                                            @p6, 
                                            @p7, 
                                            @p8, 
                                            @p9, 
                                            ''
                                        )";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.idDocVenta);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.idSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.idUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v0 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v0 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL INSERTAR AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        //DOCUMENTO
                        sql = @"update cxc set estatus_anulado='1' 
                                    where auto=@autoCxC and 
                                        estatus_anulado<>'1' and
                                        acumulado_divisa=0";
                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@autoCxC", ficha.idDocCxC);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, t1);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS DEL DOCUMENTO CXC";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        sql = @"update ventas set estatus_anulado='1' 
                                    where auto=@p1 and 
                                        estatus_anulado<>'1' and
                                        auto_remision=''";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.idDocVenta);
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        //
                        sql = "update ventas_transp_detalle set estatus_anulado_doc='1' where id_venta=@p1";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.idDocVenta);
                        var v3a = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3a == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] DETALLES DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        if (ficha.docRef.Count > 0)
                        {
                            sql = "update ventas_transp_doc set estatus_anulado='1' where id_venta=@p1";
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.idDocVenta);
                            var v3b = cn.Database.ExecuteSqlCommand(sql, p1);
                            if (v3b == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] DOC INVOLUCRADOS";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cn.SaveChanges();
                        }

                        if (ficha.aliadosInv.Count > 0)
                        {
                            sql = "update ventas_transp_aliado set estatus_anulado='1' where id_venta=@p1";
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.idDocVenta);
                            var v3c = cn.Database.ExecuteSqlCommand(sql, p1);
                            if (v3c == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] ALIADOS INVOLUCRADOS";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cn.SaveChanges();
                        }

                        //
                        sql = "update ventas_transp_item set estatus_anulado='1' where id_item=@p1";
                        foreach (var rg in ficha.itemsServicio) 
                        {
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", rg.idItem);
                            var v3d = cn.Database.ExecuteSqlCommand(sql, p1);
                            if (v3d == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS SERVICIOS INVOLUCRADOS";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cn.SaveChanges();
                        }

                        //SALDO DEL CLIENTE EN DIVISA
                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                        var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@debito", ficha.montoDivisa);
                        var xsql_cli = @"update clientes set 
                                                debitos=debitos-@debito,
                                                saldo=saldo-@debito
                                                where auto=@idCliente";
                        var r_cli = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                        if (r_cli == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR SALDO CLIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        sql = @"update transp_aliado 
                                set monto_debitos_anulado_mon_divisa=monto_debitos_anulado_mon_divisa+@montoAliado
                                where id=@idAliado";
                        //ALIADOS ACTUALIZAR MONTOS
                        foreach (var rg in ficha.aliadosInv)
                        {
                            var x1 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", rg.idAliado);
                            var x2 = new MySql.Data.MySqlClient.MySqlParameter("@montoAliado", rg.montoDivisa);
                            var r4 = cn.Database.ExecuteSqlCommand(sql, x1, x2);
                            if (r4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR MONTO ALIADOS INVOLUCRADOS";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cn.SaveChanges();
                        }

                        //ACTUALIZAR DOC ALIADOS
                        sql = @"update transp_aliado_doc 
                                set estatus_anulado='1' 
                                where id=@idReg and id_doc_ref=@idDocVenta";
                        foreach (var rg in ficha.aliadosDoc)
                        {
                            var y1 = new MySql.Data.MySqlClient.MySqlParameter("@idReg", rg.idReg);
                            var y2 = new MySql.Data.MySqlClient.MySqlParameter("@idDocVenta", ficha.idDocVenta);
                            var r5 = cn.Database.ExecuteSqlCommand(sql, y1, y2);
                            if (r5 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR DOCUMENTOS ALIADOS INVOLUCRADOS";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cn.SaveChanges();
                        }

                        //ACTUALIZAR DOC REMISION
                        sql = @"update ventas 
                                set auto_remision='',
                                    documento_remision='',
                                    tipo_remision='',
                                    situacion=''
                                where auto=@idDocRef";
                        foreach (var rg in ficha.docRef)
                        {
                            var z1 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", rg.idDocRef);
                            var r6 = cn.Database.ExecuteSqlCommand(sql, z1);
                            if (r6 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR REMISION DOCUMENTOS";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cn.SaveChanges();
                        }

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
            return result;
        }
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Anular.NotaEntrega.dataAnular> 
            TransporteDocumento_AnularNotaEntrega_GetDataAnular(string idDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Anular.NotaEntrega.dataAnular>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _sql_1 = @"select 
                                        auto_cxc as idDocCxC,
                                        auto_cliente as idCliente,
                                        monto_divisa as montoDivisa
                                    FROM ventas where auto=@idDoc";
                    var _sql = _sql_1;
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.Documento.Anular.NotaEntrega.dataAnular>(_sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        throw new Exception("DOCUMENTO [ ID ] NO ENCONTRADO");
                    }

                    _sql = @"select 
                                id_aliado as idAliado,
                                importe_divisa as montoDivisa
                            FROM ventas_transp_aliado where id_venta=@idDoc";
                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _aliados = cnn.Database.SqlQuery<DtoTransporte.Documento.Anular.NotaEntrega.FichaAliado>(_sql, xp1).ToList();

                    _sql = @"select 
                                id as idReg
                            FROM transp_aliado_doc where id_doc_ref=@idDoc";
                    xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _aliadosDoc = cnn.Database.SqlQuery<DtoTransporte.Documento.Anular.NotaEntrega.FichaAliadoDoc>(_sql, xp1).ToList();

                    _sql = @"select 
                                id_item as idItem
                            FROM ventas_transp_item where id_venta=@idDoc";
                    xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _itemServ = cnn.Database.SqlQuery<DtoTransporte.Documento.Anular.NotaEntrega.FichaItemServ>(_sql, xp1).ToList();

                    _sql = @"select 
                                id_doc_ref as idDocRef
                            FROM ventas_transp_doc where id_venta=@idDoc";
                    xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _docRef = cnn.Database.SqlQuery<DtoTransporte.Documento.Anular.NotaEntrega.FichaDocRef>(_sql, xp1).ToList();

                    _ent.aliadosInv = _aliados;
                    _ent.aliadosDoc = _aliadosDoc;
                    _ent.itemsServ = _itemServ;
                    _ent.docRef = _docRef;
                    result.Entidad = _ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.Resultado
            TransporteDocumento_AnularNotaEntrega_Verifica(DtoTransporte.Documento.Anular.NotaEntrega.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        //DOCUMENTO
                        var _sql = @"select 
                                        estatus_anulado as estatus,
                                        acumulado_divisa as monto
                                    from cxc
                                    where auto=@autoCxC";
                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@autoCxC", ficha.idDocCxC);
                        var _docCxC = cn.Database.SqlQuery<docVtaVerifAnular>(_sql, t1).FirstOrDefault();
                        if (_docCxC == null)
                        {
                            result.Mensaje = "DOCUMENTO [ CXC ] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (_docCxC.estatus.Trim().ToUpper()=="1")
                        {
                            result.Mensaje = "DOCUMENTO [ CXC] ESTATUS INCORRECTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (_docCxC.monto >0)
                        {
                            result.Mensaje = "DOCUMENTO [ CXC] POSSE UN PAGO REGISTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        _sql = @"select 
                                    estatus_anulado as estatus,
                                    auto_remision as autoRemision
                                from ventas 
                                where auto=@p1";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.idDocVenta);
                        var _docVta = cn.Database.SqlQuery<docVtaVerifAnular>(_sql, p1).FirstOrDefault();
                        if (_docVta ==null)
                        {
                            result.Mensaje = "DOCUMENTO [ VENTA ] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (_docVta.estatus.Trim().ToUpper()=="1")
                        {
                            result.Mensaje = "DOCUMENTO [ VENTA ] ESTATUS INCORRECTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
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
            return result;
        }
    }
}