using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos.Transporte
{
    public interface ITranspDoc_GestionAliado
    {
        //
        // GESTION ALIADO EN EL DOCUMENTO: DONDE PUEDA OBTENER LOS ALIADOS ACTIVOS 
        // EN EL DOCUMENTO, PODER ANULAR UN ALIADO, PODER INSERTAR UN NUEVO ALIADO AL DOCUMENTO 
        //
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.GestionAliados.ObtenerLista.Ficha>
            TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(string id);
        DtoLib.Resultado
            TransporteDocumento_GestionAliados_AnularAliado(DtoTransporte.Documento.GestionAliados.AnularAliado.Ficha ficha);
        DtoLib.ResultadoEntidad<int>
            TransporteDocumento_GestionAliados_InsertarAliado(DtoTransporte.Documento.GestionAliados.InsertarAliado.Ficha ficha);
    }
}