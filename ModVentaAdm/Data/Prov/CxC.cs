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


        public OOB.Resultado.Lista<OOB.CxC.Tools.CtasPendiente.Ficha> 
            CxC_Tool_CtasPendiente_GetLista(OOB.CxC.Tools.CtasPendiente.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.CxC.Tools.CtasPendiente.Ficha>();

            var filtroDTO = new DtoLibPos.CxC.Tools.CtasPendiente.Filtro()
            {
            };
            var r01 = MyData.CxC_Tool_CtasPendiente_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var lst = new List<OOB.CxC.Tools.CtasPendiente.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.CxC.Tools.CtasPendiente.Ficha()
                        {
                            idCliente = s.idCliente,
                            acumulado = s.acumulado,
                            ciRif = s.ciRif,
                            cntDocPend = s.cntDocPend,
                            cntFactPend = s.cntFactPend,
                            importe = s.importe,
                            limiteFactPend = s.limiteFactPend,
                            limiteMontoCredito = s.limiteMontoCredito,
                            nombreRazonSocial = s.nombreRazonSocial,
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