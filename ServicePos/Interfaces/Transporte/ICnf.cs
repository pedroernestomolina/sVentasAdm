using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte 
{
    public interface ICnf
    {
        DtoLib.ResultadoEntidad<string>
            TransporteCnf_NotasPresupuesto_Get();
        DtoLib.Resultado
            TransporteCnf_NotasPresupuesto_Editar(string notas);
        DtoLib.ResultadoEntidad<string>
            TransporteCnf_NotasFactura_Get();
        DtoLib.Resultado
            TransporteCnf_NotasFactura_Editar(string notas);
    }
}