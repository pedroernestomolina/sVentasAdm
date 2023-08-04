using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ICnf
    {
        OOB.Resultado.FichaEntidad<string>
            TransporteCnf_NotasPresupuesto_Get();
        OOB.Resultado.Ficha
            TransporteCnf_NotasPresupuesto_Editar(string notas);
        OOB.Resultado.FichaEntidad<string>
            TransporteCnf_NotasFactura_Get();
        OOB.Resultado.Ficha
            TransporteCnf_NotasFactura_Editar(string notas);
    }
}