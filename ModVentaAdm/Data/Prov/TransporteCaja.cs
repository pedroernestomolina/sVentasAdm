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
        public OOB.Resultado.Lista<OOB.Transporte.Caja.Lista.Ficha> 
            Transporte_Caja_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Caja.Lista.Ficha>();
            var r01 = MyData.Transporte_Caja_GetLista();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Caja.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Caja.Lista.Ficha()
                        {
                            id = s.id,
                            codigo = s.codigo,
                            descripcion = s.descripcion,
                            estatusAnulado = s.estatusAnulado,
                            montoPorAnulaciones = s.montoPorAnulaciones,
                            montoPorEgresos = s.montoPorEgresos,
                            montoPorIngresos = s.montoPorIngresos,
                            saldoInicial = s.saldoInicial,
                            esDivisa = s.esDivisa,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
    }
}
