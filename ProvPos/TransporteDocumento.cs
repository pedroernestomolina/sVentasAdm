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
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado> 
            TransporteDocumento_AgregarPresupuesto(DtoTransporte.Documento.Agregar.Presupuesto.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString().Trim().PadLeft(4, '0');
                        var fechaNula = new DateTime(2000, 1, 1);

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_ventas_presupuesto=a_ventas_presupuesto+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var aDoc= cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var nDoc= cn.Database.SqlQuery<int>("select a_ventas_presupuesto from sistema_contadores").FirstOrDefault();
                        var largo =10;
                        var fechaVenc = fechaSistema.AddDays(ficha.diasCredito);
                        var autoDoc = aDoc.ToString().Trim().PadLeft(largo, '0');
                        var docNumero = nDoc.ToString().Trim().PadLeft(largo, '0');

                        var _sql = @"INSERT INTO `ventas` (
                                        `auto`, 
                                        `documento`, 
                                        `fecha`, 
                                        `fecha_vencimiento`, 
                                        `razon_social`, 
                                        `dir_fiscal`, 
                                        `ci_rif`, 
                                        `tipo`, 
                                        `exento`, 
                                        `base1`, 
                                        `base2`, 
                                        `base3`, 
                                        `impuesto1`, 
                                        `impuesto2`, 
                                        `impuesto3`, 
                                        `base`, 
                                        `impuesto`, 
                                        `total`, 
                                        `tasa1`, 
                                        `tasa2`, 
                                        `tasa3`, 
                                        `nota`, 
                                        `tasa_retencion_iva`, 
                                        `tasa_retencion_islr`, 
                                        `retencion_iva`, 
                                        `retencion_islr`, 
                                        `auto_cliente`, 
                                        `codigo_cliente`, 
                                        `mes_relacion`, 
                                        `control`, 
                                        `fecha_registro`, 
                                        `orden_compra`, 
                                        `dias`, 
                                        `descuento1`, 
                                        `descuento2`, 
                                        `cargos`, 
                                        `descuento1p`, 
                                        `descuento2p`, 
                                        `cargosp`, 
                                        `columna`, 
                                        `estatus_anulado`, 
                                        `aplica`, 
                                        `comprobante_retencion`, 
                                        `subtotal_neto`, 
                                        `telefono`, 
                                        `factor_cambio`, 
                                        `codigo_vendedor`, 
                                        `vendedor`, 
                                        `auto_vendedor`, 
                                        `fecha_pedido`, 
                                        `pedido`, 
                                        `condicion_pago`,
                                        `usuario`, 
                                        `codigo_usuario`, 
                                        `codigo_sucursal`, 
                                        `hora`, 
                                        `transporte`, 
                                        `codigo_transporte`, 
                                        `monto_divisa`, 
                                        `despachado`, 
                                        `dir_despacho`, 
                                        `estacion`, 
                                        `auto_recibo`, 
                                        `recibo`, 
                                        `renglones`, 
                                        `saldo_pendiente`, 
                                        `ano_relacion`, 
                                        `comprobante_retencion_islr`, 
                                        `dias_validez`, 
                                        `auto_usuario`, 
                                        `auto_transporte`, 
                                        `situacion`, 
                                        `signo`, 
                                        `serie`, 
                                        `tarifa`, 
                                        `tipo_remision`, 
                                        `documento_remision`, 
                                        `auto_remision`, 
                                        `documento_nombre`, 
                                        `subtotal_impuesto`, 
                                        `subtotal`, 
                                        `auto_cxc`, 
                                        `tipo_cliente`, 
                                        `planilla`, 
                                        `expediente`, 
                                        `anticipo_iva`, 
                                        `terceros_iva`, 
                                        `neto`, 
                                        `costo`, 
                                        `utilidad`, 
                                        `utilidadp`, 
                                        `documento_tipo`, 
                                        `ci_titular`, 
                                        `nombre_titular`, 
                                        `ci_beneficiario`, 
                                        `nombre_beneficiario`, 
                                        `clave`, 
                                        `denominacion_fiscal`, 
                                        `cambio`, 
                                        `estatus_validado`, 
                                        `cierre`, 
                                        `fecha_retencion`, 
                                        `estatus_cierre_contable`, 
                                        `cierre_ftp`, 
                                        `porct_bono_por_pago_divisa`, 
                                        `cnt_divisa_aplica_bono_por_pago_divisa`, 
                                        `monto_bono_por_pago_divisa`, 
                                        `monto_bono_en_divisa_por_pago_divisa`, 
                                        `monto_por_vuelto_en_efectivo`, 
                                        `monto_por_vuelto_en_divisa`, 
                                        `monto_por_vuelto_en_pago_movil`, 
                                        `cnt_divisa_por_vuelto_en_divisa`,
                                        `estatus_bono_por_pago_divisa`, 
                                        `estatus_vuelto_por_pago_movil`,
                                        `estatus_fiscal`, 
                                        `z_fiscal`
                                    ) 
                                    VALUES 
                                    (
                                        @autoDoc, 
                                        @numDoc, 
                                        @fechaEmi, 
                                        @fechaVen, 
                                        @razonSocial,
                                        @dirFiscal,
                                        @ciRif,
                                        @tipoDoc, 
                                        @montoExento, 
                                        @montoBase1, 
                                        @montoBase2, 
                                        @montoBase3, 
                                        @montoImp1, 
                                        @montoImp2, 
                                        @montoImp3, 
                                        @montoBase, 
                                        @montoImp, 
                                        @total, 
                                        @tasa1, 
                                        @tasa2, 
                                        @tasa3, 
                                        @nota,
                                        '0.00', 
                                        '0.00', 
                                        '0.00', 
                                        '0.00', 
                                        @autoCliente, 
                                        @codigoCliente, 
                                        @mesRelacion, 
                                        @control, 
                                        @fechaRegistro, 
                                        '',
                                        @diasCredito, 
                                        @dscto1, 
                                        @dscto2, 
                                        @cargos, 
                                        @dscto1p, 
                                        @dscto2p, 
                                        @cargosp, 
                                        '', 
                                        '', 
                                        '', 
                                        '', 
                                        @subtotalNeto,
                                        @telefono, 
                                        @factorCambio,
                                        @codigoVend, 
                                        @vendedor, 
                                        @autoVendedor, 
                                        '2000-01-01', 
                                        '', 
                                        @condPago, 
                                        @usuario, 
                                        @codUsuario, 
                                        @codSucursal, 
                                        @hora,
                                        '',
                                        '', 
                                        @montoDivisa, 
                                        '', 
                                        '', 
                                        @estacion, 
                                        '', 
                                        '', 
                                        @cntRenglones,
                                        '0.00', 
                                        @anoRelacion, 
                                        '', 
                                        @diasValidez, 
                                        @autoUsuario, 
                                        '',     
                                        '', 
                                        @signo, 
                                        '', 
                                        '', 
                                        @tipoRemision, 
                                        @docRemision, 
                                        @autoRemision, 
                                        @docNombre, 
                                        @subtotalImp, 
                                        @subTotal, 
                                        '', 
                                        '', 
                                        '', 
                                        '', 
                                        '0.00', 
                                        '0.00', 
                                        @neto,
                                        '0.00',
                                        '0.00', 
                                        '0.00', 
                                        @docCodigo,
                                        '', 
                                        '', 
                                        '', 
                                        '', 
                                        '', 
                                        '', 
                                        '0.00', 
                                        '', 
                                        '', 
                                        '2000-01-01',
                                        '', 
                                        '',
                                        '0.00',
                                        '0.00',
                                        '0.00',
                                        '0.00',
                                        '0.00',
                                        '0.00',
                                        '0.00',
                                        '0.00',
                                        '', 
                                        '', 
                                        '', 
                                        '0')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoDoc", autoDoc);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@numDoc", docNumero);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmi", fechaSistema.Date);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVen", fechaVenc);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@razonSocial", ficha.RazonSocial);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@dirFiscal",ficha.DirFiscal);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@ciRif",ficha.CiRif);
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@tipoDoc", ficha.TipoDoc);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@montoExento", ficha.montoExento);
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase1", ficha.montoBase1);
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase2", ficha.montoBase2);
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase3", ficha.montoBase3);
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp1", ficha.montoImpuesto1);
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp2", ficha.montoImpuesto2);
                        var p15 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp3", ficha.montoImpuesto3);
                        var p16 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase", ficha.montoBase);
                        var p17 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp", ficha.montoImpuesto);
                        var p18 = new MySql.Data.MySqlClient.MySqlParameter("@total", ficha.Total);
                        var p19 = new MySql.Data.MySqlClient.MySqlParameter("@tasa1", ficha.Tasa1);
                        var p20 = new MySql.Data.MySqlClient.MySqlParameter("@tasa2", ficha.Tasa2);
                        var p21 = new MySql.Data.MySqlClient.MySqlParameter("@tasa3", ficha.Tasa3);
                        var p22 = new MySql.Data.MySqlClient.MySqlParameter("@autoCliente", ficha.idCliente);
                        var p23 = new MySql.Data.MySqlClient.MySqlParameter("@codigoCliente", ficha.codCliente);
                        var p24 = new MySql.Data.MySqlClient.MySqlParameter("@mesRelacion", mesRelacion);
                        var p25 = new MySql.Data.MySqlClient.MySqlParameter("@control", ficha.control);
                        var p26 = new MySql.Data.MySqlClient.MySqlParameter("@fechaRegistro", fechaSistema.Date);
                        var p27 = new MySql.Data.MySqlClient.MySqlParameter("@diasCredito", ficha.diasCredito);
                        var p28 = new MySql.Data.MySqlClient.MySqlParameter("@dscto1", ficha.descuento1);
                        var p29 = new MySql.Data.MySqlClient.MySqlParameter("@dscto2", ficha.descuento2);
                        var p30 = new MySql.Data.MySqlClient.MySqlParameter("@cargos", ficha.cargos);
                        var p31 = new MySql.Data.MySqlClient.MySqlParameter("@dscto1p", ficha.descuento1p);
                        var p32 = new MySql.Data.MySqlClient.MySqlParameter("@dscto2p", ficha.descuento2p);
                        var p33 = new MySql.Data.MySqlClient.MySqlParameter("@cargosp", ficha.cargosp);
                        var p34 = new MySql.Data.MySqlClient.MySqlParameter("@subtotalNeto", ficha.subTotalNeto);
                        var p35 = new MySql.Data.MySqlClient.MySqlParameter("@telefono", ficha.telefono);
                        var p36 = new MySql.Data.MySqlClient.MySqlParameter("@factorCambio", ficha.factorCambio);
                        var p37 = new MySql.Data.MySqlClient.MySqlParameter("@codigoVend", ficha.codVendedor);
                        var p38 = new MySql.Data.MySqlClient.MySqlParameter("@vendedor", ficha.vendedor);
                        var p39 = new MySql.Data.MySqlClient.MySqlParameter("@autoVendedor", ficha.idVendedor);
                        var p40 = new MySql.Data.MySqlClient.MySqlParameter("@condPago", ficha.condPago);
                        var p41 = new MySql.Data.MySqlClient.MySqlParameter("@usuario", ficha.usuario);
                        var p42 = new MySql.Data.MySqlClient.MySqlParameter("@codUsuario", ficha.codUsuario);
                        var p43 = new MySql.Data.MySqlClient.MySqlParameter("@codSucursal", ficha.codSucursal);
                        var p44 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                        var p45 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa",ficha.montoDivisa);
                        var p46 = new MySql.Data.MySqlClient.MySqlParameter("@estacion",ficha.estacion);
                        var p47 = new MySql.Data.MySqlClient.MySqlParameter("@cntRenglones", ficha.cntRenglones);
                        var p48 = new MySql.Data.MySqlClient.MySqlParameter("@anoRelacion", anoRelacion);
                        var p49 = new MySql.Data.MySqlClient.MySqlParameter("@diasValidez", ficha.diasValidez);
                        var p50 = new MySql.Data.MySqlClient.MySqlParameter("@autoUsuario", ficha.idUsuario);
                        var p51 = new MySql.Data.MySqlClient.MySqlParameter("@signo", ficha.signo);
                        var p53 = new MySql.Data.MySqlClient.MySqlParameter("@tipoRemision", ficha.tipoRemision);
                        var p54 = new MySql.Data.MySqlClient.MySqlParameter("@docRemision", ficha.docRemision);
                        var p55 = new MySql.Data.MySqlClient.MySqlParameter("@autoRemision", ficha.idRemision);
                        var p56 = new MySql.Data.MySqlClient.MySqlParameter("@docNombre", ficha.docNombre);
                        var p57 = new MySql.Data.MySqlClient.MySqlParameter("@subtotalImp", ficha.subTotalImpuesto);
                        var p58 = new MySql.Data.MySqlClient.MySqlParameter("@subTotal", ficha.subTotal);
                        var p59 = new MySql.Data.MySqlClient.MySqlParameter("@neto", ficha.neto);
                        var p60 = new MySql.Data.MySqlClient.MySqlParameter("@docCodigo", ficha.docCodigo);
                        var p61 = new MySql.Data.MySqlClient.MySqlParameter("@nota", ficha.nota);
                        var r = cn.Database.ExecuteSqlCommand(_sql,
                                                                p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
                                                                p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                                                p21, p22, p23, p24, p25, p26, p27, p28, p29, p30,
                                                                p31, p32, p33, p34, p35, p36, p37, p38, p39, p40,
                                                                p41, p42, p43, p44, p45, p46, p47, p48, p49, p50,
                                                                p51, p53, p54, p55, p56, p57, p58, p59, p60,
                                                                p61);
                        cn.SaveChanges();
                        
                        var _sql_I = @"INSERT INTO ventas_transp_item (
                                    `id_item`, 
                                    `id_venta`,
                                    `servicio_desc`, 
                                    `solicitador_por`, 
                                    `modulo_cargar`, 
                                    `cnt_dias`, 
                                    `cnt_unidades`, 
                                    `precio_neto_divisa`, 
                                    `dscto`, 
                                    `alicuota_id`, 
                                    `alicuota_tasa`, 
                                    `alicuota_desc`, 
                                    `aliado_id`, 
                                    `aliado_cirif`, 
                                    `aliado_codigo`, 
                                    `aliado_desc`, 
                                    `aliado_precio_divisa`, 
                                    `notas`, 
                                    `fecha_doc`, 
                                    `hora_doc`, 
                                    `signo_doc`, 
                                    `tipo_doc`, 
                                    `estatus_anulado`,
                                    `importe`
                                ) 
                                VALUES 
                                (
                                    null, 
                                    @idVenta,
                                    @servicioDesc, 
                                    @solicitadorPor, 
                                    @moduloCargar, 
                                    @cntDias, 
                                    @cntUnidades, 
                                    @precioNetoDivisa, 
                                    @dscto, 
                                    @alicuotaId, 
                                    @alicuotaTasa, 
                                    @alicuotaDesc, 
                                    @aliadoId, 
                                    @aliadoCirif, 
                                    @aliadoCodigo, 
                                    @aliadoDesc, 
                                    @aliadoPrecioDivisa, 
                                    @notas, 
                                    @fechaDoc, 
                                    @horaDoc, 
                                    @signoDoc, 
                                    @tipoDoc, 
                                    @estatusAnulado, 
                                    @importe 
                                )";
                        foreach (var it in ficha.items)
                        {
                            var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                            var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@servicioDesc", it.servicioDesc);
                            var xp3 = new MySql.Data.MySqlClient.MySqlParameter("@solicitadorPor", it.solicitadorPor);
                            var xp4 = new MySql.Data.MySqlClient.MySqlParameter("@moduloCargar", it.moduloCargar);
                            var xp5 = new MySql.Data.MySqlClient.MySqlParameter("@cntDias", it.cntDias);
                            var xp6 = new MySql.Data.MySqlClient.MySqlParameter("@cntUnidades", it.cntUnidades);
                            var xp7 = new MySql.Data.MySqlClient.MySqlParameter("@precioNetoDivisa", it.precioNetoDivisa);
                            var xp8 = new MySql.Data.MySqlClient.MySqlParameter("@dscto", it.dscto);
                            var xp9 = new MySql.Data.MySqlClient.MySqlParameter("@alicuotaId", it.alicuotaId);
                            var xp10 = new MySql.Data.MySqlClient.MySqlParameter("@alicuotaTasa", it.alicuotaTasa);
                            var xp11 = new MySql.Data.MySqlClient.MySqlParameter("@alicuotaDesc", it.alicuotaDesc);
                            var xp12 = new MySql.Data.MySqlClient.MySqlParameter("@aliadoId", it.aliadoId);
                            var xp13 = new MySql.Data.MySqlClient.MySqlParameter("@aliadoCirif", it.aliadoCirif);
                            var xp14 = new MySql.Data.MySqlClient.MySqlParameter("@aliadoCodigo", it.aliadoCodigo);
                            var xp15 = new MySql.Data.MySqlClient.MySqlParameter("@aliadoDesc", it.aliadoDesc);
                            var xp16 = new MySql.Data.MySqlClient.MySqlParameter("@aliadoPrecioDivisa", it.aliadoPrecioDivisa);
                            var xp17 = new MySql.Data.MySqlClient.MySqlParameter("@notas", it.notas);
                            var xp18 = new MySql.Data.MySqlClient.MySqlParameter("@fechaDoc", fechaSistema.Date);
                            var xp19 = new MySql.Data.MySqlClient.MySqlParameter("@horaDoc", fechaSistema.ToShortTimeString());
                            var xp20 = new MySql.Data.MySqlClient.MySqlParameter("@signoDoc", it.signoDoc);
                            var xp21 = new MySql.Data.MySqlClient.MySqlParameter("@tipoDoc", it.tipoDoc);
                            var xp22 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", it.estatusAnulado);
                            var xp23 = new MySql.Data.MySqlClient.MySqlParameter("@importe", it.importe);
                            var r2 = cn.Database.ExecuteSqlCommand(_sql_I,
                                                                    xp1, xp2, xp3, xp4, xp5, xp6, xp7, xp8, xp9, xp10,
                                                                    xp11, xp12, xp13, xp14, xp15, xp16, xp17, xp18, xp19, xp20,
                                                                    xp21, xp22,xp23);
                            cn.SaveChanges();
                            //
                            _sql = "SELECT LAST_INSERT_ID()";
                            var idEnt = cn.Database.SqlQuery<int>(_sql).FirstOrDefault();
                            //
                            var _sql_F = @"INSERT INTO ventas_transp_item_fecha 
                                        (
                                            id_venta, id_item, fecha, hora, nota) 
                                        VALUES 
                                        (
                                            @idVenta, @idItem, @fecha, @hora, @nota)";
                            foreach (var fech in it.fechas)
                            {
                                var yp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                                var yp2 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", idEnt);
                                var yp3 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fech.fecha);
                                var yp4 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fech.hora);
                                var yp5 = new MySql.Data.MySqlClient.MySqlParameter("@nota", fech.nota);
                                var r3 = cn.Database.ExecuteSqlCommand(_sql_F, yp1, yp2, yp3, yp4, yp5);
                                cn.SaveChanges();
                            }
                        }

                        ts.Complete();
                        var ret = new DtoTransporte.Documento.Agregar.Resultado()
                        {
                            autoDoc = autoDoc,
                            numDoc = docNumero,
                        };
                        result.Entidad = ret;
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
        public DtoLib.ResultadoLista<DtoTransporte.Documento.Remision.Lista.Ficha> 
            TransporteDocumento_Remision_ListaBy(DtoTransporte.Documento.Remision.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Documento.Remision.Lista.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql_1 = @"select 
                                        auto as docId, 
                                        documento as docNumero,
                                        fecha as docFechaEmision, 
                                        hora as docHoraEmision, 
                                        monto_divisa as docMontoMonedaDiv, 
                                        total as docMontoMonedaAct,
                                        tipo as docTipo, 
                                        documento_nombre as docNombre, 
                                        razon_social clienteNombre, 
                                        ci_rif as clienteCiRif, 
                                        factor_cambio as factorCambio, 
                                        signo as docSigno, 
                                        renglones as docCntRenglones
                                    FROM ventas ";
                    var _sql_2 = @" where 1=1 ";
                    if (filtro.idCliente != "")
                    {
                        p1.ParameterName = "@idCliente";
                        p1.Value = filtro.idCliente;
                        _sql_2 += " and auto_cliente = @idCliente ";
                    }
                    if (filtro.codTipoDoc != "")
                    {
                        p2.ParameterName = "@codTipoDoc";
                        p2.Value = filtro.codTipoDoc;
                        _sql_2 += " and tipo = @codTipoDoc ";
                    }
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Documento.Remision.Lista.Ficha>(_sql, p1, p2).ToList();
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