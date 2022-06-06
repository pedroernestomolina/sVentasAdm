using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface ICliente
    {

        DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha> Cliente_GetLista(DtoLibPos.Cliente.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> Cliente_GetFichaById(string id);
        DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> Cliente_GetFichaByCiRif(string ciRif);
        DtoLib.ResultadoAuto Cliente_Agregar(DtoLibPos.Cliente.Agregar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Editar.ObtenerData.Ficha> Cliente_Editar_GetFicha(string autoId);
        DtoLib.Resultado Cliente_Editar(DtoLibPos.Cliente.Editar.Actualizar.Ficha ficha);
        DtoLib.ResultadoLista<DtoLibPos.Cliente.Documento.Ficha> Cliente_Documento_GetLista(DtoLibPos.Cliente.Documento.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Cliente.Articulos.Ficha> Cliente_ArticuloVenta_GetLista(DtoLibPos.Cliente.Articulos.Filtro filtro);
        DtoLib.Resultado Cliente_Activar(DtoLibPos.Cliente.EstatusActivarInactivar.Ficha ficha);
        DtoLib.Resultado Cliente_Inactivar(DtoLibPos.Cliente.EstatusActivarInactivar.Ficha ficha);

    }

}