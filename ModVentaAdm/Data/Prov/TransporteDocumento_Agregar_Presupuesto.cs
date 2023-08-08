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
                nota = ficha.nota,
                docModuloCargar = ficha.docModuloCargar,
                docSolicitadoPor = ficha.docSolicitadoPor,
                estatusPendiente=ficha.estatusPendiente,
                fechaEmision= ficha.fechaEmision,
                fechaVencimiento=ficha.fechaVencimiento,
                items = ficha.items.Select(s =>
                {
                    var nr = new DtoTransporte.Documento.Agregar.Presupuesto.FichaDetalle()
                    {
                        alicuotaDesc = s.alicuotaDesc,
                        alicuotaId = s.alicuotaId,
                        alicuotaTasa = s.alicuotaTasa,
                        cntDias = s.cntDias,
                        cntUnidades = s.cntUnidades,
                        dscto = s.dscto,
                        estatusAnulado = s.estatusAnulado,
                        notas = s.notas,
                        precioNetoDivisa = s.precioNetoDivisa,
                        servicioDesc = s.servicioDesc,
                        signoDoc = s.signoDoc,
                        tipoDoc = s.tipoDoc,
                        importe = s.importe,
                        unidadesDesc = s.unidadesDesc,
                        servicioId = s.servicioId,
                        servicioCodigo = s.servicioCodigo,
                        servicioDetalle = s.servicioDetalle,
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
                        alidos = s.aliados.Select(xx =>
                        {
                            var nr3 = new DtoTransporte.Documento.Agregar.Presupuesto.Aliado()
                            {
                                ciRif = xx.ciRif,
                                cntDias = xx.cntDias,
                                codigo = xx.codigo,
                                desc = xx.desc,
                                id = xx.id,
                                importe = xx.importe,
                                precioUnitDivisa = xx.precioUnitDivisa,
                            };
                            return nr3;
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
    }
}