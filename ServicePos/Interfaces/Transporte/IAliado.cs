using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte 
{
    public interface IAliado
    {
        DtoLib.ResultadoId
            TransporteAliado_Agregar(DtoTransporte.Aliado.Agregar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoTransporte.Aliado.Entidad.Ficha>
            TransporteAliado_GetById(int idAliado);
        DtoLib.ResultadoLista<DtoTransporte.Aliado.Entidad.Ficha>
            TransporteAliado_GetLista(DtoTransporte.Aliado.Busqueda.Filtro filtro);
    }
}