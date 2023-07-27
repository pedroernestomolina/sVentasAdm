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
            TransporteAliado_Agregar(OOB.Transporte.Aliado.Agregar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaId();
            var fichaDTO = new DtoTransporte.Aliado.Agregar.Ficha
            {
                ciRif = ficha.ciRif,
                codigo = ficha.codigo,
                dirFiscal = ficha.dirFiscal,
                nombreRazonSocial = ficha.nombreRazonSocial,
                personaContacto = ficha.personaContacto,
                telefonos = ficha.telefonos.Select(s=> 
                {
                    var nr = new DtoTransporte.Aliado.Agregar.Telefono()
                    {
                        numero = s.numero,
                    };
                    return nr;
                }).ToList(),
            };
            var r01 = MyData.TransporteAliado_Agregar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Id = r01.Id;
            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Aliado.Entidad.Ficha> 
            TransporteAliado_GetById(int idAliado)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Aliado.Entidad.Ficha>();
            var r01 = MyData.TransporteAliado_GetById(idAliado);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _ent= r01.Entidad;
            result.Entidad = new OOB.Transporte.Aliado.Entidad.Ficha()
            {
                id = _ent.id,
                ciRif = _ent.ciRif,
                codigo = _ent.codigo,
                dirFiscal = _ent.dirFiscal,
                nombreRazonSocial = _ent.nombreRazonSocial,
                personaContacto = _ent.personaContacto,
                telefonos = _ent.telefonos.Select(s =>
                {
                    var nr = new OOB.Transporte.Aliado.Entidad.Telefono()
                    {
                        numero = s.numero,
                    };
                    return nr;
                }).ToList(),
            };
            return result;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Aliado.Entidad.Ficha> 
            TransporteAliado_GetLista(OOB.Transporte.Aliado.Busqueda.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Aliado.Entidad.Ficha>();
            var filtroDTO = new DtoTransporte.Aliado.Busqueda.Filtro()
            {
                cadena = filtro.cadena,
                metodoBusqueda = (DtoTransporte.Aliado.Tipos.MetodoBusqueda)filtro.metodoBusqueda,
                tipoAliado = (DtoTransporte.Aliado.Tipos.TipoAliado)filtro.tipoAliado,
            };
            var r01 = MyData.TransporteAliado_GetLista (filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var _lst = new List<OOB.Transporte.Aliado.Entidad.Ficha>();
            if (r01.Lista != null) 
            {
                if (r01.Lista.Count > 0) 
                {
                    _lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Aliado.Entidad.Ficha()
                        {
                            id = s.id,
                            ciRif = s.ciRif,
                            codigo = s.codigo,
                            dirFiscal = "",
                            nombreRazonSocial = s.nombreRazonSocial,
                            personaContacto = "",
                            telefonos = null,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = _lst;
            return result;
        }
        public OOB.Resultado.Ficha 
            TransporteAliado_Editar(OOB.Transporte.Aliado.Editar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();
            var fichaDTO = new DtoTransporte.Aliado.Editar.Ficha
            {
                idAliado = ficha.idAliado,
                ciRif = ficha.ciRif,
                codigo = ficha.codigo,
                dirFiscal = ficha.dirFiscal,
                nombreRazonSocial = ficha.nombreRazonSocial,
                personaContacto = ficha.personaContacto,
                telefonos = ficha.telefonos.Select(s =>
                {
                    var nr = new DtoTransporte.Aliado.Editar.Telefono()
                    {
                        numero = s.numero,
                    };
                    return nr;
                }).ToList(),
            };
            var r01 = MyData.TransporteAliado_Editar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return result;
        }
    }
}