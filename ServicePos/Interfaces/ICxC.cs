using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface ICxC
    {

        DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Ficha>
            CxC_Tool_CtasPendiente_GetLista(DtoLibPos.CxC.Tools.CtasPendiente.Filtro filtro);

    }

}