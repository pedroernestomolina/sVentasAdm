using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.Factura
{
    
    public class GestionItem: AgregarEditarItem.IGestion
    {

        private int _idItemAgregado;


        public int IdItemAgregado { get { return _idItemAgregado; } }


        public bool AgregarItem(AgregarEditarItem.data data, int idTempVenta)
        {
            _idItemAgregado = -1;
            var prd = data.Producto;
            var ficha = new OOB.Venta.Temporal.Item.Registrar.Ficha()
            {
                validarExistencia = data.GetRupturaPorExistencia,
                itemEncabezado = new OOB.Venta.Temporal.Item.Registrar.ItemEncabezado()
                {
                    id = idTempVenta,
                    monto = data.GetImporteFull,
                    montoDivisa = data.GetImporteDivisaFull,
                    renglones = 1,
                },
                itemDetalle = new OOB.Venta.Temporal.Item.Registrar.ItemDetalle()
                {
                    autoDepartamento = prd.AutoDepartamento,
                    autoGrupo = prd.AutoGrupo,
                    autoProducto = prd.Auto,
                    autoSubGrupo = prd.AutoSubGrupo,
                    autoTasaIva = prd.AutoTasaIva,
                    cantidad = data.GetCantidad,
                    categroiaProducto = prd.Categoria,
                    codigoProducto = prd.CodigoPrd,
                    costo = prd.Costo,
                    costoPromd = prd.CostoProm,
                    costoPromdUnd = prd.CostoPromUnd,
                    costoUnd = prd.CostoUnd,
                    decimalesProducto = data.GetDecimales,
                    dsctoPorct = data.GetDsctoPorct,
                    empaqueCont = data.GetEmpqCont,
                    empaqueDesc = data.GetEmpqDesc,
                    estatusPesadoProducto = prd.EstatusPesado,
                    estatusReservaMerc = "1",
                    idVenta = idTempVenta,
                    nombreProducto = prd.NombrePrd,
                    notas = data.GetNotas,
                    precioNeto = data.GetPrecioNeto,
                    precioNetoDivisa = data.GetPrecioNetoDivisa,
                    tarifaPrecio = data.GetIdPrecio,
                    tasaIva = prd.TasaImpuesto,
                    tipoIva = prd.TipoIva,
                    autoDeposito = data.GetIdDeposito,
                    cantidadUnd = data.GetCantidadUnd,
                    total = data.GetImporteFull,
                    totalDivisa = data.GetImporteDivisaFull,
                    estatusRemision="",
                },
                itemActDeposito = new OOB.Venta.Temporal.Item.Registrar.ItemActDeposito()
                {
                    autoDeposito = data.GetIdDeposito,
                    autoProducto = prd.Auto,
                    cntActualizar = data.GetCantidadUnd,
                    prdDescripcion = prd.NombrePrd,
                },
            };
            var r01 = Sistema.MyData.Venta_Temporal_Item_Registrar(ficha);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _idItemAgregado = r01.Id;

            return true;
        }

        public bool EditarItem(AgregarEditarItem.data data, int idTempVenta, int idItemEditar)
        {
            var r00 = Sistema.MyData.Venta_Temporal_Item_GetFichaById(idItemEditar);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return false;
            }

            //ITEM ELIMNAR
            var it= r00.Entidad;
            var fichaEliminar = new OOB.Venta.Temporal.Item.Eliminar.Ficha()
            {
                itemEncabezado = new OOB.Venta.Temporal.Item.Eliminar.ItemEncabezado()
                {
                    id = idTempVenta,
                    monto = it.total,
                    montoDivisa = it.totalDivisa,
                    renglones = 1,
                },
                itemDetalle = new OOB.Venta.Temporal.Item.Eliminar.ItemDetalle()
                {
                    id = idItemEditar,
                },
                itemActDeposito = null,
            };
            if (it.estatusReservaMerc=="1")
            {
                var xficha = new OOB.Venta.Temporal.Item.Eliminar.ItemActDeposito()
                {
                    autoDeposito = it.autoDeposito,
                    autoProducto = it.autoProducto,
                    cntActualizar = it.cantidadUnd,
                    prdDescripcion = it.nombreProducto,
                };
                fichaEliminar.itemActDeposito = xficha;
            }

            //ITEM REGISTRAR
            _idItemAgregado = -1;
            var prd = data.Producto;
            var fichaAgregar = new OOB.Venta.Temporal.Item.Registrar.Ficha()
            {
                validarExistencia = data.GetRupturaPorExistencia,
                itemEncabezado = new OOB.Venta.Temporal.Item.Registrar.ItemEncabezado()
                {
                    id = idTempVenta,
                    monto = data.GetImporteFull,
                    montoDivisa = data.GetImporteDivisaFull,
                    renglones = 1,
                },
                itemDetalle = new OOB.Venta.Temporal.Item.Registrar.ItemDetalle()
                {
                    autoDepartamento = prd.AutoDepartamento,
                    autoGrupo = prd.AutoGrupo,
                    autoProducto = prd.Auto,
                    autoSubGrupo = prd.AutoSubGrupo,
                    autoTasaIva = prd.AutoTasaIva,
                    cantidad = data.GetCantidad,
                    categroiaProducto = prd.Categoria,
                    codigoProducto = prd.CodigoPrd,
                    costo = prd.Costo,
                    costoPromd = prd.CostoProm,
                    costoPromdUnd = prd.CostoPromUnd,
                    costoUnd = prd.CostoUnd,
                    decimalesProducto = data.GetDecimales,
                    dsctoPorct = data.GetDsctoPorct,
                    empaqueCont = data.GetEmpqCont,
                    empaqueDesc = data.GetEmpqDesc,
                    estatusPesadoProducto = prd.EstatusPesado,
                    estatusReservaMerc = "1",
                    idVenta = idTempVenta,
                    nombreProducto = prd.NombrePrd,
                    notas = data.GetNotas,
                    precioNeto = data.GetPrecioNeto,
                    precioNetoDivisa = data.GetPrecioNetoDivisa,
                    tarifaPrecio = data.GetIdPrecio,
                    tasaIva = prd.TasaImpuesto,
                    tipoIva = prd.TipoIva,
                    autoDeposito = data.GetIdDeposito,
                    cantidadUnd = data.GetCantidadUnd,
                    total = data.GetImporteFull,
                    totalDivisa = data.GetImporteDivisaFull,
                    estatusRemision = it.estatusRemision,
                },
                itemActDeposito = new OOB.Venta.Temporal.Item.Registrar.ItemActDeposito()
                {
                    autoDeposito = data.GetIdDeposito,
                    autoProducto = prd.Auto,
                    cntActualizar = data.GetCantidadUnd,
                    prdDescripcion = prd.NombrePrd,
                },
            };

            var ficha = new OOB.Venta.Temporal.Item.Actualizar.Ficha()
            {
                itemEliminar= fichaEliminar,
                itemRegistrar = fichaAgregar,
            };
            var r01 = Sistema.MyData.Venta_Temporal_Item_Actualizar(ficha);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _idItemAgregado = r01.Id;

            return true;
        }

    }

}