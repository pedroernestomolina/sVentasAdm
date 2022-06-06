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

        public DtoLib.ResultadoLista<DtoLibPos.Sucursal.Lista.Ficha> Sucursal_GetLista(DtoLibPos.Sucursal.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Sucursal.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = " select auto as id, codigo, nombre ";
                    var sql_2 = " from empresa_sucursal ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Sucursal.Lista.Ficha>(sql).ToList();
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Sucursal.Entidad.Ficha> Sucursal_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sucursal.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.empresa_sucursal.Find(id);
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] SUCURSAL NO ENCONTRADO";
                        return result;
                    }

                    var _autoGrupo=ent.autoEmpresaGrupo;
                    var _nGrupo="";
                    var _pManejar="";
                    var entGrupo= cnn.empresa_grupo.Find(_autoGrupo);
                    if (entGrupo!=null)
                    {
                        _nGrupo=entGrupo.nombre;
                        _pManejar=entGrupo.idPrecio;
                    }

                    var nr = new DtoLibPos.Sucursal.Entidad.Ficha()
                    {
                        id = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        nombreGrupo=_nGrupo,
                        precioManejar=_pManejar,
                        estatusVentaMayor=ent.estatus_facturar_mayor
                    };
                    result.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Sucursal.Entidad.Ficha> Sucursal_GetFicha_ByCodigo(string codigo)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sucursal.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.empresa_sucursal.FirstOrDefault(f => f.codigo.Trim().ToUpper() == codigo.Trim().ToUpper());
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ CODIGO ] SUCURSAL NO ENCONTRADO";
                        return result;
                    }

                    var _autoGrupo = ent.autoEmpresaGrupo;
                    var _nGrupo = "";
                    var _pManejar = "";
                    var entGrupo = cnn.empresa_grupo.Find(_autoGrupo);
                    if (entGrupo != null)
                    {
                        _nGrupo = entGrupo.nombre;
                        _pManejar = entGrupo.idPrecio;
                    }

                    var nr = new DtoLibPos.Sucursal.Entidad.Ficha()
                    {
                        id = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        nombreGrupo = _nGrupo,
                        precioManejar = _pManejar,
                        estatusVentaMayor = ent.estatus_facturar_mayor
                    };
                    result.Entidad = nr;
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