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
        public OOB.Resultado.FichaEntidad<OOB.Transporte.ClienteAnticipo.Obtener.Ficha> 
            Transporte_Cliente_Anticipo_Obtener_ById(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.ClienteAnticipo.Obtener.Ficha>();
            var r01 = MyData.Transporte_Cliente_Anticipo_Obtener_ById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var s=r01.Entidad;
            var _ent = new OOB.Transporte.ClienteAnticipo.Obtener.Ficha()
            {
                ciRif = s.ciRif,
                id = s.id,
                montoDiv = s.montoDiv,
                nombreRazonSocial = s.nombreRazonSocial,
            };
            result.Entidad = _ent;
            return result;
        }
        public OOB.Resultado.Lista<OOB.Transporte.ClienteAnticipo.ListaMov.Ficha> 
            Transporte_Cliente_Anticipo_GetLista(OOB.Transporte.ClienteAnticipo.ListaMov.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.ClienteAnticipo.ListaMov.Ficha>();
            var filtroDTO = new DtoTransporte.ClienteAnticipo.ListaMov.Filtro()
            {
                Desde = filtro.Desde,
                Hasta = filtro.Hasta,
                Estatus = filtro.Estatus,
                IdCliente = filtro.IdCliente,
            };
            var r01 = MyData.Transporte_Cliente_Anticipo_GetLista (filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.ClienteAnticipo.ListaMov.Ficha>();
            if (r01.Lista!=null)
            {
                if (r01.Lista.Count > 0) 
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.ClienteAnticipo.ListaMov.Ficha()
                        {
                            aplicaRet = s.aplicaRet,
                            ciRifCliente = s.ciRifCliente,
                            estatusAnulado = s.estatusAnulado,
                            fechaReg = s.fechaReg,
                            idMov = s.idMov,
                            montoMonDiv = s.montoMonDiv,
                            montoRecMonDiv = s.montoRecMonDiv,
                            nombreCliente = s.nombreCliente,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
        //
        public OOB.Resultado.Ficha 
            Transporte_Cliente_Anticipo_Anular(int idMov)
        {
            var rt = new OOB.Resultado.Ficha();
            var r01 = MyData.Transporte_Cliente_Anticipo_Anular_ObtenerData (idMov);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var r02 = MyData.Transporte_Cliente_Anticipo_Anular(r01.Entidad);
            if (r02.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r02.Mensaje);
            }
            return rt;
        }
    }
}