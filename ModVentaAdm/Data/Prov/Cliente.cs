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
        public OOB.Resultado.Lista<OOB.Maestro.Cliente.Entidad.Ficha> 
            Cliente_GetLista(OOB.Maestro.Cliente.Lista.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Maestro.Cliente.Entidad.Ficha>();

            var filtroDto = new DtoLibPos.Cliente.Lista.Filtro()
            {
                cadena = filtro.cadena,
                preferenciaBusqueda = (DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda)filtro.metodoBusqueda,
            };
            var r01 = MyData.Cliente_GetLista(filtroDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Maestro.Cliente.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Maestro.Cliente.Entidad.Ficha()
                        {
                            id= s.auto,
                            ciRif= s.ciRif,
                            codigo= s.codigo,
                            razonSocial = s.nombre,
                            estatus=s.estatus,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }

        public OOB.Resultado.FichaEntidad<OOB.Maestro.Cliente.Entidad.Ficha> 
            Cliente_GetFicha(string autoCliente)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Maestro.Cliente.Entidad.Ficha>();

            var r01 = MyData.Cliente_GetFichaById(autoCliente);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Maestro.Cliente.Entidad.Ficha()
            {
                id = s.id,
                idGrupo = s.idGrupo,
                idEstado = s.idEstado,
                idZona = s.idZona,
                idVendedor = s.idVendedor,
                idCobrador = s.idCobrador,
                tarifa = s.tarifa,
                categoria = s.categoria,
                nivel = s.nivel,
                ciRif = s.ciRif,
                codigo = s.codigo,
                razonSocial = s.razonSocial,
                dirFiscal = s.dirFiscal,
                dirDespacho = s.dirDespacho,
                pais = s.pais,
                contacto = s.contacto,
                telefono1 = s.telefono1,
                telefono2 = s.telefono2,
                email = s.email,
                celular = s.celular,
                fax = s.fax,
                webSite = s.webSite,
                codPostal = s.codPostal,
                estatusCredito = s.estatusCredito,
                dscto = s.dscto,
                cargo = s.cargo,
                limiteDoc = s.limiteDoc,
                diasCredito = s.diasCredito,
                limiteCredito = s.limiteCredito,
                estatus = s.estatus,
                cobrador = s.cobrador,
                denFiscal = s.denFiscal,
                estado = s.estado,
                fechaAlta = s.fechaAlta,
                fechaBaja = s.fechaBaja,
                grupo = s.grupo,
                vendedor = s.vendedor,
                zona = s.zona,
                vendedorCodigo = s.vendedorCodigo,
            };
            rt.Entidad = nr;

            return rt;
        }


        public OOB.Resultado.Lista<OOB.Maestro.Cliente.Documento.Ficha>
            Cliente_Documentos_GetLista(OOB.Maestro.Cliente.Documento.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Maestro.Cliente.Documento.Ficha>();
            var xtipo = "";
            switch (filtro.tipoDoc)
            {
                case OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.Factura:
                    xtipo = "01";
                    break;
                case OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.NotaDebito:
                    xtipo = "02";
                    break;
                case OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.NotaCredito:
                    xtipo = "03";
                    break;
                case OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.NotaEntrega:
                    xtipo = "04";
                    break;
                case OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.Presupuesto:
                    xtipo = "05";
                    break;
                case OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.Pedido:
                    xtipo = "06";
                    break;
            }

            var filtroDto = new DtoLibPos.Cliente.Documento.Filtro()
            {
                autoCliente = filtro.autoCliente,
                desde = filtro.desde,
                hasta = filtro.hasta,
                tipoDoc = xtipo,
            };
            var r01 = MyData.Cliente_Documento_GetLista(filtroDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Maestro.Cliente.Documento.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var rg = new OOB.Maestro.Cliente.Documento.Ficha()
                        {
                            id = s.id,
                            codTipoDoc = s.codTipoDoc,
                            documento = s.documento,
                            estatus = s.estatus,
                            fecha = s.fecha,
                            monto = s.monto,
                            montoDivisa = s.montoDivisa,
                            serie = s.serie,
                            tasaDivisa = s.tasaDivisa,
                            nombreTipoDoc = s.nombreTipoDoc,
                        };
                        return rg;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }

        public OOB.Resultado.Lista<OOB.Maestro.Cliente.Articulos.Ficha> 
            Cliente_ArticulosVenta_GetLista(OOB.Maestro.Cliente.Articulos.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Maestro.Cliente.Articulos.Ficha>();

            var filtroDto = new DtoLibPos.Cliente.Articulos.Filtro()
            {
                autoCliente=filtro.autoCliente,
                desde = filtro.desde,
                hasta = filtro.hasta,
            };
            var r01 = MyData.Cliente_ArticuloVenta_GetLista(filtroDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Maestro.Cliente.Articulos.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var rg = new OOB.Maestro.Cliente.Articulos.Ficha()
                        {
                            cantidad = s.cantidad,
                            cantUnd = s.cantUnd,
                            codigoPrd = s.codigoPrd,
                            codTipoDoc = s.codTipoDoc,
                            nombreTipoDoc = s.nombreTipoDoc,
                            contenidoEmp = s.contenidoEmp,
                            documento = s.documento,
                            empaque = s.empaque,
                            estatus = s.estatus,
                            fecha = s.fecha,
                            nombrePrd = s.nombrePrd,
                            serie = s.serie,
                            signo = s.signo,
                            tasaCambio = s.tasaCambio,
                            precioUnd = s.precioUnd,
                        };
                        return rg;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }


        public OOB.Resultado.FichaAuto 
            Cliente_Agregar(OOB.Maestro.Cliente.Agregar.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaAuto();

            var fichaDTO = new DtoLibPos.Cliente.Agregar.Ficha()
            {
                codigoSucursalRegistro = ficha.codigoSucursalRegistro,
                codigo = ficha.codigo,
                nombre = "",
                ciRif = ficha.ciRif,
                razonSocial = ficha.razonSocial,
                autoGrupo = ficha.autoGrupo,
                dirFiscal = ficha.dirFiscal,
                dirDespacho = ficha.dirDespacho,
                contacto = ficha.contacto,
                telefono = ficha.telefono,
                email = ficha.email,
                webSite = ficha.webSite,
                pais = ficha.pais,
                denominacionFiscal = ficha.denominacionFiscal,
                autoEstado = ficha.autoEstado,
                autoZona = ficha.autoZona,
                codigoPostal = ficha.codigoPostal,
                retencionIva = ficha.retencionIva,
                retencionIslr = ficha.retencionIslr,
                autoVendedor = ficha.autoVendedor,

                tarifa = ficha.tarifa,
                descuento = ficha.descuento,
                recargo = ficha.recargo,
                estatusCredito = ficha.estatusCredito,
                diasCredito = ficha.diasCredito,
                limiteCredito = ficha.limiteCredito,
                docPendientes = ficha.docPendientes,
                estatusMorosidad = ficha.estatusMorosidad,
                estatusLunes = ficha.estatusLunes,
                estatusMartes = ficha.estatusMartes,
                estatusMiercoles = ficha.estatusMiercoles,
                estatusJueves = ficha.estatusJueves,
                estatusViernes = ficha.estatusViernes,
                estatusSabado = ficha.estatusSabado,
                estatusDomingo = ficha.estatusDomingo,
                autoCobrador = ficha.autoCobrador,
                anticipos = ficha.anticipos,
                debitos = ficha.debitos,
                creditos = ficha.creditos,
                saldo = ficha.saldo,
                disponible = ficha.disponible,

                memo = ficha.memo,
                aviso = ficha.aviso,
                estatus = ficha.estatus,
                cuenta = ficha.cuenta,
                iban = ficha.iban,
                swit = ficha.swit,
                autoAgencia = ficha.autoAgencia,
                dirBanco = ficha.dirBanco,
                autoCodigoCobrar = ficha.autoCodigoCobrar,
                autoCodigoIngreso = ficha.autoCodigoIngreso,
                autoCodigoAnticipos = ficha.autoCodigoAnticipos,
                categoria = ficha.categoria,
                descuentoProntoPago = ficha.descuentoProntoPago,
                importeUltPago = ficha.importeUltPago,
                importeUltVenta = ficha.importeUltVenta,
                telefono2 = ficha.telefono2,
                fax = ficha.fax,
                celular = ficha.celular,

                abc = ficha.abc,
                montoClasificacion = ficha.montoClasificacion,
            };
            var r01 = MyData.Cliente_Agregar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            result.Auto = r01.Auto;
            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Maestro.Cliente.Editar.ObtenerData.Ficha> 
            Cliente_Editar_GetFicha(string autoCli)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Maestro.Cliente.Editar.ObtenerData.Ficha>();

            var r01 = MyData.Cliente_Editar_GetFicha(autoCli);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Maestro.Cliente.Editar.ObtenerData.Ficha()
            {
                idGrupo = s.idGrupo,
                idEstado = s.idEstado,
                idZona = s.idZona,
                idVendedor = s.idVendedor,
                idCobrador = s.idCobrador ,
                tarifa = s.tarifa,
                categoria = s.categoria,
                nivel = s.nivel,
                ciRif = s.ciRif,
                codigo = s.codigo,
                razonSocial = s.razonSocial,
                dirFiscal = s.dirFiscal,
                dirDespacho = s.dirDespacho,
                pais = s.pais,
                contacto = s.contacto,
                telefono1 = s.telefono1,
                telefono2 = s.telefono2,
                email = s.email,
                celular = s.celular,
                fax = s.fax,
                webSite = s.webSite,
                codPostal = s.codPostal,
                estatusCredito = s.estatusCredito,
                dscto = s.dscto,
                cargo = s.cargo,
                limiteDoc = s.limiteDoc,
                diasCredito = s.diasCredito,
                limiteCredito = s.limiteCredito,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.Ficha 
            Cliente_Editar(OOB.Maestro.Cliente.Editar.Actualizar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.Cliente.Editar.Actualizar.Ficha()
            {
                autoId=ficha.idAuto,
                idGrupo = ficha.idGrupo,
                idEstado = ficha.idEstado,
                idZona = ficha.idZona,
                idVendedor = ficha.idVendedor,
                idCobrador = ficha.idCobrador,
                tarifa = ficha.tarifa,
                categoria = ficha.categoria,
                nivel = ficha.nivel,
                ciRif = ficha.ciRif,
                codigo = ficha.codigo,
                razonSocial = ficha.razonSocial,
                dirFiscal = ficha.dirFiscal,
                dirDespacho = ficha.dirDespacho,
                pais = ficha.pais,
                contacto = ficha.contacto,
                telefono1 = ficha.telefono1,
                telefono2 = ficha.telefono2,
                email = ficha.email,
                celular = ficha.celular,
                fax = ficha.fax,
                webSite = ficha.webSite,
                codPostal = ficha.codPostal,
                estatusCredito = ficha.estatusCredito,
                dscto = ficha.dscto,
                cargo = ficha.cargo,
                limiteDoc = ficha.limiteDoc,
                diasCredito = ficha.diasCredito,
                limiteCredito = ficha.limiteCredito,
            };
            var r01 = MyData.Cliente_Editar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Ficha 
            Cliente_Activar(OOB.Maestro.Cliente.EstatusActivarInactivar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.Cliente.EstatusActivarInactivar.Ficha()
            {
                autoId = ficha.autoId,
            };
            var r01 = MyData.Cliente_Activar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        public OOB.Resultado.Ficha 
            Cliente_Inactivar(OOB.Maestro.Cliente.EstatusActivarInactivar.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();

            var fichaDTO = new DtoLibPos.Cliente.EstatusActivarInactivar.Ficha()
            {
                autoId = ficha.autoId,
            };
            var r01 = MyData.Cliente_Inactivar(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            return result;
        }
        //
        public OOB.Resultado.Lista<OOB.Maestro.Cliente.Lista.Ficha> 
            Cliente_GetLista_Resumen(string filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Maestro.Cliente.Lista.Ficha>();
            //
            var r01 = MyData.Cliente_GetLista_Resumen(filtro);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var list = new List<OOB.Maestro.Cliente.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Maestro.Cliente.Lista.Ficha()
                        {
                            id = s.auto,
                            ciRif = s.ciRif,
                            codigo = s.codigo,
                            nombre = s.nombre,
                            estatus = s.estatus,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;
            //
            return rt;
        }
    }
}