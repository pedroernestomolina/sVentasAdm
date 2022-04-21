using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{

    public interface IClienteZona
    {

        OOB.Resultado.Lista<OOB.Maestro.Zona.Entidad.Ficha> ClienteZona_GetLista(OOB.Maestro.Zona.Lista.Filtro filtro);
        OOB.Resultado.FichaEntidad<OOB.Maestro.Zona.Entidad.Ficha> ClienteZona_GetFichaById(string id);
        OOB.Resultado.FichaAuto ClienteZona_Agregar(OOB.Maestro.Zona.Agregar.Ficha ficha);
        OOB.Resultado.Ficha ClienteZona_Editar(OOB.Maestro.Zona.Editar.Ficha ficha);

    }

}