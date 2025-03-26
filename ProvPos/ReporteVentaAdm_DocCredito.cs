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
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.DocCredito.Ficha> 
            ReportesAdm_Ventas_DocCredito(DtoLibPos.Reportes.VentaAdministrativa.DocCredito.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.DocCredito.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@desde", filtro.desde);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@hasta", filtro.hasta);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"SELECT 
                                    v.auto as idDoc,
                                    v.documento as numeroDoc,
                                    v.fecha as fechaEmiDoc,
                                    v.documento_nombre as moduloDoc, 
                                    v.documento_tipo as nombreDoc,
                                    v.tipo as codigoDoc, 
                                    v.ci_rif as ciRifEntidad,
                                    v.razon_social as razonSocialEntidad,
                                    v.dir_fiscal as dirFiscalEntidad,
                                    v.monto_divisa as montoDivisa,
                                    v.factor_cambio as tasaCambio,
                                    v.codigo_sucursal as codigoSuc";
                    var sql_2 = @" from ventas as v ";
                    var sql_3 = @" where v.fecha>=@desde and 
                                        v.fecha <= @hasta and 
                                        v.estatus_anulado='0' and 
                                        v.estatus_credito='1' ";
                    var sql_4 = @"";
                    if (filtro.codSucursal != "")
                    {
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                        sql_3 += " and v.codigo_sucursal=@suc ";
                    }
                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.DocCredito.Ficha>(sql, p1, p2, p3).ToList();
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