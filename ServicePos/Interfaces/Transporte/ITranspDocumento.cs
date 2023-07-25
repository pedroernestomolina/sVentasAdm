using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITranspDocumento
    {
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>
          TransporteDocumento_AgregarPresupuesto(DtoTransporte.Documento.Agregar.Presupuesto.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarFactura(DtoTransporte.Documento.Agregar.Factura.Ficha ficha);

        //
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Presupuesto.Ficha>
            TransporteDocumento_EntidadPresupuesto_GetById(string idDoc);
        DtoLib.ResultadoLista<DtoTransporte.Documento.Entidad.Presupuesto.FichaAliado>
            TransporteDocumento_EntidadPresupuesto_GetAliadosById(string idDoc);

        //
        DtoLib.ResultadoLista<DtoTransporte.Documento.Remision.Lista.Ficha>
            TransporteDocumento_Remision_ListaBy(DtoTransporte.Documento.Remision.Lista.Filtro filtro);

        //
        DtoLib.Resultado
            TransporteDocumento_AnularPresupuesto(DtoTransporte.Documento.Anular.Presupuesto.Ficha ficha);
        DtoLib.Resultado
            TransporteDocumento_AnularNotaEntrega(DtoTransporte.Documento.Anular.NotaEntrega.Ficha ficha);
    }
}