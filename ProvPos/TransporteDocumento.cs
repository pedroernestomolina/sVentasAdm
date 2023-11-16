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
                                        igtf_monto_mon_act as igtfMontoMonAct
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
                    result.Entidad = new DtoTransporte.Documento.Entidad.Venta.Ficha()
                    {
                        encabezado = _ent,
                        detalles = _det,
                        turnos= _lstTurno
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
    }
}