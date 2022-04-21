using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    public interface IPermiso
    {

        OOB.Resultado.FichaEntidad <string> Permiso_PedirClaveAcceso_NivelMaximo();
        OOB.Resultado.FichaEntidad<string> Permiso_PedirClaveAcceso_NivelMedio();
        OOB.Resultado.FichaEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo();
        //

        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Configuracion(string autoGrupoUsuario);
        //

        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Reportes(string autoGrupoUsuario);
        //

        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteGrupo(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Agregar(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Editar(string idGrupoUsu);
        //

        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteZona(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteZona_Agregar(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteZona_Editar(string idGrupoUsu);
        //

        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente (string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_Agregar(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_Editar(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_Reportes(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_ActivarInactivar(string idGrupoUsu);
        //

        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Adm_AnularDocumento(string idGrupoUsu);
        //

        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_AnularItem(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_DarDsctoItem(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_VisualizarCosto(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_PrecioLibre(string idGrupoUsu);
        OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_DsctoGlobal(string idGrupoUsu);
        //

    }

}