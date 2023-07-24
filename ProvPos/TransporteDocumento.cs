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
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Presupuesto.Ficha> 
            TransporteDocumento_EntidadPresupuesto_GetById(string idDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Presupuesto.Ficha>(); 
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc",idDoc);
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
                                importe 
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
                    var _sql_2 = @" where 1=1 and estatus_anulado!='1' and auto_remision='' ";
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