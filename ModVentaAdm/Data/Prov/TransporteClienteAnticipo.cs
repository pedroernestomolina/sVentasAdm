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
        public OOB.Resultado.FichaEntidad<int> 
            Transporte_Cliente_Anticipo_Agregar(OOB.Transporte.ClienteAnticipo.Agregar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<int>();
            var fichaDTO = new DtoTransporte.ClienteAnticipo.Agregar.Ficha();
            var mov = new DtoTransporte.ClienteAnticipo.Agregar.Movimiento()
            {
                aplicaRet = ficha.mov.aplicaRet,
                ciRifCliente = ficha.mov.ciRifCliente,
                fechaEmision = ficha.mov.fechaEmision,
                motivo = ficha.mov.motivo,
                sustraendoRet = ficha.mov.sustraendoRet,
                tasaFactor = ficha.mov.tasaFactor,
                tasaRet = ficha.mov.tasaRet,
                idCliente = ficha.mov.idCliente,
                montoAnticipoMonAct = ficha.mov.montoAnticipoMonAct,
                montoAnticipoMonDiv = ficha.mov.montoAnticipoMonDiv,
                montoRecibidoMonAct = ficha.mov.montoRecibidoMonAct,
                montoRecibidoMonDiv = ficha.mov.montoRecibidoMonDiv,
                nombreRazonSocialCliente = ficha.mov.nombreRazonSocialCliente,
                reciboNumero = ficha.mov.reciboNumero,
                retencion = ficha.mov.retencion,
                totalRet = ficha.mov.totalRet,
            };
            fichaDTO.mov = mov;
            fichaDTO.caja = ficha.caja.Select(s =>
            {
                var rg = new DtoTransporte.ClienteAnticipo.Agregar.Caja()
                {
                    codCaja = s.codCaja,
                    descCaja = s.descCaja,
                    idCaja = s.idCaja,
                    monto = s.monto,
                    cajaMov = new DtoTransporte.ClienteAnticipo.Agregar.CajaMov()
                    {
                        descMov = s.cajaMov.descMov,
                        factorCambio = s.cajaMov.factorCambio,
                        fechaMov = s.cajaMov.fechaMov,
                        montoMovMonAct = s.cajaMov.montoMovMonAct,
                        montoMovMonDiv = s.cajaMov.montoMovMonDiv,
                        movFueDivisa = s.cajaMov.movFueDivisa,
                    },
                };
                return rg;
            }).ToList();
            var r01 = MyData.Transporte_Cliente_Anticipo_Agregar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Entidad = r01.Id;
            return result;
        }
    }
}