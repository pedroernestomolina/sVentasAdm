using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    public interface IClienteGrupo
    {
        OOB.Resultado.Lista<OOB.Maestro.Grupo.Entidad.Ficha> 
            ClienteGrupo_GetLista(OOB.Maestro.Grupo.Lista.Filtro filtro);
        OOB.Resultado.FichaEntidad<OOB.Maestro.Grupo.Entidad.Ficha> 
            ClienteGrupo_GetFichaById(string id);
        OOB.Resultado.FichaAuto 
            ClienteGrupo_Agregar(OOB.Maestro.Grupo.Agregar.Ficha ficha);
        OOB.Resultado.Ficha 
            ClienteGrupo_Editar(OOB.Maestro.Grupo.Editar.Ficha ficha);
    }
}