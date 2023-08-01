using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.ResultadoId 
            TransporteServPrest_Agregar(DtoTransporte.ServPrest.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();
            try
            {
                using (var ctx = new PosEntities(ProvPos.Provider._cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codigo", ficha.codigo);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@descripcion", ficha.descripcion);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@detalle", ficha.detalle);
                        var _sql = @"INSERT INTO transp_servicio (
                                        `codigo`, 
                                        `descripcion`, 
                                        `detalle`) 
                                    VALUES (
                                        @codigo,
                                        @descripcion,
                                        @detalle)";
                        var r = ctx.Database.ExecuteSqlCommand(_sql, p1, p2, p3);
                        if (r == 0)
                        {
                            throw new Exception("PROBLEMA AL REGISTRAR SERVICIO");
                        }
                        ctx.SaveChanges();

                        _sql = "SELECT LAST_INSERT_ID()";
                        var idEnt = ctx.Database.SqlQuery<int>(_sql).FirstOrDefault();

                        ts.Complete();
                        result.Id = idEnt;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.Resultado 
            TransporteServPrest_Editar(DtoTransporte.ServPrest.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var ctx = new PosEntities(ProvPos.Provider._cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p0 = new MySql.Data.MySqlClient.MySqlParameter("@id", ficha.idFicha);
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codigo", ficha.codigo);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@descripcion", ficha.descripcion);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@detalle", ficha.detalle);
                        var _sql = @"update transp_servicio
                                        set codigo=@codigo,
                                            descripcion=@descripcion,
                                            detalle=@detalle 
                                    where id=@id";
                        var r = ctx.Database.ExecuteSqlCommand(_sql, p0, p1, p2, p3);
                        if (r == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR SERVCION");
                        }
                        ctx.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoEntidad<DtoTransporte.ServPrest.Entidad.Ficha> 
            TransporteServPrest_GetById(int idFicha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.ServPrest.Entidad.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idFicha);
                    var _sql = @"select 
                                    id,
                                    codigo,
                                    descripcion,
                                    detalle
                                from transp_servicio
                                where id=@id";
                    var r1 = cnn.Database.SqlQuery<DtoTransporte.ServPrest.Entidad.Ficha>(_sql, p1).FirstOrDefault();
                    if (r1 == null)
                    {
                        throw new Exception("SERVCIO NO ENCONTRADO");
                    }
                    result.Entidad = r1;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoLista<DtoTransporte.ServPrest.Entidad.Ficha> 
            TransporteServPrest_GetLista(DtoTransporte.ServPrest.Busqueda.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.ServPrest.Entidad.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql_1 = @"select 
                                    id,
                                    codigo,
                                    descripcion,
                                    detalle
                                from transp_servicio ";
                    var _sql_2 = @" where 1=1 ";
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.ServPrest.Entidad.Ficha>(_sql, p1).ToList();
                    result.Lista = _lst;
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
