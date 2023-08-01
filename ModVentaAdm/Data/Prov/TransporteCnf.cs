using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{
    public partial class DataPrv : IData
    {
        public OOB.Resultado.FichaEntidad<string> 
            TransporteCnf_NotasPresupuesto_Get()
        {
            var result = new OOB.Resultado.FichaEntidad<string>();
            var r01 = MyData.TransporteCnf_NotasPresupuesto_Get();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Entidad = r01.Entidad;
            return result;
        }
        public OOB.Resultado.Ficha 
            TransporteCnf_NotasPresupuesto_Editar(string notas)
        {
            var result = new OOB.Resultado.Ficha();
            var r01 = MyData.TransporteCnf_NotasPresupuesto_Editar(notas);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return result;
        }
    }
}