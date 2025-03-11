using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    public interface ICxC_CargarData
    {
        OOB.Resultado.FichaEntidad<OOB.CxC.CargarData.Cliente.Ficha>
            CxC_CapturarData_Cliente_ById(string id);
    }
}