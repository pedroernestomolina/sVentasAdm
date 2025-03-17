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
        public DtoLib.ResultadoEntidad<DtoLibPos.CxC.CapturarData.Cliente.Ficha> 
            CxC_CapturarData_Cliente_ById(string id)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.CxC.CapturarData.Cliente.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", id);
                    var sql_1 = @"SELECT 
                                    client.auto as idClient,
                                    client.ci_rif as ciRifClient,
                                    client.codigo as codigoClient,
                                    client.nombre as aliasClient,
                                    client.razon_social as nombreRazonSocialClient,
                                    client.dir_fiscal as dirFiscalClient,
                                    client.telefono as telefonoClient,
                                    client.telefono2 as telefono2Client,
                                    client.email as emailClient,
                                    client.auto_vendedor as idVendedor,
                                    client.auto_cobrador as idCobrador,
                                    client.estatus_credito as estatusCreditoClient,
                                    client.limite_credito as limiteCreditoClient,
                                    client.dias_credito as diasCreditoClient,
                                    client.fecha_ult_venta as fechaUltVentaClient,
                                    client.fecha_ult_pago as fechaUltPagoClient,
                                    client.anticipos as montoAnticiposClient,
                                    client.estatus as estatusClient,
                                    vend.nombre as nombreVend,
                                    vend.codigo as codigoVend,
                                    cob.nombre as nombreCobrad,
                                    cob.codigo as codigoCobrad,
                                    (select 
                                        sum(resta_divisa) 
                                     from cxc 
                                     where auto_cliente=@idCliente and 
                                         resta_divisa>0 and 
                                         tipo_documento='NCR' and  
                                         estatus_anulado='0') as montoNtCreditoDisponible
                                FROM clientes as client
                                join vendedores as vend on vend.auto=client.auto_vendedor
                                join empresa_cobradores as cob on cob.auto=client.auto_cobrador
                                WHERE client.auto=@idCliente";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<DtoLibPos.CxC.CapturarData.Cliente.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null) 
                    {
                        throw new Exception("CLIENTE NO ENCONTRADO");
                    }
                    rt.Entidad = ent;
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