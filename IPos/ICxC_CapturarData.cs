using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface ICxC_CapturarData
    {
        DtoLib.ResultadoEntidad<DtoLibPos.CxC.CapturarData.Cliente.Ficha>
            CxC_CapturarData_Cliente_ById(string id);
    }
}