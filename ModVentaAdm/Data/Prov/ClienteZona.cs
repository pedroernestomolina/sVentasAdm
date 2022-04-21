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

        public OOB.Resultado.Lista<OOB.Maestro.Zona.Entidad.Ficha> ClienteZona_GetLista(OOB.Maestro.Zona.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Maestro.Zona.Entidad.Ficha>();

            var filtroDTO = new DtoLibPos.ClienteZona.Lista.Filtro()
            {
            };
            var r01 = MyData.ClienteZona_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Maestro.Zona.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Maestro.Zona.Entidad.Ficha()
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

        public OOB.Resultado.FichaEntidad<OOB.Maestro.Zona.Entidad.Ficha> ClienteZona_GetFichaById(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Maestro.Zona.Entidad.Ficha>();

            var r01 = MyData.ClienteZona_GetFichaById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var s = r01.Entidad;
            var nr = new OOB.Maestro.Zona.Entidad.Ficha()
            {
                auto = s.auto,
                codigo = s.codigo,
                nombre = s.nombre,
            };
            result.Entidad = nr;

            return result;
        }

        public OOB.Resultado.FichaAuto ClienteZona_Agregar(OOB.Maestro.Zona.Agregar.Ficha ficha)
        {
            var rt = new OOB.Resultado.FichaAuto();

            var fichaDTO = new DtoLibPos.ClienteZona.Agregar.Ficha()
            {
                nombre = ficha.nombre,
                codigo = ficha.codigo,
            };
            var r01 = MyData.ClienteZona_Agregar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            rt.Auto = r01.Auto;

            return rt;
        }

        public OOB.Resultado.Ficha ClienteZona_Editar(OOB.Maestro.Zona.Editar.Ficha ficha)
        {
            var rt = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.ClienteZona.Editar.Ficha()
            {
                auto = ficha.auto,
                nombre = ficha.nombre,
                codigo = ficha.codigo,
            };
            var r01 = MyData.ClienteZona_Editar(fichaDTO);
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