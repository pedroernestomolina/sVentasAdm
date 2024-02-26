using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteDocumento
    {
        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarPresupuesto(OOB.Transporte.Documento.Agregar.Presupuesto.Ficha ficha);
        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarFactura(OOB.Transporte.Documento.Agregar.Factura.Ficha ficha);

        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Entidad.Presupuesto.Ficha >
            TransporteDocumento_EntidadPresupuesto_GetById(string idDoc);
        OOB.Resultado.Lista<OOB.Transporte.Documento.Entidad.Presupuesto.FichaAliado >
            TransporteDocumento_EntidadPresupuesto_GetAliadosById(string idDoc);

        OOB.Resultado.Lista<OOB.Transporte.Documento.Remision.Lista.Ficha>
            TransporteDocumento_Remision_ListaBy(OOB.Transporte.Documento.Remision.Lista.Filtro filtro);

        OOB.Resultado.Ficha
            TransporteDocumento_AnularPresupuesto(OOB.Transporte.Documento.Anular.Presupuesto.Ficha ficha);
        OOB.Resultado.Ficha
            TransporteDocumento_AnularVenta(OOB.Transporte.Documento.Anular.Venta.Ficha ficha);

        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Entidad.Venta.Ficha>
            TransporteDocumento_EntidadVenta_GetById(string idDoc);
        
        OOB.Resultado.FichaEntidad<int>
            TransporteDocumento_Presupuesto_Pendiente_Cnt();

        //
        OOB.Resultado.Lista<OOB.Transporte.Documento.Lista.Pendiente.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_Pendiente();
        OOB.Resultado.Ficha
            TransporteDocumento_AnularPresupuesto_Pendiente(string idDoc);

        //
        OOB.Resultado.Lista<OOB.Documento.Lista.Ficha>
            TransporteDocumento_GetLista(OOB.Documento.Lista.Filtro filtro);

        //
        OOB.Resultado.Lista<OOB.Transporte.Documento.GetAliados.Presupuesto.Ficha>
            TransporteDocumento_GetAliados_Presupuesto(string idDoc);

        OOB.Resultado.Lista<OOB.Transporte.Documento.GetServicios.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_GetServicios(string idDoc);
        
        OOB.Resultado.Lista<OOB.Transporte.Documento.GetTurnos.Presupuesto.Ficha>
            TransporteDocumento_Presupuesto_GetTurnos(string idDoc);

        //
        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarFactura_From_HojaServ(OOB.Transporte.Documento.Agregar.FacturaFromHojaServ.Ficha ficha);

        //
        OOB.Resultado.Lista<OOB.Transporte.Documento.GetTurnos.Documento.Ficha>
            TransporteDocumento_Documento_GetTurnos(string idDoc);

        //LISTA DE DOCUMENTOS APTOS PARA APLICAR NOTA DE CREDITO
        OOB.Resultado.Lista<OOB.Documento.Lista.Ficha>
            TransporteDocumento_Documento_AplicanNotaCredito_FiltradoByCliente(string cliente);
        //DEL DOCUMENTO SELECCIONADO PARA APLICAR NOTA DE CREDITO, OBTEN DATA
        OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha>
            TransporteDocumento_Documento_AplicaNotaCredito_GetData(string idDoc);
        //AGREGAR NOTA DE CREDITO
        OOB.Resultado.FichaEntidad<string>
            TransporteDocumento_Documento_NotaCredito_Agregar(OOB.Transporte.Documento.Agregar.NotaCredito.Nueva.Ficha ficha);
    }
}