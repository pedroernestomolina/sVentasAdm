using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    public interface IReportes
    {
        OOB.Resultado.Lista<OOB.Reportes.GeneralDocumento.Ficha> 
            Reportes_GeneralDocumento(OOB.Reportes.GeneralDocumento.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.GeneralPorDepartamento.Ficha> 
            Reportes_GeneralPorDepartamento(OOB.Reportes.GeneralPorDepartamento.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.GeneralPorGrupo.Ficha> 
            Reportes_GeneralPorGrupo(OOB.Reportes.GeneralPorGrupo.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.Resumen.Ficha> 
            Reportes_Resumen(OOB.Reportes.Resumen.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.VentaPorProducto.Ficha> 
            Reportes_VentaPorProducto(OOB.Reportes.VentaPorProducto.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.GeneralDocumentoDetalle.Ficha> 
            Reportes_GeneralDocumentoDetalle(OOB.Reportes.GeneralDocumentoDetalle.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.Consolidado.Ficha> 
            Reportes_Consolidado(OOB.Reportes.Consolidado.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.Utilidad.Venta.Ficha>
            Reportes_UtilidadVenta (OOB.Reportes.Utilidad.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.Utilidad.Producto.Ficha> 
            Reportes_UtilidadProducto(OOB.Reportes.Utilidad.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.Utilidad.Consolidado.Ficha> 
            Reportes_UtilidadConsolidado(OOB.Reportes.Utilidad.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.LibroVenta.Ficha> 
            ReportesAdm_LibroVenta(OOB.Reportes.LibroVenta.Filtro filtro);
        //
        OOB.Resultado.Lista<OOB.Reportes.Vendedor.Resumen.Ficha>
            ReportesAdm_VentasPorVendedor_Resumen(OOB.Reportes.Vendedor.Filtro filtro);
        OOB.Resultado.Lista<OOB.Reportes.Vendedor.Detallado.Ficha>
            ReportesAdm_VentasPorVendedor_Detallado(OOB.Reportes.Vendedor.Filtro filtro);
    }
}