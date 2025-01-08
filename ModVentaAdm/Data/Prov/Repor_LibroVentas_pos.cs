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
        public OOB.Resultado.Lista<OOB.Reportes.LibroVenta.Ficha> 
            Reporte_LibroVenta_Pos(OOB.Reportes.LibroVenta.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.LibroVenta.Ficha>();
            //
            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Filtro()
            {
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.Reporte_LibroVentas_Pos(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }//
            var list = new List<OOB.Reportes.LibroVenta.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.LibroVenta.Ficha()
                        {
                            ciRifDoc = s.ciRifDoc,
                            codigoDoc = s.codigoDoc,
                            codigoSucursalDoc = s.codigoSucursalDoc,
                            fechaDoc = s.fechaDoc,
                            fechaRetencionIva = s.fechaRetencionIva,
                            montoBase1 = s.montoBase1,
                            montoBase2 = s.montoBase2,
                            montoExento = s.montoExento,
                            montoImpuesto1 = s.montoImpuesto1,
                            montoImpuesto2 = s.montoImpuesto2,
                            montoRetencionIva = s.montoRetencionIva,
                            montoTotal = s.montoTotal,
                            nombreRazonSocialDoc = s.nombreRazonSocialDoc,
                            numAplicaDoc = s.numAplicaDoc,
                            numControlDoc = s.numControlDoc,
                            numDoc = s.numDoc,
                            signoDoc = s.signoDoc,
                            tasaIva1 = s.tasaIva1,
                            tasaIva2 = s.tasaIva2,
                            tasaRetencionIva = s.tasaRetencionIva,
                            comprobanteRetencionIva = s.comprobanteRetencionIva,
                            auto = s.auto,
                            estatus = s.estatus,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;
            //
            return rt;
        }
    }
}