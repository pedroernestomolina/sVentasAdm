using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.ResultadoId
            Transporte_Cliente_Anticipo_Agregar(DtoTransporte.ClienteAnticipo.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString().Trim().PadLeft(4, '0');
                        //
                        //INSERTAR ANTICIPO CLIENTE
                        var sql = @"INSERT INTO clientes_anticipo_mov (
                                    id, 
                                    id_cliente,
                                    fecha_emision, 
                                    fecha_registro, 
                                    cirif_cliente,
                                    nombre_razonsocial_cliente,
                                    monto_anticipo_mon_act,
                                    monto_anticipo_mon_div, 
                                    tasa_factor, 
                                    motivo, 
                                    aplica_ret,
                                    tasa_ret, 
                                    sustraendo_ret,
                                    retencion,
                                    total_retencion, 
                                    monto_recibido_mon_act,
                                    monto_recibido_mon_div, 
                                    estatus_anulado,
                                    recibo_numero)
                                VALUES (
                                    NULL, 
                                    @id_cliente,
                                    @fecha_emision, 
                                    @fecha_registro, 
                                    @cirif_cliente,
                                    @nombre_razonsocial_cliente,
                                    @monto_anticipo_mon_act,
                                    @monto_anticipo_mon_div, 
                                    @tasa_factor, 
                                    @motivo, 
                                    @aplica_ret,
                                    @tasa_ret, 
                                    @sustraendo_ret,
                                    @retencion,
                                    @total_retencion, 
                                    @monto_recibido_mon_act,
                                    @monto_recibido_mon_div, 
                                    '0',
                                    recibo_numero)";
                        var mov = ficha.mov;
                        var p00 = new MySql.Data.MySqlClient.MySqlParameter("@id_cliente", mov.idCliente);
                        var p01 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_emision", mov.fechaEmision);
                        var p02 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_registro", fechaSistema.Date);
                        var p03 = new MySql.Data.MySqlClient.MySqlParameter("@cirif_cliente", mov.ciRifCliente);
                        var p04 = new MySql.Data.MySqlClient.MySqlParameter("@nombre_razonsocial_cliente", mov.nombreRazonSocialCliente);
                        var p05 = new MySql.Data.MySqlClient.MySqlParameter("@monto_anticipo_mon_act", mov.montoAnticipoMonAct);
                        var p06 = new MySql.Data.MySqlClient.MySqlParameter("@monto_anticipo_mon_div", mov.montoAnticipoMonDiv);
                        var p07 = new MySql.Data.MySqlClient.MySqlParameter("@tasa_factor", mov.tasaFactor);
                        var p08 = new MySql.Data.MySqlClient.MySqlParameter("@motivo", mov.motivo);
                        var p09 = new MySql.Data.MySqlClient.MySqlParameter("@aplica_ret", mov.aplicaRet);
                        //
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter("@tasa_ret", mov.tasaRet);
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter("@sustraendo_ret", mov.sustraendoRet);
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter("@retencion", mov.retencion);
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter("@total_retencion", mov.totalRet);
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter("@monto_recibido_mon_act", mov.montoRecibidoMonAct);
                        var p15 = new MySql.Data.MySqlClient.MySqlParameter("@monto_recibido_mon_div", mov.montoRecibidoMonDiv);
                        var p16 = new MySql.Data.MySqlClient.MySqlParameter("@recibo_numero", mov.reciboNumero);
                        //
                        var r1 = cnn.Database.ExecuteSqlCommand(sql,
                            p00, p01, p02, p03, p04, p05, p06, p07, p08, p09,
                            p10, p11, p12, p13, p14, p15, p16);
                        if (r1 == 0)
                        {
                            result.Mensaje = "ERROR AL INSERTAR ANTICIPO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();
                        //
                        sql = "SELECT LAST_INSERT_ID()";
                        var idAnticipo = cnn.Database.SqlQuery<int>(sql).FirstOrDefault();
                        //
                        // ACTUALIZAR CLIENTE
                        sql = @"update clientes set
                                    anticipos= anticipos+@monto
                                where auto=@idCliente";
                        p01 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.mov.idCliente);
                        p02 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.mov.montoAnticipoMonDiv);
                        var r2 = cnn.Database.ExecuteSqlCommand(sql, p01, p02);
                        if (r2 == 0)
                        {
                            result.Mensaje = "ERROR AL ACTUALIZAR ANTICPO-CLIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();
                        //
                        //INSERTAR MOVIMIENTO - CAJA
                        foreach (var rg in ficha.caja)
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
                            p00 = new MySql.Data.MySqlClient.MySqlParameter("@id_caja", rg.idCaja);
                            p01 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_reg", fechaSistema.Date);
                            p02 = new MySql.Data.MySqlClient.MySqlParameter("@concepto_mov", cjMov.descMov);
                            p03 = new MySql.Data.MySqlClient.MySqlParameter("@monto_mov_mon_act", cjMov.montoMovMonAct);
                            p04 = new MySql.Data.MySqlClient.MySqlParameter("@monto_mov_mon_div", cjMov.montoMovMonDiv);
                            p05 = new MySql.Data.MySqlClient.MySqlParameter("@factor_cambio_mov", cjMov.factorCambio);
                            p06 = new MySql.Data.MySqlClient.MySqlParameter("@mov_fue_divisa", cjMov.movFueDivisa ? "1" : "0");
                            var r3 = cnn.Database.ExecuteSqlCommand(sql, p00, p01, p02, p03, p04, p05, p06);
                            if (r3 == 0)
                            {
                                result.Mensaje = "ERROR AL INSERTAR CAJA - MOVIMIENTO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                            //
                            sql = "SELECT LAST_INSERT_ID()";
                            var idCjMov = cnn.Database.SqlQuery<int>(sql).FirstOrDefault();
                            //
                            // INSERTAR CAJA AFECTADA POR ANITCIPO DEL ALIADO
                            sql = @"INSERT INTO clientes_anticipo_caj (
                                        id, 
                                        id_cliente_anticipo_mov, 
                                        id_caja, 
                                        id_caja_mov,
                                        id_cliente,
                                        fecha_reg,
                                        monto_mov,
                                        mov_fue_div,
                                        cod_caja,
                                        desc_caja,
                                        estatus_anulado) 
                                    VALUES (
                                        NULL,
                                        @id_cliente_anticipo_mov, 
                                        @id_caja, 
                                        @id_caja_mov,
                                        @id_cliente,
                                        @fecha_reg,
                                        @monto_mov,
                                        @mov_fue_div,
                                        @cod_caja,
                                        @desc_caja,
                                        '0')";
                            p00 = new MySql.Data.MySqlClient.MySqlParameter("@id_cliente_anticipo_mov", idAnticipo);
                            p01 = new MySql.Data.MySqlClient.MySqlParameter("@id_caja", rg.idCaja);
                            p02 = new MySql.Data.MySqlClient.MySqlParameter("@id_caja_mov", idCjMov);
                            p03 = new MySql.Data.MySqlClient.MySqlParameter("@id_cliente", ficha.mov.idCliente);
                            p04 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_reg", fechaSistema.Date);
                            p05 = new MySql.Data.MySqlClient.MySqlParameter("@monto_mov", rg.monto);
                            p06 = new MySql.Data.MySqlClient.MySqlParameter("@mov_fue_div", cjMov.movFueDivisa ? "1" : "0");
                            p07 = new MySql.Data.MySqlClient.MySqlParameter("@cod_caja", rg.codCaja);
                            p08 = new MySql.Data.MySqlClient.MySqlParameter("@desc_caja", rg.descCaja);
                            var r4 = cnn.Database.ExecuteSqlCommand(sql, p00, p01, p02, p03, p04, p05, p06, p07, p08);
                            if (r4 == 0)
                            {
                                result.Mensaje = "ERROR AL INSERTAR CLIENTE-ANITICIPO-CAJA";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                            //
                            // ACTUALIZAR SALDO CAJAS 
                            sql = @"update transp_caja set 
                                        monto_ingreso=monto_ingreso+@monto
                                    where id=@idCaja";
                            p00 = new MySql.Data.MySqlClient.MySqlParameter("@idCaja", rg.idCaja);
                            p01 = new MySql.Data.MySqlClient.MySqlParameter("@monto", rg.monto);
                            var r5 = cnn.Database.ExecuteSqlCommand(sql, p00, p01);
                            if (r5 == 0)
                            {
                                result.Mensaje = "ERROR AL ACTUALIZAR CAJA - SALDO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        }
                        ts.Commit();
                        result.Id = idAnticipo;
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
        public DtoLib.ResultadoEntidad<DtoTransporte.ClienteAnticipo.Obtener.Ficha> 
            Transporte_Cliente_Anticipo_Obtener_ById(string idCliente)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.ClienteAnticipo.Obtener.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"select 
                                        auto as id,
                                        ci_rif as ciRif,
                                        razon_social as nombreRazonSocial,
                                        anticipos as montoDiv
                                    FROM clientes 
                                    where auto=@idCliente";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", idCliente);
                    var _sql = _sql_1;
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.ClienteAnticipo.Obtener.Ficha>(_sql, p1).FirstOrDefault();
                    if (_ent == null) 
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "CLIENTE NO ENCONTRADO";
                        return result;
                    }
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
        public DtoLib.ResultadoLista<DtoTransporte.ClienteAnticipo.ListaMov.Ficha> 
            Transporte_Cliente_Anticipo_GetLista(DtoTransporte.ClienteAnticipo.ListaMov.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.ClienteAnticipo.ListaMov.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"SELECT 
                                        id as idMov,
                                        cirif_cliente as ciRifCliente,
                                        nombre_razonsocial_cliente as nombreCliente,
                                        monto_anticipo_mon_div as montoMonDiv,
                                        aplica_ret as aplicaRet,
                                        monto_recibido_mon_div as montoRecMonDiv,
                                        fecha_registro as fechaReg,
                                        estatus_anulado estatusAnulado
                                    FROM clientes_anticipo_mov ";
                    var _sql_2 = @" WHERE 1=1 ";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro != null)
                    {
                        if (filtro.Desde.HasValue)
                        {
                            _sql_2 += " and fecha_registro>=@desde ";
                            p1.ParameterName = "@desde";
                            p1.Value = filtro.Desde.Value;
                        }
                        if (filtro.Hasta.HasValue)
                        {
                            _sql_2 += " and fecha_registro<=@hasta ";
                            p2.ParameterName = "@hasta";
                            p2.Value = filtro.Hasta.Value;
                        }
                        if (filtro.Estatus != "")
                        {
                            _sql_2 += " and estatus_anulado=@estatus ";
                            p4.ParameterName = "@estatus";
                            p4.Value = filtro.Estatus.Trim().ToUpper() == "I" ? "1" : "0";
                        }
                        if (filtro.IdCliente!= "")
                        {
                            _sql_2 += " and id_cliente=@idCliente";
                            p3.ParameterName = "@idCliente";
                            p3.Value = filtro.IdCliente;
                        }
                    }
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.ClienteAnticipo.ListaMov.Ficha>(_sql, p1, p2, p3, p4).ToList();
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