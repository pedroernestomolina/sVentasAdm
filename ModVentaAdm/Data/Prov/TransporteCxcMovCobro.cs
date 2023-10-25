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
        public OOB.Resultado.Lista<OOB.Transporte.CxcMovCobro.ListaMov.Ficha> 
            Transporte_CxcMovCobro_GetLista(OOB.Transporte.CxcMovCobro.ListaMov.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.CxcMovCobro.ListaMov.Ficha>();
            var filtroDTO = new DtoTransporte.CxcMovCobro.ListaMov.Filtro()
            {
                Desde = filtro.Desde,
                Hasta = filtro.Hasta,
                Estatus = filtro.Estatus,
                IdCliente = filtro.IdCliente,
            };
            var r01 = MyData.Transporte_CxcMovCobro_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.CxcMovCobro.ListaMov.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.CxcMovCobro.ListaMov.Ficha()
                        {
                            ciRifCliente = s.ciRifCliente,
                            estatusAnulado = s.estatusAnulado,
                            fechaEmision = s.fechaEmision,
                            idMov = s.idMov,
                            importeDiv = s.importeDiv,
                            montoAnticipoDiv = s.montoAnticipoDiv,
                            montoRecibidoDiv = s.montoRecibidoDiv,
                            nombreCliente = s.nombreCliente,
                            numRecibo = s.numRecibo,
                            montoRetDiv = s.montoRetDiv,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
        public OOB.Resultado.Ficha 
            Transporte_CxcMovCobro_Anular(string idRecibo)
        {
            var rt = new OOB.Resultado.Ficha();
            var r01 = MyData.Transporte_CxcMovCobro_Anular_ObtenerData (idRecibo);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var r02 = MyData.Transporte_CxcMovCobro_Anular(r01.Entidad);
            if (r02.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r02.Mensaje);
            }
            return rt;
        }
    }
}