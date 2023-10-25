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
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoTransporte.CxcMovCobro.ListaMov.Ficha> 
            Transporte_CxcMovCobro_GetLista(DtoTransporte.CxcMovCobro.ListaMov.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.CxcMovCobro.ListaMov.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"SELECT 
                                        auto as idMov,
                                        documento as numRecibo,
                                        fecha as fechaEmision,
                                        ci_rif as ciRifCliente,
                                        cliente as nombreCliente,
                                        importe_divisa as importeDiv,
                                        monto_recibido_divisa as montoRecibidoDiv,
                                        retenciones as montoRetDiv,
                                        anticipos as montoAnticipoDiv,
                                        estatus_anulado as estatusAnulado
                                    FROM cxc_recibos ";
                    var _sql_2 = @" WHERE 1=1 ";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro != null)
                    {
                        if (filtro.Desde.HasValue)
                        {
                            _sql_2 += " and fecha>=@desde ";
                            p1.ParameterName = "@desde";
                            p1.Value = filtro.Desde.Value;
                        }
                        if (filtro.Hasta.HasValue)
                        {
                            _sql_2 += " and fecha<=@hasta ";
                            p2.ParameterName = "@hasta";
                            p2.Value = filtro.Hasta.Value;
                        }
                        if (filtro.Estatus != "")
                        {
                            _sql_2 += " and estatus_anulado=@estatus ";
                            p4.ParameterName = "@estatus";
                            p4.Value = filtro.Estatus.Trim().ToUpper() == "I" ? "1" : "0";
                        }
                        if (filtro.IdCliente != "")
                        {
                            _sql_2 += " and auto_cliente=@idCliente";
                            p3.ParameterName = "@idCliente";
                            p3.Value = filtro.IdCliente;
                        }
                    }
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.CxcMovCobro.ListaMov.Ficha>(_sql, p1, p2, p3, p4).ToList();
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
        public DtoLib.Resultado 
            Transporte_CxcMovCobro_Anular(DtoTransporte.CxcMovCobro.Anular.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaNula = new DateTime(2000, 1, 1);
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        //
                        if (1 == 1)
                        {
                            //SE ACTUALIZA ESTATUS ANULADO DOCUMENTO CXC PAGO 
                            var sql = @"update cxc set 
                                            estatus_anulado='1'
                                        where auto=@idCxcPago";
                            var p1 = new MySql.Data.MySqlClient.MySqlParameter("idCxcPago", ficha.idCxcPago);
                            var rp1 = cn.Database.ExecuteSqlCommand(sql,p1);
                            if (rp1 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR ESTATUS ANULADO CXC - PAGO");
                            }
                            cn.SaveChanges();
                            //
                            //SE ACTUALIZA ESTATUS ANULADO DOCUMENTO CXC RECIBO
                            sql = @"update cxc_recibos set 
                                            estatus_anulado='1'
                                        where auto=@idCxcRecibo";
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("idCxcRecibo", ficha.idCxcRecibo);
                            var rp2 = cn.Database.ExecuteSqlCommand(sql, p1);
                            if (rp2 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR ESTATUS ANULADO CXC - RECIBO");
                            }
                            cn.SaveChanges();
                            //
                            //ACTUALIZAR SALDOS DE DOCUMENTOS INVOLUCRADOS EN EL COBRO 
                            foreach (var rg in ficha.docCobrado)
                            {
                                var tp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoDocCxC", rg.autoDocCxc);
                                var tp2 = new MySql.Data.MySqlClient.MySqlParameter("@montoDiv", rg.importeDiv);
                                sql = @"update cxc set 
                                            acumulado_divisa=acumulado_divisa-@montoDiv, 
                                            resta_divisa=resta_divisa+@montoDiv,
                                            estatus_cancelado='0'
                                            where auto=@autoDocCxC";
                                var rp3 = cn.Database.ExecuteSqlCommand(sql, tp1, tp2);
                                if (rp3 == 0) 
                                {
                                    throw new Exception("PROBLEMA AL ACTUALIZAR SALDO DCUMENTOS INVOLUCRADOS");
                                }
                                cn.SaveChanges();
                            }
                            //
                            //ACTUALIZAR ESTATUS ANULADO METODOS DE COBRO USADOS
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCxcRecibo", ficha.idCxcRecibo);
                            sql = @"update cxc_medio_pago set 
                                        estatus_anulado='1'
                                    where auto_recibo=@idCxcRecibo";
                            var rp4 = cn.Database.ExecuteSqlCommand(sql, p1);
                            cn.SaveChanges();
                            //
                            //ACTUALIZAR SALDO CLIENTE/ANTICIPO USADO
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                            var p2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.importe);
                            var p3 = new MySql.Data.MySqlClient.MySqlParameter("@montoAnticipo", ficha.anticipoRecibido);
                            sql = @"update clientes set 
                                                creditos=creditos-@monto,
                                                saldo=saldo+@monto,
                                                anticipos=anticipos+@montoAnticipo
                                                where auto=@idCliente";
                            var rp5 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3);
                            if (rp5 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR SALDO CLIENTE / ANTICIPO USADO");
                            }
                            cn.SaveChanges();
                            //
                            //
