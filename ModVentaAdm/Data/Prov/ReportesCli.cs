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

        public OOB.Resultado.Lista<OOB.ReporteCli.Maestro.Ficha> ReportesCli_Maestro(OOB.ReporteCli.Maestro.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.ReporteCli.Maestro.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.Clientes.Maestro.Filtro()
            {
                estatus = filtro.estatus,
                estCategoria = filtro.estCategoria,
                estCredito = filtro.estCredito,
                estNivel = filtro.estNivel,
                estTarifa = filtro.estTarifa,
                idCobrador = filtro.idCobrador,
                idEstado = filtro.idEstado,
                idGrupo = filtro.idGrupo,
                idVendedor = filtro.idVendedor,
                idZona = filtro.idZona,
            };
            var r01 = MyData.ReportesCli_Maestro (filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.ReporteCli.Maestro.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.ReporteCli.Maestro.Ficha()
                        {
                            celular = s.celular,
                            ciRif = s.ciRif,
                            codigo = s.codigo,
                            dirFiscal = s.dirFiscal,
                            nombre = s.nombre,
                            telefono1 = s.telefono1,
                            telefono2 = s.telefono2,
                            estatus=s.estatus,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }

    }

}