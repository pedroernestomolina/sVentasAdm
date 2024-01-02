using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITranspCxcMovCobro
    {
        DtoLib.ResultadoLista<DtoTransporte.CxcMovCobro.ListaMov.Ficha>
            Transporte_CxcMovCobro_GetLista(DtoTransporte.CxcMovCobro.ListaMov.Filtro filtro);
        //
        DtoLib.ResultadoEntidad<DtoTransporte.CxcMovCobro.Anular.Ficha>
            Transporte_CxcMovCobro_Anular_ObtenerData(string idRecibo);
        DtoLib.Resultado
            Transporte_CxcMovCobro_Anular(DtoTransporte.CxcMovCobro.Anular.Ficha ficha);
        //
        DtoLib.ResultadoEntidad<DtoTransporte.CxcMovCobro.Entidad.Ficha>
            Transporte_CxcMovCobro_GetById(string idMov);
    }
}
