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
            CxC_AnularCxcAdm(OOB.CxC.Anular.CxcAdm.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();
            //
            var fichaDto = new DtoLibPos.CxC.Anular.CxcAdm.Ficha()
            {
                 idCxcAdm=ficha.idCxcAdm,
            };
            var r01 = MyData.CxC_AnularCxcAdm(fichaDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            //
            return result;
        }
    }
}