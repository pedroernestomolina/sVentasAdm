using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{
    public partial class DataPrv : IData
    {
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.GetAliados.Info.Ficha> 
            TransporteDocumento_Get_Documento_Aliados_ByIdDoc(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.GetAliados.Info.Ficha>();
            //
            var r01 = MyData.TransporteDocumento_Get_Documento_Aliados_ByIdDoc(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.GetAliados.Info.Item>();
            if (r01.Entidad != null) 
            {
                if (r01.Entidad.Items != null) 
                {
                    if (r01.Entidad.Items.Count > 0) 
                    {
                        lst = r01.Entidad.Items.Select(s =>
                        {
                            var nr = new OOB.Transporte.Documento.GetAliados.Info.Item()
                            {
                                aliadoId = s.aliadoId,
                                alidoCiRif = s.alidoCiRif,
                                alidoMontoDiv = s.alidoMontoDiv,
                                alidoNombre = s.alidoNombre,
                                docCodigoTipo = s.docCodigoTipo,
                                docFecha = s.docFecha,
                                docId = s.docId,
                                docMontoDiv = s.docMontoDiv,
                                docNombre = s.docNombre,
                                docNumero = s.docNumero,
                                entidadCiRif = s.entidadCiRif,
                                entidadId = s.entidadId,
                                entidadNombre = s.entidadNombre,
                                servCodigo = s.servCodigo,
                                servDescripcion = s.servDescripcion,
                                servDetalle = s.servDetalle,
                                servImporteDiv = s.servImporteDiv,
                            };
                            return nr;
                        }).ToList();
                    };
                }
            }
            result.Entidad = new OOB.Transporte.Documento.GetAliados.Info.Ficha()
            {
                Items = lst,
            };
            //
            return result;
        }
    }
}