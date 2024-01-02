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
        //
        public DtoLib.ResultadoEntidad<DtoTransporte.CxcMovCobro.Anular.Ficha> 
            Transporte_CxcMovCobro_Anular_ObtenerData(string idRecibo)
        {
            return ServiceProv.Transporte_CxcMovCobro_Anular_ObtenerData(idRecibo);
        }
        public DtoLib.Resultado 
            Transporte_CxcMovCobro_Anular(DtoTransporte.CxcMovCobro.Anular.Ficha ficha)
        {
            return ServiceProv.Transporte_CxcMovCobro_Anular(ficha);
        }
        //
        public DtoLib.ResultadoEntidad<DtoTransporte.CxcMovCobro.Entidad.Ficha> 
            Transporte_CxcMovCobro_GetById(string idMov)
        {
            return ServiceProv.Transporte_CxcMovCobro_GetById(idMov);
        }
    }
}
