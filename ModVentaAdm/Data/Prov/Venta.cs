using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{

    public partial class DataPrv : IData
    {

        public OOB.Resultado.FichaId Venta_Temporal_Encabezado_Registrar(OOB.Venta.Temporal.Encabezado.Registrar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaId();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar.Ficha()
            {
                autoCliente = ficha.autoCliente,
                autoDeposito = ficha.autoDeposito,
                autoSistDocumento = ficha.autoSistDocumento,
                autoSucursal = ficha.autoSucursal,
                autoUsuario = ficha.autoUsuario,
                ciRifCliente = ficha.ciRifCliente,
                estatusPendiente = ficha.estatusPendiente,
                factorDivisa = ficha.factorDivisa,
                idEquipo = ficha.idEquipo,
                monto = ficha.monto,
                montoDivisa = ficha.montoDivisa,
                nombreDeposito = ficha.nombreDeposito,
                nombreSistDocumento = ficha.nombreSistDocumento,
                nombreSucursal = ficha.nombreSucursal,
                nombreUsuario = ficha.nombreUsuario,
                razonSocialCliente = ficha.razonSocialCliente,
                renglones = ficha.renglones,
                //
                autoCobrador = ficha.autoCobrador,
                autoRemision = ficha.autoRemision,
                autoTransporte = ficha.autoTransporte,
                autoVendedor = ficha.autoVendedor,
                codigoCliente = ficha.codigoCliente,
                diasCredito = ficha.diasCredito,
                diasValidez = ficha.diasValidez,
                dirDespacho = ficha.dirDespacho,
                dirFiscalCliente = ficha.dirFiscalCliente,
                documentoRemision = ficha.documentoRemision,
                estatusCredito = ficha.estatusCredito,
                notasDoc = ficha.notasDoc,
                tarifaPrecioCliente = ficha.tarifaPrecioCliente,
                tipoRemision = ficha.tipoRemision,
                nombreTipoDocRemision = ficha.nombreTipoDocRemision,
            };
            var r01 = MyData.VentaAdm_Temporal_Encabezado_Registrar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            result.Id = r01.Id;
            return result;
        }
        public OOB.Resultado.Ficha Venta_Temporal_Encabezado_Eliminar(int idEncabezado)
        {
            var result = new OOB.Resultado.Ficha();

            var r01 = MyData.VentaAdm_Temporal_Encabezado_Eliminar(idEncabezado);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Ficha Venta_Temporal_Encabezado_Editar(OOB.Venta.Temporal.Encabezado.Editar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Encabezado.Editar.Ficha()
            {
                id = ficha.id,
                autoCliente = ficha.autoCliente,
                autoDeposito = ficha.autoDeposito,
                autoSucursal = ficha.autoSucursal,
                ciRifCliente = ficha.ciRifCliente,
                nombreDeposito = ficha.nombreDeposito,
                nombreSucursal = ficha.nombreSucursal,
                razonSocialCliente = ficha.razonSocialCliente,
                //
                autoCobrador = ficha.autoCobrador,
                autoTransporte = ficha.autoTransporte,
                autoVendedor = ficha.autoVendedor,
                codigoCliente = ficha.codigoCliente,
                diasCredito = ficha.diasCredito,
                diasValidez = ficha.diasValidez,
                dirDespacho = ficha.dirDespacho,
                dirFiscalCliente = ficha.dirFiscalCliente,
                estatusCredito = ficha.estatusCredito,
                tarifaPrecioCliente = ficha.tarifaPrecioCliente,
            };
            var r01 = MyData.VentaAdm_Temporal_Encabezado_Editar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }

        //
        public OOB.Resultado.Ficha Venta_Temporal_Anular(OOB.Venta.Temporal.Anular.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Anular.Ficha()
            {
                IdEncabezado = ficha.IdEncabezado,
                Items = ficha.Items.Select(s =>
                {
                    var rg = new DtoLibPos.VentaAdm.Temporal.Anular.Item()
                    {
                        idItem = s.idItem,
                    };
                    return rg;
                }).ToList(),
                ItemsActDeposito = ficha.ItemsActDeposito.Select(s =>
                {
                    var rg = new DtoLibPos.VentaAdm.Temporal.Anular.ItemActDeposito()
                    {
                        prdDescripcion = s.prdDescripcion,
                        autoDeposito = s.autoDeposito,
                        autoProducto = s.autoProducto,
                        cntActualizar = s.cntActualizar,
                    };
                    return rg;
                }).ToList(),
            };
            var r01 = MyData.VentaAdm_Temporal_Anular(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.FichaEntidad<int> VentaAdm_Temporal_Recuperar(OOB.Venta.Temporal.Recuperar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<int>();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha()
            {
                autoSistDocumento = ficha.autoSistDocumento,
                autoUsuario = ficha.autoUsuario,
                idEquipo = ficha.idEquipo,
            };
            var r01 = MyData.VentaAdm_Temporal_Recuperar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Entidad = r01.Entidad;

            return result;
        }
        public OOB.Resultado.FichaEntidad<int> VentaAdm_Temporal_Recuperar_GetCantidadDoc(OOB.Venta.Temporal.Recuperar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<int>();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha()
            {
                autoSistDocumento = ficha.autoSistDocumento,
                autoUsuario = ficha.autoUsuario,
                idEquipo = ficha.idEquipo,
            };
            var r01 = MyData.VentaAdm_Temporal_Recuperar_GetCantidaDoc(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Entidad = r01.Entidad;

            return result;
        }

        //
        public OOB.Resultado.FichaId Venta_Temporal_Item_Registrar(OOB.Venta.Temporal.Item.Registrar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaId();

            var xenc = ficha.itemEncabezado;
            var xit = ficha.itemDetalle;
            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.Ficha()
            {
                validarExistencia = ficha.validarExistencia,
                itemEncabezado = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.ItemEncabezado()
                {
                    id = xenc.id,
                    monto = xenc.monto,
                    montoDivisa = xenc.montoDivisa,
                    renglones = xenc.renglones,
                },
                itemActDeposito = null,
                itemDetalle = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.ItemDetalle()
                {
                    autoDepartamento = xit.autoDepartamento,
                    autoGrupo = xit.autoGrupo,
                    autoProducto = xit.autoProducto,
                    autoSubGrupo = xit.autoSubGrupo,
                    autoTasaIva = xit.autoTasaIva,
                    cantidad = xit.cantidad,
                    categroiaProducto = xit.categroiaProducto,
                    codigoProducto = xit.codigoProducto,
                    costo = xit.costo,
                    costoPromd = xit.costoPromd,
                    costoPromdUnd = xit.costoPromdUnd,
                    costoUnd = xit.costoUnd,
                    decimalesProducto = xit.decimalesProducto,
                    dsctoPorct = xit.dsctoPorct,
                    empaqueCont = xit.empaqueCont,
                    empaqueDesc = xit.empaqueDesc,
                    estatusPesadoProducto = xit.estatusPesadoProducto,
                    estatusReservaMerc = xit.estatusReservaMerc,
                    idVenta = xit.idVenta,
                    nombreProducto = xit.nombreProducto,
                    notas = xit.notas,
                    precioNeto = xit.precioNeto,
                    precioNetoDivisa = xit.precioNetoDivisa,
                    tarifaPrecio = xit.tarifaPrecio,
                    tasaIva = xit.tasaIva,
                    tipoIva = xit.tipoIva,
                    autoDeposito = xit.autoDeposito,
                    cantidadUnd = xit.cantidadUnd,
                    total = xit.total,
                    totalDivisa = xit.totalDivisa,
                    estatusRemision = xit.estatusRemision,
                    nombreDeposito=xit.nombreDeposito,
                },
            };
            if (ficha.itemActDeposito != null)
            {
                var xdep = ficha.itemActDeposito;
                fichaDTO.itemActDeposito = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.ItemActDeposito()
                {
                    autoDeposito = xdep.autoDeposito,
                    autoProducto = xdep.autoProducto,
                    cntActualizar = xdep.cntActualizar,
                    prdDescripcion = xdep.prdDescripcion,
                };
            }
            var r01 = MyData.VentaAdm_Temporal_Item_Registrar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            result.Id = r01.Id;
            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Venta.Temporal.Item.Entidad.Ficha> Venta_Temporal_Item_GetFichaById(int idItem)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Venta.Temporal.Item.Entidad.Ficha>();

            var r01 = MyData.VentaAdm_Temporal_Item_GetFichaById(idItem);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var xit= r01.Entidad;
            result.Entidad = new OOB.Venta.Temporal.Item.Entidad.Ficha()
            {
                id=xit.id,
                autoDepartamento = xit.autoDepartamento,
                autoGrupo = xit.autoGrupo,
                autoProducto = xit.autoProducto,
                autoSubGrupo = xit.autoSubGrupo,
                autoTasaIva = xit.autoTasaIva,
                cantidad = xit.cantidad,
                categroiaProducto = xit.categroiaProducto,
                codigoProducto = xit.codigoProducto,
                costo = xit.costo,
                costoPromd = xit.costoPromd,
                costoPromdUnd = xit.costoPromdUnd,
                costoUnd = xit.costoUnd,
                decimalesProducto = xit.decimalesProducto,
                dsctoPorct = xit.dsctoPorct,
                empaqueCont = xit.empaqueCont,
                empaqueDesc = xit.empaqueDesc,
                estatusPesadoProducto = xit.estatusPesadoProducto,
                estatusReservaMerc = xit.estatusReservaMerc,
                nombreProducto = xit.nombreProducto,
                notas = xit.notas,
                precioNeto = xit.precioNeto,
                precioNetoDivisa = xit.precioNetoDivisa,
                tarifaPrecio = xit.tarifaPrecio,
                tasaIva = xit.tasaIva,
                tipoIva = xit.tipoIva,
                autoDeposito = xit.autoDeposito,
                cantidadUnd = xit.cantidadUnd,
                total = xit.total,
                totalDivisa = xit.totalDivisa,
                estatusRemision= xit.estatusRemision,
            };
            return result;
        }
        public OOB.Resultado.Ficha Venta_Temporal_Item_Eliminar(OOB.Venta.Temporal.Item.Eliminar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var xenc = ficha.itemEncabezado;
            var xit = ficha.itemDetalle;
            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.Ficha()
            {
                itemEncabezado = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.ItemEncabezado()
                {
                    id = xenc.id,
                    monto = xenc.monto,
                    montoDivisa = xenc.montoDivisa,
                    renglones = xenc.renglones,
                },
                itemActDeposito = null,
                itemDetalle = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.ItemDetalle()
                {
                    id = xit.id,
                },
            };
            if (ficha.itemActDeposito != null)
            {
                var xdep = ficha.itemActDeposito;
                fichaDTO.itemActDeposito = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.ItemActDeposito()
                {
                    autoDeposito = xdep.autoDeposito,
                    autoProducto = xdep.autoProducto,
                    cntActualizar = xdep.cntActualizar,
                    prdDescripcion = xdep.prdDescripcion,
                };
            }
            var r01 = MyData.VentaAdm_Temporal_Item_Eliminar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Ficha Venta_Temporal_Item_Limpiar(OOB.Venta.Temporal.Item.Limpiar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var xenc = ficha.itemEncabezado;
            var xit = ficha.itemDetalle;
            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Item.Limpiar.Ficha()
            {
                itemEncabezado = new DtoLibPos.VentaAdm.Temporal.Item.Limpiar.ItemEncabezado()
                {
                    id = xenc.id,
                },
                itemActDeposito = null,
                itemDetalle = xit.Select(s=> {
                    var rt = new DtoLibPos.VentaAdm.Temporal.Item.Limpiar.ItemDetalle()
                    {
                        id = s.id,
                    };
                    return rt;
                }).ToList(),
            };
            if (ficha.itemActDeposito != null)
            {
                fichaDTO.itemActDeposito = ficha.itemActDeposito.Select(s =>
                {
                    var rt = new DtoLibPos.VentaAdm.Temporal.Item.Limpiar.ItemActDeposito()
                    {
                        autoDeposito = s.autoDeposito,
                        autoProducto = s.autoProducto,
                        cntActualizar = s.cntActualizar,
                        prdDescripcion = s.prdDescripcion,
                    };
                    return rt;
                }).ToList();
            }
            var r01 = MyData.VentaAdm_Temporal_Item_Limpiar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.FichaId Venta_Temporal_Item_Actualizar(OOB.Venta.Temporal.Item.Actualizar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaId();

            var xenc = ficha.itemEliminar.itemEncabezado;
            var xit = ficha.itemEliminar.itemDetalle;
            var eliminarDTO = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.Ficha()
            {
                itemEncabezado = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.ItemEncabezado()
                {
                    id = xenc.id,
                    monto = xenc.monto,
                    montoDivisa = xenc.montoDivisa,
                    renglones = xenc.renglones,
                },
                itemActDeposito = null,
                itemDetalle = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.ItemDetalle()
                {
                    id = xit.id,
                },
            };
            if (ficha.itemEliminar.itemActDeposito != null)
            {
                var xdep = ficha.itemEliminar.itemActDeposito;
                eliminarDTO.itemActDeposito = new DtoLibPos.VentaAdm.Temporal.Item.Eliminar.ItemActDeposito()
                {
                    autoDeposito = xdep.autoDeposito,
                    autoProducto = xdep.autoProducto,
                    cntActualizar = xdep.cntActualizar,
                    prdDescripcion = xdep.prdDescripcion,
                };
            }


            var xencAgregar = ficha.itemRegistrar.itemEncabezado;
            var xitAgregar = ficha.itemRegistrar.itemDetalle;
            var agregarDTO = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.Ficha()
            {
                validarExistencia = ficha.itemRegistrar.validarExistencia,
                itemEncabezado = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.ItemEncabezado()
                {
                    id = xencAgregar.id,
                    monto = xencAgregar.monto,
                    montoDivisa = xencAgregar.montoDivisa,
                    renglones = xencAgregar.renglones,
                },
                itemActDeposito = null,
                itemDetalle = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.ItemDetalle()
                {
                    autoDepartamento = xitAgregar.autoDepartamento,
                    autoGrupo = xitAgregar.autoGrupo,
                    autoProducto = xitAgregar.autoProducto,
                    autoSubGrupo = xitAgregar.autoSubGrupo,
                    autoTasaIva = xitAgregar.autoTasaIva,
                    cantidad = xitAgregar.cantidad,
                    categroiaProducto = xitAgregar.categroiaProducto,
                    codigoProducto = xitAgregar.codigoProducto,
                    costo = xitAgregar.costo,
                    costoPromd = xitAgregar.costoPromd,
                    costoPromdUnd = xitAgregar.costoPromdUnd,
                    costoUnd = xitAgregar.costoUnd,
                    decimalesProducto = xitAgregar.decimalesProducto,
                    dsctoPorct = xitAgregar.dsctoPorct,
                    empaqueCont = xitAgregar.empaqueCont,
                    empaqueDesc = xitAgregar.empaqueDesc,
                    estatusPesadoProducto = xitAgregar.estatusPesadoProducto,
                    estatusReservaMerc = xitAgregar.estatusReservaMerc,
                    idVenta = xitAgregar.idVenta,
                    nombreProducto = xitAgregar.nombreProducto,
                    notas = xitAgregar.notas,
                    precioNeto = xitAgregar.precioNeto,
                    precioNetoDivisa = xitAgregar.precioNetoDivisa,
                    tarifaPrecio = xitAgregar.tarifaPrecio,
                    tasaIva = xitAgregar.tasaIva,
                    tipoIva = xitAgregar.tipoIva,
                    autoDeposito = xitAgregar.autoDeposito,
                    cantidadUnd = xitAgregar.cantidadUnd,
                    total = xitAgregar.total,
                    totalDivisa = xitAgregar.totalDivisa,
                    estatusRemision = xitAgregar.estatusRemision,
                    nombreDeposito = xitAgregar.nombreDeposito,
                },
            };
            if (ficha.itemRegistrar.itemActDeposito != null)
            {
                var xdep = ficha.itemRegistrar.itemActDeposito;
                agregarDTO.itemActDeposito = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.ItemActDeposito()
                {
                    autoDeposito = xdep.autoDeposito,
                    autoProducto = xdep.autoProducto,
                    cntActualizar = xdep.cntActualizar,
                    prdDescripcion = xdep.prdDescripcion,
                };
            }

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Item.Actualizar.Ficha()
            {
                 itemEliminar=eliminarDTO,
                 itemRegistrar=agregarDTO,
            };
            var r01 = MyData.VentaAdm_Temporal_Item_Actualizar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Id = r01.Id;

            return result;
        }
        public OOB.Resultado.Lista<OOB.Venta.Temporal.Item.Entidad.Ficha> Venta_Temporal_Item_GetLista(int idItemporal)
        {
            var rt = new OOB.Resultado.Lista<OOB.Venta.Temporal.Item.Entidad.Ficha>();

            var r01 = MyData.VentaAdm_Temporal_Item_GetLista(idItemporal);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var lst = new List<OOB.Venta.Temporal.Item.Entidad.Ficha>();
            if (r01.Lista != null) 
            {
                if (r01.Lista.Count > 0) 
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var det = new OOB.Venta.Temporal.Item.Entidad.Ficha()
                        {
                            id = s.id,
                            autoDepartamento = s.autoDepartamento,
                            autoGrupo = s.autoGrupo,
                            autoProducto = s.autoProducto,
                            autoSubGrupo = s.autoSubGrupo,
                            autoTasaIva = s.autoTasaIva,
                            cantidad = s.cantidad,
                            categroiaProducto = s.categroiaProducto,
                            codigoProducto = s.codigoProducto,
                            costo = s.costo,
                            costoPromd = s.costoPromd,
                            costoPromdUnd = s.costoPromdUnd,
                            costoUnd = s.costoUnd,
                            decimalesProducto = s.decimalesProducto,
                            dsctoPorct = s.dsctoPorct,
                            empaqueCont = s.empaqueCont,
                            empaqueDesc = s.empaqueDesc,
                            estatusPesadoProducto = s.estatusPesadoProducto,
                            estatusReservaMerc = s.estatusReservaMerc,
                            nombreProducto = s.nombreProducto,
                            notas = s.notas,
                            precioNeto = s.precioNeto,
                            precioNetoDivisa = s.precioNetoDivisa,
                            tarifaPrecio = s.tarifaPrecio,
                            tasaIva = s.tasaIva,
                            tipoIva = s.tipoIva,
                            autoDeposito = s.autoDeposito,
                            cantidadUnd = s.cantidadUnd,
                            total = s.total,
                            totalDivisa = s.totalDivisa,
                            estatusRemision = s.estatusRemision,
                            nombreDeposito= s.nombreDeposito,
                        };
                        return det;
                    }).ToList();
                }
            }
            rt.ListaD = lst;

            return rt;
        }

        //
        public OOB.Resultado.Ficha VentaAdm_Temporal_Pendiente_Dejar(OOB.Venta.Temporal.Pendiente.Dejar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Pendiente.Dejar.Ficha()
            {
                idTemporal = ficha.idTemporal,
            };
            var r01 = MyData.VentaAdm_Temporal_Pendiente_Dejar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.FichaEntidad<int> VentaAdm_Temporal_Pendiente_GetCantidadDoc(OOB.Venta.Temporal.Pendiente.Cantidad.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<int>();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Pendiente.Cantidad.Ficha()
            {
                autoSistDocumento = ficha.autoSistDocumento,
                autoUsuario = ficha.autoUsuario,
                idEquipo = ficha.idEquipo,
            };
            var r01 = MyData.VentaAdm_Temporal_Pendiente_GetCantidaDoc(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Entidad = r01.Entidad;

            return result;
        }
        public OOB.Resultado.Lista<OOB.Venta.Temporal.Pendiente.Lista.Ficha> VentaAdm_Temporal_Pendiente_GetLista(OOB.Venta.Temporal.Pendiente.Lista.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Venta.Temporal.Pendiente.Lista.Ficha>();

            var filtroDto = new DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Filtro()
            {
                autoSistDocumento = filtro.autoSistDocumento,
                autoUsuario = filtro.autoUsuario,
                idEquipo = filtro.idEquipo,
            };
            var r01 = MyData.VentaAdm_Temporal_Pendiente_GetLista(filtroDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var lst = new List<OOB.Venta.Temporal.Pendiente.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Venta.Temporal.Pendiente.Lista.Ficha()
                        {
                            ciRifCliente = s.ciRifCliente,
                            depositoNombre = s.deposito,
                            fecha = s.fecha,
                            hora = s.hora,
                            id = s.id,
                            monto = s.monto,
                            montoDivisa = s.montoDivisa,
                            nombreCliente = s.nombreCliente,
                            renglones = s.renglones,
                            sucursalNombre = s.sucursal,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = lst;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Venta.Temporal.Pendiente.Entidad.Ficha> VentaAdm_Temporal_Pendiente_Abrir(int idTemp)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Venta.Temporal.Pendiente.Entidad.Ficha>();

            var r01 = MyData.VentaAdm_Temporal_Pendiente_Abrir(idTemp);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var xe = r01.Entidad.encabezado;
            DateTime? fechaRemision = null;
            if (xe.fechaRemision != new DateTime(2000, 1, 1))
                fechaRemision = xe.fechaRemision;
            var enc = new OOB.Venta.Temporal.Encabezado.Entidad.Ficha()
            {
                autoCliente = xe.autoCliente,
                autoCobrador = xe.autoCobrador,
                autoDeposito = xe.autoDeposito,
                autoSistDocumento = xe.autoSistDocumento,
                autoSucursal = xe.autoSucursal,
                autoTransporte = xe.autoTransporte,
                autoUsuario = xe.autoUsuario,
                autoVendedor = xe.autoVendedor,
                ciRifCliente = xe.ciRifCliente,
                codigoCliente = xe.codigoCliente,
                diasCredito = xe.diasCredito,
                diasValidez = xe.diasValidez,
                dirDespacho = xe.dirDespacho,
                dirFiscalCliente = xe.dirFiscalCliente,
                estatusCredito = xe.estatusCredito,
                estatusPendiente = xe.estatusPendiente,
                factorDivisa = xe.factorDivisa,
                fecha = xe.fecha,
                hora = xe.hora,
                id = xe.id,
                idEquipo = xe.idEquipo,
                monto = xe.monto,
                montoDivisa = xe.montoDivisa,
                nombreDeposito = xe.nombreDeposito,
                nombreSistDocumento = xe.nombreSistDocumento,
                nombreSucursal = xe.nombreSucursal,
                nombreUsuario = xe.nombreUsuario,
                notasDoc = xe.notasDoc,
                razonSocialCliente = xe.razonSocialCliente,
                renglones = xe.renglones,
                tarifaPrecioCliente = xe.tarifaPrecioCliente,
                //
                autoDocRemision = xe.autoRemision,
                numeroDocRemision = xe.documentoRemision,
                codigoDocRemision = xe.tipoRemision,
                nombreDocRemision = xe.nombreTipoDocRemision,
                fechaDocRemision = fechaRemision, 
            };
            var lst = r01.Entidad.items.Select(s =>
            {
                var det = new OOB.Venta.Temporal.Item.Entidad.Ficha()
                {
                    id = s.id,
                    autoDepartamento = s.autoDepartamento,
                    autoGrupo = s.autoGrupo,
                    autoProducto = s.autoProducto,
                    autoSubGrupo = s.autoSubGrupo,
                    autoTasaIva = s.autoTasaIva,
                    cantidad = s.cantidad,
                    categroiaProducto = s.categroiaProducto,
                    codigoProducto = s.codigoProducto,
                    costo = s.costo,
                    costoPromd = s.costoPromd,
                    costoPromdUnd = s.costoPromdUnd,
                    costoUnd = s.costoUnd,
                    decimalesProducto = s.decimalesProducto,
                    dsctoPorct = s.dsctoPorct,
                    empaqueCont = s.empaqueCont,
                    empaqueDesc = s.empaqueDesc,
                    estatusPesadoProducto = s.estatusPesadoProducto,
                    estatusReservaMerc = s.estatusReservaMerc,
                    nombreProducto = s.nombreProducto,
                    notas = s.notas,
                    precioNeto = s.precioNeto,
                    precioNetoDivisa = s.precioNetoDivisa,
                    tarifaPrecio = s.tarifaPrecio,
                    tasaIva = s.tasaIva,
                    tipoIva = s.tipoIva,
                    autoDeposito = s.autoDeposito,
                    cantidadUnd = s.cantidadUnd,
                    total = s.total,
                    totalDivisa = s.totalDivisa,
                    estatusRemision = s.estatusRemision,
                    nombreDeposito=s.nombreDeposito,
                };
                return det;
            }).ToList();
            rt.Entidad = new OOB.Venta.Temporal.Pendiente.Entidad.Ficha()
            {
                Encabezado = enc,
                Items = lst,
            };

            return rt;
        }

        //
        public OOB.Resultado.Ficha VentaAdm_Temporal_Remision_Registrar(OOB.Venta.Temporal.Remision.Registrar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Remision.Registrar.Ficha()
            {
                idTemporal = ficha.idTemporal,
                autoDoc = ficha.autoDoc,
                codigoDoc = ficha.codigoDoc,
                fechaDoc = ficha.fechaDoc,
                nombreDoc = ficha.nombreDoc,
                numeroDoc = ficha.numeroDoc,
                monto = ficha.monto,
                montoDivisa = ficha.montoDivisa,
                renglones = ficha.renglones,
                item = ficha.items.Select(s =>
                {
                    var it = new DtoLibPos.VentaAdm.Temporal.Item.Registrar.ItemDetalle()
                    {
                        autoDepartamento = s.autoDepartamento,
                        autoGrupo = s.autoGrupo,
                        autoProducto = s.autoProducto,
                        autoSubGrupo = s.autoSubGrupo,
                        autoTasaIva = s.autoTasaIva,
                        cantidad = s.cantidad,
                        categroiaProducto = s.categroiaProducto,
                        codigoProducto = s.codigoProducto,
                        costo = s.costo,
                        costoPromd = s.costoPromd,
                        costoPromdUnd = s.costoPromdUnd,
                        costoUnd = s.costoUnd,
                        decimalesProducto = s.decimalesProducto,
                        dsctoPorct = s.dsctoPorct,
                        empaqueCont = s.empaqueCont,
                        empaqueDesc = s.empaqueDesc,
                        estatusPesadoProducto = s.estatusPesadoProducto,
                        estatusReservaMerc = s.estatusReservaMerc,
                        idVenta = s.idVenta,
                        nombreProducto = s.nombreProducto,
                        notas = s.notas,
                        precioNeto = s.precioNeto,
                        precioNetoDivisa = s.precioNetoDivisa,
                        tarifaPrecio = s.tarifaPrecio,
                        tasaIva = s.tasaIva,
                        tipoIva = s.tipoIva,
                        autoDeposito = s.autoDeposito,
                        cantidadUnd = s.cantidadUnd,
                        total = s.total,
                        totalDivisa = s.totalDivisa,
                        estatusRemision = s.estatusRemision,
                        nombreDeposito = s.nombreDeposito,
                    };
                    return it;
                }).ToList(),
            };

            var r01 = MyData.VentaAdm_Temporal_Remision_Registrar (fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }

        //
        public OOB.Resultado.Ficha Venta_Temporal_SetNotas(OOB.Venta.Temporal.Cambios.Notas.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Cambiar.Notas.Ficha()
            {
                id = ficha.id,
                notas = ficha.notas,
            };
            var r01 = MyData.VentaAdm_Temporal_SetNotas(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Ficha Venta_Temporal_SetTasaDivisa(OOB.Venta.Temporal.Cambios.TasaDivisa.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.VentaAdm.Temporal.Cambiar.TasaDivisa.Ficha()
            {
                id = ficha.id,
                montoDivisa = ficha.montoDivisa,
                tasaDivisa = ficha.tasaDivisa,
                items = ficha.items.Select(s => 
                {
                    var nr = new DtoLibPos.VentaAdm.Temporal.Cambiar.TasaDivisa.Item()
                    {
                        id = s.id,
                        descProducto = s.descProducto,
                        totalDivisa = s.totalDivisa,
                    };
                    return nr;
                }).ToList(),
            };
            var r01 = MyData.VentaAdm_Temporal_SetTasaDivisa(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }

    }

}