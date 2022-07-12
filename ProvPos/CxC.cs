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

        public DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Lista.Ficha> 
            CxC_Tool_CtasPendiente_GetLista(DtoLibPos.CxC.Tools.CtasPendiente.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codigoSuc", filtro.codSucursal);
                    var sql_1 = @"SELECT
                                        cl.auto as idCliente,
                                        cl.ci_rif as ciRif, 
                                        cl.razon_social as nombreRazonSocial, 
                                        sum(c.monto_divisa*c.signo) as importe, 
                                        sum(c.acumulado_divisa*c.signo) as acumulado, 
                                        count(*) as cntDocPend, 
                                        cl.doc_pendientes as limiteFacPend, 
                                        cl.limite_credito as limiteMontoCredito,
                                        (SELECT count(*)  
                                            FROM cxc 
                                            where auto_cliente=cl.auto 
                                            and estatus_cancelado='0'
                                            and tipo_documento='FAC'
                                            and estatus_anulado='0'
                                            GROUP BY  c.auto_cliente
                                        ) as cntFactPend
                                        FROM cxc as c
                                        join clientes as cl on c.auto_cliente=cl.auto
                                        where c.estatus_cancelado='0'
                                        and c.tipo_documento<>'PAG'
                                        and c.estatus_anulado='0'
                                        and c.codigo_sucursal=@codigoSuc
                                        group by c.auto_cliente";
                    var sql = sql_1;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CxC.Tools.CtasPendiente.Lista.Ficha>(sql,p1).ToList();
                    result.Lista = lst;
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
            CxC_Agregar(DtoLibPos.CxC.Agregar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

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

            return result;
        }
        public DtoLib.Resultado 
            CxC_AgregarNotaCreditoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

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
                            fecha = fechaSistema.Date ,
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

            return result;
        }
        public DtoLib.Resultado 
            CxC_AgregarNotaDebitoAdm(DtoLibPos.CxC.AgregarNotaAdm.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

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

                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.autoCliente);
                        var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.montoDivisaDoc);
                        var xsql_cli = @"update clientes set 
                                                debitos=debitos+@monto,
                                                saldo=saldo+@monto
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

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.CxC.DocumentosPend.Ficha>
            CxC_DocumentosPend_GetLista(DtoLibPos.CxC.DocumentosPend.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.CxC.DocumentosPend.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    c.auto as autoDoc,  
                                    c.fecha as fechaEmisionDoc,
                                    c.tipo_documento as tipoDoc,
                                    c.documento as numeroDoc,
                                    c.fecha_vencimiento as fechaVencDoc,
                                    c.nota as notasDoc, 
                                    c.monto_divisa as importeDoc,
                                    c.acumulado_divisa as acumuladoDoc, 
                                    c.auto_cliente as autoCliente,
                                    c.codigo_cliente as codifgoCliente,
                                    c.cliente as nombreCliente,
                                    c.ci_rif as ciRifCliente,
                                    c.signo as signoDoc,
                                    c.serie as serieDoc,
                                    c.dias as diasCreditoDoc, 
                                    c.tasa_divisa as tasaCambioDoc,
                                    c.codigo_sucursal as codSucursal, 
                                    v.auto as autoVendedor,
                                    v.nombre as nombreVendedor
                                    FROM cxc as c
                                    join vendedores as v on v.auto=c.auto_vendedor ";
                    var sql_2 = @" WHERE c.estatus_cancelado='0'
                                    and c.tipo_documento<>'PAG'
                                    and c.estatus_anulado='0' ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.idCliente != "")
                    {
                        sql_2 += " and c.auto_cliente=@idCliente ";
                        p1.ParameterName = "@idCliente";
                        p1.Value = filtro.idCliente;
                    }
                    var sql = sql_1 + sql_2;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CxC.DocumentosPend.Ficha>(sql, p1).ToList();
                    result.Lista = lst;
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
            CxC_Agregar_Verificar_ClienteCredito(string idCliente, decimal monto)
        {
            var rt = new DtoLib.Resultado();

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

            return rt;
        }
        public DtoLib.ResultadoEntidad<int> 
            CxC_Get_ContadorNotaCreditoAdm()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT a_cxc_nc
                                    FROM sistema_contadores ";
                    var sql = sql_1; 
                    var cnt = cnn.Database.SqlQuery<int>(sql).FirstOrDefault();
                    result.Entidad = cnt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<int> 
            CxC_Get_ContadorNotaDebitoAdm()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT a_cxc_nd
                                    FROM sistema_contadores ";
                    var sql = sql_1;
                    var cnt = cnn.Database.SqlQuery<int>(sql).FirstOrDefault();
                    result.Entidad = cnt;
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
            CxC_GestionCobro_Agregar(DtoLibPos.CxC.GestionCobro.Ficha ficha)
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
                        var sql = "update sistema_contadores set a_cxc=a_cxc+1, a_cxc_recibo=a_cxc_recibo+1, a_cxc_recibo_numero=a_cxc_recibo_numero+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aCxC = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var aCxCRecibo = cn.Database.SqlQuery<int>("select a_cxc_recibo from sistema_contadores").FirstOrDefault();
                        var aCxCReciboNumero = cn.Database.SqlQuery<int>("select a_cxc_recibo_numero from sistema_contadores").FirstOrDefault();
                        var largo = 10 - ficha.SucPrefijo.Length;
                        var autoCxCPago = ficha.SucPrefijo + aCxC.ToString().Trim().PadLeft(largo, '0');
                        var autoRecibo = ficha.SucPrefijo + aCxCRecibo.ToString().Trim().PadLeft(largo, '0');
                        var reciboNUmero = ficha.SucPrefijo + aCxCReciboNumero.ToString().Trim().PadLeft(largo, '0');

                        
                        if (1==1)
                        {
                            //SE GENERA EL DOCUMENTO DE PAGO
                            var sql_1 = @"INSERT INTO cxc (
                                        `auto` ,
                                        `c_cobranza` ,
                                        `c_cobranzap` ,
                                        `fecha` ,
                                        `tipo_documento` ,
                                        `documento` ,
                                        `fecha_vencimiento` ,
                                        `nota` ,
                                        `importe` ,
                                        `acumulado` ,
                                        `auto_cliente` ,
                                        `cliente` ,
                                        `ci_rif` ,
                                        `codigo_cliente` ,
                                        `estatus_cancelado` ,
                                        `resta` ,
                                        `estatus_anulado` ,
                                        `auto_documento` ,
                                        `numero` ,
                                        `auto_agencia` ,
                                        `agencia` ,
                                        `signo` ,
                                        `auto_vendedor` ,
                                        `c_departamento` ,
                                        `c_ventas` ,
                                        `c_ventasp` ,
                                        `serie` ,
                                        `importe_neto` ,
                                        `dias` ,
                                        `castigop` ,
                                        `cierre_ftp` ,
                                        `monto_divisa` ,
                                        `tasa_divisa` ,
                                        `acumulado_divisa` ,
                                        `codigo_sucursal`,
                                        resta_divisa,
                                        importe_neto_divisa,
                                        estatus_doc_cxc)
                                    VALUES ( 
                                        {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10},
                                        {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20},
                                        {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30},
                                        {31}, {32}, {33}, {34}, {35}, {36}, {37})";
                            var rt1 = cn.Database.ExecuteSqlCommand(sql_1,
                                autoCxCPago,
                                0m, 
                                0m, 
                                fechaSistema.Date,
                                "PAG", 
                                reciboNUmero, 
                                fechaSistema.Date, 
                                ficha.Cobro.Nota, 
                                ficha.Cobro.Importe, 
                                0m,
                                ficha.Cobro.AutoCliente,
                                ficha.Cobro.Cliente,
                                ficha.Cobro.CiRif, 
                                ficha.Cobro.CodigoCliente, 
                                "0", 
                                0m, 
                                "0",
                                autoRecibo, 
                                "", 
                                "0000000001",
                                "",
                                -1,
                                ficha.Cobro.AutoVendedor,
                                0m,
                                0m,
                                0m,
                                "",
                                0m,
                                0,
                                0m,
                                "",
                                ficha.Cobro.MontoDivisa,
                                ficha.Cobro.TasaDivisa,
                                0m,
                                ficha.SucPrefijo,
                                0m,
                                0m,
                                "1");
                            cn.SaveChanges();

                            //SE GENERA RECIBO DE COBRO
                            var sql_2 = @"INSERT INTO cxc_recibos (
                                            auto,
                                            documento,
                                            fecha,
                                            auto_usuario,
                                            importe,
                                            usuario,
                                            monto_recibido,
                                            cobrador,
                                            auto_cliente,
                                            cliente,
                                            ci_rif,
                                            codigo,
                                            estatus_anulado,
                                            direccion,
                                            telefono,
                                            auto_cobrador,
                                            anticipos,
                                            cambio,
                                            nota,
                                            codigo_cobrador,
                                            auto_cxc,
                                            retenciones,
                                            descuentos,
                                            hora,
                                            cierre,
                                            cierre_ftp, 
                                            importe_divisa,
                                            monto_recibido_divisa,
                                            cambio_divisa,
                                            estatus_doc_cxc, 
                                            codigo_sucursal)
                                        VALUES ( 
                                            {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10},
                                            {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20},
                                            {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30})";
                            var rt2 = cn.Database.ExecuteSqlCommand(sql_2,
                                autoRecibo,
                                reciboNUmero,
                                fechaSistema.Date,
                                ficha.Recibo.AutoUsuario,
                                ficha.Recibo.Importe,
                                ficha.Recibo.Usuario,
                                ficha.Recibo.MontoRecibido,
                                ficha.Recibo.Cobrador,
                                ficha.Recibo.AutoCliente,
                                ficha.Recibo.Cliente,
                                ficha.Recibo.CiRif,
                                ficha.Recibo.Codigo,
                                "0",
                                ficha.Recibo.Direccion,
                                ficha.Recibo.Telefono,
                                ficha.Recibo.AutoCobrador,
                                0m,
                                ficha.Recibo.Cambio,
                                ficha.Recibo.Nota,
                                ficha.Recibo.CodigoCobrador,
                                autoCxCPago,
                                0m,
                                0m,
                                fechaSistema.ToShortTimeString(),
                                "",
                                "",
                                ficha.Recibo.ImporteDivisa,
                                ficha.Recibo.MontoRecibidoDivisa,
                                ficha.Recibo.CambioDivisa,
                                "1", 
                                ficha.SucPrefijo);
                            cn.SaveChanges();

                            //LISTA DE DOCUMENTOS INCLUIDOS EN RECIBO DE COBRO
                            foreach (var rg in ficha.Documentos)
                            {
                                var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoCxC", rg.AutoCxC);
                                var p2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", rg.Importe);
                                var p3 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa", rg.ImporteDivisa);
                                var p4 = new MySql.Data.MySqlClient.MySqlParameter("@estatusDocCancelado", rg.EstatusDocCancelado);
                                var sql_3a = @"update cxc set 
                                                    acumulado=acumulado+@monto,
                                                    resta=resta-@monto,
                                                    acumulado_divisa=acumulado_divisa+@montoDivisa, 
                                                    resta_divisa=resta_divisa-@montoDivisa,
                                                    estatus_cancelado=@estatusDocCancelado
                                                    where auto=@autoCxC";
                                var rt3a = cn.Database.ExecuteSqlCommand(sql_3a, p1, p2, p3, p4);
                                cn.SaveChanges();

                                var sql_3b = @"INSERT INTO cxc_documentos (
                                            id,
                                            fecha,  
                                            tipo_documento, 
                                            documento,
                                            importe, 
                                            operacion,
                                            auto_cxc,
                                            auto_cxc_pago, 
                                            auto_cxc_recibo,    
                                            numero_recibo, 
                                            fecha_recepcion, 
                                            dias, 
                                            castigop, 
                                            comisionp, 
                                            cierre_ftp,
                                            importe_divisa,
                                            estatus_doc_cxc,
                                            codigo_sucursal, 
                                            notas)
                                        VALUES (
                                            {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10},
                                            {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18})";
                                var rt3b = cn.Database.ExecuteSqlCommand(sql_3b,
                                    rg.Id,
                                    fechaSistema.Date,
                                    rg.TipoDocumento,
                                    rg.DocumentoNro,
                                    rg.Importe,
                                    "Pago",
                                    rg.AutoCxC,
                                    autoCxCPago,
                                    autoRecibo,
                                    reciboNUmero,
                                    fechaNula.Date,
                                    0,
                                    0m,
                                    0m,
                                    "",
                                    rg.ImporteDivisa,
                                    "1",
                                    ficha.SucPrefijo,
                                    rg.Notas);
                                cn.SaveChanges();
                            }

                            //METODOS DE COBRO
                            foreach (var fp in ficha.MetodosPago)
                            {
                                var sql_InsertarCxCMedioPago = @"INSERT INTO cxc_medio_pago (
                                            auto_recibo, 
                                            auto_medio_pago,
                                            auto_agencia,
                                            medio, 
                                            codigo, 
                                            monto_recibido, 
                                            fecha,
                                            estatus_anulado, 
                                            numero, 
                                            agencia, 
                                            auto_usuario,
                                            lote, 
                                            referencia, 
                                            auto_cobrador, 
                                            cierre, 
                                            fecha_agencia, 
                                            cierre_ftp,
                                            opBanco,
                                            opNroCta,
                                            opNroRef,
                                            opFecha,
                                            opDetalle,
                                            opMonto,
                                            opTasa,
                                            opAplicaConversion,
                                            estatus_doc_cxc,
                                            codigo_sucursal)
                                        VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, 
                                                {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20},
                                                {21}, {22}, {23}, {24}, {25}, {26})";
                                var vCxcMedioPago = cn.Database.ExecuteSqlCommand(sql_InsertarCxCMedioPago,
                                    autoRecibo,
                                    fp.AutoMedioPago,
                                    "",
                                    fp.Medio,
                                    fp.Codigo,
                                    fp.MontoRecibido,
                                    fechaSistema.Date,
                                    "0",
                                    "",
                                    "",
                                    fp.AutoUsuario,
                                    fp.Lote,
                                    fp.Referencia,
                                    fp.AutoCobrador,
                                    fp.Cierre,
                                    fechaNula,
                                    "",
                                    //
                                    fp.OpBanco,
                                    fp.OpNroCta,
                                    fp.OpNroRef,
                                    fp.OpFecha.Date,
                                    fp.OpDetalle,
                                    fp.OpMonto,
                                    fp.OpTasa,
                                    fp.OpAplicaConversion,
                                    "1",
                                    ficha.SucPrefijo);
                                cn.SaveChanges();
                            }

                            var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.saldoCliente.idCliente );
                            var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.saldoCliente.monto);
                            var xsql_cli = @"update clientes set 
                                                creditos=creditos+@monto,
                                                saldo=saldo-@monto
                                                where auto=@idCliente";
                            var rt4 = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                            cn.SaveChanges();


                            if (ficha.notaAdm != null) 
                            {
                                var t1 = cn.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc=a_cxc+1, a_cxc_nc=a_cxc_nc+1");
                                var cntCxC = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                                var cntNCrAdm = cn.Database.SqlQuery<int>("select a_cxc_nc from sistema_contadores").FirstOrDefault();

                                //DOCUMENTO CXC
                                var _largo = 10 - ficha.SucPrefijo .Trim().Length;
                                var xaCxC = ficha.SucPrefijo + cntCxC.ToString().Trim().PadLeft(_largo, '0');
                                var docNCrAdm = (cntNCrAdm + 1).ToString().Trim().PadLeft(10, '0');
                                var entCxC = new cxc()
                                {
                                    auto = xaCxC,
                                    c_cobranza = 0m,
                                    c_cobranzap = 0m,
                                    fecha = fechaSistema.Date,
                                    tipo_documento = ficha.notaAdm.tipoDoc,
                                    documento = docNCrAdm,
                                    fecha_vencimiento = fechaSistema.Date,
                                    nota = ficha.notaAdm.notasDoc,
                                    importe = ficha.notaAdm.montoDoc,
                                    acumulado = 0m,
                                    auto_cliente = ficha.notaAdm.autoCliente,
                                    cliente = ficha.notaAdm.nombreCliente,
                                    ci_rif = ficha.notaAdm.ciRifCliente,
                                    codigo_cliente = ficha.notaAdm.codigoCliente,
                                    estatus_cancelado = "0",
                                    resta = ficha.notaAdm.montoDoc,
                                    estatus_anulado = "0",
                                    auto_documento = xaCxC,
                                    numero = "",
                                    auto_agencia = "0000000001",
                                    agencia = "",
                                    signo = ficha.notaAdm.signoDoc,
                                    auto_vendedor = ficha.notaAdm.autoVendedor,
                                    c_departamento = 0m,
                                    c_ventas = 0m,
                                    c_ventasp = 0m,
                                    serie = "",
                                    importe_neto = ficha.notaAdm.montoDoc,
                                    dias = 0,
                                    castigop = 0m,
                                    cierre_ftp = "",
                                    acumulado_divisa = 0m,
                                    codigo_sucursal = ficha.notaAdm.codSucursal,
                                    monto_divisa = ficha.notaAdm.montoDivisaDoc,
                                    tasa_divisa = ficha.notaAdm.tasaCambioDoc,
                                    resta_divisa = ficha.notaAdm.montoDivisaDoc,
                                    importe_neto_divisa = ficha.notaAdm.montoDivisaDoc,
                                    estatus_doc_cxc = "1",
                                };
                                cn.cxc.Add(entCxC);
                                cn.SaveChanges();

                                var xxcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.notaAdm.autoCliente);
                                var xxcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.notaAdm.montoDivisaDoc);
                                var xxsql_cli = @"update clientes set 
                                                creditos=creditos+@monto,
                                                saldo=saldo-@monto
                                                where auto=@idCliente";
                                var t2 = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
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
        public DtoLib.Resultado 
            CxC_GestionCobro_Verificar_Agregar(DtoLibPos.CxC.GestionCobro.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.clientes.Find(ficha.saldoCliente.idCliente);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ficha.saldoCliente.monto > ent.saldo)
                    {
                        rt.Mensaje = "MONTO ABONAR SUPERIOR AL SALDO DEL CLIENTE";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    foreach (var rg in ficha.Documentos)
                    {
                        var doc = Environment.NewLine + "Documento: " + rg.DocumentoNro + ", Tipo: " + rg.TipoDocumento;
                        var entCxC = cnn.cxc.Find(rg.AutoCxC);
                        if (entCxC == null)
                        {
                            rt.Mensaje = "[ ID ] DOCUMENTO CXC NO ENCONTRADO" + doc;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (entCxC.estatus_anulado.Trim().ToUpper() == "1") 
                        {
                            rt.Mensaje = "[ ID ] DOCUMENTO CXC ANULADO" + doc;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (entCxC.estatus_cancelado.Trim().ToUpper() == "1")
                        {
                            rt.Mensaje = "[ ID ] DOCUMENTO CXC YA PAGADO" + doc;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (rg.ImporteDivisa > entCxC.resta_divisa)
                        {
                            rt.Mensaje = "[ ID ] DOCUMENTO CXC MONTO ABONAR SUPERIOR AL MONTO QUE RESTA" + doc;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}