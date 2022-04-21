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

        public OOB.Resultado.Lista<OOB.Documento.Lista.Ficha> Documento_Get_Lista(OOB.Documento.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Documento.Lista.Ficha>();

            var xestatus = DtoLibPos.Documento.Lista.Filtro.enumEstatus.SinDefinir;
            switch (filtro.estatus.Trim().ToUpper()) 
            {
                case "ACTIVO":
                    xestatus = DtoLibPos.Documento.Lista.Filtro.enumEstatus.Activo;
                    break;
                case "ANULADO":
                    xestatus = DtoLibPos.Documento.Lista.Filtro.enumEstatus.Anulado;
                    break;
                default:
                    break;
            }

            var filtroDTO = new DtoLibPos.Documento.Lista.Filtro()
            {
                idArqueo = filtro.idArqueo,
                idCliente = filtro.idCliente,
                idProducto = filtro.idProducto,
                codSucursal = filtro.codSucursal,
                codTipoDocumento = filtro.codTipoDocumento,
                fecha = new DtoLibPos.Documento.Lista.Filtro.Fecha() { desde = filtro.desde, hasta = filtro.hasta },
                estatus = xestatus,
                palabraClave = filtro.palabraClave,
            };
            var r01 = MyData.Documento_Get_Lista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Documento.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Documento.Lista.Ficha()
                        {
                            CiRif = s.CiRif,
                            Control = s.Control,
                            DocCodigo = s.DocCodigo,
                            DocNombre = s.DocNombre,
                            DocNumero = s.DocNumero,
                            DocSigno = s.DocSigno,
                            Estatus = s.Estatus,
                            FechaEmision = s.FechaEmision,
                            HoraEmision = s.HoraEmision,
                            Id = s.Id,
                            Monto = s.Monto,
                            NombreRazonSocial = s.NombreRazonSocial,
                            Renglones = s.Renglones,
                            Serie = s.Serie,
                            MontoDivisa = s.MontoDivisa,
                            DocAplica = s.DocAplica,
                            DocSituacion = s.DocSituacion,
                            SucursalCod = s.SucursalCod,
                            SucursalDesc = s.SucursalDesc,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Documento.Entidad.Ficha> Documento_GetById(string idAuto)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Documento.Entidad.Ficha>();

            var r01 = MyData.Documento_GetById(idAuto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var s = r01.Entidad;
            var nr = new OOB.Documento.Entidad.Ficha()
            {
                AnoRelacion = s.AnoRelacion,
                AnticipoIva = s.AnticipoIva,
                Aplica = s.Aplica,
                Auto = s.Auto,
                AutoCliente = s.AutoCliente,
                AutoRemision = s.AutoRemision,
                AutoTransporte = s.AutoTransporte,
                AutoUsuario = s.AutoUsuario,
                AutoVendedor = s.AutoVendedor,
                Base1 = s.Base1,
                Base2 = s.Base2,
                Base3 = s.Base3,
                Cambio = s.Cambio,
                Cargos = s.Cargos,
                Cargosp = s.Cargosp,
                CiBeneficiario = s.CiBeneficiario,
                Cierre = s.Cierre,
                CierreFtp = s.CierreFtp,
                CiRif = s.CiRif,
                CiTitular = s.CiTitular,
                Clave = s.Clave,
                CodigoCliente = s.CodigoCliente,
                CodigoSucursal = s.CodigoSucursal,
                CodigoTransporte = s.CodigoTransporte,
                CodigoUsuario = s.CodigoUsuario,
                CodigoVendedor = s.CodigoVendedor,
                Columna = s.Columna,
                ComprobanteRetencion = s.ComprobanteRetencion,
                ComprobanteRetencionIslr = s.ComprobanteRetencionIslr,
                CondicionPago = s.CondicionPago,
                Control = s.Control,
                Costo = s.Costo,
                DenominacionFiscal = s.DenominacionFiscal,
                Descuento1 = s.Descuento1,
                Descuento1p = s.Descuento1p,
                Descuento2 = s.Descuento2,
                Descuento2p = s.Descuento2p,
                Despachado = s.Despachado,
                Dias = s.Dias,
                DiasValidez = s.DiasValidez,
                DirDespacho = s.DirDespacho,
                DirFiscal = s.DirFiscal,
                DocumentoNombre = s.DocumentoNombre,
                DocumentoNro = s.DocumentoNro,
                DocumentoRemision = s.DocumentoRemision,
                DocumentoTipo = s.DocumentoTipo,
                Estacion = s.Estacion,
                EstatusAnulado = s.EstatusAnulado,
                EstatusCierreContable = s.EstatusCierreContable,
                EstatusValidado = s.EstatusValidado,
                Exento = s.Exento,
                Expendiente = s.Expendiente,
                FactorCambio = s.FactorCambio,
                Fecha = s.Fecha,
                FechaPedido = s.FechaPedido,
                FechaVencimiento = s.FechaVencimiento,
                Hora = s.Hora,
                Impuesto = s.Impuesto,
                Impuesto1 = s.Impuesto1,
                Impuesto2 = s.Impuesto2,
                Impuesto3 = s.Impuesto3,
                MBase = s.MBase,
                MesRelacion = s.MesRelacion,
                MontoDivisa = s.MontoDivisa,
                Neto = s.Neto,
                NombreBeneficiario = s.NombreBeneficiario,
                NombreTitular = s.NombreTitular,
                Nota = s.Nota,
                OrdenCompra = s.OrdenCompra,
                Pedido = s.Pedido,
                Planilla = s.Planilla,
                Prefijo = s.Prefijo,
                RazonSocial = s.RazonSocial,
                Renglones = s.Renglones,
                RetencionIslr = s.RetencionIslr,
                RetencionIva = s.RetencionIva,
                SaldoPendiente = s.SaldoPendiente,
                Serie = s.Serie,
                Signo = s.Signo,
                Situacion = s.Situacion,
                SubTotal = s.SubTotal,
                SubTotalImpuesto = s.SubTotalImpuesto,
                SubTotalNeto = s.SubTotalNeto,
                Tarifa = s.Tarifa,
                Tasa1 = s.Tasa1,
                Tasa2 = s.Tasa2,
                Tasa3 = s.Tasa3,
                TasaRetencionIslr = s.TasaRetencionIslr,
                TasaRetencionIva = s.TasaRetencionIva,
                Telefono = s.Telefono,
                TercerosIva = s.TercerosIva,
                Tipo = s.Tipo,
                TipoCliente = s.TipoCliente,
                TipoRemision = s.TipoRemision,
                Total = s.Total,
                Transporte = s.Transporte,
                Usuario = s.Usuario,
                Utilidad = s.Utilidad,
                Utilidadp = s.Utilidadp,
                Vendedor = s.Vendedor,
                AutoDocCxC = s.AutoDocCxC,
                AutoReciboCxC = s.AutoReciboCxC,
                items = s.items.Select(ss =>
                {
                    var xr = new OOB.Documento.Entidad.FichaItem()
                    {
                        EstatusPesado = ss.EstatusPesado,
                        AutoCliente = ss.AutoCliente,
                        AutoDepartamento = ss.AutoDepartamento,
                        AutoDeposito = ss.AutoDeposito,
                        AutoGrupo = ss.AutoGrupo,
                        AutoProducto = ss.AutoProducto,
                        AutoSubGrupo = ss.AutoSubGrupo,
                        AutoTasa = ss.AutoTasa,
                        AutoVendedor = ss.AutoVendedor,
                        Cantidad = ss.Cantidad,
                        CantidadUnd = ss.CantidadUnd,
                        Categoria = ss.Categoria,
                        CierreFtp = ss.CierreFtp,
                        Cobranza = ss.Cobranza,
                        Cobranzap = ss.Cobranzap,
                        CobranzapVendedor = ss.CobranzapVendedor,
                        CobranzaVendedor = ss.CobranzaVendedor,
                        Codigo = ss.Codigo,
                        CodigoDeposito = ss.CodigoDeposito,
                        CodigoVendedor = ss.CodigoVendedor,
                        ContenidoEmpaque = ss.ContenidoEmpaque,
                        Corte = ss.Corte,
                        CostoCompra = ss.CostoCompra,
                        CostoPromedioUnd = ss.CostoPromedioUnd,
                        CostoUnd = ss.CostoUnd,
                        CostoVenta = ss.CostoVenta,
                        Decimales = ss.Decimales,
                        Deposito = ss.Deposito,
                        Descuento1 = ss.Descuento1,
                        Descuento1p = ss.Descuento1p,
                        Descuento2 = ss.Descuento2,
                        Descuento2p = ss.Descuento2p,
                        Descuento3 = ss.Descuento3,
                        Descuento3p = ss.Descuento3p,
                        Detalle = ss.Detalle,
                        DiasGarantia = ss.DiasGarantia,
                        Empaque = ss.Empaque,
                        EstatusAnulado = ss.EstatusAnulado,
                        EstatusChecked = ss.EstatusChecked,
                        EstatusCorte = ss.EstatusCorte,
                        EstatusGarantia = ss.EstatusGarantia,
                        EstatusSerial = ss.EstatusSerial,
                        Impuesto = ss.Impuesto,
                        Nombre = ss.Nombre,
                        PrecioFinal = ss.PrecioFinal,
                        PrecioItem = ss.PrecioItem,
                        PrecioNeto = ss.PrecioNeto,
                        PrecioSugerido = ss.PrecioSugerido,
                        PrecioUnd = ss.PrecioUnd,
                        Signo = ss.Signo,
                        Tarifa = ss.Tarifa,
                        Tasa = ss.Tasa,
                        Tipo = ss.Tipo,
                        Total = ss.Total,
                        TotalDescuento = ss.TotalDescuento,
                        TotalNeto = ss.TotalNeto,
                        Utilidad = ss.Utilidad,
                        Utilidadp = ss.Utilidadp,
                        Ventas = ss.Ventas,
                        Ventasp = ss.Ventasp,
                        VentaspVendedor = ss.VentaspVendedor,
                        VentasVendedor = ss.VentasVendedor,
                        X = ss.X,
                        Y = ss.Y,
                        Z = ss.Z,
                    };
                    return xr;
                }).ToList(),
            };
            result.Entidad = nr;

            return result;
        }
        //

        public OOB.Resultado.FichaAuto Documento_Agregar_Presupuesto(OOB.Documento.Agregar.Presupuesto.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaAuto();

            var fichaDTO = new DtoLibPos.DocumentoAdm.Agregar.Presupuesto.Ficha()
            {
                RazonSocial = ficha.RazonSocial,
                DirFiscal = ficha.DirFiscal,
                CiRif = ficha.CiRif,
                Tipo = ficha.CodigoTipoDoc,
                Exento = ficha.Exento,
                Base1 = ficha.Base1,
                Base2 = ficha.Base2,
                Base3 = ficha.Base3,
                Impuesto1 = ficha.Impuesto1,
                Impuesto2 = ficha.Impuesto2,
                Impuesto3 = ficha.Impuesto3,
                MBase = ficha.MBase,
                Impuesto = ficha.Impuesto,
                Total = ficha.Total,
                Tasa1 = ficha.Tasa1,
                Tasa2 = ficha.Tasa2,
                Tasa3 = ficha.Tasa3,
                Nota = ficha.Nota,
                TasaRetencionIva = ficha.TasaRetencionIva,
                TasaRetencionIslr = ficha.TasaRetencionIslr,
                RetencionIva = ficha.TasaRetencionIva,
                RetencionIslr = ficha.RetencionIslr,
                AutoCliente = ficha.AutoCliente,
                CodigoCliente = ficha.CodigoCliente,
                Control = ficha.Control,
                OrdenCompra = ficha.OrdenCompra,
                Dias = ficha.Dias,
                Descuento1 = ficha.Descuento1,
                Descuento2 = ficha.Descuento2,
                Cargos = ficha.Cargos,
                Descuento1p = ficha.Descuento1p,
                Descuento2p = ficha.Descuento2p,
                Cargosp = ficha.Cargosp,
                Columna = ficha.Columna,
                EstatusAnulado = ficha.EstatusAnulado,
                Aplica = ficha.Aplica,
                ComprobanteRetencion = ficha.ComprobanteRetencion,
                SubTotalNeto = ficha.SubTotalNeto,
                Telefono = ficha.Telefono,
                FactorCambio = ficha.FactorCambio,
                CodigoVendedor = ficha.CodigoVendedor,
                Vendedor = ficha.Vendedor,
                AutoVendedor = ficha.AutoVendedor,
                Pedido = ficha.Pedido,
                CondicionPago = ficha.CondicionPago,
                Usuario = ficha.Usuario,
                CodigoUsuario = ficha.CodigoUsuario,
                CodigoSucursal = ficha.CodigoSucursal,
                Transporte = ficha.Transporte,
                CodigoTransporte = ficha.CodigoTransporte,
                MontoDivisa = ficha.MontoDivisa,
                Despachado = ficha.Despachado,
                DirDespacho = ficha.DirDespacho,
                Estacion = ficha.Estacion,
                Renglones = ficha.Renglones,
                SaldoPendiente = ficha.SaldoPendiente,
                ComprobanteRetencionIslr = ficha.ComprobanteRetencionIslr,
                DiasValidez = ficha.DiasValidez,
                AutoUsuario = ficha.AutoUsuario,
                AutoTransporte = ficha.AutoTransporte,
                Situacion = ficha.Situacion,
                Signo = ficha.SignoTipoDoc,
                Serie = ficha.SiglasTipoDoc,
                Tarifa = ficha.Tarifa,
                TipoRemision = ficha.TipoRemision,
                DocumentoRemision = ficha.DocumentoRemision,
                AutoRemision = ficha.AutoRemision,
                DocumentoNombre = ficha.NombreTipoDoc,
                SubTotalImpuesto = ficha.SubTotalImpuesto,
                SubTotal = ficha.SubTotal,
                TipoCliente = ficha.TipoCliente,
                Planilla = ficha.Planilla,
                Expendiente = ficha.Expendiente,
                AnticipoIva = ficha.AnticipoIva,
                TercerosIva = ficha.TercerosIva,
                Neto = ficha.Neto,
                Costo = ficha.Costo,
                Utilidad = ficha.Utilidad,
                Utilidadp = ficha.Utilidadp,
                DocumentoTipo = ficha.TipoTipoDoc,
                CiTitular = ficha.CiTitular,
                NombreTitular = ficha.NombreTitular,
                CiBeneficiario = ficha.CiBeneficiario,
                NombreBeneficiario = ficha.NombreBeneficiario,
                Clave = ficha.Clave,
                DenominacionFiscal = ficha.DenominacionFiscal,
                Cambio = ficha.Cambio,
                Cierre = ficha.Cierre,
                CierreFtp = ficha.CierreFtp,
                EstatusCierreContable = ficha.EstatusCierreContable,
                EstatusValidado = ficha.EstatusValidado,
                FechaPedido = ficha.FechaPedido,
                Prefijo = ficha.Prefijo,
            };
            var detalles = ficha.Detalles.Select(s =>
            {
                var nr = new DtoLibPos.DocumentoAdm.Agregar.Presupuesto.FichaDetalle()
                {
                    AutoProducto = s.AutoProducto,
                    Codigo = s.Codigo,
                    Nombre = s.Nombre,
                    AutoDepartamento = s.AutoDepartamento,
                    AutoGrupo = s.AutoGrupo,
                    AutoSubGrupo = s.AutoSubGrupo,
                    AutoDeposito = s.AutoDeposito,
                    Cantidad = s.Cantidad,
                    Empaque = s.Empaque,
                    PrecioNeto = s.PrecioNeto,
                    Descuento1p = s.Descuento1p,
                    Descuento2p = s.Descuento2p,
                    Descuento3p = s.Descuento3p,
                    Descuento1 = s.Descuento1,
                    Descuento2 = s.Descuento2,
                    Descuento3 = s.Descuento3,
                    CostoVenta = s.CostoVenta,
                    TotalNeto = s.TotalNeto,
                    Tasa = s.Tasa,
                    Impuesto = s.Impuesto,
                    Total = s.Total,
                    EstatusAnulado = s.EstatusAnulado,
                    Tipo = s.Tipo,
                    Deposito = s.Deposito,
                    Signo = s.Signo,
                    PrecioFinal = s.PrecioFinal,
                    AutoCliente = s.AutoCliente,
                    Decimales = s.Decimales,
                    ContenidoEmpaque = s.ContenidoEmpaque,
                    CantidadUnd = s.CantidadUnd,
                    PrecioUnd = s.PrecioUnd,
                    CostoUnd = s.CostoUnd,
                    Utilidad = s.Utilidad,
                    Utilidadp = s.Utilidadp,
                    PrecioItem = s.PrecioItem,
                    EstatusGarantia = s.EstatusGarantia,
                    EstatusSerial = s.EstatusSerial,
                    CodigoDeposito = s.CodigoDeposito,
                    DiasGarantia = s.DiasGarantia,
                    Detalle = s.Detalle,
                    PrecioSugerido = s.PrecioSugerido,
                    AutoTasa = s.AutoTasa,
                    EstatusCorte = s.EstatusCorte,
                    X = s.X,
                    Y = s.Y,
                    Z = s.Z,
                    Corte = s.Corte,
                    Categoria = s.Categoria,
                    Cobranzap = s.Cobranzap,
                    Ventasp = s.Ventasp,
                    CobranzapVendedor = s.CobranzapVendedor,
                    VentaspVendedor = s.VentaspVendedor,
                    Cobranza = s.Cobranza,
                    Ventas = s.Ventas,
                    CobranzaVendedor = s.CobranzaVendedor,
                    VentasVendedor = s.VentasVendedor,
                    CostoPromedioUnd = s.CostoPromedioUnd,
                    CostoCompra = s.CostoCompra,
                    EstatusChecked = s.EstatusChecked,
                    Tarifa = s.Tarifa,
                    TotalDescuento = s.TotalDescuento,
                    CodigoVendedor = s.CodigoVendedor,
                    AutoVendedor = s.AutoVendedor,
                };
                return nr;
            }).ToList();
            fichaDTO.Detalles = detalles;
            fichaDTO.VentaTemporal = new DtoLibPos.DocumentoAdm.Agregar.Presupuesto.FichaTemporalVenta() { id = ficha.VentaTemporal.id, };

            var r01 = MyData.DocumentoAdm_Agregar_Presupuesto(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Auto = r01.Auto;

            return result;
        }
        public OOB.Resultado.Ficha Documento_Anular_Presupuesto(OOB.Documento.Anular.Presupuesto.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.DocumentoAdm.Anular.Prersupuesto.Ficha()
            {
                autoDocumento = ficha.autoDocumento,
                auditoria = new DtoLibPos.DocumentoAdm.Anular.Prersupuesto.FichaAuditoria()
                {
                    autoSistemaDocumento = ficha.auditoria.autoSistemaDocumento,
                    autoUsuario = ficha.auditoria.autoUsuario,
                    codigo = ficha.auditoria.codigo,
                    estacion = ficha.auditoria.estacion,
                    motivo = ficha.auditoria.motivo,
                    usuario = ficha.auditoria.usuario,
                },
            };
            var r01 = MyData.DocumentoAdm_Anular_Presupuesto(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }

        public OOB.Resultado.FichaAuto Documento_Agregar_Pedido(OOB.Documento.Agregar.Pedido.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaAuto();

            var fichaDTO = new DtoLibPos.DocumentoAdm.Agregar.Pedido.Ficha();
            var fichaEnc = ficha.Encabezado;
            var Encabezado= new DtoLibPos.DocumentoAdm.Agregar.Pedido.FichaEncabezado()
            {
                RazonSocial = fichaEnc.RazonSocial,
                DirFiscal = fichaEnc.DirFiscal,
                CiRif = fichaEnc.CiRif,
                Tipo = fichaEnc.CodigoTipoDoc,
                Exento = fichaEnc.Exento,
                Base1 = fichaEnc.Base1,
                Base2 = fichaEnc.Base2,
                Base3 = fichaEnc.Base3,
                Impuesto1 = fichaEnc.Impuesto1,
                Impuesto2 = fichaEnc.Impuesto2,
                Impuesto3 = fichaEnc.Impuesto3,
                MBase = fichaEnc.MBase,
                Impuesto = fichaEnc.Impuesto,
                Total = fichaEnc.Total,
                Tasa1 = fichaEnc.Tasa1,
                Tasa2 = fichaEnc.Tasa2,
                Tasa3 = fichaEnc.Tasa3,
                Nota = fichaEnc.Nota,
                TasaRetencionIva = fichaEnc.TasaRetencionIva,
                TasaRetencionIslr = fichaEnc.TasaRetencionIslr,
                RetencionIva = fichaEnc.TasaRetencionIva,
                RetencionIslr = fichaEnc.RetencionIslr,
                AutoCliente = fichaEnc.AutoCliente,
                CodigoCliente = fichaEnc.CodigoCliente,
                Control = fichaEnc.Control,
                OrdenCompra = fichaEnc.OrdenCompra,
                Dias = fichaEnc.Dias,
                Descuento1 = fichaEnc.Descuento1,
                Descuento2 = fichaEnc.Descuento2,
                Cargos = fichaEnc.Cargos,
                Descuento1p = fichaEnc.Descuento1p,
                Descuento2p = fichaEnc.Descuento2p,
                Cargosp = fichaEnc.Cargosp,
                Columna = fichaEnc.Columna,
                EstatusAnulado = fichaEnc.EstatusAnulado,
                Aplica = fichaEnc.Aplica,
                ComprobanteRetencion = fichaEnc.ComprobanteRetencion,
                SubTotalNeto = fichaEnc.SubTotalNeto,
                Telefono = fichaEnc.Telefono,
                FactorCambio = fichaEnc.FactorCambio,
                CodigoVendedor = fichaEnc.CodigoVendedor,
                Vendedor = fichaEnc.Vendedor,
                AutoVendedor = fichaEnc.AutoVendedor,
                Pedido = fichaEnc.Pedido,
                CondicionPago = fichaEnc.CondicionPago,
                Usuario = fichaEnc.Usuario,
                CodigoUsuario = fichaEnc.CodigoUsuario,
                CodigoSucursal = fichaEnc.CodigoSucursal,
                Transporte = fichaEnc.Transporte,
                CodigoTransporte = fichaEnc.CodigoTransporte,
                MontoDivisa = fichaEnc.MontoDivisa,
                Despachado = fichaEnc.Despachado,
                DirDespacho = fichaEnc.DirDespacho,
                Estacion = fichaEnc.Estacion,
                Renglones = fichaEnc.Renglones,
                SaldoPendiente = fichaEnc.SaldoPendiente,
                ComprobanteRetencionIslr = fichaEnc.ComprobanteRetencionIslr,
                DiasValidez = fichaEnc.DiasValidez,
                AutoUsuario = fichaEnc.AutoUsuario,
                AutoTransporte = fichaEnc.AutoTransporte,
                Situacion = fichaEnc.Situacion,
                Signo = fichaEnc.SignoTipoDoc,
                Serie = fichaEnc.SiglasTipoDoc,
                Tarifa = fichaEnc.Tarifa,
                TipoRemision = fichaEnc.TipoRemision,
                DocumentoRemision = fichaEnc.DocumentoRemision,
                AutoRemision = fichaEnc.AutoRemision,
                DocumentoNombre = fichaEnc.NombreTipoDoc,
                SubTotalImpuesto = fichaEnc.SubTotalImpuesto,
                SubTotal = fichaEnc.SubTotal,
                TipoCliente = fichaEnc.TipoCliente,
                Planilla = fichaEnc.Planilla,
                Expendiente = fichaEnc.Expendiente,
                AnticipoIva = fichaEnc.AnticipoIva,
                TercerosIva = fichaEnc.TercerosIva,
                Neto = fichaEnc.Neto,
                Costo = fichaEnc.Costo,
                Utilidad = fichaEnc.Utilidad,
                Utilidadp = fichaEnc.Utilidadp,
                DocumentoTipo = fichaEnc.TipoTipoDoc,
                CiTitular = fichaEnc.CiTitular,
                NombreTitular = fichaEnc.NombreTitular,
                CiBeneficiario = fichaEnc.CiBeneficiario,
                NombreBeneficiario = fichaEnc.NombreBeneficiario,
                Clave = fichaEnc.Clave,
                DenominacionFiscal = fichaEnc.DenominacionFiscal,
                Cambio = fichaEnc.Cambio,
                Cierre = fichaEnc.Cierre,
                CierreFtp = fichaEnc.CierreFtp,
                EstatusCierreContable = fichaEnc.EstatusCierreContable,
                EstatusValidado = fichaEnc.EstatusValidado,
                FechaPedido = fichaEnc.FechaPedido,
                Prefijo = fichaEnc.Prefijo,
            };
            fichaDTO.Encabezado = Encabezado;
            var detalles = ficha.Detalles.Select(s =>
            {
                var nr = new DtoLibPos.DocumentoAdm.Agregar.Pedido.FichaDetalle()
                {
                    AutoProducto = s.AutoProducto,
                    Codigo = s.Codigo,
                    Nombre = s.Nombre,
                    AutoDepartamento = s.AutoDepartamento,
                    AutoGrupo = s.AutoGrupo,
                    AutoSubGrupo = s.AutoSubGrupo,
                    AutoDeposito = s.AutoDeposito,
                    Cantidad = s.Cantidad,
                    Empaque = s.Empaque,
                    PrecioNeto = s.PrecioNeto,
                    Descuento1p = s.Descuento1p,
                    Descuento2p = s.Descuento2p,
                    Descuento3p = s.Descuento3p,
                    Descuento1 = s.Descuento1,
                    Descuento2 = s.Descuento2,
                    Descuento3 = s.Descuento3,
                    CostoVenta = s.CostoVenta,
                    TotalNeto = s.TotalNeto,
                    Tasa = s.Tasa,
                    Impuesto = s.Impuesto,
                    Total = s.Total,
                    EstatusAnulado = s.EstatusAnulado,
                    Tipo = s.Tipo,
                    Deposito = s.Deposito,
                    Signo = s.Signo,
                    PrecioFinal = s.PrecioFinal,
                    AutoCliente = s.AutoCliente,
                    Decimales = s.Decimales,
                    ContenidoEmpaque = s.ContenidoEmpaque,
                    CantidadUnd = s.CantidadUnd,
                    PrecioUnd = s.PrecioUnd,
                    CostoUnd = s.CostoUnd,
                    Utilidad = s.Utilidad,
                    Utilidadp = s.Utilidadp,
                    PrecioItem = s.PrecioItem,
                    EstatusGarantia = s.EstatusGarantia,
                    EstatusSerial = s.EstatusSerial,
                    CodigoDeposito = s.CodigoDeposito,
                    DiasGarantia = s.DiasGarantia,
                    Detalle = s.Detalle,
                    PrecioSugerido = s.PrecioSugerido,
                    AutoTasa = s.AutoTasa,
                    EstatusCorte = s.EstatusCorte,
                    X = s.X,
                    Y = s.Y,
                    Z = s.Z,
                    Corte = s.Corte,
                    Categoria = s.Categoria,
                    Cobranzap = s.Cobranzap,
                    Ventasp = s.Ventasp,
                    CobranzapVendedor = s.CobranzapVendedor,
                    VentaspVendedor = s.VentaspVendedor,
                    Cobranza = s.Cobranza,
                    Ventas = s.Ventas,
                    CobranzaVendedor = s.CobranzaVendedor,
                    VentasVendedor = s.VentasVendedor,
                    CostoPromedioUnd = s.CostoPromedioUnd,
                    CostoCompra = s.CostoCompra,
                    EstatusChecked = s.EstatusChecked,
                    Tarifa = s.Tarifa,
                    TotalDescuento = s.TotalDescuento,
                    CodigoVendedor = s.CodigoVendedor,
                    AutoVendedor = s.AutoVendedor,
                };
                return nr;
            }).ToList();
            fichaDTO.Detalles = detalles;
            var itemsBLoquear = ficha.ItemDepositoBloquear.Select(s =>
                {
                    var nr = new DtoLibPos.DocumentoAdm.Agregar.Pedido.FichaItemDepositoBloquear()
                    {
                        autoDeposito = s.autoDeposito,
                        autoProducto = s.autoProducto,
                        cntUnd = s.cntUnd,
                        depDescripcion = s.depDescripcion,
                        prdDescripcion = s.prdDescripcion,
                    };
                    return nr;
                }).ToList();
            fichaDTO.ItemDepositoBloquear = itemsBLoquear;
            fichaDTO.VentaTemporal = new DtoLibPos.DocumentoAdm.Agregar.Pedido.FichaTemporalVenta() { id = ficha.VentaTemporal.id, };
            fichaDTO.ValidarRupturaPorExistencia = ficha.ValidarRupturaPorExistencia;

            var r01 = MyData.DocumentoAdm_Agregar_Pedido(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            result.Auto = r01.Auto;

            return result;
        }

    }

}