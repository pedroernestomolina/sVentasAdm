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
        public OOB.Resultado.FichaId 
            TransporteServPrest_Agregar(OOB.Transporte.ServPrest.Agregar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaId();
            var fichaDTO = new DtoTransporte.ServPrest.Agregar.Ficha
            {
                codigo = ficha.codigo,
                descripcion = ficha.descripcion,
                detalle = ficha.detalle,
            };
            var r01 = MyData.TransporteServPrest_Agregar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Id = r01.Id;
            return result;
        }
        public OOB.Resultado.Ficha 
            TransporteServPrest_Editar(OOB.Transporte.ServPrest.Editar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();
            var fichaDTO = new DtoTransporte.ServPrest.Editar.Ficha
            {
                idFicha = ficha.idFicha,
                codigo = ficha.codigo,
                descripcion = ficha.descripcion,
                detalle = ficha.detalle,
            };
            var r01 = MyData.TransporteServPrest_Editar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Transporte.ServPrest.Entidad.Ficha> 
            TransporteServPrest_GetById(int idFicha)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.ServPrest.Entidad.Ficha>();
            var r01 = MyData.TransporteServPrest_GetById(idFicha);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _ent = r01.Entidad;
            result.Entidad = new OOB.Transporte.ServPrest.Entidad.Ficha()
            {
                id = _ent.id,
                codigo = _ent.codigo,
                descripcion = _ent.descripcion,
                detalle = _ent.detalle,
            };
            return result;
        }
        public OOB.Resultado.Lista<OOB.Transporte.ServPrest.Entidad.Ficha> 
            TransporteServPrest_GetLista(OOB.Transporte.ServPrest.Busqueda.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.ServPrest.Entidad.Ficha>();
            var filtroDTO = new DtoTransporte.ServPrest.Busqueda.Filtro()
            {
            };
            var r01 = MyData.TransporteServPrest_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.ServPrest.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.ServPrest.Entidad.Ficha()
                        {
                            id = s.id,
                            codigo = s.codigo,
                            descripcion = s.descripcion,
                            detalle = s.detalle,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = _lst;
            return result;
        }
    }
}