using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoTransporte.Documento.GetAliados.Info.Ficha> 
            TransporteDocumento_Get_Documento_Aliados_ByIdDoc(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Documento.GetAliados.Info.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", id);
                    var _sql_1 = @"SELECT 
                                        vent.auto as docId,
	                                    aliadoDoc.doc_numero as docNumero,
                                        aliadoDoc.doc_fecha as docFecha,
                                        aliadoDoc.doc_nombre as docNombre,
                                        aliadoDoc.doc_codigo as docCodigoTipo,
                                        vent.monto_divisa as docMontoDiv,
                                        vent.auto_cliente as entidadId,
                                        vent.razon_social as entidadNombre,
                                        vent.ci_rif as entidadCiRif,
                                        aliado.id as aliadoId,
                                        aliado.nombreRazonSocial as aliadoNombre,
                                        aliado.ciRif as aliadoCiRif,
                                        aliadoDoc.importe_divisa as aliadoMontoDiv,
                                        aliadoServ.codigo_serv as servCodigo,
                                        aliadoServ.desc_serv as servDescripcion,
                                        aliadoServ.detalle_serv as servDetalle,
                                        aliadoServ.importe_serv_div as servImporteDiv
                                    FROM transp_aliado_doc as aliadoDoc
                                    join transp_aliado as aliado on aliado.id=aliadoDoc.id_aliado
                                    join ventas as vent on vent.auto=aliadoDoc.id_doc_ref
                                    join transp_aliado_doc_servicio as aliadoServ on aliadoServ.id_aliado_doc=aliadoDoc.id
                                    WHERE id_doc_ref=@idDoc";
                    var _sql = _sql_1;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Documento.GetAliados.Info.Item>(_sql, p1).ToList();
                    result.Entidad = new DtoTransporte.Documento.GetAliados.Info.Ficha()
                    {
                        Items = _lst,
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
    }
}