using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface ICliente
    {

        OOB.Resultado.FichaEntidad<OOB.Maestro.Cliente.Entidad.Ficha> Cliente_GetFicha(string autoCliente);
        OOB.Resultado.Lista<OOB.Maestro.Cliente.Entidad.Ficha> Cliente_GetLista(OOB.Maestro.Cliente.Lista.Filtro filtro);
        OOB.Resultado.Lista<OOB.Maestro.Cliente.Documento.Ficha> Cliente_Documentos_GetLista(OOB.Maestro.Cliente.Documento.Filtro filtro);
        OOB.Resultado.Lista<OOB.Maestro.Cliente.Articulos.Ficha> Cliente_ArticulosVenta_GetLista(OOB.Maestro.Cliente.Articulos.Filtro filtro);
        OOB.Resultado.FichaAuto Cliente_Agregar(OOB.Maestro.Cliente.Agregar.Ficha ficha);
        OOB.Resultado.FichaEntidad<OOB.Maestro.Cliente.Editar.ObtenerData.Ficha> Cliente_Editar_GetFicha(string autoCli);
        OOB.Resultado.Ficha Cliente_Editar(OOB.Maestro.Cliente.Editar.Actualizar.Ficha ficha);
        OOB.Resultado.Ficha Cliente_Activar(OOB.Maestro.Cliente.EstatusActivarInactivar.Ficha ficha);
        OOB.Resultado.Ficha Cliente_Inactivar(OOB.Maestro.Cliente.EstatusActivarInactivar.Ficha ficha);

    }

}