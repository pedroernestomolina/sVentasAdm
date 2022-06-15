using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{

    public partial class Service : IService
    {

        public DtoLib.ResultadoLista<DtoLibPos.CxC.Tools.CtasPendiente.Ficha> 
            CxC_Tool_CtasPendiente_GetLista(DtoLibPos.CxC.Tools.CtasPendiente.Filtro filtro)
        {
            return ServiceProv.CxC_Tool_CtasPendiente_GetLista(filtro);
        }

    }

}