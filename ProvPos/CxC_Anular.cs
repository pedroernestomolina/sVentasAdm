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
        private class anularCxCAdm 
        {
            public string estatuAnulado { get; set; }
            public string estatusDocGeneradoModuloCxC { get; set; }
            public string tipoDocAdm { get; set; }
            public int signoTipoDocAdm { get; set; }
            public decimal importeDiv { get; set; }
            public decimal montoAcumuladoDiv { get; set; }
            public string idCliente { get; set; }
        }
        public DtoLib.Resultado 
            CxC_AnularCxcAdm(DtoLibPos.CxC.Anular.CxcAdm.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCxc", ficha.idCxcAdm);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        var _sql = @"select 
                                        estatus_anulado as estatuAnulado, 
                                        acumulado_divisa as montoAcumuladoDiv,
                                        estatus_doc_cxc as estatusDocGeneradoModuloCxC,
                                        tipo_documento as tipoDocAdm,
                                        signo as signoTipoDocAdm,
                                        monto_divisa as importeDiv,
                                        auto_cliente as idCliente
                                    from cxc
                                    where auto=@idCxc";
                        var ent = ctx.Database.SqlQuery<anularCxCAdm>(_sql, p1).FirstOrDefault();
                        if (ent == null) 
                        {
                            throw new Exception("DOCUMENTO ADMINISTRATIVO DE CXC NO ENCONTRADO");
                        }
                        if (ent.estatuAnulado.Trim().ToUpper()=="1")
                        {
                            throw new Exception("DOCUMENTO ADMINISTRATIVO DE CXC ANULADO");
                        }
                        if (ent.estatusDocGeneradoModuloCxC.Trim().ToUpper()!="1")
                        {
                            throw new Exception("TIPO DOCUMENTO A ANULAR NO ES UN DOCUMENTO ADMINISTRATIVO DE CXC ");
                        }
                        if (ent.montoAcumuladoDiv>0M)
                        {
                            throw new Exception("DOCUMENTO ADMINISTRATIVO DE CXC POSEE UN ABONO");
                        }
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCxc", ficha.idCxcAdm);
                        _sql = @"update cxc set 
                                        estatus_anulado='1'
                                    where auto=@idCxc";
                        var r1 = ctx.Database.ExecuteSqlCommand(_sql, p1);
                        if (r1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ANULAR DOCUMENTO ADMINISTRATIVO DE CXC");
                        }
                        ctx.SaveChanges();
                        //
                        p1= new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ent.idCliente);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ent.montoAcumuladoDiv);
                        _sql = @"update clientes set 
                                    debitos=debitos-@monto,
                                    saldo=saldo-@monto
                                where auto=@idCliente";
                        if (ent.signoTipoDocAdm == -1)
                        {
                            _sql = @"update clientes set 
                                        creditos=creditos-@monto,
                                        saldo=saldo+@monto
                                    where auto=@idCliente";
                        }
                        var r2 = ctx.Database.ExecuteSqlCommand(_sql, p1, p2);
                        ctx.SaveChanges();
                        //
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
            //
            return result;
        }
    }
}