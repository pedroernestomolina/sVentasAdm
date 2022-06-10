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


        public OOB.Resultado.Lista<OOB.Maestro.Grupo.Entidad.Ficha> 
            ClienteGrupo_GetLista(OOB.Maestro.Grupo.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Maestro.Grupo.Entidad.Ficha>();

            var filtroDTO = new DtoLibPos.ClienteGrupo.Lista.Filtro()
            {
            };
            var r01 = MyData.ClienteGrupo_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Maestro.Grupo.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Maestro.Grupo.Entidad.Ficha()
                        {
                            auto = s.auto,
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
        public OOB.Resultado.FichaEntidad<OOB.Maestro.Grupo.Entidad.Ficha> 
            ClienteGrupo_GetFichaById(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Maestro.Grupo.Entidad.Ficha>();

            var r01 = MyData.ClienteGrupo_GetFichaById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var s = r01.Entidad;
            var nr = new OOB.Maestro.Grupo.Entidad.Ficha()
            {
                auto = s.auto,
                codigo = s.codigo,
                nombre = s.nombre,
            };
            result.Entidad = nr;

            return result;
        }
        public OOB.Resultado.FichaAuto 
            ClienteGrupo_Agregar(OOB.Maestro.Grupo.Agregar.Ficha ficha)
        {
            var rt = new OOB.Resultado.FichaAuto();

            var fichaDTO = new DtoLibPos.ClienteGrupo.Agregar.Ficha()
            {
                codigoSucursalRegistro = ficha.codigoSucursalRegistro,
                nombre = ficha.nombre,
                codigo = ficha.codigo,
            };
            var r01 = MyData.ClienteGrupo_Agregar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            rt.Auto = r01.Auto;

            return rt;
        }
        public OOB.Resultado.Ficha 
            ClienteGrupo_Editar(OOB.Maestro.Grupo.Editar.Ficha ficha)
        {
            var rt = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.ClienteGrupo.Editar.Ficha()
            {
                auto = ficha.auto,
                nombre = ficha.nombre,
                codigo = ficha.codigo,
            };
            var r01 = MyData.ClienteGrupo_Editar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            return rt;
        }


    }

}