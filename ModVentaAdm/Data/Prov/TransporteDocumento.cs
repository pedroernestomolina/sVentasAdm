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
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>
            TransporteDocumento_AgregarPresupuesto(OOB.Transporte.Documento.Agregar.Presupuesto.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.Agregar.Resultado>();
            var fichaDTO = new DtoTransporte.Documento.Agregar.Presupuesto.Ficha()
            {
                cargos = ficha.cargos,
                cargosp = ficha.cargosp,
                cntRenglones = ficha.cntRenglones,
                codCliente = ficha.codCliente,
                codSucursal = ficha.codSucursal,
                codUsuario = ficha.codUsuario,
                codVendedor = ficha.codVendedor,
                condPago = ficha.condPago,
                control = ficha.control,
                descuento1 = ficha.descuento1,
                descuento1p = ficha.descuento1p,
                descuento2 = ficha.descuento2,
                descuento2p = ficha.descuento2p,
                diasCredito = ficha.diasCredito,
                diasValidez = ficha.diasValidez,
                docCodigo = ficha.docCodigo,
                docNombre = ficha.docNombre,
                docRemision = ficha.docRemision,
                estacion = ficha.estacion,
                factorCambio = ficha.factorCambio,
                idCliente = ficha.idCliente,
                idRemision = ficha.idRemision,
                idUsuario = ficha.idUsuario,
                idVendedor = ficha.idVendedor,
                montoBase = ficha.montoBase,
                montoBase1 = ficha.montoBase1,
                montoBase2 = ficha.montoBase2,
                montoBase3 = ficha.montoBase3,
                montoDivisa = ficha.montoDivisa,
                montoExento = ficha.montoExento,
                montoImpuesto = ficha.montoImpuesto,
                montoImpuesto1 = ficha.montoImpuesto1,
                montoImpuesto2 = ficha.montoImpuesto2,
                montoImpuesto3 = ficha.montoImpuesto3,
                neto = ficha.neto,
                signo = ficha.signo,
                subTotal = ficha.subTotal,
                subTotalImpuesto = ficha.subTotalImpuesto,
                subTotalNeto = ficha.subTotalNeto,
                telefono = ficha.telefono,
                TipoDoc = ficha.TipoDoc,
                tipoRemision = ficha.tipoRemision,
                usuario = ficha.usuario,
                vendedor = ficha.vendedor,
                CiRif = ficha.CiRif,
                DirFiscal = ficha.DirFiscal,
                RazonSocial = ficha.RazonSocial,
                Tasa1 = ficha.Tasa1,
                Tasa2 = ficha.Tasa2,
                Tasa3 = ficha.Tasa3,
                Total = ficha.Total,
                nota= ficha.nota,
                items = ficha.items.Select(s =>
                {
                    var nr = new DtoTransporte.Documento.Agregar.Presupuesto.FichaDetalle()
                    {
                        aliadoCirif = s.aliadoCirif,
                        aliadoCodigo = s.aliadoCodigo,
                        aliadoDesc = s.aliadoDesc,
                        aliadoId = s.aliadoId,
                        aliadoPrecioDivisa = s.aliadoPrecioDivisa,
                        alicuotaDesc = s.alicuotaDesc,
                        alicuotaId = s.alicuotaId,
                        alicuotaTasa = s.alicuotaTasa,
                        cntDias = s.cntDias,
                        cntUnidades = s.cntUnidades,
                        dscto = s.dscto,
                        estatusAnulado = s.estatusAnulado,
                        moduloCargar = s.moduloCargar,
                        notas = s.notas,
                        precioNetoDivisa = s.precioNetoDivisa,
                        servicioDesc = s.servicioDesc,
                        signoDoc = s.signoDoc,
                        solicitadorPor = s.solicitadorPor,
                        tipoDoc = s.tipoDoc,
                        importe = s.importe,
                        fechas = s.fechas.Select(ss => 
                        {
                            var nr2 = new DtoTransporte.Documento.Agregar.Presupuesto.Fecha()
                            {
                                fecha = ss.fecha,
                                hora = ss.hora,
                                nota = ss.nota,
                            };
                            return nr2;
                        }).ToList(),
                    };
                    return nr;
                }).ToList(),
            };
            var r01 = MyData.TransporteDocumento_AgregarPresupuesto(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            result.Entidad = new OOB.Transporte.Documento.Agregar.Resultado()
            {
                autoDoc = r01.Entidad.autoDoc,
                numDoc = r01.Entidad.numDoc,
            };
            return result;
        }
        public OOB.Resultado.Lista<OOB.Transporte.Documento.Remision.Lista.Ficha> 
            TransporteDocumento_Remision_ListaBy(OOB.Transporte.Documento.Remision.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Transporte.Documento.Remision.Lista.Ficha>();
            var filtroDTO = new DtoTransporte.Documento.Remision.Lista.Filtro()
            {
                codTipoDoc = filtro.codTipoDoc,
                idCliente = filtro.idCliente,
            };
            var r01 = MyData.TransporteDocumento_Remision_ListaBy(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            var lst = new List<OOB.Transporte.Documento.Remision.Lista.Ficha>();
            if (r01.Lista!= null) 
            {
                if (r01.Lista.Count > 0) 
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Transporte.Documento.Remision.Lista.Ficha()
                        {
                            clienteCiRif = s.clienteCiRif,
                            clienteNombre = s.clienteNombre,
                            docCntRenglones = s.docCntRenglones,
                            docCodigo = s.docCodigo,
                            docFechaEmision = s.docFechaEmision,
                            docHoraEmision = s.docHoraEmision,
                            docId = s.docId,
                            docMontoMonedaAct = s.docMontoMonedaAct,
                            docMontoMonedaDiv = s.docMontoMonedaDiv,
                            docNombre = s.docNombre,
                            docNumero = s.docNumero,
                            docSigno = s.docSigno,
                            factorCambio = s.factorCambio,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;
            return result;
        }
    }
}