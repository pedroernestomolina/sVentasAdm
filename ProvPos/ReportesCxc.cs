using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorDoc.Ficha> 
            ReportesCxc_DetalleCobranza_PorDocumentos(DtoLibPos.Reportes.Cxc.DetallePorDoc.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorDoc.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@desde", filtro.desde);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@hasta", filtro.hasta);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select 
                                    doc.documento as numeroDoc, 
                                    doc.tipo_documento as tipoDoc,
                                    doc.numero_recibo as numeroRecibo,
                                    doc.importe_divisa as importeDivisa,
                                    doc.codigo_sucursal as codigoSuc,
                                    doc.notas as notasDoc,
                                    rec.fecha as fechaRecibo,
                                    rec.cliente as nombreEntidad,
                                    rec.ci_rif as ciRifEntidad,
                                    rec.monto_recibido_divisa as importeRecibo,
                                    rec.anticipos as montoPorAnticiposUsado,
                                    rec.anticipo_cargar as montoPorAnticiposPorCargar,
                                    rec.monto_ntcredito as montoPorNtCreditoUsado,
                                    cta.fecha as fechaDoc,
                                    cta.fecha_vencimiento asfechaVencDoc";
                    var sql_2 = @" from cxc_documentos as doc
                                        join cxc_recibos as rec on rec.auto=doc.auto_cxc_recibo
                                        join cxc as cta on cta.auto=doc.auto_cxc ";
                    var sql_3 = @" where rec.estatus_anulado='0' and 
                                        rec.fecha>=@desde AND
                                        rec.fecha<=@hasta AND
                                        rec.estatus_doc_cxc='1' ";
                    var sql_4 = @"";
                    if (filtro.codSucursal != "")
                    {
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                        sql_3 += " and doc.codigo_sucursal=@suc ";
                    }
                    if (filtro.idCliente != "")
                    {
                        p4.ParameterName = "@idCliente";
                        p4.Value = filtro.idCliente;
                        sql_3 += " and rec.auto_cliente=@idCliente ";
                    }
                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.Cxc.DetallePorDoc.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = lst;
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

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.Resumen.Ficha> 
            ReportesCxc_ResumenCobranza(DtoLibPos.Reportes.Cxc.Resumen.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.Resumen.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@desde", filtro.desde);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@hasta", filtro.hasta);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select 
                                    documento as numeroRec,
                                    fecha as fechaRec,
                                    ci_rif as ciRifEntidad,
                                    cliente as nombreEntidad,
                                    nota as notasRec,
                                    importe_divisa as importeDivisa,
                                    codigo_sucursal as codigoSuc,
                                    anticipo_cargar as montoPorAnticipoCargado,
                                    anticipos as montoPorAnticipoUsado,
                                    monto_ntcredito as montoPorNtCreditoUsado ";
                    var sql_2 = @" from cxc_recibos ";
                    var sql_3 = @" where estatus_anulado='0' and
                                        estatus_doc_cxc='1' and 
                                        fecha>=@desde and 
                                        fecha<=@hasta ";
                    var sql_4 = @"";
                    if (filtro.codSucursal != "")
                    {
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                        sql_3 += " and codigo_sucursal=@suc ";
                    }
                    if (filtro.idCliente != "")
                    {
                        p4.ParameterName = "@idCliente";
                        p4.Value = filtro.idCliente;
                        sql_3 += " and auto_cliente=@idCliente ";
                    }
                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.Cxc.Resumen.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = lst;
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

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Ficha>
            ReportesCxc_DetalleCobranza_PorMedioPago(DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@desde", filtro.desde);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@hasta", filtro.hasta);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select 
                                    rec.documento as numeroRec,
                                    rec.fecha as fechaRec,
                                    rec.ci_rif as ciRifEntidad,
                                    rec.cliente as nombreEntidad,
                                    rec.nota as notasRec,
                                    rec.importe_divisa as importeDivisa,
                                    rec.anticipo_cargar as montoPorAnticipoCargado,
                                    rec.codigo_sucursal as codigoSuc,
                                    rec.anticipos as montoPorAnticipoUsado,
                                    rec.monto_ntcredito as montoPorNtCreditoUsado,
                                    medio.medio as nombreMedio,
                                    medio.codigo as codigoMedio,
                                    medio.monto_recibido as montoRecibido,
                                    medio.opBanco,
                                    medio.opNroCta,
                                    medio.opNroRef,
                                    medio.opFecha,
                                    medio.opDetalle,
                                    medio.opMonto,
                                    medio.opTasa,
                                    medio.opAplicaConversion ";
                    var sql_2 = @" from cxc_medio_pago as medio
                                        join cxc_recibos as rec on rec.auto=medio.auto_recibo ";
                    var sql_3 = @" where rec.estatus_anulado='0' AND
                                        rec.estatus_doc_cxc='1' AND
                                        rec.fecha>=@desde AND
                                        rec.fecha<=@hasta ";
                    var sql_4 = @"";
                    if (filtro.codSucursal != "")
                    {
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                        sql_3 += " and rec.codigo_sucursal=@suc ";
                    }
                    if (filtro.idCliente != "")
                    {
                        p4.ParameterName = "@idCliente";
                        p4.Value = filtro.idCliente;
                        sql_3 += " and rec.auto_cliente=@idCliente ";
                    }
                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.Cxc.DetallePorMedioPago.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = lst;
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