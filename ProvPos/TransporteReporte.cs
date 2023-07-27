﻿using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoResumen> 
            TransporteReporte_AliadoResumen()
        {
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoResumen>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"select 
                                    codigo as codigo,
                                    ciRif as ciRif,
                                    nombreRazonSocial as aliado,
                                    monto_debitos_mon_divisa as montoDebitoMonDivisa,
                                    monto_creditos_mon_divisa as montoCreditoMonDivisa,
                                    monto_anticipos_mon_divisa as montoAnticiposMonDivisa,
                                    monto_debitos_anulado_mon_divisa as montoDebitoAnuladoMonDivisa,
                                    monto_creditos_anulado_mon_divisa as montoCreditoAnuladoMonDivisa,
                                    monto_anticipos_anulado_mon_divisa as montoAnticiposAnuladoMonDivisa
                                from transp_aliado";
                    var _sql_2 = @"";
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.AliadoResumen>(_sql).ToList();
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
        public DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleDoc> 
            TransporteReporte_AliadoDetalleDoc()
        {
            var sql = @"SELECT 
                        aliado.cirif as rifAliado, 
                        aliado.nombreRazonSocial as nombreAliado,
                        aliado.codigo as codigoAliado,
                        cli.razon_social as nombreCliente, 
                        cli.ci_rif as rifCliente,
                        aliadoDoc.doc_numero as numDoc,
                        aliadoDoc.doc_fecha as fechaDoc,
                        aliadoDoc.doc_nombre as nombreDoc,
                        aliadoDoc.importe_divisa as importe,
                        aliadoDoc.acumulado_divisa as acumulado
                    FROM transp_aliado_doc as aliadoDoc
                    join transp_aliado as aliado on aliado.id=aliadoDoc.id_aliado
                    join clientes as cli on aliadoDoc.id_cliente=cli.auto
                    where aliadoDoc.estatus_anulado<>'1'";
            var result = new DtoLib.ResultadoLista<DtoTransporte.Reporte.AliadoDetalleDoc>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = sql;
                    var _sql_2 = @"";
                    var _sql = _sql_1 + _sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoTransporte.Reporte.AliadoDetalleDoc>(_sql).ToList();
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