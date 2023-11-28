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
        public OOB.Resultado.Lista<OOB.Reportes.GeneralDocumento.Ficha> 
            Reportes_GeneralDocumento(OOB.Reportes.GeneralDocumento.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.GeneralDocumento.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Filtro()
            {
                codSucursal = filtro.idSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
                tipoDocFactura = filtro.tipoDocFactura,
                tipoDocNtDebito = filtro.tipoDocNtDebito,
                tipoDocNtCredito = filtro.tipoDocNtCredito,
                tipoDocNtEntrega = filtro.tipoDocNtEntrega,
            };
            var r01 = MyData.ReportesAdm_GeneralDocumento(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list= new List<OOB.Reportes.GeneralDocumento.Ficha>();
            if (r01.Lista != null) 
            {
                if (r01.Lista.Count > 0) 
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.GeneralDocumento.Ficha()
                        {
                            clienteCiRif = s.clienteCiRif,
                            clienteNombre = s.clienteNombre,
                            control = s.control,
                            documento = s.documento,
                            estatusDoc = s.estatusDoc,
                            factorDoc = s.factorDoc,
                            fecha = s.fecha,
                            montoCargo = s.montoCargo,
                            montoDscto = s.montoDscto,
                            nombreDoc = s.nombreDoc,
                            renglones = s.renglones,
                            serie = s.serie,
                            signoDoc = s.signoDoc,
                            tipoDoc = s.tipoDoc,
                            total = s.total,
                            totalDivisa = s.totalDivisa,
                            sucCodigo=s.sucCodigo,
                            sucNombre=s.sucNombre,
                            estacion=s.estacion,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD=list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.GeneralPorDepartamento.Ficha>
            Reportes_GeneralPorDepartamento(OOB.Reportes.GeneralPorDepartamento.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.GeneralPorDepartamento.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Filtro()
            {
                codSucursal = filtro.codigoSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_GeneralPorDepartamento(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.GeneralPorDepartamento.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.GeneralPorDepartamento.Ficha()
                        {
                            codDepart = s.codDepart,
                            costo = s.costo,
                            nombreDepart = s.nombreDepart,
                            venta = s.venta,
                            costoDivisa=s.costoDivisa,
                            ventaDivisa=s.ventaDivisa,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.GeneralPorGrupo.Ficha>
            Reportes_GeneralPorGrupo(OOB.Reportes.GeneralPorGrupo.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.GeneralPorGrupo.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Filtro()
            {
                codSucursal = filtro.codigoSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_GeneralPorGrupo(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.GeneralPorGrupo.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.GeneralPorGrupo.Ficha()
                        {
                            codGrupo=s.codGrupo,
                            costo = s.costo,
                            nombreGrupo=s.nombreGrupo,
                            venta = s.venta,
                            costoDivisa=s.costoDivisa,
                            ventaDivisa=s.ventaDivisa,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.Resumen.Ficha>
            Reportes_Resumen(OOB.Reportes.Resumen.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.Resumen.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.Resumen.Filtro()
            {
                codigoSucursal = filtro.codigoSucursal,
                desdeFecha = filtro.desde,
                hastaFecha = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_Resumen (filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.Resumen.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.Resumen.Ficha()
                        {
                            cntMov = s.cntMov,
                            codigoSuc = s.codigoSuc,
                            montoDivisa = s.montoDivisa,
                            montoTotal = s.montoTotal,
                            nombreSuc = s.nombreSuc,
                            signo = s.signo,
                            tipoDoc = s.tipoDoc,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.VentaPorProducto.Ficha>
            Reportes_VentaPorProducto(OOB.Reportes.VentaPorProducto.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.VentaPorProducto.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Filtro()
            {
                codigoSucursal = filtro.codigoSucursal,
                desdeFecha = filtro.desde,
                hastaFecha = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_VentaPorProducto(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.VentaPorProducto.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.VentaPorProducto.Ficha()
                        {
                            cantidad = s.cantidad,
                            codigoPrd = s.codigoPrd,
                            nombreDocumento = s.nombreDocumento,
                            nombrePrd = s.nombrePrd,
                            signo = s.signo,
                            totalMonto = s.totalMonto,
                            totalMontoDivisa = s.totalMontoDivisa,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.GeneralDocumentoDetalle.Ficha>
            Reportes_GeneralDocumentoDetalle(OOB.Reportes.GeneralDocumentoDetalle.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.GeneralDocumentoDetalle.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Filtro()
            {
                palabraClave=filtro.palabraClave,
                codigoSucursal = filtro.codigoSucursal,
                desdeFecha = filtro.desdeFecha,
                hastaFecha = filtro.hastaFecha,
                tipoDocFactura = filtro.tipoDocFactura,
                tipoDocNtDebito = filtro.tipoDocNtDebito,
                tipoDocNtCredito = filtro.tipoDocNtCredito,
                tipoDocNtEntrega = filtro.tipoDocNtEntrega,
            };
            var r01 = MyData.ReportesAdm_GeneralDocumentoDetalle(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.GeneralDocumentoDetalle.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.GeneralDocumentoDetalle.Ficha()
                        {
                            auto = s.auto,
                            cantidadUnd = s.cantidadUnd,
                            documento = s.documento,
                            fecha = s.fecha,
                            hora = s.hora,
                            nombreProducto = s.nombreProducto,
                            precioUnd = s.precioUnd,
                            renglones = s.renglones,
                            total = s.total,
                            totalRenglon = s.totalRenglon,
                            usuarioCodigo = s.usuarioCodigo,
                            usuarioNombre = s.usuarioNombre,
                            signo = s.signo,
                            documentoNombre = s.documentoNombre,
                            estacion = s.estacion,
                            sucCodigo = s.sucCodigo,
                            sucNombre = s.sucNombre,
                            ciRif = s.ciRif,
                            razonSocial = s.razonSocial,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.Consolidado.Ficha> 
            Reportes_Consolidado(OOB.Reportes.Consolidado.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.Consolidado.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Filtro()
            {
                codSucursal = filtro.codSucursal,
                desde= filtro.desde,
                hasta= filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_Consolidado(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.Consolidado.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.Consolidado.Ficha()
                        {
                            aplica = s.aplica,
                            codigoSuc = s.codigoSuc,
                            docNombre = s.docNombre,
                            documento = s.documento,
                            factor = s.factor,
                            fecha = s.fecha,
                            nombreSuc = s.nombreSuc,
                            signo = s.signo,
                            tipo = s.tipo,
                            total = s.total,
                            totalDivisa=s.totalDivisa,
                            caja=s.caja,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.Utilidad.Venta.Ficha> 
            Reportes_UtilidadVenta(OOB.Reportes.Utilidad.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.Utilidad.Venta.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro()
            {
                codSucursal = filtro.codSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_Utilidad_Venta(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.Utilidad.Venta.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.Utilidad.Venta.Ficha()
                        {
                            clienteCiRif = s.clienteCiRif,
                            clienteNombre = s.clienteNombre,
                            costoNeto = s.costoNeto,
                            documento = s.documento,
                            estacion = s.estacion,
                            factorDoc = s.factorDoc,
                            fecha = s.fecha,
                            nombreDoc = s.nombreDoc,
                            serie = s.serie,
                            signoDoc = s.signoDoc,
                            sucCodigo = s.sucCodigo,
                            sucNombre = s.sucNombre,
                            tipoDoc = s.tipoDoc,
                            utilidad = s.utilidad,
                            utilidadp = s.utilidadp,
                            ventaNeta = s.ventaNeta,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.Utilidad.Producto.Ficha> 
            Reportes_UtilidadProducto(OOB.Reportes.Utilidad.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.Utilidad.Producto.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro()
            {
                codSucursal = filtro.codSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_Utilidad_Producto(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.Utilidad.Producto.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.Utilidad.Producto.Ficha()
                        {
                            cantUnd = s.cantUnd,
                            costo = s.costo,
                            costoDivisa = s.costoDivisa,
                            prdCodigo = s.prdCodigo,
                            prdNombre = s.prdNombre,
                            venta = s.venta,
                            ventaDivisa = s.ventaDivisa,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.LibroVenta.Ficha>
            ReportesAdm_LibroVenta(OOB.Reportes.LibroVenta.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.LibroVenta.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Filtro()
            {
                anoRelacion = filtro.anoRelacion,
                mesRelacion = filtro.mesRelacion,
            };
            var r01 = MyData.ReportesAdm_LibroVenta(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.LibroVenta.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.LibroVenta.Ficha()
                        {
                            ciRifDoc = s.ciRifDoc,
                            codigoDoc = s.codigoDoc,
                            codigoSucursalDoc = s.codigoSucursalDoc,
                            fechaDoc = s.fechaDoc,
                            fechaRetencionIva = s.fechaRetencionIva,
                            montoBase1 = s.montoBase1,
                            montoBase2 = s.montoBase2,
                            montoExento = s.montoExento,
                            montoImpuesto1 = s.montoImpuesto1,
                            montoImpuesto2 = s.montoImpuesto2,
                            montoRetencionIva = s.montoRetencionIva,
                            montoTotal = s.montoTotal,
                            nombreRazonSocialDoc = s.nombreRazonSocialDoc,
                            numAplicaDoc = s.numAplicaDoc,
                            numControlDoc = s.numControlDoc,
                            numDoc = s.numDoc,
                            signoDoc = s.signoDoc,
                            tasaIva1 = s.tasaIva1,
                            tasaIva2 = s.tasaIva2,
                            tasaRetencionIva = s.tasaRetencionIva,
                            comprobanteRetencionIva = s.comprobanteRetencionIva,
                            auto = s.auto,
                            estatus = s.estatus,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.Utilidad.Consolidado.Ficha>
            Reportes_UtilidadConsolidado(OOB.Reportes.Utilidad.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.Utilidad.Consolidado.Ficha>();

            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro()
            {
                codSucursal = filtro.codSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_Utilidad_Consolidado(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Reportes.Utilidad.Consolidado.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.Utilidad.Consolidado.Ficha()
                        {
                            nombreDoc = s.nombreDoc,
                            signoDoc = s.signoDoc,
                            codigoSuc = s.codigoSuc,
                            nombreSuc = s.nombreSuc,
                            tipoDoc = s.tipoDoc,
                            vCosto = s.vCosto,
                            vUtilidad = s.vUtilidad,
                            vVenta = s.vVenta,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }
        //
        public OOB.Resultado.Lista<OOB.Reportes.Vendedor.Resumen.Ficha> 
            ReportesAdm_VentasPorVendedor_Resumen(OOB.Reportes.Vendedor.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.Vendedor.Resumen.Ficha>();
            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.Vendedor.Filtro()
            {
                codSucursal = filtro.codSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_VentasPorVendedor_Resumen(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var list = new List<OOB.Reportes.Vendedor.Resumen.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.Vendedor.Resumen.Ficha()
                        {
                            autoVend = s.autoVend,
                            cntDoc = s.cntDoc,
                            netoDivisa = s.netoDivisa,
                            netoMonLocal = s.netoMonLocal,
                            nombreVend = s.nombreVend,
                            codigoVend = s.codigoVend,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;
            return rt;
        }
        public OOB.Resultado.Lista<OOB.Reportes.Vendedor.Detallado.Ficha> 
            ReportesAdm_VentasPorVendedor_Detallado(OOB.Reportes.Vendedor.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Reportes.Vendedor.Detallado.Ficha>();
            var filtroDTO = new DtoLibPos.Reportes.VentaAdministrativa.Vendedor.Filtro()
            {
                codSucursal = filtro.codSucursal,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.ReportesAdm_VentasPorVendedor_Detallado(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var list = new List<OOB.Reportes.Vendedor.Detallado.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Reportes.Vendedor.Detallado.Ficha()
                        {
                            docFechaEmision = s.docFechaEmision,
                            docNombre = s.docNombre,
                            docNumero = s.docNumero,
                            docSigno = s.docSigno,
                            docTipo = s.docTipo,
                            razonSocial = s.razonSocial,
                            autoVend = s.autoVend,
                            netoDivisa = s.netoDivisa,
                            netoMonLocal = s.netoMonLocal,
                            nombreVend = s.nombreVend,
                            codigoVend = s.codigoVend,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;
            return rt;
        }
    }
}