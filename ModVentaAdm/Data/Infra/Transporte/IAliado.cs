using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface IAliado
    {
        OOB.Resultado.FichaId
            TransporteAliado_Agregar(OOB.Transporte.Aliado.Agregar.Ficha ficha);
        OOB.Resultado.FichaEntidad<OOB.Transporte.Aliado.Entidad.Ficha>
            TransporteAliado_GetById(int idAliado);
        OOB.Resultado.Lista<OOB.Transporte.Aliado.Entidad.Ficha>
            TransporteAliado_GetLista(OOB.Transporte.Aliado.Busqueda.Filtro filtro);
    }
}