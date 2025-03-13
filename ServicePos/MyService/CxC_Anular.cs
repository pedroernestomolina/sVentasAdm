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
        public DtoLib.Resultado 
            CxC_AnularCxcAdm(DtoLibPos.CxC.Anular.CxcAdm.Ficha ficha)
        {
            return ServiceProv.CxC_AnularCxcAdm(ficha);
        }
    }
}