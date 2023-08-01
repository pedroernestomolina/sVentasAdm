using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos.Transporte
{
    public interface IServicioPrest
    {
        DtoLib.ResultadoId
            TransporteServPrest_Agregar(DtoTransporte.ServPrest.Agregar.Ficha ficha);
        DtoLib.Resultado
            TransporteServPrest_Editar(DtoTransporte.ServPrest.Editar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoTransporte.ServPrest.Entidad.Ficha>
            TransporteServPrest_GetById(int idAliado);
        DtoLib.ResultadoLista<DtoTransporte.ServPrest.Entidad.Ficha>
            TransporteServPrest_GetLista(DtoTransporte.ServPrest.Busqueda.Filtro filtro);
    }
}