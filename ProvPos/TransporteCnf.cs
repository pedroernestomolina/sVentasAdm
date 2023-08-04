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
        public DtoLib.ResultadoEntidad<string> 
            TransporteCnf_NotasPresupuesto_Get()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"select 
                                    usuario
                                from sistema_configuracion
                                where codigo='GLOBAL64'";
                    var r1 = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (r1 == null)
                    {
                        throw new Exception("[ ID ] CONFIGURACION NO ENCONTRADO");
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
        public DtoLib.Resultado 
            TransporteCnf_NotasPresupuesto_Editar(string notas)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@notas", notas);
                    var _sql = @"update sistema_configuracion
                                    set usuario=@notas
                                where codigo='GLOBAL64'";
                    var r1 = cnn.Database.ExecuteSqlCommand(_sql,p1);
                    if (r1 == 0)
                    {
                        throw new Exception("[ ID ] CONFIGURACION NO ENCONTRADO");
                    }
                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            TransporteCnf_NotasFactura_Get()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"select 
                                    usuario
                                from sistema_configuracion
                                where codigo='GLOBAL65'";
                    var r1 = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (r1 == null)
                    {
                        throw new Exception("[ ID ] CONFIGURACION NO ENCONTRADO");
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
        public DtoLib.Resultado 
            TransporteCnf_NotasFactura_Editar(string notas)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@notas", notas);
                    var _sql = @"update sistema_configuracion
                                    set usuario=@notas
                                where codigo='GLOBAL65'";
                    var r1 = cnn.Database.ExecuteSqlCommand(_sql, p1);
                    if (r1 == 0)
                    {
                        throw new Exception("[ ID ] CONFIGURACION NO ENCONTRADO");
                    }
                    cnn.SaveChanges();
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