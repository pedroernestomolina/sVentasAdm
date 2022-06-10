using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov 
{
    
    public partial class DataPrv: IData
    {


        public OOB.Resultado.FichaEntidad<OOB.Sucursal.Entidad.Ficha> 
            Sucursal_GetFichaById(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Sucursal.Entidad.Ficha>();

            var r01 = MyData.Sucursal_GetFichaById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var ent = r01.Entidad;
            var nr = new OOB.Sucursal.Entidad.Ficha()
            {
                auto = ent.id,
                autoDepositoPrincipal = "",
                autoEmpresaGrupo = "",
                codigoDepositoPrincipal = "",
                nombreDepositoPrincipal = "",
                nombreEmpresaGrupo = ent.nombreGrupo,
                codigo = ent.codigo,
                nombre = ent.nombre,
            };
            result.Entidad = nr;

            return result;
        }
        public OOB.Resultado.Lista<OOB.Sucursal.Entidad.Ficha> 
            Sucursal_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Sucursal.Entidad.Ficha>();

            var filtroDTO = new DtoLibPos.Sucursal.Lista.Filtro();
            var r01 = MyData.Sucursal_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Sucursal.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sucursal.Entidad.Ficha()
                        {
                            auto = s.id,
                            codigo = s.codigo,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.FichaEntidad<string> 
            Sucursal_GetId_ByCodigo(string codigoSuc)
        {
            var result = new OOB.Resultado.FichaEntidad<string>();

            var r01 = MyData.Sucursal_GetId_ByCodigo(codigoSuc);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Entidad = r01.Entidad;

            return result;
        }


    }

}