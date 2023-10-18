using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos.Transporte
{
    public interface ITranspDocumento
    {
        //
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarPresupuesto(DtoTransporte.Documento.Agregar.Presupuesto.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarPresupuestoConRemision(DtoTransporte.Documento.Agregar.Presupuesto.Ficha ficha);

        //
        DtoLib.Resultado
            TransporteDocumento_AgregarFactura_Vericar(DtoTransporte.Documento.Agregar.Factura.Ficha ficha);
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
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Anular.NotaEntrega.dataAnular>
            TransporteDocumento_AnularNotaEntrega_GetDataAnular(string idDoc);
        DtoLib.Resultado 
            TransporteDocumento_AnularNotaEntrega_Verifica(DtoTransporte.Documento.Anular.NotaEntrega.Ficha ficha);

        //
        DtoLib.ResultadoEntidad<DtoTransporte.Documento.Entidad.Venta.Ficha>
            TransporteDocumento_EntidadVenta_GetById(string idDoc);

        //
        DtoLib.ResultadoEntidad<int>
            TransporteDocumento_Presupuesto_Pendiente_Cnt();

        //
        DtoLib.ResultadoLista<DtoTransporte.Documento.Lista.Pendiente.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_Pendiente();
        DtoLib.Resultado
            TransporteDocumento_AnularPresupuesto_Pendiente(string idDoc);

        //
        DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> 
            TransporteDocumento_GetLista(DtoLibPos.Documento.Lista.Filtro filtro);

        //
        DtoLib.ResultadoLista<DtoTransporte.Documento.GetAliados.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_GetAliados(string idDoc);

        DtoLib.ResultadoLista<DtoTransporte.Documento.GetServicios.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_GetServicios(string idDoc);
    }
}