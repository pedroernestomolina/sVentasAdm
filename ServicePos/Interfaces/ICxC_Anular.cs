using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface ICxC_Anular
    {
        DtoLib.Resultado
            CxC_AnularCxcAdm(DtoLibPos.CxC.Anular.CxcAdm.Ficha ficha);
    }
}