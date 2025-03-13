using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    public interface ICxC_Anular
    {
        OOB.Resultado.Ficha
            CxC_AnularCxcAdm(OOB.CxC.Anular.CxcAdm.Ficha ficha);
    }
}