//                            if (ficha.notaAdm != null)
//                            {
//                                var t1 = cn.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc=a_cxc+1, a_cxc_nc=a_cxc_nc+1");
//                                var cntCxC = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
//                                var cntNCrAdm = cn.Database.SqlQuery<int>("select a_cxc_nc from sistema_contadores").FirstOrDefault();

//                                //DOCUMENTO CXC
//                                var _largo = 10 - ficha.SucPrefijo.Trim().Length;
//                                var xaCxC = ficha.SucPrefijo + cntCxC.ToString().Trim().PadLeft(_largo, '0');
//                                var docNCrAdm = (cntNCrAdm + 1).ToString().Trim().PadLeft(10, '0');
//                                var entCxC = new cxc()
//                                {
//                                    auto = xaCxC,
//                                    c_cobranza = 0m,
//                                    c_cobranzap = 0m,
//                                    fecha = fechaSistema.Date,
//                                    tipo_documento = ficha.notaAdm.tipoDoc,
//                                    documento = docNCrAdm,
//                                    fecha_vencimiento = fechaSistema.Date,
//                                    nota = ficha.notaAdm.notasDoc,
//                                    importe = ficha.notaAdm.montoDoc,
//                                    acumulado = 0m,
//                                    auto_cliente = ficha.notaAdm.autoCliente,
//                                    cliente = ficha.notaAdm.nombreCliente,
//                                    ci_rif = ficha.notaAdm.ciRifCliente,
//                                    codigo_cliente = ficha.notaAdm.codigoCliente,
//                                    estatus_cancelado = "0",
//                                    resta = ficha.notaAdm.montoDoc,
//                                    estatus_anulado = "0",
//                                    auto_documento = xaCxC,
//                                    numero = "",
//                                    auto_agencia = "0000000001",
//                                    agencia = "",
//                                    signo = ficha.notaAdm.signoDoc,
//                                    auto_vendedor = ficha.notaAdm.autoVendedor,
//                                    c_departamento = 0m,
//                                    c_ventas = 0m,
//                                    c_ventasp = 0m,
//                                    serie = "",
//                                    importe_neto = ficha.notaAdm.montoDoc,
//                                    dias = 0,
//                                    castigop = 0m,
//                                    cierre_ftp = "",
//                                    acumulado_divisa = 0m,
//                                    codigo_sucursal = ficha.notaAdm.codSucursal,
//                                    monto_divisa = ficha.notaAdm.montoDivisaDoc,
//                                    tasa_divisa = ficha.notaAdm.tasaCambioDoc,
//                                    resta_divisa = ficha.notaAdm.montoDivisaDoc,
//                                    importe_neto_divisa = ficha.notaAdm.montoDivisaDoc,
//                                    estatus_doc_cxc = "1",
//                                };
//                                cn.cxc.Add(entCxC);
//                                cn.SaveChanges();

