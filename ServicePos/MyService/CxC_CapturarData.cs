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
        public DtoLib.ResultadoEntidad<DtoLibPos.CxC.CapturarData.Cliente.Ficha> 
            CxC_CapturarData_Cliente_ById(string id)
        {
            return ServiceProv.CxC_CapturarData_Cliente_ById(id);
        }
    }
}
