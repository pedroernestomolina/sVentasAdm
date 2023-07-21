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
            TransporteDocumento_AgregarFactura(DtoTransporte.Documento.Agregar.Factura.Ficha ficha)
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


                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_cxc=a_cxc+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var largo = 10;
                        var aDoc = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var autoDoc = aDoc.ToString().Trim().PadLeft(largo, '0');
                        var aCxC = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var autoCxC = aCxC.ToString().Trim().PadLeft(largo, '0');

                        var pSerie = new MySql.Data.MySqlClient.MySqlParameter("@autoSerie", ficha.serieDocId);
                        sql = "update empresa_series_fiscales set correlativo=correlativo+1 where auto=@autoSerie";
                        r1 = cn.Database.ExecuteSqlCommand(sql, pSerie);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CORRELATIVO SERIE FISCAL";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var nDoc = cn.Database.SqlQuery<int?>("select correlativo from empresa_series_fiscales where auto=@autoSerie", pSerie).FirstOrDefault();
                        if (!nDoc.HasValue)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CORRELATIVO SERIE FISCAL";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var fechaVenc = fechaSistema.AddDays(ficha.diasCredito);
                        var docNumero = nDoc.Value.ToString().Trim().PadLeft(largo, '0');

                        //INSERTAR FACTURA
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
                                        `z_fiscal`,
                                        docSolicitadoPor,
                                        docModuloCargar
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
                                        '0', 
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
                                        @serieDoc, 
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
                                        '0',
                                        @docSolicitadoPor,
                                        @docModuloCargar)";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoDoc", autoDoc);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@numDoc", docNumero);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmi", fechaSistema.Date);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVen", fechaVenc);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@razonSocial", ficha.RazonSocial);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@dirFiscal", ficha.DirFiscal);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@ciRif", ficha.CiRif);
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
                        var p45 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa", ficha.montoDivisa);
                        var p46 = new MySql.Data.MySqlClient.MySqlParameter("@estacion", ficha.estacion);
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
                        var p62 = new MySql.Data.MySqlClient.MySqlParameter("@docSolicitadoPor", ficha.docSolicitadoPor);
                        var p63 = new MySql.Data.MySqlClient.MySqlParameter("@docModuloCargar", ficha.docModuloCargar);
                        var p64 = new MySql.Data.MySqlClient.MySqlParameter("@serieDoc", ficha.serieDocDesc);
                        var r = cn.Database.ExecuteSqlCommand(_sql,
                                                                p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
                                                                p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                                                p21, p22, p23, p24, p25, p26, p27, p28, p29, p30,
                                                                p31, p32, p33, p34, p35, p36, p37, p38, p39, p40,
                                                                p41, p42, p43, p44, p45, p46, p47, p48, p49, p50,
                                                                p51, p53, p54, p55, p56, p57, p58, p59, p60,
                                                                p61, p62, p63, p64);
                        cn.SaveChanges();

                        //INSERTAR CXC
                        _sql = @"INSERT INTO cxc
                                    (
                                        auto ,
                                        c_cobranza ,
                                        c_cobranzap ,
                                        fecha ,
                                        tipo_documento ,
                                        documento ,
                                        fecha_vencimiento ,
                                        nota ,
                                        importe ,
                                        acumulado ,
                                        auto_cliente ,
                                        cliente ,
                                        ci_rif ,
                                        codigo_cliente ,
                                        estatus_cancelado ,
                                        resta ,
                                        estatus_anulado ,
                                        auto_documento ,
                                        numero ,
                                        auto_agencia ,
                                        agencia ,
                                        signo ,
                                        auto_vendedor ,
                                        c_departamento ,
                                        c_ventas ,
                                        c_ventasp ,
                                        serie ,
                                        importe_neto ,
                                        dias ,
                                        castigop ,
                                        cierre_ftp ,
                                        monto_divisa ,
                                        tasa_divisa ,
                                        acumulado_divisa ,
                                        codigo_sucursal ,
                                        resta_divisa ,
                                        importe_neto_divisa ,
                                        estatus_doc_cxc
                                    )
                                VALUES 
                                    (
                                        @autoCxC,  
                                        0, 
                                        0, 
                                        @fechaReg,
                                        'FAC', 
                                        @docNumero, 
                                        @fechaVence,
                                        '',
                                        @importe,
                                        0,
                                        @idCliente,
                                        @nombreCliente,
                                        @cirifCliente,
                                        @codigoCliente,
                                        '0',
                                        @resta,
                                        '0'
                                        @autoDoc,
                                        '',
                                        '0000000001',
                                        '',
                                        1,
                                        @autoVendedor,
                                        0,
                                        0,
                                        0,
                                        @serieDocDesc,
                                        @importeNeto,
                                        @dias,
                                        0,
                                        '',
                                        @montoDivisa,
                                        @tasaDivisa,
                                        @acumuladoDivisa,
                                        @codigoSuc,
                                        @restaDivisa,
                                        @importeNetoDivisa,
                                        @estatusDocCxc
                                    )";
                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@autoCxC",autoCxC);
                        var t2 = new MySql.Data.MySqlClient.MySqlParameter("@fechaReg",fechaSistema.Date);
                        var t3 = new MySql.Data.MySqlClient.MySqlParameter("@docNumero", docNumero);
                        var t4 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVence", fechaVenc);
                        var t5 = new MySql.Data.MySqlClient.MySqlParameter("@importe", ficha.Total);
                        var t6 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                        var t7 = new MySql.Data.MySqlClient.MySqlParameter("@nombreCliente", ficha.RazonSocial);
                        var t8 = new MySql.Data.MySqlClient.MySqlParameter("@cirifCliente", ficha.CiRif);
                        var t9 = new MySql.Data.MySqlClient.MySqlParameter("@codigoCliente", ficha.codCliente);
                        var t10 = new MySql.Data.MySqlClient.MySqlParameter("@resta", ficha.Total);
                        var t11 = new MySql.Data.MySqlClient.MySqlParameter("@autoDoc", autoDoc);
                        var t12 = new MySql.Data.MySqlClient.MySqlParameter("@autoVendedor", ficha.idVendedor);
                        var t13 = new MySql.Data.MySqlClient.MySqlParameter("@autoVendedor", ficha.serieDocDesc);
                        var t14 = new MySql.Data.MySqlClient.MySqlParameter("@importeNeto", ficha.subTotal);
                        var t15 = new MySql.Data.MySqlClient.MySqlParameter("@dias", ficha.diasCredito );
                        var t16 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa", ficha.montoDivisa );
                        var t17 = new MySql.Data.MySqlClient.MySqlParameter("@tasaDivisa", ficha.factorCambio );
                        var t18 = new MySql.Data.MySqlClient.MySqlParameter("@acumuladoDivisa", 0m );
                        var t19 = new MySql.Data.MySqlClient.MySqlParameter("@codigoSuc", ficha.codSucursal );
                        var t20 = new MySql.Data.MySqlClient.MySqlParameter("@restaDivisa", ficha.montoDivisa );
                        var t21 = new MySql.Data.MySqlClient.MySqlParameter("@importeNetoDivisa", ficha.subTotalMonDivisa);
                        var t22 = new MySql.Data.MySqlClient.MySqlParameter("@estatusDocCxc", "0");
                        r = cn.Database.ExecuteSqlCommand(_sql, t1 , t2, t3, t4, t5, t6, t7, t8, t9, t10,
                                                        t11, t12, t13, t14, t15, t16, t17, t18, t19, t20,
                                                        t21, t22);
                        cn.SaveChanges();

                        //SALDO DEL CLIENTE EN DIVISA
                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                        var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@debito", ficha.montoDivisa);
                        var xsql_cli = @"update clientes set 
                                                debitos=debitos+@debito,
                                                saldo=saldo+@debito
                                                where auto=@idCliente";
                        var r_cli = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                        if (r_cli == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR SALDO CLIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTOS DE PRESUPUESTO QUE SE USARON PARA GENERAR LA FACTURA
                        _sql = @"INSERT INTO ventas_transp_doc  
                                    (
                                        id_venta,
                                        id_cliente,
                                        fecha,
                                        estatus_anulado,
                                        id_doc_ref,
                                        doc_num_ref,
                                        doc_fecha_ref,
                                        doc_monto_ref,
                                        doc_cod_ref,
                                        doc_tipo_ref
                                    )
                                VALUES
                                    (
                                        @idVenta,
                                        @idCliente,
                                        @fecha,
                                        @estatusAnulado,
                                        @idDocRef,
                                        @docNumRef,
                                        @docFechaRef,
                                        @docMontoRef,
                                        @docCodRef,
                                        @docTipoRef
                                    )
                                ";
                        foreach (var rg in ficha.docRef)
                        {
                            var lp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                            var lp2 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                            var lp3 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
                            var lp4 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", "0");
                            var lp5 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", rg.idDoc);
                            var lp6 = new MySql.Data.MySqlClient.MySqlParameter("@docNumRef", rg.numDoc);
                            var lp7 = new MySql.Data.MySqlClient.MySqlParameter("@docFechaRef", rg.fechaDoc);
                            var lp8 = new MySql.Data.MySqlClient.MySqlParameter("@docMontoRef", rg.montoDivisaDoc);
                            var lp9 = new MySql.Data.MySqlClient.MySqlParameter("@docCodRef", rg.codigoDoc);
                            var lp10 = new MySql.Data.MySqlClient.MySqlParameter("@docTipoRef", rg.tipoDoc);
                            var r3 = cn.Database.ExecuteSqlCommand(_sql, lp1, lp2, lp3, lp4, lp5, lp6,
                                                                    lp7, lp8, lp9, lp10);
                            cn.SaveChanges();
                        }

                        //ALIADOS USADOS 
                        _sql = @"INSERT INTO ventas_transp_aliado 
                                            (
                                                id_venta,
                                                id_cliente,
                                                id_aliado,
                                                cirif,
                                                codigo,
                                                nombre_desc,
                                                precio_unit_divisa,
                                                cnt_dias,
                                                importe,
                                                estatus_anulado,
                                                fecha
                                            )
                                            VALUES 
                                            (
                                                @idVenta, 
                                                @idCliente, 
                                                @idAliado, 
                                                @ciRifAliado, 
                                                @codigoAliado, 
                                                @descAliado, 
                                                @precioUnitDiv, 
                                                @cntDias,
                                                @importe
                                                @estatusAnulado,
                                                @fechaRegistro
                                            )";
                        foreach (var aliad in ficha.aliados)
                        {
                            var zp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                            var zp2 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                            var zp3 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", aliad.idAliado);
                            var zp4 = new MySql.Data.MySqlClient.MySqlParameter("@ciRifAliado", aliad.ciRifAliado);
                            var zp5 = new MySql.Data.MySqlClient.MySqlParameter("@codigoAliado", aliad.codigoAliado);
                            var zp6 = new MySql.Data.MySqlClient.MySqlParameter("@descAliado", aliad.nombreDescAliado);
                            var zp7 = new MySql.Data.MySqlClient.MySqlParameter("@precioUnitDiv", aliad.precioUnitDivisa);
                            var zp8 = new MySql.Data.MySqlClient.MySqlParameter("@cntDias", aliad.cntDias);
                            var zp9 = new MySql.Data.MySqlClient.MySqlParameter("@importe", aliad.importeDivisa);
                            var zp10 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", "0");
                            var zp11 = new MySql.Data.MySqlClient.MySqlParameter("@fechaRegistro", fechaSistema.Date);
                            var r4 = cn.Database.ExecuteSqlCommand(_sql, zp1, zp2, zp3, zp4, zp5, zp6,
                                                                    zp7, zp8, zp9, zp10, zp11);
                            cn.SaveChanges();
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
    }
}
