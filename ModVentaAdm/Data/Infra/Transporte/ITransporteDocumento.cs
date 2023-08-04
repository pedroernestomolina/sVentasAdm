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
    }
}