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
        public OOB.Resultado.Ficha 
            TransporteDocumento_AnularPresupuesto(OOB.Transporte.Documento.Anular.Presupuesto.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();
            var fichaDTO = new DtoTransporte.Documento.Anular.Presupuesto.Ficha()
            {
                idDoc = ficha.idDoc,
                auditoria = new DtoTransporte.Documento.Anular.Presupuesto.FichaAuditoria()
                {
                    idSistemaDocumento = ficha.auditoria.idSistemaDocumento,
                    idUsuario = ficha.auditoria.idUsuario,
                    codigo = ficha.auditoria.codigo,
                    estacion = ficha.auditoria.estacion,
                    motivo = ficha.auditoria.motivo,
                    usuario = ficha.auditoria.usuario,
                },
            };
            var r01 = MyData.TransporteDocumento_AnularPresupuesto(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return result;
        }
        public OOB.Resultado.Ficha 
            TransporteDocumento_AnularVenta(OOB.Transporte.Documento.Anular.Venta.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();
            var fichaDTO = new DtoTransporte.Documento.Anular.NotaEntrega.Ficha()
            {
                idDocVenta = ficha.idDocVenta,
                idDocCxC = ficha.idDocCxC,
                idCliente = ficha.idCliente,
                montoDivisa = ficha.montoDivisa,
                auditoria = new DtoTransporte.Documento.Anular.NotaEntrega.FichaAuditoria()
                {
                    idSistemaDocumento = ficha.auditoria.idSistemaDocumento,
                    idUsuario = ficha.auditoria.idUsuario,
                    codigo = ficha.auditoria.codigo,
                    estacion = ficha.auditoria.estacion,
                    motivo = ficha.auditoria.motivo,
                    usuario = ficha.auditoria.usuario,
                },
                aliadosInv = ficha.aliadosInv.Select(s =>
                {
                    var rt = new DtoTransporte.Documento.Anular.NotaEntrega.FichaAliado()
                    {
                        idAliado = s.idAliado,
                        montoDivisa = s.montoDivisa
                    };
                    return rt;
                }).ToList(),
                aliadosDoc = ficha.aliadosDoc.Select(ss => 
                {
                    var rt = new DtoTransporte.Documento.Anular.NotaEntrega.FichaAliadoDoc()
                    {
                         idReg=ss.idReg,
                    };
                    return rt;
                }).ToList(),
            };
            var r01 = MyData.TransporteDocumento_AnularNotaEntrega(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return result;
        }
    }
}