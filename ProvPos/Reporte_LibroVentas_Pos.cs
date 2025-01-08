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
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha> 
            Reporte_LibroVentas_Pos(DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"SELECT 
                                        v.codigo_sucursal as codigoSucursalDoc, 
                                        v.fecha as fechaDoc, 
                                        v.ci_rif as cirifDoc, 
                                        v.razon_social as nombreRazonSocialDoc, 
                                        v.documento as numDoc, 
                                        v.control as numControlDoc, 
                                        v.tipo as codigoDoc, 
                                        v.aplica as numAplicaDoc, 
                                        v.total-v.monto_igtf as montoTotal, 
                                        v.exento as montoExento,
                                        v.base1 as montoBase1, 
                                        v.impuesto1 as montoImpuesto1, 
                                        v.base2 as montoBase2, 
                                        v.impuesto2 as montoImpuesto2, 
                                        v.tasa1 as tasaIva1, 
                                        v.tasa2 as tasaIva2, 
                                        v.retencion_iva as montoRetencionIva, 
                                        v.signo as signoDoc, 
                                        v.tasa_retencion_iva as tasaRetencionIva, 
                                        v.fecha_retencion as fechaRetencionIva,
                                        v.comprobante_retencion as comprobanteRetencionIva, 
                                        v.auto,
                                        v.estatus_anulado as estatus
                                    FROM ventas as v ";
                    var sql_2 = @" WHERE 1=1 and 
                                        v.fecha>=@desde and v.fecha<=@hasta
                                        and tipo in ('01','02','03') and v.estatus_mostrar_libro_venta='1' ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;
                    var sql = sql_1 + sql_2;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha>(sql, p1, p2).ToList();
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
