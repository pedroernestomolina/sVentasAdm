using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.__.Cliente.IData
{
    public interface IData
    {
        List<OOB.Maestro.Grupo.Entidad.Ficha>
            Grupo_GetLista();
        List<OOB.Sistema.Estado.Entidad.Ficha>
            Estados_GetLista();
        List<OOB.Maestro.Zona.Entidad.Ficha>
            Zonas_GetLista();
        List<OOB.Sistema.Vendedor.Entidad.Ficha>
            Vendedores_GetLista();
        List<OOB.Sistema.Cobrador.Entidad.Ficha>
            Cobradores_GetLista();
        List<OOB.Sistema.General>
            Categorias_GetLista();
        List<OOB.Sistema.General>
            Estatus_GetLista();
        List<OOB.Sistema.General>
            Nivel_GetLista();
        List<OOB.Sistema.General>
            Credito_GetLista();
    }
}