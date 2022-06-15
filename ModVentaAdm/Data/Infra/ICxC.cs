using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface ICxC
    {

        OOB.Resultado.Lista<OOB.CxC.Tools.CtasPendiente.Ficha>
            CxC_Tool_CtasPendiente_GetLista(OOB.CxC.Tools.CtasPendiente.Filtro filtro);

    }

}