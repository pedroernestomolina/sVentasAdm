using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteDocGestionAliado
    {
        //
        // GESTION ALIADO EN EL DOCUMENTO: DONDE PUEDA OBTENER LOS ALIADOS ACTIVOS 
        // EN EL DOCUMENTO, PODER ANULAR UN ALIADO, PODER INSERTAR UN NUEVO ALIADO AL DOCUMENTO 
        //
        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Ficha>
            TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(string id);
        OOB.Resultado.Ficha
            TransporteDocumento_GestionAliados_AnularAliado(OOB.Transporte.Documento.GestionAliados.AnularAliado.Ficha ficha);
        OOB.Resultado.FichaEntidad<int>
            TransporteDocumento_GestionAliados_InsertarAliado(OOB.Transporte.Documento.GestionAliados.InsertarAliado.Ficha ficha);
    }
}