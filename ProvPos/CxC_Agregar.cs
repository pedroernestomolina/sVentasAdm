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
        public DtoLib.Resultado
            CxC_Agregar(DtoLibPos.CxC.Agregar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var r = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc=a_cxc+1");
                        if (r == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CXC";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var cntCxC = ctx.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        //DOCUMENTO CXC
                        var _largo = 10 - ficha.codSucursal.Trim().Length;
                        var fechaNula = new DateTime(2000, 01, 01);
                        var aCxC = ficha.codSucursal + cntCxC.ToString().Trim().PadLeft(_largo, '0');
                        var entCxC = new cxc()
                        {
                            auto = aCxC,
                            c_cobranza = 0m,
                            c_cobranzap = 0m,
                            fecha = ficha.fechaEmisionDoc,
                            tipo_documento = ficha.tipoDoc,
                            documento = ficha.numeroDoc,
                            fecha_vencimiento = ficha.fechaVencDoc,
                            nota = ficha.notasDoc,
                            importe = ficha.montoDoc,
                            acumulado = 0m,
                            auto_cliente = ficha.autoCliente,
                            cliente = ficha.nombreCliente,
                            ci_rif = ficha.ciRifCliente,
                            codigo_cliente = ficha.codigoCliente,
                            estatus_cancelado = "0",
                            resta = ficha.montoDoc,
                            estatus_anulado = "0",
                            auto_documento = aCxC,
                            numero = "",
                            auto_agencia = "0000000001",
                            agencia = "",
                            signo = ficha.signoDoc,
                            auto_vendedor = ficha.autoVendedor,
                            c_departamento = 0m,
                            c_ventas = 0m,
                            c_ventasp = 0m,
                            serie = ficha.serieDoc,
                            importe_neto = ficha.montoDoc,
                            dias = ficha.diasCreditoDoc,
                            castigop = 0m,
                            cierre_ftp = "",
                            acumulado_divisa = 0m,
                            codigo_sucursal = ficha.codSucursal,
                            monto_divisa = ficha.montoDivisaDoc,
                            tasa_divisa = ficha.tasaCambioDoc,
                            resta_divisa = ficha.montoDivisaDoc,
                            importe_neto_divisa = ficha.montoDivisaDoc,
                            estatus_doc_cxc = "1",
                        };
                        ctx.cxc.Add(entCxC);
                        ctx.SaveChanges();
                        //
                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.autoCliente);
                        var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.montoDivisaDoc);
                        var xsql_cli = @"update clientes set 
                                                debitos=debitos+@monto,
                                                saldo=saldo+@monto
                                                where auto=@idCliente";
                        if (ficha.signoDoc == -1)
                        {
                            xsql_cli = @"update clientes set 
                                                creditos=creditos+@monto,
                                                saldo=saldo-@monto
                                                where auto=@idCliente";
                        }
                        var r1 = ctx.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                        ctx.SaveChanges();
                        //
                        ts.Complete();
                    }
                }
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
        public DtoLib.Resultado
            CxC_AgregarNotaCreditoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var r = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc=a_cxc+1, a_cxc_nc=a_cxc_nc+1");
                        if (r == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CXC";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var cntCxC = ctx.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var cntNCrAdm = ctx.Database.SqlQuery<int>("select a_cxc_nc from sistema_contadores").FirstOrDefault();
                        //DOCUMENTO CXC
                        var _largo = 10 - ficha.codSucursal.Trim().Length;
                        var fechaNula = new DateTime(2000, 01, 01);
                        var aCxC = ficha.codSucursal + cntCxC.ToString().Trim().PadLeft(_largo, '0');
                        var docNCrAdm = (cntNCrAdm + 1).ToString().Trim().PadLeft(10, '0');
                        var entCxC = new cxc()
                        {
                            auto = aCxC,
                            c_cobranza = 0m,
                            c_cobranzap = 0m,
                            fecha = fechaSistema.Date,
                            tipo_documento = ficha.tipoDoc,
                            documento = docNCrAdm,
                            fecha_vencimiento = fechaSistema.Date,
                            nota = ficha.notasDoc,
                            importe = ficha.montoDoc,
                            acumulado = 0m,
                            auto_cliente = ficha.autoCliente,
                            cliente = ficha.nombreCliente,
                            ci_rif = ficha.ciRifCliente,
                            codigo_cliente = ficha.codigoCliente,
                            estatus_cancelado = "0",
                            resta = ficha.montoDoc,
                            estatus_anulado = "0",
                            auto_documento = aCxC,
                            numero = "",
                            auto_agencia = "0000000001",
                            agencia = "",
                            signo = ficha.signoDoc,
                            auto_vendedor = ficha.autoVendedor,
                            c_departamento = 0m,
                            c_ventas = 0m,
                            c_ventasp = 0m,
                            serie = "",
                            importe_neto = ficha.montoDoc,
                            dias = 0,
                            castigop = 0m,
                            cierre_ftp = "",
                            acumulado_divisa = 0m,
                            codigo_sucursal = ficha.codSucursal,
                            monto_divisa = ficha.montoDivisaDoc,
                            tasa_divisa = ficha.tasaCambioDoc,
                            resta_divisa = ficha.montoDivisaDoc,
                            importe_neto_divisa = ficha.montoDivisaDoc,
                            estatus_doc_cxc = "1",
                        };
                        ctx.cxc.Add(entCxC);
                        ctx.SaveChanges();
                        //
                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.autoCliente);
                        var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.montoDivisaDoc);
                        var xsql_cli = @"update clientes set 
                                                creditos=creditos+@monto,
                                                saldo=saldo-@monto
                                                where auto=@idCliente";
                        var r1 = ctx.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                        ctx.SaveChanges();
                        ts.Complete();
                    }
                }
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
        public DtoLib.Resultado
            CxC_AgregarNotaDebitoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var r = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc=a_cxc+1, a_cxc_nd=a_cxc_nd+1");
                        if (r == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CXC";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var cntCxC = ctx.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var cntNDbAdm = ctx.Database.SqlQuery<int>("select a_cxc_nd from sistema_contadores").FirstOrDefault();
                        //DOCUMENTO CXC
                        var _largo = 10 - ficha.codSucursal.Trim().Length;
                        var fechaNula = new DateTime(2000, 01, 01);
                        var aCxC = ficha.codSucursal + cntCxC.ToString().Trim().PadLeft(_largo, '0');
                        var docNDbAdm = (cntNDbAdm + 1).ToString().Trim().PadLeft(10, '0');
                        var entCxC = new cxc()
                        {
                            auto = aCxC,
                            c_cobranza = 0m,
                            c_cobranzap = 0m,
                            fecha = fechaSistema.Date,
                            tipo_documento = ficha.tipoDoc,
                            documento = docNDbAdm,
                            fecha_vencimiento = fechaSistema.Date,
                            nota = ficha.notasDoc,
                            importe = ficha.montoDoc,
                            acumulado = 0m,
                            auto_cliente = ficha.autoCliente,
                            cliente = ficha.nombreCliente,
                            ci_rif = ficha.ciRifCliente,
                            codigo_cliente = ficha.codigoCliente,
                            estatus_cancelado = "0",
                            resta = ficha.montoDoc,
                            estatus_anulado = "0",
                            auto_documento = aCxC,
                            numero = "",
                            auto_agencia = "0000000001",
                            agencia = "",
                            signo = ficha.signoDoc,
                            auto_vendedor = ficha.autoVendedor,
                            c_departamento = 0m,
                            c_ventas = 0m,
                            c_ventasp = 0m,
                            serie = "",
                            importe_neto = ficha.montoDoc,
                            dias = 0,
                            castigop = 0m,
                            cierre_ftp = "",
                            acumulado_divisa = 0m,
                            codigo_sucursal = ficha.codSucursal,
                            monto_divisa = ficha.montoDivisaDoc,
                            tasa_divisa = ficha.tasaCambioDoc,
                            resta_divisa = ficha.montoDivisaDoc,
                            importe_neto_divisa = ficha.montoDivisaDoc,
                            estatus_doc_cxc = "1",
                        };
                        ctx.cxc.Add(entCxC);
                        ctx.SaveChanges();
                        //
                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.autoCliente);
                        var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.montoDivisaDoc);
                        var xsql_cli = @"update clientes set 
                                                debitos=debitos+@monto,
                                                saldo=saldo+@monto
                                                where auto=@idCliente";
                        var r1 = ctx.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                        ctx.SaveChanges();
                        //
                        ts.Complete();
                    }
                }
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
        public DtoLib.Resultado
            CxC_Agregar_Verificar_ClienteCredito(string idCliente, decimal monto)
        {
            var rt = new DtoLib.Resultado();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.clientes.Find(idCliente);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        rt.Mensaje = "CLIENTE ANULADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus_credito.Trim().ToUpper() != "1")
                    {
                        rt.Mensaje = "CLIENTE NO ACTIVO PARA CREDITO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    //                    if ((ent.debitos + monto) > ent.limite_credito)
                    //                    {
                    //                        rt.Mensaje = "MONTO LIMITE ASIGNADO SUPERADO";
                    //                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                    //                        return rt;
                    //                    }
                    //                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", idCliente);
                    //                    var sql = @"SELECT count(*) as cnt 
                    //                                FROM cxc
                    //                                where tipo_documento='FAC' 
                    //                                and auto_cliente=@idCliente
                    //                                and estatus_cancelado='0' 
                    //                                and estatus_anulado='0'";
                    //                    var cnt = cnn.Database.SqlQuery<int>(sql, p1).FirstOrDefault();
                    //                    if ((cnt + 1) > ent.doc_pendientes)
                    //                    {
                    //                        rt.Mensaje = "MONTO LIMITE DOCUMENTOS PENDIENTES ASIGNADO SUPERADO";
                    //                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                    //                        return rt;
                    //                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
    }
}