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
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Presupuesto.Ficha>
            TransporteDocumento_EntidadPresupuesto_GetById(string idDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Presupuesto.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _sql_1 = @"select 
                                        auto as idDoc, 
                                        documento as docNUmero,
                                        fecha as docFechaEmision, 
                                        fecha_vencimiento as docFechaVence, 
                                        razon_social as clienteNombre,  
                                        dir_fiscal as clienteDirFiscal, 
                                        ci_rif as clienteCiRif, 
                                        tipo as docCodigoTipo, 
                                        exento as montoExento, 
                                        base1 as montoBase1, 
                                        base2 as montoBase2, 
                                        base3 as montoBase3, 
                                        impuesto1 as impuesto1, 
                                        impuesto2 as impuesto2, 
                                        impuesto3 as impuesto3, 
                                        base as montoBase,
                                        impuesto as montoImpuesto,
                                        total as docTotal, 
                                        tasa1 as tasa1, 
                                        tasa2 as tasa2, 
                                        tasa3 as tasa3, 
                                        nota as notasObs, 
                                        auto_cliente as clienteId,
                                        codigo_cliente as clienteCodigo,
                                        mes_relacion as mesRelacion,
                                        fecha_registro as fechaRegistro,
                                        dias as diasCredito,
                                        descuento1,
                                        descuento2,
                                        cargos,
                                        descuento1p,
                                        descuento2p,
                                        cargosp,
                                        estatus_anulado as estatusAnulado,
                                        subtotal_neto as subtotalNeto,
                                        telefono as clienteTelefono,
                                        factor_cambio as factorCambio,
                                        codigo_vendedor as vendedorCodigo,
                                        vendedor as vendedorNombre,
                                        auto_vendedor as vendedorId,
                                        condicion_pago as condPago,
                                        usuario as usuarioNombre,
                                        codigo_usuario as usuarioCodigo,
                                        codigo_sucursal as codSucursal,
                                        hora as horaRegistro,
                                        monto_divisa as montoDivisa,
                                        estacion,
                                        renglones as cntRenglones,
                                        ano_relacion as anoRelacion,
                                        dias_validez as diasValidez,
                                        auto_usuario as usuarioId,
                                        signo as docSigno,
                                        documento_nombre as docNombre,
                                        subtotal_impuesto as subTotalImpuesto,
                                        subtotal,
                                        neto as montoNeto,
                                        documento_tipo as docModulo,
                                        docSolicitadoPor as docSolicitadoPor,
                                        docModuloCargar as docModuloCargar
                                    FROM ventas where auto=@idDoc";
                    var _sql = _sql_1;
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Presupuesto.FichaEncabezado>(_sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        throw new Exception("DOCUMENTO [ ID ] NO ENCONTRADO");
                    }

                    _sql = @"select 
                                id_item as id,
                                servicio_desc as servicioDesc,
                                cnt_dias as cntDias,
                                cnt_unidades as cntUnidades,   
                                precio_neto_divisa as precioNetoDivisa,
                                dscto,
                                alicuota_id as alicuotaId,
                                alicuota_tasa as alicuotaTasa,
                                alicuota_desc as alicuotaDesc,
                                notas,
                                importe,
                                unidades_desc as unidadesDesc,
                                servicio_id as servicioId,
                                servicio_codigo as servicioCodigo,
                                servicio_detalle as servicioDetalle,
                                turno_estatus as turnoEstatus,
                                turno_id as turnoId,
                                turno_desc as turnoDesc,
                                turno_cnt_dias as turnoCntDias
                            FROM ventas_transp_item where id_venta=@idDoc";
                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _det = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Presupuesto.FichaDetalle>(_sql, xp1).ToList();
                    if (_det == null)
                    {
                        throw new Exception("ITEMS DOCUMENTO NO ENCONTRADOS");
                    }
                    foreach (var reg in _det)
                    {
                        _sql = @"select 
                                    fecha,
                                    hora,
                                    nota
                                FROM ventas_transp_item_fecha where id_venta=@idDoc and id_item=@idItem";
                        var yp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                        var yp2 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", reg.id);
                        var _serv = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Presupuesto.FichaFechaServ>(_sql, yp1, yp2).ToList();
                        reg.fechaServ = _serv;

                        _sql = @"select 
                                    id_aliado as idAliado,
                                    cirif_aliado as ciRif,
                                    codigo_aliado as codigo,
                                    desc_alido as descripcion,
                                    precio_unit_divisa as precioUnitDivisa,
                                    cnt_dias as cntDias,
                                    importe as importe
                                FROM ventas_transp_item_aliado where id_venta=@idDoc and id_item=@idItem";
                        var zp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                        var zp2 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", reg.id);
                        var _aliad = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Presupuesto.FichaAliado>(_sql, zp1, zp2).ToList();
                        reg.aliados = _aliad;
                    }
                    result.Entidad = new DtoTransporte.Documento.Entidad.Presupuesto.Ficha()
                    {
                        encabezado = _ent,
                        items = _det,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoLista<DtoTransporte.Documento.Entidad.Presupuesto.FichaAliado>
            TransporteDocumento_EntidadPresupuesto_GetAliadosById(string idDoc)
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Documento.Entidad.Presupuesto.FichaAliado>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _sql = @"select 
                                    id_aliado as idAliado,
                                    cirif_aliado as ciRif,
                                    codigo_aliado as codigo,
                                    desc_alido as descripcion,
                                    precio_unit_divisa as precioUnitDivisa,
                                    cnt_dias as cntDias,
                                    importe as importe
                                FROM ventas_transp_item_aliado where id_venta=@idDoc ";
                    var zp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _aliad = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Presupuesto.FichaAliado>(_sql, zp1).ToList();
                    result.Lista = _aliad;
                }
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
                                        tipo as docCodigo, 
                                        documento_nombre as docNombre, 
                                        razon_social clienteNombre, 
                                        ci_rif as clienteCiRif, 
                                        factor_cambio as factorCambio, 
                                        signo as docSigno, 
                                        renglones as docCntRenglones,
                                        estatus_anulado as estatusAnulado, 
                                        docSolicitadoPor as docSolicitadoPor,
                                        docModuloCargar as docModuloCargar
                                    FROM ventas ";
                    var _sql_2 = @" where 1=1 and docEstatusPendiente<>'1' ";
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
                    if (filtro.esPorRemision)
                    {
                        _sql_2 += " and estatus_anulado<>'1' and auto_remision='' ";
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

        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Venta.Ficha>
            TransporteDocumento_EntidadVenta_GetById(string idDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Venta.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _sql_1 = @"select 
                                        auto as idDoc, 
                                        documento as docNUmero,
                                        fecha as docFechaEmision, 
                                        fecha_vencimiento as docFechaVence, 
                                        razon_social as clienteNombre,  
                                        dir_fiscal as clienteDirFiscal, 
                                        ci_rif as clienteCiRif, 
                                        tipo as docCodigoTipo, 
                                        exento as montoExento, 
                                        base1 as montoBase1, 
                                        base2 as montoBase2, 
                                        base3 as montoBase3, 
                                        impuesto1 as impuesto1, 
                                        impuesto2 as impuesto2, 
                                        impuesto3 as impuesto3, 
                                        base as montoBase,
                                        impuesto as montoImpuesto,
                                        total as docTotal, 
                                        tasa1 as tasa1, 
                                        tasa2 as tasa2, 
                                        tasa3 as tasa3, 
                                        nota as notasObs, 
                                        auto_cliente as clienteId,
                                        codigo_cliente as clienteCodigo,
                                        mes_relacion as mesRelacion,
                                        fecha_registro as fechaRegistro,
                                        dias as diasCredito,
                                        descuento1,
                                        descuento2,
                                        cargos,
                                        descuento1p,
                                        descuento2p,
                                        cargosp,
                                        estatus_anulado as estatusAnulado,
                                        subtotal_neto as subtotalNeto,
                                        telefono as clienteTelefono,
                                        factor_cambio as factorCambio,
                                        codigo_vendedor as vendedorCodigo,
                                        vendedor as vendedorNombre,
                                        auto_vendedor as vendedorId,
                                        condicion_pago as condPago,
                                        usuario as usuarioNombre,
                                        codigo_usuario as usuarioCodigo,
                                        codigo_sucursal as codSucursal,
                                        hora as horaRegistro,
                                        monto_divisa as montoDivisa,
                                        estacion,
                                        renglones as cntRenglones,
                                        ano_relacion as anoRelacion,
                                        dias_validez as diasValidez,
                                        auto_usuario as usuarioId,
                                        signo as docSigno,
                                        documento_nombre as docNombre,
                                        subtotal_impuesto as subTotalImpuesto,
                                        subtotal,
                                        neto as montoNeto,
                                        documento_tipo as docModulo,
                                        docSolicitadoPor as docSolicitadoPor,
                                        docModuloCargar as docModuloCargar,
                                        igtf_tasa as igtfTasa,
                                        igtf_monto_mon_act as igtfMontoMonAct,
                                        notas_periodo_lapso as notasPeriodoLapso
                                    FROM ventas where auto=@idDoc";
                    var _sql = _sql_1;
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Venta.FichaEncabezado>(_sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        throw new Exception("DOCUMENTO [ ID ] NO ENCONTRADO");
                    }
                    //
                    _sql = @"select 
                                detalle as detalle,
                                cnt_dias as cntDias,
                                precio_neto_mon_local as precioNetoMonLocal,
                                precio_neto_mon_divisa as precioNetoMonDivisa,
                                descuento_monto_mon_local as dsctoMontoMonLocal,
                                descuento_monto_mon_divisa as dsctoMontoMonDivisa,
                                descuento_porct as dsctoPorc,
                                alicuota_id as alicuotaId,
                                alicuota_tasa as alicuotaTasa,
                                impuesto_mon_local as impuestoMonLocal,
                                impuesto_mon_divisa as impuestoMonDivisa,
                                alicuota_desc as alicuotaDesc,
                                precio_item_mon_local as precioItemMonLocal,
                                precio_item_mon_divisa as precioItemMonDivisa,
                                precio_final_mon_local as precioFinalMonLocal,
                                precio_final_mon_divisa as precioFinalMonDivisa,
                                importe_neto_mon_local as importeNetoMonLocal,
                                importe_neto_mon_divisa as importeNetoMonDivisa,
                                importe_total_mon_local as importeTotalMonLocal,
                                importe_total_mon_divisa as importeTotalMonDivisa,
                                total_mon_local as totalMonLocal,
                                total_mon_divisa as totalMonDivisa,
                                id_doc_ref as idDocRef,
                                doc_num_ref as numDocRef,
                                doc_fecha_ref as fechaDocRef,
                                doc_monto_ref as montoDocRef,
                                doc_codigo_ref as codigoDocRef,
                                tipo_procedencia_item as tipoProcedenciaItem,
                                id_item_servicio as idItemServicio,
                                mostrar_item_doc_final as mostrarItemDocFinal
                            FROM ventas_transp_detalle where id_venta=@idDoc";
                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _det = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Venta.FichaDetalle>(_sql, xp1).ToList();
                    if (_det == null)
                    {
                        throw new Exception("ITEMS DOCUMENTO NO ENCONTRADOS");
                    }
                    //
                    _sql = @"select 
                                turno_detalle as detalle,
                                turno_importe as importe,
                                turno_ruta as ruta
                            FROM ventas_transp_turno where id_venta=@idDoc";
                    xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _lstTurno= cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Venta.Turno>(_sql, xp1).ToList();
                    //
                    _sql = @"SELECT
                                it.id_venta as idVenta,
                                it.servicio_desc servDesc,
                                it.cnt_dias as cntDias,
                                it.cnt_unidades as cntVehic,
                                it.precio_neto_divisa as pnetoDiv,
                                it.notas as notas,
                                it.importe as importe,
                                it.unidades_desc as descVehic,
                                it.servicio_codigo as servCod,
                                it.servicio_detalle as servDet,
                                it.turno_estatus as turnEstatus,
                                it.turno_desc turnDesc,
                                it.turno_cnt_dias as turnCntDias,
                                det.doc_num_ref as docNroRef,
                                det.tipo_procedencia_item as docTipoProcedencia
                            FROM ventas_transp_item as it
                            join ventas_transp_detalle as det on det.id_doc_ref=it.id_venta 
                            WHERE det.id_venta=@idDoc";
                    xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _lstDetTurno = cnn.Database.SqlQuery<DtoTransporte.Documento.Entidad.Venta.DetTurno>(_sql, xp1).ToList();
                    //
                    result.Entidad = new DtoTransporte.Documento.Entidad.Venta.Ficha()
                    {
                        encabezado = _ent,
                        detalles = _det,
                        turnos= _lstTurno,
                        detTurno=_lstDetTurno 
                    };
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
            TransporteDocumento_Presupuesto_Pendiente_Cnt()
        {
            var result = new DtoLib.ResultadoEntidad<int>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"select count(*) as cnt 
                                    FROM ventas where tipo='05' and 
                                        estatus_anulado<>'1' and 
                                        docEstatusPendiente='1'";
                    var _sql = _sql_1;
                    var _cnt= cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                    if (_cnt.HasValue)
                    {
                        result.Entidad = _cnt.Value;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.Lista.Pendiente.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_Pendiente()
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Documento.Lista.Pendiente.Presupuesto.Ficha>();
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
                                        tipo as docCodigo, 
                                        documento_nombre as docNombre, 
                                        razon_social clienteNombre, 
                                        ci_rif as clienteCiRif, 
                                        factor_cambio as factorCambio, 
                                        signo as docSigno, 
                                        renglones as docCntRenglones,
                                        estatus_anulado as estatusAnulado, 
                                        docSolicitadoPor as docSolicitadoPor,
                                        docModuloCargar as docModuloCargar
                                    FROM ventas ";
                    var _sql_2 = @" where 1=1 and estatus_anulado<>'1' and auto_remision='' and docEstatusPendiente='1' ";
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Documento.Lista.Pendiente.Presupuesto.Ficha>(_sql, p1, p2).ToList();
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

        public DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> 
            TransporteDocumento_GetLista(DtoLibPos.Documento.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p9 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select v.auto as id, 
                                v.documento as docNumero, 
                                v.control, 
                                v.fecha as fechaEmision, 
                                v.hora as horaEmision, 
                                v.razon_social as nombreRazonSocial, 
                                v.ci_Rif as cirif, 
                                v.total as monto, 
                                v.estatus_Anulado as estatus, 
                                v.renglones, 
                                v.serie, 
                                v.monto_divisa as montoDivisa, 
                                v.tipo as docCodigo, 
                                v.signo as docSigno,    
                                v.documento_nombre as docNombre, 
                                v.aplica as docAplica, 
                                v.codigo_sucursal as sucursalCod, 
                                v.situacion as docSituacion,    
                                es.nombre as sucursalDesc,
                                v.clave as claveSistema
                                FROM ventas as v ";
                    var sql_2 = " join empresa_sucursal as es on v.codigo_sucursal=es.codigo ";
                    var sql_3 = " where 1=1 and docEstatusPendiente<>'1' ";

                    if (filtro.idArqueo != "")
                    {
                        sql_3 += " and v.cierre=@p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = filtro.idArqueo;
                    }
                    if (filtro.codTipoDocumento != "")
                    {
                        sql_3 += " and v.tipo=@p2 ";
                        p2.ParameterName = "@p2";
                        p2.Value = filtro.codTipoDocumento;
                    }
                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@p3 ";
                        p3.ParameterName = "@p3";
                        p3.Value = filtro.codSucursal;
                    }
                    if (filtro.fecha != null)
                    {
                        sql_3 += " and v.fecha>=@p4 and v.fecha<=@p5 ";
                        p4.ParameterName = "@p4";
                        p4.Value = filtro.fecha.desde;
                        p5.ParameterName = "@p5";
                        p5.Value = filtro.fecha.hasta;
                    }
                    if (filtro.idCliente != "")
                    {
                        sql_3 += " and v.auto_cliente=@p6 ";
                        p6.ParameterName = "@p6";
                        p6.Value = filtro.idCliente;
                    }
                    if (filtro.idProducto != "")
                    {
                        sql_2 += @" join ventas_detalle as vd on v.auto= vd.auto_documento and vd.auto_producto=@idProducto ";
                        p7.ParameterName = "@idProducto";
                        p7.Value = filtro.idProducto;
                    }
                    if (filtro.estatus != DtoLibPos.Documento.Lista.Filtro.enumEstatus.SinDefinir)
                    {
                        var xEstatus = "0";
                        if (filtro.estatus == DtoLibPos.Documento.Lista.Filtro.enumEstatus.Anulado)
                            xEstatus = "1";

                        sql_3 += " and v.estatus_anulado=@estatus ";
                        p8.ParameterName = "@estatus";
                        p8.Value = xEstatus;
                    }
                    if (filtro.palabraClave != "")
                    {
                        p9.ParameterName = "@clave";
                        p9.Value = "%" + filtro.palabraClave + "%";
                        sql_3 += " and (v.ci_rif LIKE @clave or v.razon_social LIKE @clave) ";
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Documento.Lista.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
                    rt.Lista = q;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.GetAliados.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_GetAliados(string idDoc)
        {
            var rt = new DtoLib.ResultadoLista<DtoTransporte.Documento.GetAliados.Presupuesto.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    aliado.id as idAliado, 
                                    aliado.ciRif as ciRif, 
                                    aliado.nombreRazonSocial as nombre, 
                                    sum(item.importe) as importe
                                FROM ventas_transp_item_aliado as item
                                join transp_aliado as aliado on aliado.id=item.id_aliado
                                where item.id_venta=@idDoc
                                group by item.id_aliado";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var lst = cnn.Database.SqlQuery<DtoTransporte.Documento.GetAliados.Presupuesto.Ficha>(sql, p1).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.GetServicios.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_GetServicios(string idDoc)
        {
            var rt = new DtoLib.ResultadoLista<DtoTransporte.Documento.GetServicios.Presupuesto.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    itAliado.id_aliado as idAliado,
                                    itAliado.importe as importeServ,
                                    it.servicio_id as idServ,
                                    it.servicio_codigo as codServ,
                                    it.servicio_desc as descServ,
                                    it.servicio_detalle as detServ
                                FROM ventas_transp_item_aliado as itAliado
                                join ventas_transp_item as it on it.id_item=itAliado.id_item
                                where it.id_venta=@idDoc";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var lst = cnn.Database.SqlQuery<DtoTransporte.Documento.GetServicios.Presupuesto.Ficha>(sql, p1).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }

        public DtoLib.ResultadoLista<DtoTransporte.Documento.GetTurnos.Presupuesto.Ficha> 
            TransporteDocumento_Presupuesto_GetTurnos(string idDoc)
        {
            var rt = new DtoLib.ResultadoLista<DtoTransporte.Documento.GetTurnos.Presupuesto.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    it.importe as importeMonDiv,
                                    it.turno_id as turnoId,
                                    it.turno_desc as turnoDesc,
                                    it.servicio_detalle as turnoRuta
                                FROM ventas_transp_item as it
                                where it.id_venta=@idDoc and it.turno_estatus='1'";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var lst = cnn.Database.SqlQuery<DtoTransporte.Documento.GetTurnos.Presupuesto.Ficha>(sql, p1).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }

        //
        public DtoLib.ResultadoLista<DtoTransporte.Documento.GetTurnos.Documento.Ficha> 
            TransporteDocumento_Documento_GetTurnos(string idDoc)
        {
            var rt = new DtoLib.ResultadoLista<DtoTransporte.Documento.GetTurnos.Documento.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    it.turno_importe as importeMonDiv,
                                    it.turno_detalle as turnoDesc,
                                    it.turno_ruta as turnoRuta
                                FROM ventas_transp_turno as it
                                where it.id_venta=@idDoc
                                union (
                                    select 
                                        dt.importe_total_mon_divisa as importeMonDiv,
                                        '' as turnoDesc,
                                        dt.detalle as turnoRuta
                                    FROM ventas_transp_detalle as dt
                                    where dt.id_venta=@idDoc and mostrar_item_doc_final='1')";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var lst = cnn.Database.SqlQuery<DtoTransporte.Documento.GetTurnos.Documento.Ficha>(sql, p1).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }


        //
        public DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> 
            TransporteDocumento_Documento_AplicanNotaCredito_FiltradoByCliente(string cliente)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select v.auto as id, 
                                v.documento as docNumero, 
                                v.control, 
                                v.fecha as fechaEmision, 
                                v.hora as horaEmision, 
                                v.razon_social as nombreRazonSocial, 
                                v.ci_Rif as cirif, 
                                v.total as monto, 
                                v.estatus_Anulado as estatus, 
                                v.renglones, 
                                v.serie, 
                                v.monto_divisa as montoDivisa, 
                                v.tipo as docCodigo, 
                                v.signo as docSigno,    
                                v.documento_nombre as docNombre, 
                                v.aplica as docAplica, 
                                v.codigo_sucursal as sucursalCod, 
                                v.situacion as docSituacion,    
                                es.nombre as sucursalDesc,
                                v.clave as claveSistema
                                FROM ventas as v ";
                    var sql_2 = " join empresa_sucursal as es on v.codigo_sucursal=es.codigo ";
                    var sql_3 = @" where 1=1 and 
                                        v.tipo='01' and 
                                        v.estatus_anulado='0' and 
                                        v.codigo_sucursal='01' ";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (cliente.Trim()!= "")
                    {
                        p1.ParameterName = "@clave";
                        p1.Value = "%" + cliente.Trim().ToUpper() + "%";
                        sql_3 += " and (v.documento LIKE @clave or v.ci_rif LIKE @clave or v.razon_social LIKE @clave) ";
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Documento.Lista.Ficha>(sql, p1).ToList();
                    rt.Lista = q;
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
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha> 
            TransporteDocumento_Documento_AplicaNotaCredito_GetData(string idDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _sql_1 = @"select 
                                        auto as idDoc, 
                                        documento as docNumero,
                                        fecha as docFechaEmision, 
                                        razon_social as clienteNombre,  
                                        dir_fiscal as clienteDirFiscal, 
                                        ci_rif as clienteCiRif, 
                                        tipo as docCodigoTipo, 
                                        exento as montoExento, 
                                        base1 as montoBase1, 
                                        base2 as montoBase2, 
                                        base3 as montoBase3, 
                                        impuesto1 as montoImpuesto1, 
                                        impuesto2 as montoImpuesto2, 
                                        impuesto3 as montoImpuesto3, 
                                        base as montoBase,
                                        impuesto as montoImpuesto,
                                        total as docTotal, 
                                        tasa1 as tasa1, 
                                        tasa2 as tasa2, 
                                        tasa3 as tasa3, 
                                        auto_cliente as clienteId,
                                        codigo_cliente as clienteCodigo,
                                        telefono as clienteTelefono,
                                        factor_cambio as factorCambio,
                                        codigo_vendedor as vendedorCodigo,
                                        vendedor as vendedorNombre,
                                        auto_vendedor as vendedorId,
                                        monto_divisa as montoDivisa,
                                        neto as montoNeto,
                                        documento_tipo as docModulo,
                                        codigo_sucursal as codigoSucursal
                                    FROM ventas where auto=@idDoc";
                    var _sql = _sql_1;
                    var _ent = cnn.Database.SqlQuery<DtoTransporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha>(_sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        throw new Exception("DOCUMENTO [ ID ] NO ENCONTRADO");
                    }
                    result.Entidad = _ent;
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
        public DtoLib.ResultadoEntidad<string>
            TransporteDocumento_Documento_NotaCredito_Agregar(DtoTransporte.Documento.Agregar.NotaCredito.Nueva.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString().Trim().PadLeft(4, '0');
                        var fechaNula = new DateTime(2000, 1, 1);
                        //
                        //ACTUALIZAr CONTADORES
                        //
                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_cxc=a_cxc+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR TABLA CONTADORES");
                        }
                        var aVenta = cnn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var aCxC = cnn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var largo = 0;
                        largo = 10 - ficha.PrefijoSuc.Length;
                        var fechaVenc = fechaSistema.Date;
                        var autoVenta = ficha.PrefijoSuc + aVenta.ToString().Trim().PadLeft(largo, '0');
                        var autoCxC = ficha.PrefijoSuc + aCxC.ToString().Trim().PadLeft(largo, '0');
                        //
                        //ACTUALIZAR SERIE FISCAL
                        //
                        var m1 = new MySql.Data.MySqlClient.MySqlParameter();
                        m1.ParameterName = "@m1";
                        m1.Value = ficha.serieDocId;
                        var xsql = "update empresa_series_fiscales set correlativo=correlativo+1 where auto=@m1";
                        var xr1 = cnn.Database.ExecuteSqlCommand(xsql, m1);
                        if (xr1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR SERIE FISCAL DOCUMENTO");
                        }
                        var adoc = cnn.Database.SqlQuery<int>("select correlativo from empresa_series_fiscales where auto=@m1", m1).FirstOrDefault();
                        var documentoNro = adoc.ToString().Trim().PadLeft(10, '0');
                        var aCxCRecibo = 0;
                        var aCxCReciboNumero = 0;
                        var autoRecibo = "";
                        var reciboNumero = "";
                        //
                        //DOCUMENTO NOTA/CREDITO
                        //
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
                                        docEstatusPendiente,
                                        igtf_tasa,
                                        igtf_monto_mon_act,
                                        igtf_aplica,
                                        igtf_monto_mon_div,
                                        notas_periodo_lapso) 
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
                                        '', 
                                        @fechaRegistro, 
                                        '',
                                        0, 
                                        0, 
                                        0, 
                                        0, 
                                        0, 
                                        0, 
                                        0, 
                                        '', 
                                        '0', 
                                        @aplica, 
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
                                        0,
                                        '',
                                        '',
                                        '',
                                        0,
                                        0,
                                        0,
                                        '0',
                                        '')";
                        var p01 = new MySql.Data.MySqlClient.MySqlParameter("@autoDoc", autoVenta);
                        var p02 = new MySql.Data.MySqlClient.MySqlParameter("@numDoc", documentoNro);
                        var p03 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmi", fechaSistema.Date);
                        var p04 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVen", fechaSistema.Date);
                        var p05 = new MySql.Data.MySqlClient.MySqlParameter("@razonSocial", ficha.Doc.RazonSocial);
                        var p06 = new MySql.Data.MySqlClient.MySqlParameter("@dirFiscal", ficha.Doc.DirFiscal);
                        var p07 = new MySql.Data.MySqlClient.MySqlParameter("@ciRif", ficha.Doc.CiRif);
                        var p08 = new MySql.Data.MySqlClient.MySqlParameter("@tipoDoc", ficha.Doc.docCodigo);
                        var p09 = new MySql.Data.MySqlClient.MySqlParameter("@montoExento", ficha.Doc.montoExento);
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase1", ficha.Doc.montoBase1);
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase2", ficha.Doc.montoBase2);
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase3", ficha.Doc.montoBase3);
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp1", ficha.Doc.montoImpuesto1);
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp2", ficha.Doc.montoImpuesto2);
                        var p15 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp3", ficha.Doc.montoImpuesto3);
                        var p16 = new MySql.Data.MySqlClient.MySqlParameter("@montoBase", ficha.Doc.montoBase);
                        var p17 = new MySql.Data.MySqlClient.MySqlParameter("@montoImp", ficha.Doc.montoImpuesto);
                        var p18 = new MySql.Data.MySqlClient.MySqlParameter("@total", ficha.Doc.Total);
                        var p19 = new MySql.Data.MySqlClient.MySqlParameter("@tasa1", ficha.Doc.Tasa1);
                        var p20 = new MySql.Data.MySqlClient.MySqlParameter("@tasa2", ficha.Doc.Tasa2);
                        var p21 = new MySql.Data.MySqlClient.MySqlParameter("@tasa3", ficha.Doc.Tasa3);
                        var p22 = new MySql.Data.MySqlClient.MySqlParameter("@autoCliente", ficha.Doc.idCliente);
                        var p23 = new MySql.Data.MySqlClient.MySqlParameter("@codigoCliente", ficha.Doc.codCliente);
                        var p24 = new MySql.Data.MySqlClient.MySqlParameter("@mesRelacion", mesRelacion);
                        var p25 = new MySql.Data.MySqlClient.MySqlParameter("@fechaRegistro", fechaSistema.Date);
                        var p26 = new MySql.Data.MySqlClient.MySqlParameter("@subtotalNeto", ficha.Doc.subTotalNeto);
                        var p27 = new MySql.Data.MySqlClient.MySqlParameter("@telefono", ficha.Doc.telefono);
                        var p28 = new MySql.Data.MySqlClient.MySqlParameter("@factorCambio", ficha.Doc.factorCambio);
                        var p29 = new MySql.Data.MySqlClient.MySqlParameter("@codigoVend", ficha.Doc.codVendedor);
                        var p30 = new MySql.Data.MySqlClient.MySqlParameter("@vendedor", ficha.Doc.vendedor);
                        var p31 = new MySql.Data.MySqlClient.MySqlParameter("@autoVendedor", ficha.Doc.idVendedor);
                        var p32 = new MySql.Data.MySqlClient.MySqlParameter("@condPago", ficha.Doc.condPago);
                        var p33 = new MySql.Data.MySqlClient.MySqlParameter("@usuario", ficha.Doc.usuario);
                        var p34 = new MySql.Data.MySqlClient.MySqlParameter("@codUsuario", ficha.Doc.codUsuario);
                        var p35 = new MySql.Data.MySqlClient.MySqlParameter("@codSucursal", ficha.Doc.codSucursal);
                        var p36 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                        var p37 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa", ficha.Doc.montoDivisa);
                        var p38 = new MySql.Data.MySqlClient.MySqlParameter("@estacion", ficha.estacion);
                        var p39 = new MySql.Data.MySqlClient.MySqlParameter("@cntRenglones", ficha.Doc.cntRenglones);
                        var p40 = new MySql.Data.MySqlClient.MySqlParameter("@anoRelacion", anoRelacion);
                        var p41 = new MySql.Data.MySqlClient.MySqlParameter("@diasValidez", ficha.Doc.diasValidez);
                        var p42 = new MySql.Data.MySqlClient.MySqlParameter("@autoUsuario", ficha.Doc.idUsuario);
                        var p43 = new MySql.Data.MySqlClient.MySqlParameter("@signo", ficha.Doc.signo);
                        var p44 = new MySql.Data.MySqlClient.MySqlParameter("@tipoRemision", ficha.Doc.tipoRemision);
                        var p45 = new MySql.Data.MySqlClient.MySqlParameter("@docRemision", ficha.Doc.docRemision);
                        var p46 = new MySql.Data.MySqlClient.MySqlParameter("@autoRemision", ficha.Doc.idRemision);
                        var p47 = new MySql.Data.MySqlClient.MySqlParameter("@docNombre", ficha.Doc.docNombre);
                        var p48 = new MySql.Data.MySqlClient.MySqlParameter("@subtotalImp", ficha.Doc.subTotalImpuesto);
                        var p49 = new MySql.Data.MySqlClient.MySqlParameter("@subTotal", ficha.Doc.subTotal);
                        var p50 = new MySql.Data.MySqlClient.MySqlParameter("@neto", ficha.Doc.neto);
                        var p51 = new MySql.Data.MySqlClient.MySqlParameter("@docCodigo", ficha.Doc.TipoDoc);
                        var p52 = new MySql.Data.MySqlClient.MySqlParameter("@nota", ficha.Doc.nota);
                        var p53 = new MySql.Data.MySqlClient.MySqlParameter("@serieDoc", ficha.serieDocDesc);
                        var p54 = new MySql.Data.MySqlClient.MySqlParameter("@autoDocCxC", autoCxC);
                        var p55 = new MySql.Data.MySqlClient.MySqlParameter("@aplica", ficha.Doc.docRemision);
                        var r = cnn.Database.ExecuteSqlCommand(_sql,
                                                                p01, p02, p03, p04, p05, p06, p07, p08, p09, p10,
                                                                p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                                                p21, p22, p23, p24, p25, p26, p27, p28, p29, p30,
                                                                p31, p32, p33, p34, p35, p36, p37, p38, p39, p40,
                                                                p41, p42, p43, p44, p45, p46, p47, p48, p49, p50,
                                                                p51, p52, p53, p54, p55);
                        if (r == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR DOCUMENTO DE VENTA");
                        }
                        cnn.SaveChanges();
                        //
                        //SALDO DEL CLIENTE
                        //
                        var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.Doc.idCliente);
                        var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.Doc.montoDivisa);
                        var xsql_cli = @"update clientes set 
                                                creditos=creditos+@monto,
                                                saldo=saldo-@monto
                                                where auto=@idCliente";
                        var r_cli = cnn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                        if (r_cli == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR SALDO CLIENTE");
                        }
                        //
                        //DOCUMENTO CXC
                        //
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
                                        importe_neto,
                                        dias,
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
                                        @signo,
                                        @autoVendedor,
                                        0,
                                        0,
                                        0,
                                        @serieDocDesc,
                                        @importeNeto,
                                        0,
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
                        var t01 = new MySql.Data.MySqlClient.MySqlParameter("@autoCxC", autoCxC);
                        var t02 = new MySql.Data.MySqlClient.MySqlParameter("@fechaReg", fechaSistema.Date);
                        var t03 = new MySql.Data.MySqlClient.MySqlParameter("@docNumero", documentoNro);
                        var t04 = new MySql.Data.MySqlClient.MySqlParameter("@fechaVence", fechaSistema.Date);
                        var t05 = new MySql.Data.MySqlClient.MySqlParameter("@importe", ficha.Doc.Total);
                        var t06 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.Doc.idCliente);
                        var t07 = new MySql.Data.MySqlClient.MySqlParameter("@nombreCliente", ficha.Doc.RazonSocial);
                        var t08 = new MySql.Data.MySqlClient.MySqlParameter("@cirifCliente", ficha.Doc.CiRif);
                        var t09 = new MySql.Data.MySqlClient.MySqlParameter("@codigoCliente", ficha.Doc.codCliente);
                        var t10 = new MySql.Data.MySqlClient.MySqlParameter("@resta", ficha.Doc.Total);
                        var t11 = new MySql.Data.MySqlClient.MySqlParameter("@autoDoc", autoVenta);
                        var t12 = new MySql.Data.MySqlClient.MySqlParameter("@autoVendedor", ficha.Doc.idVendedor);
                        var t13 = new MySql.Data.MySqlClient.MySqlParameter("@serieDocDesc", ficha.serieDocDesc);
                        var t14 = new MySql.Data.MySqlClient.MySqlParameter("@importeNeto", ficha.Doc.subTotal);
                        var t15 = new MySql.Data.MySqlClient.MySqlParameter("@montoDivisa", ficha.Doc.montoDivisa);
                        var t16 = new MySql.Data.MySqlClient.MySqlParameter("@tasaDivisa", ficha.Doc.factorCambio);
                        var t17 = new MySql.Data.MySqlClient.MySqlParameter("@codigoSuc", ficha.Doc.codSucursal);
                        var t18 = new MySql.Data.MySqlClient.MySqlParameter("@restaDivisa", ficha.Doc.montoDivisa);
                        var t19 = new MySql.Data.MySqlClient.MySqlParameter("@importeNetoDivisa", ficha.Doc.subTotalMonDivisa);
                        var t20 = new MySql.Data.MySqlClient.MySqlParameter("@estatusDocCxc", "0");
                        var t21 = new MySql.Data.MySqlClient.MySqlParameter("@tipoDocSiglas", ficha.Doc.docSiglas);
                        var t22 = new MySql.Data.MySqlClient.MySqlParameter("@signo", ficha.Doc.signo);
                        r = cnn.Database.ExecuteSqlCommand(_sql, t01, t02, t03, t04, t05, t06, t07, t08, t09, t10,
                                                                 t11, t12, t13, t14, t15, t16, t17, t18, t19, t20,
                                                                 t21, t22);
                        if (r == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR DOCUMENTO CXC");
                        }
                        cnn.SaveChanges();
                        rt.Entidad = autoVenta;
                        //
                        ts.Complete();
                    }
                };
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                rt.Mensaje = Helpers.MYSQL_VerificaError(ex);
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                rt.Mensaje = Helpers.ENTITY_VerificaError(ex);
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
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