using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface IServicioPrest
    {
        OOB.Resultado.FichaId
            TransporteServPrest_Agregar(OOB.Transporte.ServPrest.Agregar.Ficha ficha);
        OOB.Resultado.Ficha
            TransporteServPrest_Editar(OOB.Transporte.ServPrest.Editar.Ficha ficha);
        OOB.Resultado.FichaEntidad<OOB.Transporte.ServPrest.Entidad.Ficha>
            TransporteServPrest_GetById(int idAliado);
        OOB.Resultado.Lista<OOB.Transporte.ServPrest.Entidad.Ficha>
            TransporteServPrest_GetLista(OOB.Transporte.ServPrest.Busqueda.Filtro filtro);
    }
}