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

        public DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Ficha> 
            CxC_Tool_CtasPendiente_GetLista(DtoLibPos.CxC.Tools.CtasPendiente.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"SELECT
                                        cl.auto as idCliente,
                                        cl.ci_rif as ciRif, 
                                        cl.razon_social as nombreRazonSocial, 
                                        sum(c.monto_divisa*c.signo) as importe, 
                                        sum(c.acumulado_divisa*c.signo) as acumulado, 
                                        count(*) as cntDocPend, 
                                        cl.doc_pendientes as limiteFacPend, 
                                        cl.limite_credito as limiteMontoCredito,
                                        (SELECT count(*)  
                                            FROM cxc 
                                            where auto_cliente=cl.auto 
                                            and estatus_cancelado='0'
                                            and tipo_documento='FAC'
                                            and estatus_anulado='0'
                                            GROUP BY  c.auto_cliente
                                        ) as cntFactPend
                                        FROM cxc as c
                                        join clientes as cl on c.auto_cliente=cl.auto
                                        where c.estatus_cancelado='0'
                                        and c.tipo_documento<>'PAG'
                                        and c.estatus_anulado='0'
                                        group by c.auto_cliente";
                    var sql = sql_1;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CxC.Tools.CtasPendiente.Ficha>(sql,p1).ToList();
                    result.Lista = lst;
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