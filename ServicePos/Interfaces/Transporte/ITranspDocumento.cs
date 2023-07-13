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
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Presupuesto.Ficha>
            TransporteDocumento_EntidadPresupuesto_GetById(string idDoc);

        DtoLib.ResultadoLista<DtoTransporte.Documento.Remision.Lista.Ficha>
            TransporteDocumento_Remision_ListaBy(DtoTransporte.Documento.Remision.Lista.Filtro filtro);
    }
}