using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos.Transporte
{
    public interface ICnf
    {
        DtoLib.ResultadoEntidad<string>
            TransporteCnf_NotasPresupuesto_Get();
        DtoLib.Resultado
            TransporteCnf_NotasPresupuesto_Editar(string notas);
    }
}