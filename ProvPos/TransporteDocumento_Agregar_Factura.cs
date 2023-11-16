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
                            throw new Exception("PROBLEMA AL ACTUALIZAR TABLA CONTADORES");
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
                            throw new Exception("PROBLEMA AL ACTUALIZAR CORRELATIVO SERIE FISCAL");
                        }
                        var nDoc = cn.Database.SqlQuery<int?>("select correlativo from empresa_series_fiscales where auto=@autoSerie", pSerie).FirstOrDefault();
                        if (!nDoc.HasValue)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CORRELATIVO SERIE FISCAL");
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
                                        @autoDocCxC, 
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
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmi", ficha.fechaEmision);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVen", ficha.fechaVencimiento);
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
                        var p65 = new MySql.Data.MySqlClient.MySqlParameter("@autoDocCxC", autoCxC);
                        var r = cn.Database.ExecuteSqlCommand(_sql,
                                                                p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
                                                                p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                                                p21, p22, p23, p24, p25, p26, p27, p28, p29, p30,
                                                                p31, p32, p33, p34, p35, p36, p37, p38, p39, p40,
                                                                p41, p42, p43, p44, p45, p46, p47, p48, p49, p50,
                                                                p51, p53, p54, p55, p56, p57, p58, p59, p60,
                                                                p61, p62, p63, p64, p65);
                        if (r == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR DOCUMENTO DE VENTA");
                        }
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
                                        @tipoDocSiglas, 
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
                                        '0',
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
                                        0,
                                        @codigoSuc,
                                        @restaDivisa,
                                        @importeNetoDivisa,
                                        @estatusDocCxc
                                    )";
                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@autoCxC", autoCxC);
                        var t2 = new MySql.Data.MySqlClient.MySqlParameter("@fechaReg", ficha.fechaEmision);
                        var t3 = new MySql.Data.MySqlClient.MySqlParameter("@docNumero", docNumero);
                        var t4 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVence", ficha.fechaVencimiento);
                        var t5 = new MySql.Data.MySqlClient.MySqlParameter("@importe", ficha.Total);
                        var t6 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                        var t7 = new MySql.Data.MySqlClient.MySqlParameter("@nombreCliente", ficha.RazonSocial);
                        var t8 = new MySql.Data.MySqlClient.MySqlParameter("@cirifCliente", ficha.CiRif);
                        var t9 = new MySql.Data.MySqlClient.MySqlParameter("@codigoCliente", ficha.codCliente);
                        var t10 = new MySql.Data.MySqlClient.MySqlParameter("@resta", ficha.Total);
                        var t11 = new MySql.Data.MySqlClient.MySqlParameter("@autoDoc", autoDoc);
                        var t12 = new MySql.Data.MySqlClient.MySqlParameter("@autoVendedor", ficha.idVendedor);
                        var t13 = new MySql.Data.MySqlClient.MySqlParameter("@serieDocDesc", ficha.serieDocDesc);
                        var t14 = new MySql.Data.MySqlClient.MySqlParameter("@importeNeto", ficha.subTotal);
                        var t15 = new MySql.Data.MySqlClient.MySqlParameter("@dias", ficha.diasCredito);
                        var t16 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa", ficha.montoDivisa);
                        var t17 = new MySql.Data.MySqlClient.MySqlParameter("@tasaDivisa", ficha.factorCambio);
                        var t19 = new MySql.Data.MySqlClient.MySqlParameter("@codigoSuc", ficha.codSucursal);
                        var t20 = new MySql.Data.MySqlClient.MySqlParameter("@restaDivisa", ficha.montoDivisa);
                        var t21 = new MySql.Data.MySqlClient.MySqlParameter("@importeNetoDivisa", ficha.subTotalMonDivisa);
                        var t22 = new MySql.Data.MySqlClient.MySqlParameter("@estatusDocCxc", "0");
                        var t23 = new MySql.Data.MySqlClient.MySqlParameter("@tipoDocSiglas", ficha.tipoDocSiglas);
                        r = cn.Database.ExecuteSqlCommand(_sql, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10,
                                                        t11, t12, t13, t14, t15, t16, t17, t19, t20,
                                                        t21, t22, t23);
                        if (r == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR DOCUMENTO CXC");
                        }
                        cn.SaveChanges();

                        var _sqlDetFct = @"INSERT INTO ventas_transp_detalle
                                    (
                                        id_venta ,
                                        detalle ,
                                        cnt_dias ,
                                        precio_neto_mon_local ,
                                        precio_neto_mon_divisa ,
                                        descuento_monto_mon_local ,
                                        descuento_monto_mon_divisa ,
                                        descuento_porct ,
                                        alicuota_id ,
                                        alicuota_tasa ,
                                        impuesto_mon_local ,
                                        impuesto_mon_divisa ,
                                        alicuota_desc ,
                                        precio_item_mon_local ,
                                        precio_item_mon_divisa ,
                                        precio_final_mon_local ,
                                        precio_final_mon_divisa ,
                                        importe_neto_mon_local ,
                                        importe_neto_mon_divisa ,
                                        importe_total_mon_local ,
                                        importe_total_mon_divisa ,
                                        total_mon_local ,
                                        total_mon_divisa ,
                                        estatus_anulado_doc ,
                                        fecha_doc ,
                                        id_cliente ,
                                        signo_doc ,
                                        codigo_doc ,
                                        id_doc_ref ,
                                        doc_num_ref ,
                                        doc_fecha_ref ,
                                        doc_monto_ref ,
                                        doc_codigo_ref,
                                        tipo_procedencia_item,
                                        id_item_servicio,
                                        mostrar_item_doc_final)
                                VALUES
                                    (
                                        @id_venta ,
                                        @detalle ,
                                        @cnt_dias ,
                                        @precio_neto_mon_local ,
                                        @precio_neto_mon_divisa ,
                                        @descuento_monto_mon_local ,
                                        @descuento_monto_mon_divisa ,
                                        @descuento_porct ,
                                        @alicuota_id ,
                                        @alicuota_tasa ,
                                        @impuesto_mon_local ,
                                        @impuesto_mon_divisa ,
                                        @alicuota_desc ,
                                        @precio_item_mon_local ,
                                        @precio_item_mon_divisa ,
                                        @precio_final_mon_local ,
                                        @precio_final_mon_divisa ,
                                        @importe_neto_mon_local ,
                                        @importe_neto_mon_divisa ,
                                        @importe_total_mon_local ,
                                        @importe_total_mon_divisa ,
                                        @total_mon_local ,
                                        @total_mon_divisa ,
                                        @estatus_anulado_doc ,
                                        @fecha_doc ,
                                        @id_cliente ,
                                        @signo_doc ,
                                        @codigo_doc ,
                                        @id_doc_ref ,
                                        @doc_num_ref ,
                                        @doc_fecha_ref ,
                                        @doc_monto_ref ,
                                        @doc_codigo_ref,
                                        @tipo_procedencia_item,
                                        @id_item_servicio,
                                        @mostrar_item_doc_final)";
                        foreach (var rg in ficha.items)
                        {
                            var _idItemServicio = -1;
                            //EN CASO DE SER UN SERVICIO SE AGREGA PRIMERO EL DETALLE DEL SERVCIO
                            if (rg.servicioDetalle != null)
                            {
                                var it = rg.servicioDetalle;
                                var _sql_I = @"INSERT INTO ventas_transp_item (
                                    `id_item`, 
                                    `id_venta`,
                                    `servicio_desc`, 
                                    `cnt_dias`, 
                                    `cnt_unidades`, 
                                    `precio_neto_divisa`, 
                                    `dscto`, 
                                    `alicuota_id`, 
                                    `alicuota_tasa`, 
                                    `alicuota_desc`, 
                                    `notas`, 
                                    `fecha_doc`, 
                                    `hora_doc`, 
                                    `signo_doc`, 
                                    `tipo_doc`, 
                                    `estatus_anulado`,
                                    `importe`,
                                    unidades_desc,
                                    servicio_id,
                                    servicio_codigo,
                                    servicio_detalle
                                ) 
                                VALUES 
                                (
                                    null, 
                                    @idVenta,
                                    @servicioDesc, 
                                    @cntDias, 
                                    @cntUnidades, 
                                    @precioNetoDivisa, 
                                    @dscto, 
                                    @alicuotaId, 
                                    @alicuotaTasa, 
                                    @alicuotaDesc, 
                                    @notas, 
                                    @fechaDoc, 
                                    @horaDoc, 
                                    @signoDoc, 
                                    @tipoDoc, 
                                    @estatusAnulado, 
                                    @importe,
                                    @unidades_desc,
                                    @servicio_id,
                                    @servicio_codigo,
                                    @servicio_detalle
                                )";
                                var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                                var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@servicioDesc", it.servicioDesc);
                                var xp5 = new MySql.Data.MySqlClient.MySqlParameter("@cntDias", it.cntDias);
                                var xp6 = new MySql.Data.MySqlClient.MySqlParameter("@cntUnidades", it.cntUnidades);
                                var xp7 = new MySql.Data.MySqlClient.MySqlParameter("@precioNetoDivisa", it.precioNetoDivisa);
                                var xp8 = new MySql.Data.MySqlClient.MySqlParameter("@dscto", it.dscto);
                                var xp9 = new MySql.Data.MySqlClient.MySqlParameter("@alicuotaId", it.alicuotaId);
                                var xp10 = new MySql.Data.MySqlClient.MySqlParameter("@alicuotaTasa", it.alicuotaTasa);
                                var xp11 = new MySql.Data.MySqlClient.MySqlParameter("@alicuotaDesc", it.alicuotaDesc);
                                var xp17 = new MySql.Data.MySqlClient.MySqlParameter("@notas", it.notas);
                                var xp18 = new MySql.Data.MySqlClient.MySqlParameter("@fechaDoc", ficha.fechaEmision);
                                var xp19 = new MySql.Data.MySqlClient.MySqlParameter("@horaDoc", fechaSistema.ToShortTimeString());
                                var xp20 = new MySql.Data.MySqlClient.MySqlParameter("@signoDoc", it.signoDoc);
                                var xp21 = new MySql.Data.MySqlClient.MySqlParameter("@tipoDoc", it.tipoDoc);
                                var xp22 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", it.estatusAnulado);
                                var xp23 = new MySql.Data.MySqlClient.MySqlParameter("@importe", it.importe);
                                var xp24 = new MySql.Data.MySqlClient.MySqlParameter("@unidades_desc", it.unidadesDesc);
                                var xp25 = new MySql.Data.MySqlClient.MySqlParameter("@servicio_id", it.servicioId);
                                var xp26 = new MySql.Data.MySqlClient.MySqlParameter("@servicio_codigo", it.servicioCodigo);
                                var xp27 = new MySql.Data.MySqlClient.MySqlParameter("@servicio_detalle", it.servicioDetalle);
                                var r2 = cn.Database.ExecuteSqlCommand(_sql_I,
                                                                        xp1, xp2, xp5, xp6, xp7, xp8, xp9, xp10,
                                                                        xp11, xp17, xp18, xp19, xp20,
                                                                        xp21, xp22, xp23, xp24, xp25, xp26, xp27);
                                if (r2 == 0)
                                {
                                    throw new Exception("PROBLEMA AL INSERTAR ITEM SERVICIO");
                                }
                                cn.SaveChanges();
                                //
                                _sql = "SELECT LAST_INSERT_ID()";
                                var idEnt = cn.Database.SqlQuery<int>(_sql).FirstOrDefault();
                                //
                                _idItemServicio = idEnt;

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
                                    if (r3 == 0)
                                    {
                                        throw new Exception("PROBLEMA AL INSERTAR FECHA SERVICIO");
                                    }
                                    cn.SaveChanges();
                                }

                                var _sql_G = @"INSERT INTO ventas_transp_item_aliado 
                                            (
                                                id_venta,
                                                id_item,
                                                id_aliado,
                                                cirif_aliado,
                                                codigo_aliado,
                                                desc_alido,
                                                precio_unit_divisa,
                                                cnt_dias,
                                                importe
                                            )
                                            VALUES 
                                            (
                                                @idVenta, 
                                                @idItem, 
                                                @idAliado, 
                                                @ciRifAliado, 
                                                @codigoAliado, 
                                                @descAliado, 
                                                @precioUnitDiv, 
                                                @cntDias,
                                                @importe
                                            )";
                                foreach (var aliad in it.alidos)
                                {
                                    var zp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                                    var zp2 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", idEnt);
                                    var zp3 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", aliad.id);
                                    var zp4 = new MySql.Data.MySqlClient.MySqlParameter("@ciRifAliado", aliad.ciRif);
                                    var zp5 = new MySql.Data.MySqlClient.MySqlParameter("@codigoAliado", aliad.codigo);
                                    var zp6 = new MySql.Data.MySqlClient.MySqlParameter("@descAliado", aliad.desc);
                                    var zp7 = new MySql.Data.MySqlClient.MySqlParameter("@precioUnitDiv", aliad.precioUnitDivisa);
                                    var zp8 = new MySql.Data.MySqlClient.MySqlParameter("@cntDias", aliad.cntDias);
                                    var zp9 = new MySql.Data.MySqlClient.MySqlParameter("@importe", aliad.importe);
                                    var r4 = cn.Database.ExecuteSqlCommand(_sql_G, zp1, zp2, zp3, zp4, zp5, zp6, zp7, zp8, zp9);
                                    if (r4 == 0)
                                    {
                                        throw new Exception("PROBLEMA AL INSERTAR ALIADO SERVICIO");
                                    }
                                    cn.SaveChanges();
                                }
                            }

                            //INSERTAR DETALLE DOCUMENTO
                            var ld1 = new MySql.Data.MySqlClient.MySqlParameter("@id_venta", autoDoc);
                            var ld2 = new MySql.Data.MySqlClient.MySqlParameter("@detalle", rg.detalle);
                            var ld3 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_dias", rg.cntDias);
                            var ld4 = new MySql.Data.MySqlClient.MySqlParameter("@precio_neto_mon_local", rg.precioNetoMonLocal);
                            var ld5 = new MySql.Data.MySqlClient.MySqlParameter("@precio_neto_mon_divisa", rg.precioNetoMonDivisa);
                            var ld6 = new MySql.Data.MySqlClient.MySqlParameter("@descuento_monto_mon_local", rg.dsctoMontoMonLocal);
                            var ld7 = new MySql.Data.MySqlClient.MySqlParameter("@descuento_monto_mon_divisa", rg.dsctoMontoMonDivisa);
                            var ld8 = new MySql.Data.MySqlClient.MySqlParameter("@descuento_porct", rg.dsctoPorc);
                            var ld9 = new MySql.Data.MySqlClient.MySqlParameter("@alicuota_id", rg.alicuotaId);
                            var ld10 = new MySql.Data.MySqlClient.MySqlParameter("@alicuota_tasa", rg.alicuotaTasa);
                            var ld11 = new MySql.Data.MySqlClient.MySqlParameter("@impuesto_mon_local", rg.impuestoMonLocal);
                            var ld12 = new MySql.Data.MySqlClient.MySqlParameter("@impuesto_mon_divisa", rg.impuestoMonDivisa);
                            var ld13 = new MySql.Data.MySqlClient.MySqlParameter("@alicuota_desc", rg.alicuotaDesc);
                            var ld14 = new MySql.Data.MySqlClient.MySqlParameter("@precio_item_mon_local", rg.precioItemMonLocal);
                            var ld15 = new MySql.Data.MySqlClient.MySqlParameter("@precio_item_mon_divisa", rg.precioItemMonDivisa);
                            var ld16 = new MySql.Data.MySqlClient.MySqlParameter("@precio_final_mon_local", rg.precioFinalMonLocal);
                            var ld17 = new MySql.Data.MySqlClient.MySqlParameter("@precio_final_mon_divisa", rg.precioFinalMonDivisa);
                            var ld18 = new MySql.Data.MySqlClient.MySqlParameter("@importe_neto_mon_local", rg.importeNetoMonLocal);
                            var ld19 = new MySql.Data.MySqlClient.MySqlParameter("@importe_neto_mon_divisa", rg.importeNetoMonDivisa);
                            var ld20 = new MySql.Data.MySqlClient.MySqlParameter("@importe_total_mon_local", rg.importeTotalMonLocal);
                            var ld21 = new MySql.Data.MySqlClient.MySqlParameter("@importe_total_mon_divisa", rg.importeTotalMonDivisa);
                            var ld22 = new MySql.Data.MySqlClient.MySqlParameter("@total_mon_local", rg.totalMonLocal);
                            var ld23 = new MySql.Data.MySqlClient.MySqlParameter("@total_mon_divisa", rg.totalMonDivisa);
                            var ld24 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_anulado_doc", "0");
                            var ld25 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_doc", ficha.fechaEmision);
                            var ld26 = new MySql.Data.MySqlClient.MySqlParameter("@id_cliente", ficha.idCliente);
                            var ld27 = new MySql.Data.MySqlClient.MySqlParameter("@signo_doc", ficha.signo);
                            var ld28 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_doc", ficha.TipoDoc);
                            var ld29 = new MySql.Data.MySqlClient.MySqlParameter("@id_doc_ref", rg.idDocRef);
                            var ld30 = new MySql.Data.MySqlClient.MySqlParameter("@doc_num_ref", rg.numDocRef);
                            var ld31 = new MySql.Data.MySqlClient.MySqlParameter("@doc_fecha_ref", rg.fechaDocRef);
                            var ld32 = new MySql.Data.MySqlClient.MySqlParameter("@doc_monto_ref", rg.montoDocRef);
                            var ld33 = new MySql.Data.MySqlClient.MySqlParameter("@doc_codigo_ref", rg.codigoDocRef);
                            var ld34 = new MySql.Data.MySqlClient.MySqlParameter("@tipo_procedencia_item", rg.tipoProcedenciaItem);
                            var ld35 = new MySql.Data.MySqlClient.MySqlParameter("@id_item_servicio", _idItemServicio);
                            var ld36 = new MySql.Data.MySqlClient.MySqlParameter("@mostrar_item_doc_final", rg.mostrarItemDocFinal ? "1" : "0");
                            var r_det = cn.Database.ExecuteSqlCommand(_sqlDetFct, ld1, ld2, ld3, ld4, ld5, ld6, ld7, ld8, ld9, ld10,
                                                                            ld11, ld12, ld13, ld14, ld15, ld16, ld17, ld18, ld19, ld20,
                                                                            ld21, ld22, ld23, ld24, ld25, ld26, ld27, ld28, ld29, ld30,
                                                                            ld31, ld32, ld33, ld34, ld35, ld36);
                            if (r_det == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR ITEM");
                            }
                            cn.SaveChanges();
                        }

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
                            throw new Exception("PROBLEMA AL ACTUALIZAR SALDO CLIENTE");
                        }
                        cn.SaveChanges();

                        //DOCUMENTOS DE REFERENCIA QUE SE USARON PARA GENERAR LA FACTURA
                        _sql = @"INSERT INTO ventas_transp_doc  
                                    (
                                        id_venta,
                                        id_cliente,
                                        fecha,
                                        estatus_anulado,
                                        id_doc_ref,
                                        doc_num_ref,
                                        doc_fecha_ref,
                                        doc_monto_divisa_ref,
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
                                        @docMontoDivisaRef,
                                        @docCodRef,
                                        @docTipoRef
                                    )
                                ";
                        foreach (var rg in ficha.docRef)
                        {
                            var lp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                            var lp2 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                            var lp3 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", ficha.fechaEmision);
                            var lp4 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", "0");
                            var lp5 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", rg.idDoc);
                            var lp6 = new MySql.Data.MySqlClient.MySqlParameter("@docNumRef", rg.numDoc);
                            var lp7 = new MySql.Data.MySqlClient.MySqlParameter("@docFechaRef", rg.fechaDoc);
                            var lp8 = new MySql.Data.MySqlClient.MySqlParameter("@docMontoDivisaRef", rg.montoDivisaDoc);
                            var lp9 = new MySql.Data.MySqlClient.MySqlParameter("@docCodRef", rg.codigoDoc);
                            var lp10 = new MySql.Data.MySqlClient.MySqlParameter("@docTipoRef", rg.tipoDoc);
                            var r3 = cn.Database.ExecuteSqlCommand(_sql, lp1, lp2, lp3, lp4, lp5, lp6,
                                                                    lp7, lp8, lp9, lp10);
                            if (r3 == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR VENTA - DOCUMENTO - REF");
                            }
                            cn.SaveChanges();

                            //ACTUALIZO LA REMISION DE DOCUMENTOS DE REFERENCIA
                            var xlp1 = new MySql.Data.MySqlClient.MySqlParameter("@idRemision", autoDoc);
                            var xlp2 = new MySql.Data.MySqlClient.MySqlParameter("@codDocRemision", ficha.TipoDoc);
                            var xlp3 = new MySql.Data.MySqlClient.MySqlParameter("@numDocRemision", docNumero);
                            var xlp4 = new MySql.Data.MySqlClient.MySqlParameter("@autoDocRef", rg.idDoc);
                            var xlp5 = new MySql.Data.MySqlClient.MySqlParameter("@situacion", "PROCESADO");
                            var _sql_2 = @"update ventas set 
                                                auto_remision=@idRemision,
                                                tipo_remision=@codDocRemision,
                                                documento_remision=@numDocRemision,
                                                situacion=@situacion
                                            where auto=@autoDocRef";
                            var xr3 = cn.Database.ExecuteSqlCommand(_sql_2, xlp1, xlp2, xlp3, xlp4, xlp5);
                            if (xr3 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR REMISION DOCUMENTO");
                            }
                            cn.SaveChanges();
                        }

                        //ALIADOS USADOS RESUMEN
                        foreach (var aliadoRes in ficha.aliadosResumen)
                        {
                            _sql = @"update transp_aliado set 
                                    monto_debitos_mon_divisa=monto_debitos_mon_divisa+@monto
                                where id=@idAliado";
                            var ylp1 = new MySql.Data.MySqlClient.MySqlParameter("@monto", aliadoRes.montoDivisa);
                            var ylp2 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", aliadoRes.idAliado);
                            var xr4 = cn.Database.ExecuteSqlCommand(_sql, ylp1, ylp2);
                            if (xr4 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR SALDO ALIADO");
                            }
                            cn.SaveChanges();

                            //REGISTRAR ALIADO USADO EN EL DOCUMENTO FACTURA
                            _sql = @"INSERT INTO ventas_transp_aliado 
                                            (
                                                id_venta,
                                                id_cliente,
                                                id_aliado,
                                                importe_divisa,
                                                fecha,
                                                estatus_anulado
                                            )
                                            VALUES 
                                            (
                                                @idVenta, 
                                                @idCliente, 
                                                @idAliado, 
                                                @importeDivisa,
                                                @fechaRegistro,
                                                @estatusAnulado
                                            )";
                            var zp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                            var zp2 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                            var zp3 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", aliadoRes.idAliado);
                            var zp4 = new MySql.Data.MySqlClient.MySqlParameter("@importeDivisa", aliadoRes.montoDivisa);
                            var zp5 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", "0");
                            var zp6 = new MySql.Data.MySqlClient.MySqlParameter("@fechaRegistro", ficha.fechaEmision);
                            var xr5 = cn.Database.ExecuteSqlCommand(_sql, zp1, zp2, zp3, zp4, zp5, zp6);
                            cn.SaveChanges();
                            if (xr5 == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR VENTA - ALIADO - REF");
                            }
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
                                            @estatusAnulado
                                        )";
                            zp1 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", aliadoRes.idAliado);
                            zp2 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", autoDoc);
                            zp3 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                            zp4 = new MySql.Data.MySqlClient.MySqlParameter("@docNumero", docNumero);
                            zp5 = new MySql.Data.MySqlClient.MySqlParameter("@docFecha", ficha.fechaEmision);
                            zp6 = new MySql.Data.MySqlClient.MySqlParameter("@docCodigo", ficha.TipoDoc);
                            var zp7 = new MySql.Data.MySqlClient.MySqlParameter("@docNombre", ficha.docNombre);
                            var zp8 = new MySql.Data.MySqlClient.MySqlParameter("@importeDivisa", aliadoRes.montoDivisa);
                            var zp10 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", "0");
                            var xr6 = cn.Database.ExecuteSqlCommand(_sql, zp1, zp2, zp3, zp4, zp5, zp6, zp7, zp8, zp10);
                            cn.SaveChanges();
                            if (xr6 == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR ALIADO - DOCUMENTO");
                            }
                            //
                            sql = "SELECT LAST_INSERT_ID()";
                            var idAliadoDoc = cn.Database.SqlQuery<int>(sql).FirstOrDefault();
                            //
                            //SERVICIOS PRESTADOS POR EL ALIADO
                            foreach (var serv in aliadoRes.servicios)
                            {
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
                                var sv1 = new MySql.Data.MySqlClient.MySqlParameter("@id_aliado_doc", idAliadoDoc);
                                var sv2 = new MySql.Data.MySqlClient.MySqlParameter("@id_serv", serv.id);
                                var sv3 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_serv", serv.codigo);
                                var sv4 = new MySql.Data.MySqlClient.MySqlParameter("@desc_serv", serv.desc);
                                var sv5 = new MySql.Data.MySqlClient.MySqlParameter("@detalle_serv", serv.detalle);
                                var sv6 = new MySql.Data.MySqlClient.MySqlParameter("@importe_serv_div", serv.importe);
                                var xsv = cn.Database.ExecuteSqlCommand(_sql, sv1, sv2, sv3, sv4, sv5, sv6);
                                cn.SaveChanges();
                                if (xsv == 0)
                                {
                                    throw new Exception("PROBLEMA AL INSERTAR ALIADO - DOCUMENTO - SERVICIO");
                                }
                            }
                        }
                        //ALIADOS DOCUMENTO REFERENCIA 
                        foreach (var aliadoDocRef in ficha.aliadosDocRef)
                        {
                            _sql = @"insert into ventas_transp_aliado_detalle (
                                        id_venta,
                                        id_doc_ref,
                                        id_aliado,
                                        estatus_anulado
                                    )
                                    VALUES (
                                        @idVenta, 
                                        @idDocRef, 
                                        @idAliado, 
                                        '0')";
                            var zlp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                            var zlp2 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", aliadoDocRef.idDocRef == "" ? autoDoc : aliadoDocRef.idDocRef);
                            var zlp3 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", aliadoDocRef.idAliado);
                            var zr1 = cn.Database.ExecuteSqlCommand(_sql, zlp1, zlp2, zlp3);
                            if (zr1 == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR ALIADO DETALLE DOC REFERENCIA");
                            }
                            cn.SaveChanges();
                        }
                        //
                        //TURNOS 
                        if (ficha.turnos != null)
                        {
                            foreach (var turno in ficha.turnos)
                            {
                                _sql = @"insert into ventas_transp_turno (
                                        id_venta,
                                        turno_detalle,
                                        turno_importe,
                                        turno_ruta)
                                    VALUES (
                                        @idVenta, 
                                        @turno_detalle,
                                        @turno_importe,
                                        @turno_ruta)";
                                var tur1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                                var tur2 = new MySql.Data.MySqlClient.MySqlParameter("@turno_detalle", turno.detalle);
                                var tur3 = new MySql.Data.MySqlClient.MySqlParameter("@turno_importe", turno.importe);
                                var tur4 = new MySql.Data.MySqlClient.MySqlParameter("@turno_ruta", turno.ruta );
                                var tur = cn.Database.ExecuteSqlCommand(_sql, tur1, tur2, tur3, tur4);
                                if (tur== 0)
                                {
                                    throw new Exception("PROBLEMA AL INSERTAR TURNOS");
                                }
                                cn.SaveChanges();
                            }
                        }
                        //
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

        private class docVerif
        {
            public string estatus { get; set; }
            public string autoRemision { get; set; }
            public docVerif()
            {
                estatus = "";
                autoRemision = "";
            }
        }
        public DtoLib.Resultado
            TransporteDocumento_AgregarFactura_Vericar(DtoTransporte.Documento.Agregar.Factura.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        //SALDO DEL CLIENTE EN DIVISA
                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                        var xsql_cli = @"select estatus from clientes
                                                where auto=@idCliente";
                        var _estatus = cn.Database.SqlQuery<string>(xsql_cli, xcli_1).FirstOrDefault();
                        if (_estatus == null)
                        {
                            result.Mensaje = "CLIENTE [ ID ] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (_estatus.Trim().ToUpper() != "ACTIVO")
                        {
                            result.Mensaje = "CLIENTE [ ESTATUS ] INCORRECTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        foreach (var rg in ficha.docRef)
                        {
                            var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", rg.idDoc);
                            var _sql = @"select 
                                            estatus_anulado as estatus, 
                                            auto_remision as autoRemision 
                                        from ventas
                                        where auto=@idDocRef";
                            var _docV = cn.Database.SqlQuery<docVerif>(_sql, p1).FirstOrDefault();
                            if (_docV == null)
                            {
                                result.Mensaje = "DOCUMENTO [ ID ] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            if (_docV.estatus.Trim().ToUpper() == "1")
                            {
                                result.Mensaje = "DOCUMENTO [ ESTATUS ] INCORRECTO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            if (_docV.autoRemision.Trim().ToUpper() != "")
                            {
                                result.Mensaje = "DOCUMENTO [ REMISION ] INCORRECTO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        foreach (var rg in ficha.aliadosResumen)
                        {
                            var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idAliado", rg.idAliado);
                            var _sql = @"select estatus from transp_aliado
                                                where id=@idAliado";
                            var _estatusAliad = cn.Database.SqlQuery<string>(_sql, p1).FirstOrDefault();
                            if (_estatusAliad == null)
                            {
                                result.Mensaje = "ALIADO [ ID ] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            if (_estatusAliad.Trim().ToUpper() == "1")
                            {
                                result.Mensaje = "ALIADO [ ESTATUS ] INCORRECTO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
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

        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarFactura_From_HojasServicio(DtoTransporte.Documento.Agregar.FacturaFromHojaServ.Ficha ficha)
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
                        //
                        var sql = "update sistema_contadores set a_ventas=a_ventas+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR TABLA CONTADORES");
                        }
                        //
                        var largo = 10;
                        var aDoc = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var autoDoc = aDoc.ToString().Trim().PadLeft(largo, '0');
                        var pSerie = new MySql.Data.MySqlClient.MySqlParameter("@autoSerie", ficha.serieDocId);
                        sql = "update empresa_series_fiscales set correlativo=correlativo+1 where auto=@autoSerie";
                        r1 = cn.Database.ExecuteSqlCommand(sql, pSerie);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CORRELATIVO SERIE FISCAL");
                        }
                        var nDoc = cn.Database.SqlQuery<int?>("select correlativo from empresa_series_fiscales where auto=@autoSerie", pSerie).FirstOrDefault();
                        if (!nDoc.HasValue)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CORRELATIVO SERIE FISCAL");
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
                                        docModuloCargar,
                                        igtf_tasa,
                                        igtf_monto_mon_act,
                                        igtf_aplica) 
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
                                        @autoDocCxC, 
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
                                        @docModuloCargar,
                                        0,
                                        0,
                                        '0')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoDoc", autoDoc);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@numDoc", docNumero);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmi", ficha.fechaEmision);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVen", ficha.fechaVencimiento);
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
                        var p65 = new MySql.Data.MySqlClient.MySqlParameter("@autoDocCxC","");
                        var r = cn.Database.ExecuteSqlCommand(_sql,
                                                                p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
                                                                p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                                                p21, p22, p23, p24, p25, p26, p27, p28, p29, p30,
                                                                p31, p32, p33, p34, p35, p36, p37, p38, p39, p40,
                                                                p41, p42, p43, p44, p45, p46, p47, p48, p49, p50,
                                                                p51, p53, p54, p55, p56, p57, p58, p59, p60,
                                                                p61, p62, p63, p64, p65);
                        if (r == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR DOCUMENTO DE VENTA");
                        }
                        cn.SaveChanges();
                        //
                        //INSERTAR DETALLES
                        var _sqlDetFct = @"INSERT INTO ventas_transp_detalle
                                    (
                                        id_venta ,
                                        detalle ,
                                        cnt_dias ,
                                        precio_neto_mon_local ,
                                        precio_neto_mon_divisa ,
                                        descuento_monto_mon_local ,
                                        descuento_monto_mon_divisa ,
                                        descuento_porct ,
                                        alicuota_id ,
                                        alicuota_tasa ,
                                        impuesto_mon_local ,
                                        impuesto_mon_divisa ,
                                        alicuota_desc ,
                                        precio_item_mon_local ,
                                        precio_item_mon_divisa ,
                                        precio_final_mon_local ,
                                        precio_final_mon_divisa ,
                                        importe_neto_mon_local ,
                                        importe_neto_mon_divisa ,
                                        importe_total_mon_local ,
                                        importe_total_mon_divisa ,
                                        total_mon_local ,
                                        total_mon_divisa ,
                                        estatus_anulado_doc ,
                                        fecha_doc ,
                                        id_cliente ,
                                        signo_doc ,
                                        codigo_doc ,
                                        id_doc_ref ,
                                        doc_num_ref ,
                                        doc_fecha_ref ,
                                        doc_monto_ref ,
                                        doc_codigo_ref,
                                        tipo_procedencia_item,
                                        id_item_servicio,
                                        mostrar_item_doc_final)
                                VALUES
                                    (
                                        @id_venta ,
                                        @detalle ,
                                        @cnt_dias ,
                                        @precio_neto_mon_local ,
                                        @precio_neto_mon_divisa ,
                                        @descuento_monto_mon_local ,
                                        @descuento_monto_mon_divisa ,
                                        @descuento_porct ,
                                        @alicuota_id ,
                                        @alicuota_tasa ,
                                        @impuesto_mon_local ,
                                        @impuesto_mon_divisa ,
                                        @alicuota_desc ,
                                        @precio_item_mon_local ,
                                        @precio_item_mon_divisa ,
                                        @precio_final_mon_local ,
                                        @precio_final_mon_divisa ,
                                        @importe_neto_mon_local ,
                                        @importe_neto_mon_divisa ,
                                        @importe_total_mon_local ,
                                        @importe_total_mon_divisa ,
                                        @total_mon_local ,
                                        @total_mon_divisa ,
                                        @estatus_anulado_doc ,
                                        @fecha_doc ,
                                        @id_cliente ,
                                        @signo_doc ,
                                        @codigo_doc ,
                                        @id_doc_ref ,
                                        @doc_num_ref ,
                                        @doc_fecha_ref ,
                                        @doc_monto_ref ,
                                        @doc_codigo_ref,
                                        @tipo_procedencia_item,
                                        @id_item_servicio,
                                        '0')";
                        var _idItemServicio = -1;
                        foreach (var rg in ficha.items)
                        {
                            //INSERTAR DETALLE DOCUMENTO
                            var ld1 = new MySql.Data.MySqlClient.MySqlParameter("@id_venta", autoDoc);
                            var ld2 = new MySql.Data.MySqlClient.MySqlParameter("@detalle", rg.detalle);
                            var ld3 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_dias", rg.cntDias);
                            var ld4 = new MySql.Data.MySqlClient.MySqlParameter("@precio_neto_mon_local", rg.precioNetoMonLocal);
                            var ld5 = new MySql.Data.MySqlClient.MySqlParameter("@precio_neto_mon_divisa", rg.precioNetoMonDivisa);
                            var ld6 = new MySql.Data.MySqlClient.MySqlParameter("@descuento_monto_mon_local", rg.dsctoMontoMonLocal);
                            var ld7 = new MySql.Data.MySqlClient.MySqlParameter("@descuento_monto_mon_divisa", rg.dsctoMontoMonDivisa);
                            var ld8 = new MySql.Data.MySqlClient.MySqlParameter("@descuento_porct", rg.dsctoPorc);
                            var ld9 = new MySql.Data.MySqlClient.MySqlParameter("@alicuota_id", rg.alicuotaId);
                            var ld10 = new MySql.Data.MySqlClient.MySqlParameter("@alicuota_tasa", rg.alicuotaTasa);
                            var ld11 = new MySql.Data.MySqlClient.MySqlParameter("@impuesto_mon_local", rg.impuestoMonLocal);
                            var ld12 = new MySql.Data.MySqlClient.MySqlParameter("@impuesto_mon_divisa", rg.impuestoMonDivisa);
                            var ld13 = new MySql.Data.MySqlClient.MySqlParameter("@alicuota_desc", rg.alicuotaDesc);
                            var ld14 = new MySql.Data.MySqlClient.MySqlParameter("@precio_item_mon_local", rg.precioItemMonLocal);
                            var ld15 = new MySql.Data.MySqlClient.MySqlParameter("@precio_item_mon_divisa", rg.precioItemMonDivisa);
                            var ld16 = new MySql.Data.MySqlClient.MySqlParameter("@precio_final_mon_local", rg.precioFinalMonLocal);
                            var ld17 = new MySql.Data.MySqlClient.MySqlParameter("@precio_final_mon_divisa", rg.precioFinalMonDivisa);
                            var ld18 = new MySql.Data.MySqlClient.MySqlParameter("@importe_neto_mon_local", rg.importeNetoMonLocal);
                            var ld19 = new MySql.Data.MySqlClient.MySqlParameter("@importe_neto_mon_divisa", rg.importeNetoMonDivisa);
                            var ld20 = new MySql.Data.MySqlClient.MySqlParameter("@importe_total_mon_local", rg.importeTotalMonLocal);
                            var ld21 = new MySql.Data.MySqlClient.MySqlParameter("@importe_total_mon_divisa", rg.importeTotalMonDivisa);
                            var ld22 = new MySql.Data.MySqlClient.MySqlParameter("@total_mon_local", rg.totalMonLocal);
                            var ld23 = new MySql.Data.MySqlClient.MySqlParameter("@total_mon_divisa", rg.totalMonDivisa);
                            var ld24 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_anulado_doc", "0");
                            var ld25 = new MySql.Data.MySqlClient.MySqlParameter("@fecha_doc", ficha.fechaEmision);
                            var ld26 = new MySql.Data.MySqlClient.MySqlParameter("@id_cliente", ficha.idCliente);
                            var ld27 = new MySql.Data.MySqlClient.MySqlParameter("@signo_doc", ficha.signo);
                            var ld28 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_doc", ficha.TipoDoc);
                            var ld29 = new MySql.Data.MySqlClient.MySqlParameter("@id_doc_ref", rg.idDocRef);
                            var ld30 = new MySql.Data.MySqlClient.MySqlParameter("@doc_num_ref", rg.numDocRef);
                            var ld31 = new MySql.Data.MySqlClient.MySqlParameter("@doc_fecha_ref", rg.fechaDocRef);
                            var ld32 = new MySql.Data.MySqlClient.MySqlParameter("@doc_monto_ref", rg.montoDocRef);
                            var ld33 = new MySql.Data.MySqlClient.MySqlParameter("@doc_codigo_ref", rg.codigoDocRef);
                            var ld34 = new MySql.Data.MySqlClient.MySqlParameter("@tipo_procedencia_item", rg.tipoProcedenciaItem);
                            var ld35 = new MySql.Data.MySqlClient.MySqlParameter("@id_item_servicio", _idItemServicio);
                            var r_det = cn.Database.ExecuteSqlCommand(_sqlDetFct, ld1, ld2, ld3, ld4, ld5, ld6, ld7, ld8, ld9, ld10,
                                                                            ld11, ld12, ld13, ld14, ld15, ld16, ld17, ld18, ld19, ld20,
                                                                            ld21, ld22, ld23, ld24, ld25, ld26, ld27, ld28, ld29, ld30,
                                                                            ld31, ld32, ld33, ld34, ld35);
                            if (r_det == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR ITEM");
                            }
                            cn.SaveChanges();
                        }
                        //
                        //DOCUMENTOS DE REFERENCIA QUE SE USARON PARA GENERAR LA FACTURA
                        _sql = @"INSERT INTO ventas_transp_doc  
                                    (
                                        id_venta,
                                        id_cliente,
                                        fecha,
                                        estatus_anulado,
                                        id_doc_ref,
                                        doc_num_ref,
                                        doc_fecha_ref,
                                        doc_monto_divisa_ref,
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
                                        @docMontoDivisaRef,
                                        @docCodRef,
                                        @docTipoRef
                                    )
                                ";
                        foreach (var rg in ficha.docRef)
                        {
                            var lp1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                            var lp2 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                            var lp3 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", ficha.fechaEmision);
                            var lp4 = new MySql.Data.MySqlClient.MySqlParameter("@estatusAnulado", "0");
                            var lp5 = new MySql.Data.MySqlClient.MySqlParameter("@idDocRef", rg.idDoc);
                            var lp6 = new MySql.Data.MySqlClient.MySqlParameter("@docNumRef", rg.numDoc);
                            var lp7 = new MySql.Data.MySqlClient.MySqlParameter("@docFechaRef", rg.fechaDoc);
                            var lp8 = new MySql.Data.MySqlClient.MySqlParameter("@docMontoDivisaRef", rg.montoDivisaDoc);
                            var lp9 = new MySql.Data.MySqlClient.MySqlParameter("@docCodRef", rg.codigoDoc);
                            var lp10 = new MySql.Data.MySqlClient.MySqlParameter("@docTipoRef", rg.tipoDoc);
                            var r3 = cn.Database.ExecuteSqlCommand(_sql, lp1, lp2, lp3, lp4, lp5, lp6,
                                                                    lp7, lp8, lp9, lp10);
                            if (r3 == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR VENTA - DOCUMENTO - REF");
                            }
                            cn.SaveChanges();
                            //
                            //ACTUALIZO LA REMISION DE DOCUMENTOS DE REFERENCIA
                            var xlp1 = new MySql.Data.MySqlClient.MySqlParameter("@idRemision", autoDoc);
                            var xlp2 = new MySql.Data.MySqlClient.MySqlParameter("@codDocRemision", ficha.TipoDoc);
                            var xlp3 = new MySql.Data.MySqlClient.MySqlParameter("@numDocRemision", docNumero);
                            var xlp4 = new MySql.Data.MySqlClient.MySqlParameter("@autoDocRef", rg.idDoc);
                            var xlp5 = new MySql.Data.MySqlClient.MySqlParameter("@situacion", "PROCESADO");
                            var _sql_2 = @"update ventas set 
                                                auto_remision=@idRemision,
                                                tipo_remision=@codDocRemision,
                                                documento_remision=@numDocRemision,
                                                situacion=@situacion
                                            where auto=@autoDocRef";
                            var xr3 = cn.Database.ExecuteSqlCommand(_sql_2, xlp1, xlp2, xlp3, xlp4, xlp5);
                            if (xr3 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR REMISION DOCUMENTO");
                            }
                            cn.SaveChanges();
                        }
                        //
                        //TURNOS 
                        if (ficha.turnos != null)
                        {
                            foreach (var turno in ficha.turnos)
                            {
                                _sql = @"insert into ventas_transp_turno (
                                        id_venta,
                                        turno_detalle,
                                        turno_importe,
                                        turno_ruta)
                                    VALUES (
                                        @idVenta, 
                                        @turno_detalle,
                                        @turno_importe,
                                        @turno_ruta)";
                                var tur1 = new MySql.Data.MySqlClient.MySqlParameter("@idVenta", autoDoc);
                                var tur2 = new MySql.Data.MySqlClient.MySqlParameter("@turno_detalle", turno.detalle);
                                var tur3 = new MySql.Data.MySqlClient.MySqlParameter("@turno_importe", turno.importe);
                                var tur4 = new MySql.Data.MySqlClient.MySqlParameter("@turno_ruta", turno.ruta);
                                var tur = cn.Database.ExecuteSqlCommand(_sql, tur1, tur2, tur3, tur4);
                                if (tur == 0)
                                {
                                    throw new Exception("PROBLEMA AL INSERTAR TURNOS");
                                }
                                cn.SaveChanges();
                            }
                        }
                        //
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