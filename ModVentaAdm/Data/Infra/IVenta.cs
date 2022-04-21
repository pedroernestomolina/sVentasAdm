using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface IVenta
    {

        OOB.Resultado.FichaId  Venta_Temporal_Encabezado_Registrar(OOB.Venta.Temporal.Encabezado.Registrar.Ficha ficha);
        OOB.Resultado.Ficha Venta_Temporal_Encabezado_Editar(OOB.Venta.Temporal.Encabezado.Editar.Ficha ficha);
        OOB.Resultado.Ficha Venta_Temporal_Encabezado_Eliminar(int idEncabezado);

        //
        OOB.Resultado.FichaId Venta_Temporal_Item_Registrar(OOB.Venta.Temporal.Item.Registrar.Ficha ficha);
        OOB.Resultado.FichaId Venta_Temporal_Item_Actualizar(OOB.Venta.Temporal.Item.Actualizar.Ficha ficha);
        OOB.Resultado.Ficha Venta_Temporal_Item_Eliminar(OOB.Venta.Temporal.Item.Eliminar.Ficha ficha);
        OOB.Resultado.Ficha Venta_Temporal_Item_Limpiar(OOB.Venta.Temporal.Item.Limpiar.Ficha ficha);
        OOB.Resultado.FichaEntidad<OOB.Venta.Temporal.Item.Entidad.Ficha> Venta_Temporal_Item_GetFichaById(int idItem);
        OOB.Resultado.Lista<OOB.Venta.Temporal.Item.Entidad.Ficha> Venta_Temporal_Item_GetLista(int idItemporal);
        
        //
        OOB.Resultado.Ficha Venta_Temporal_Anular(OOB.Venta.Temporal.Anular.Ficha ficha);
        OOB.Resultado.FichaEntidad<int> VentaAdm_Temporal_Recuperar(OOB.Venta.Temporal.Recuperar.Ficha ficha);
        OOB.Resultado.FichaEntidad<int> VentaAdm_Temporal_Recuperar_GetCantidadDoc(OOB.Venta.Temporal.Recuperar.Ficha ficha);

        //
        OOB.Resultado.FichaEntidad<OOB.Venta.Temporal.Pendiente.Entidad.Ficha> VentaAdm_Temporal_Pendiente_Abrir(int idTemp);
        OOB.Resultado.Ficha VentaAdm_Temporal_Pendiente_Dejar(OOB.Venta.Temporal.Pendiente.Dejar.Ficha ficha);
        OOB.Resultado.FichaEntidad<int> VentaAdm_Temporal_Pendiente_GetCantidadDoc(OOB.Venta.Temporal.Pendiente.Cantidad.Ficha ficha);
        OOB.Resultado.Lista<OOB.Venta.Temporal.Pendiente.Lista.Ficha> VentaAdm_Temporal_Pendiente_GetLista(OOB.Venta.Temporal.Pendiente.Lista.Filtro filtro);

        //
        OOB.Resultado.Ficha VentaAdm_Temporal_Remision_Registrar(OOB.Venta.Temporal.Remision.Registrar.Ficha ficha);

        //
        OOB.Resultado.Ficha Venta_Temporal_SetNotas(OOB.Venta.Temporal.Cambios.Notas.Ficha ficha);
        OOB.Resultado.Ficha Venta_Temporal_SetTasaDivisa(OOB.Venta.Temporal.Cambios.TasaDivisa.Ficha ficha);

    }

}