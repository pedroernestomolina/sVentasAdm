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
            TransporteAliado_Agregar(DtoTransporte.Aliado.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();
            try
            {
                using (var ctx = new PosEntities(ProvPos.Provider._cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codigo",ficha.codigo);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@ciRif", ficha.ciRif);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@nombreRazonSocial", ficha.nombreRazonSocial);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@dirFiscal", ficha.dirFiscal);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@contacto", ficha.personaContacto);
                        var _sql = @"INSERT INTO transp_aliado (
                                        `codigo`, 
                                        `ciRif`, 
                                        `nombreRazonSocial`, 
                                        `dirFiscal`, 
                                        `personaContacto`, 
                                        `estatus`) 
                                    VALUES (
                                        @codigo,
                                        @cirif,
                                        @nombreRazonSocial,
                                        @dirFiscal,
                                        @contacto,
                                        '0')";
                        var r = ctx.Database.ExecuteSqlCommand(_sql, p1, p2, p3, p4, p5);
                        if (r == 0) 
                        {
                            throw new Exception("PROBLEMA AL REGISTRAR ALIADO");
                        }
                        ctx.SaveChanges();

                        _sql = "SELECT LAST_INSERT_ID()";
                        var idEnt = ctx.Database.SqlQuery<int>(_sql).FirstOrDefault();

                        foreach (var rg in ficha.telefonos) 
                        {
                            p1.ParameterName = "@idAliado";
                            p1.Value= idEnt;
                            p2.ParameterName = "@numero";
                            p2.Value = rg.numero;
                            _sql = @"INSERT INTO transp_aliado_telefono (
                                        `idAliado`, 
                                        `numero`) 
                                    VALUES (
                                        @idAliado,
                                        @numero)";
                            r = ctx.Database.ExecuteSqlCommand(_sql, p1, p2);
                            if (r == 0)
                            {
                                throw new Exception("PROBLEMA AL REGISTRAR TELEFONO");
                            }
                            ctx.SaveChanges();
                        }
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
        public DtoLib.ResultadoEntidad<DtoTransporte.Aliado.Entidad.Ficha> 
            TransporteAliado_GetById(int idAliado)
        {
            var result = new DtoLib.ResultadoEntidad<DtoTransporte.Aliado.Entidad.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idAliado);
                    var _sql = @"select 
                                    id,
                                    codigo,
                                    ciRif,
                                    nombreRazonSocial,
                                    dirFiscal,
                                    personaContacto
                                from transp_aliado  
                                where id=@id";
                    var r1 = cnn.Database.SqlQuery<DtoTransporte.Aliado.Entidad.Ficha>(_sql, p1).FirstOrDefault();
                    if (r1 == null) 
                    {
                        throw new Exception("ALIADO NO ENCONTRADO");
                    }

                    p1.ParameterName = "@idAliado";
                    p1.Value = idAliado;
                    _sql = @"select numero from transp_aliado_telefono where idAliado=@idAliado";
                    var r2 = cnn.Database.SqlQuery<DtoTransporte.Aliado.Entidad.Telefono>(_sql, p1).ToList();
                    r1.telefonos = r2;
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
        public DtoLib.ResultadoLista<DtoTransporte.Aliado.Entidad.Ficha> 
            TransporteAliado_GetLista(DtoTransporte.Aliado.Busqueda.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Aliado.Entidad.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql_1 = @"select 
                                    id,
                                    codigo,
                                    ciRif,
                                    nombreRazonSocial
                                from transp_aliado ";
                    var _sql_2 = @" where 1=1 ";
                    if (filtro.cadena != "")
                    {
                        var valor = "";
                        if (filtro.metodoBusqueda == DtoTransporte.Aliado.Tipos.MetodoBusqueda.PorCodigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                _sql_2 += " and codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                _sql_2 += " and codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.metodoBusqueda == DtoTransporte.Aliado.Tipos.MetodoBusqueda.PorDescripcion)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                _sql_2 += " and nombreRazonSocial like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                _sql_2 += " and nombreRazonSocial like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.metodoBusqueda == DtoTransporte.Aliado.Tipos.MetodoBusqueda.PorCiRif)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                _sql_2 += " and ciRif like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                _sql_2 += " and ciRif like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;
                    }
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Aliado.Entidad.Ficha>(_sql, p1).ToList();
                    result.Lista  = _lst;
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