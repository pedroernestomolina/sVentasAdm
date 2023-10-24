using LibEntityPos;
using System;
using System.Collections.Generic;
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
                            cn.SaveChanges();
                            //
                            //SE ACTUALIZA ESTATUS ANULADO DOCUMENTO CXC RECIBO
                            sql = @"update cxc_recibos set 
                                            estatus_anulado='1'
                                        where auto=@idCxcRecibo";
                            p1 = new MySql.Data.MySqlClient.MySqlParameter("idCxcPago", ficha.idCxcRecibo);
                            var rp2 = cn.Database.ExecuteSqlCommand(sql, p1);
                            cn.SaveChanges();
                            //
                            //ACTUALIZAR SALDOS DE DOCUMENTOS INVOLUCRADOS EN EL COBRO 
                            foreach (var rg in ficha.docCobrado)
                            {
                                var tp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoCxC", rg.autoCxC);
                                var tp2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", rg.importe);
                                var tp3 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa", rg.importeDivisa);
                                var tp4 = new MySql.Data.MySqlClient.MySqlParameter("@estatusDocCancelado", rg.EstatusDocCancelado);
                                sql = @"update cxc set 
                                            acumulado=acumulado-@monto,
                                            resta=resta+@monto,
                                            acumulado_divisa=acumulado_divisa-@montoDivisa, 
                                            resta_divisa=resta_divisa+@montoDivisa,
                                            estatus_cancelado='0'
                                            where auto=@autoCxC";
                                var rt3a = cn.Database.ExecuteSqlCommand(sql, tp1, tp2, tp3, tp4);
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

                            var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.autoCliente);
                            var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.Recibo.ImporteDivisa);
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
                                var _largo = 10 - ficha.SucPrefijo.Trim().Length;
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
                            //
                            if (ficha.montoAnticipo > 0m)
                            {
                                // ACTUALIZAR CLIENTE
                                sql = @"update clientes set
                                    anticipos= anticipos-@montoAnticipo
                                where auto=@autoCliente";
                                var t00 = new MySql.Data.MySqlClient.MySqlParameter("@autoCliente", ficha.autoCliente);
                                var t01 = new MySql.Data.MySqlClient.MySqlParameter("@montoAnticipo", ficha.montoAnticipo);
                                var rp0 = cn.Database.ExecuteSqlCommand(sql, t00, t01);
                                if (rp0 == 0)
                                {
                                    result.Mensaje = "ERROR AL ACTUALIZAR ANTICPO-CLIENTE";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                cn.SaveChanges();
                            }
                            if (ficha.retencion != null)
                            {
                                var fRet = ficha.retencion;
                                //INSERTAR REGISTRO DE RETENCION
                                var _sql = @"INSERT INTO cxc_recibos_retencion (
                                                auto_recibo, 
                                                monto_aplicar_mon_act, 
                                                tasa_ret, 
                                                retencion_mon_act, 
                                                sustraendo_mon_act, 
                                                total_ret, 
                                                factor_cambio, 
                                                auto_cliente, 
                                                fecha_registro, 
                                                estatus_anulado, 
                                                id) 
                                            VALUES(
                                                @auto_recibo, 
                                                @monto_aplicar_mon_act, 
                                                @tasa_ret, 
                                                @retencion_mon_act, 
                                                @sustraendo_mon_act, 
                                                @total_ret, 
                                                @factor_cambio, 
                                                @auto_cliente, 
                                                @fecha_registro, 
                                                '0',
                                                NULL)";
                                var t0 = new MySql.Data.MySqlClient.MySqlParameter("@auto_recibo", autoRecibo);
                                var t1 = new MySql.Data.MySqlClient.MySqlParameter("@monto_aplicar_mon_act", fRet.montoAplicarRetMonAct);
                                var t2 = new MySql.Data.MySqlClient.MySqlParameter("@tasa_ret", fRet.tasaRet);
                                var t3 = new MySql.Data.MySqlClient.MySqlParameter("@retencion_mon_act", fRet.retencionMonAct);
                                var t4 = new MySql.Data.MySqlClient.MySqlParameter("@sustraendo_mon_act", fRet.sustraendoMonAct);
                                var t5 = new MySql.Data.MySqlClient.MySqlParameter("@total_ret", fRet.totalRetMonAct);
                                var t6 = new MySql.Data.MySqlClient.MySqlParameter("@factor_cambio", fRet.factorCambio);
                                var t7 = new MySql.Data.MySqlClient.MySqlParameter("@auto_cliente", ficha.autoCliente);
                                var t8 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_registro", fechaSistema.Date);
                                var rp1 = cn.Database.ExecuteSqlCommand(_sql,
                                                        t0, t1, t2, t3, t4, t5, t6, t7, t8);
                                cn.SaveChanges();
                            }
                        }
                        if (ficha.cajas != null)
                        {
                            foreach (var rg in ficha.cajas)
                            {
                                sql = @"INSERT INTO transp_caja_mov (
                                        id, 
                                        id_caja, 
                                        fecha_reg, 
                                        concepto_mov, 
                                        tipo_mov, 
                                        monto_mov_mon_act,
                                        monto_mov_mon_div, 
                                        factor_cambio_mov, 
                                        estatus_anulado_mov,
                                        mov_fue_divisa,
                                        signo)
                                    VALUES (
                                        NULL, 
                                        @id_caja, 
                                        @fecha_reg, 
                                        @concepto_mov, 
                                        'I', 
                                        @monto_mov_mon_act,
                                        @monto_mov_mon_div, 
                                        @factor_cambio_mov, 
                                        '0',
                                        @mov_fue_divisa,
                                        1)";
                                var cjMov = rg.cajaMov;
                                var p00 = new MySql.Data.MySqlClient.MySqlParameter("@id_caja", rg.idCaja);
                                var p01 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_reg", fechaSistema.Date);
                                var p02 = new MySql.Data.MySqlClient.MySqlParameter("@concepto_mov", cjMov.descMov);
                                var p03 = new MySql.Data.MySqlClient.MySqlParameter("@monto_mov_mon_act", cjMov.montoMovMonAct);
                                var p04 = new MySql.Data.MySqlClient.MySqlParameter("@monto_mov_mon_div", cjMov.montoMovMonDiv);
                                var p05 = new MySql.Data.MySqlClient.MySqlParameter("@factor_cambio_mov", cjMov.factorCambio);
                                var p06 = new MySql.Data.MySqlClient.MySqlParameter("@mov_fue_divisa", cjMov.movFueDivisa ? "1" : "0");
                                var rp2 = cn.Database.ExecuteSqlCommand(sql, p00, p01, p02, p03, p04, p05, p06);
                                if (rp2 == 0)
                                {
                                    result.Mensaje = "ERROR AL INSERTAR CAJA - MOVIMIENTO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                cn.SaveChanges();
                                //
                                sql = "SELECT LAST_INSERT_ID()";
                                var idCjMov = cn.Database.SqlQuery<int>(sql).FirstOrDefault();
                                //
                                // INSERTAR CAJA AFECTADA POR ANITCIPO DEL ALIADO
                                sql = @"INSERT INTO cxc_recibos_caj (
                                        id, 
                                        auto_recibo,
                                        auto_cliente,
                                        fecha_registro,
                                        estatus_anulado,
                                        id_caja, 
                                        cod_caja,
                                        desc_caja,
                                        id_caja_mov,
                                        monto,
                                        es_divisa) 
                                    VALUES (
                                        NULL,
                                        @auto_recibo,
                                        @auto_cliente,
                                        @fecha_registro,
                                        '0',
                                        @id_caja, 
                                        @cod_caja,
                                        @desc_caja,
                                        @id_caja_mov,
                                        @monto,
                                        @es_divisa)";
                                p00 = new MySql.Data.MySqlClient.MySqlParameter("@auto_recibo", autoRecibo);
                                p01 = new MySql.Data.MySqlClient.MySqlParameter("@auto_cliente", ficha.autoCliente);
                                p02 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_registro", fechaSistema.Date);
                                p03 = new MySql.Data.MySqlClient.MySqlParameter("@id_caja", rg.idCaja);
                                p04 = new MySql.Data.MySqlClient.MySqlParameter("@cod_caja", rg.codCaja);
                                p05 = new MySql.Data.MySqlClient.MySqlParameter("@desc_caja", rg.descCaja);
                                p06 = new MySql.Data.MySqlClient.MySqlParameter("@id_caja_mov", idCjMov);
                                var p07 = new MySql.Data.MySqlClient.MySqlParameter("@monto", rg.monto);
                                var p08 = new MySql.Data.MySqlClient.MySqlParameter("@es_divisa", cjMov.movFueDivisa ? "1" : "0");
                                var rp3 = cn.Database.ExecuteSqlCommand(sql, p00, p01, p02, p03, p04, p05, p06, p07, p08);
                                if (rp3 == 0)
                                {
                                    result.Mensaje = "ERROR AL INSERTAR CXC-RECIBO-CAJA";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                cn.SaveChanges();
                                //
                                // ACTUALIZAR SALDO CAJAS 
                                sql = @"update transp_caja set 
                                        monto_ingreso=monto_ingreso+@monto
                                    where id=@idCaja";
                                p00 = new MySql.Data.MySqlClient.MySqlParameter("@idCaja", rg.idCaja);
                                p01 = new MySql.Data.MySqlClient.MySqlParameter("@monto", rg.monto);
                                var rp4 = cn.Database.ExecuteSqlCommand(sql, p00, p01);
                                if (rp4 == 0)
                                {
                                    result.Mensaje = "ERROR AL ACTUALIZAR CAJA - SALDO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
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
    }
}