//                                var xxcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.notaAdm.autoCliente);
//                                var xxcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.notaAdm.montoDivisaDoc);
//                                var xxsql_cli = @"update clientes set 
//                                                creditos=creditos+@monto,
//                                                saldo=saldo-@monto
//                                                where auto=@idCliente";
//                                var t2 = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
//                                cn.SaveChanges();
//                            }
                            //
                            //ACTUALIZAR ESTATUS ANULADO RETENCION POR COBRANZA
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCxcRecibo", ficha.idCxcRecibo);
                            sql = @"update cxc_recibos_retencion set 
                                        estatus_anulado='1'
                                    where auto_recibo=@idCxcRecibo";
                            var rp6 = cn.Database.ExecuteSqlCommand(sql, p1);
                            cn.SaveChanges();
                            //
                            //ACTUALIZAR CAJAS USADA
                            foreach (var rg in ficha.cajas)
                            {
                                sql = @"update transp_caja_mov set
                                        estatus_anulado_mov='1'
                                    where id=@idCajaMov";
                                p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCajaMov", rg.idCajaMov);
                                var rp7 = cn.Database.ExecuteSqlCommand(sql, p1);
                                if (rp7 == 0)
                                {
                                    throw new Exception("ERROR AL ACTUALIZAR ESTATUS ANULADO CAJA - MOVIMIENTO");
                                }
                                cn.SaveChanges();
                                //
                                // ACTUALIZAR ESTATUS CLIENTE-CAJA-MOV
                                sql = @"update cxc_recibos_caj set
                                        estatus_anulado='1'
                                    where id=@idMov";
                                p1 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", rg.idMov);
                                var rp8 = cn.Database.ExecuteSqlCommand(sql, p1);
                                if (rp8 == 0)
                                {
                                    throw new Exception("ERROR AL ACTUALIZAR ESTATUS ANULADO CLIENTE-ANITICIPO-CAJA");
                                }
                                cn.SaveChanges();
                                //
                                // ACTUALIZAR SALDO CAJAS 
                                sql = @"update transp_caja set 
                                        monto_ingreso_anulado=monto_ingreso_anulado+@monto
                                    where id=@idCaja";
                                p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCaja", rg.idCaja);
                                p2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", rg.montoMov);
                                var rp9 = cn.Database.ExecuteSqlCommand(sql, p1, p2);
                                if (rp9 == 0)
                                {
                                    throw new Exception("ERROR AL ACTUALIZAR CAJA - SALDO");
                                }
                                cn.SaveChanges();
                            }
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
        public DtoLib.ResultadoEntidad<DtoTransporte.CxcMovCobro.Anular.Ficha> 
            Transporte_CxcMovCobro_Anular_ObtenerData(string idRecibo)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.CxcMovCobro.Anular.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"SELECT 
                                    auto as idCxcRecibo,
                                    auto_cxc as idCxcPago,
                                    auto_cliente as idCliente,
                                    importe_divisa as importe,
                                    anticipos as anticipoRecibido
                                FROM cxc_recibos
                                where auto=@idRecibo";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idRecibo", idRecibo);
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.CxcMovCobro.Anular.Ficha>(sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        throw new Exception("MOVIMIENTO NO ENCONTRADO");
                    }
                    //
                    sql = @"SELECT
                                auto_cxc as autoDocCxc ,
                                importe_divisa as importeDiv
                            FROM cxc_documentos
                            where auto_cxc_recibo=@idRecibo";
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idRecibo", idRecibo);
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.CxcMovCobro.Anular.Documento>(sql, p1).ToList();
                    _ent.docCobrado = _lst;
                    //
                    sql = @"SELECT
                                id as idMov,
                                id_Caja as idCaja,
                                id_caja_mov as idCajaMov,
                                monto as montoMov
                            FROM cxc_recibos_caj
                            where auto_recibo=@idRecibo";
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idRecibo", idRecibo);
                    var _lstCj = cnn.Database.SqlQuery<DtoTransporte.CxcMovCobro.Anular.Caja>(sql, p1).ToList();
                    _ent.cajas = _lstCj;
                    //
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
    }
}