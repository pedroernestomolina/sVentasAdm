using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface IReportesCli
    {

        OOB.Resultado.Lista<OOB.ReporteCli.Maestro.Ficha> ReportesCli_Maestro(OOB.ReporteCli.Maestro.Filtro filtro);

    }

}