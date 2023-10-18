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
        public DtoLib.ResultadoLista<DtoTransporte.Caja.Lista.Ficha> 
            Transporte_Caja_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Caja.Lista.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"select 
                                        id as id,
                                        codigo,
                                        descripcion,
                                        saldo_inicial as saldoInicial,
                                        monto_ingreso-monto_ingreso_anulado as montoPorIngresos,
                                        monto_egreso-monto_egreso_anulado as montoPorEgresos,
                                        0 as montoPorAnulaciones,
                                        estatus_anulado as estatusAnulado,
                                        es_divisa as esDivisa
                                    FROM transp_caja";
                    var _sql = _sql_1;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Caja.Lista.Ficha>(_sql).ToList();
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