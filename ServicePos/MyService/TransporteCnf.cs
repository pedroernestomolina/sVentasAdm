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
        public DtoLib.ResultadoEntidad<string> 
            TransporteCnf_NotasPresupuesto_Get()
        {
            return ServiceProv.TransporteCnf_NotasPresupuesto_Get();
        }
        public DtoLib.Resultado 
            TransporteCnf_NotasPresupuesto_Editar(string notas)
        {
            return ServiceProv.TransporteCnf_NotasPresupuesto_Editar(notas);
        }
        public DtoLib.ResultadoEntidad<string> 
            TransporteCnf_NotasFactura_Get()
        {
            return ServiceProv.TransporteCnf_NotasFactura_Get();
        }
        public DtoLib.Resultado 
            TransporteCnf_NotasFactura_Editar(string notas)
        {
            return ServiceProv.TransporteCnf_NotasFactura_Editar(notas);
        }
    }
}