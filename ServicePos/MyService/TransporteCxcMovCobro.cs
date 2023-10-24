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
        public DtoLib.ResultadoLista<DtoTransporte.CxcMovCobro.ListaMov.Ficha> 
            Transporte_CxcMovCobro_GetLista(DtoTransporte.CxcMovCobro.ListaMov.Filtro filtro)
        {
            return ServiceProv.Transporte_CxcMovCobro_GetLista(filtro);
        }
    }
}
