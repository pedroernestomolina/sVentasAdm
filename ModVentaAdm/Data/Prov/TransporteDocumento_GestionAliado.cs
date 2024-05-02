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
        public OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Ficha> 
            TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Ficha>();
            //
            var r01 = MyData.TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            //
            var lst = new List<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item>();
            OOB.Transporte.Documento.GestionAliados.ObtenerLista.Documento doc;
            if (r01.Entidad != null)
            {
                var _doc = r01.Entidad.documento;
                doc = new OOB.Transporte.Documento.GestionAliados.ObtenerLista.Documento()
                {
                    ciRifEntidadDoc = _doc.ciRifEntidadDoc,
                    codigoTipoDoc = _doc.codigoTipoDoc,
                    entidadDoc = _doc.entidadDoc,
                    estatusDoc = _doc.estatusDoc,
                    fechaDoc = _doc.fechaDoc,
                    idDoc = _doc.idDoc,
                    idEntidadDoc = _doc.idEntidadDoc,
                    montoDivDoc = _doc.montoDivDoc,
                    nombreDoc = _doc.nombreDoc,
                    numeroDoc = _doc.numeroDoc,
                    serieDoc = _doc.serieDoc,
                };
                if (r01.Entidad.items != null)
                {
                    if (r01.Entidad.items.Count > 0)
                    {
                        lst = r01.Entidad.items.Select(s =>
                        {
                            var nr = new OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item()
                            {
                                acumuladoDivAliadoDoc = s.acumuladoDivAliadoDoc,
                                aliadoCiRif = s.aliadoCiRif,
                                aliadoCod = s.aliadoCod,
                                aliadoId = s.aliadoId,
                                aliadoNombre = s.aliadoNombre,
                                estatusAnuladoAliadoDoc = s.estatusAnuladoAliadoDoc,
                                idRefAliadoDoc = s.idRefAliadoDoc,
                                importeDivAliadoDoc = s.importeDivAliadoDoc,
                            };
                            return nr;
                        }).ToList();
                    };
                }
            }
            else 
            {
                doc = new OOB.Transporte.Documento.GestionAliados.ObtenerLista.Documento();
            }
            result.Entidad = new OOB.Transporte.Documento.GestionAliados.ObtenerLista.Ficha()
            {
                documento= doc,
                items = lst,
            };
            //
            return result;
        }
        public OOB.Resultado.Ficha 
            TransporteDocumento_GestionAliados_AnularAliado(OOB.Transporte.Documento.GestionAliados.AnularAliado.Ficha ficha)
        {
            var result = new OOB.Resultado.Ficha();
            //
            var fichaDTO = new DtoTransporte.Documento.GestionAliados.AnularAliado.Ficha()
            {
                idAliado = ficha.idAliado,
                idRefAliadoDoc = ficha.idRefAliadoDoc,
                idUsuario = ficha.idUsuario,
                montoAnular = ficha.montoAnular,
                nombreUsuario = ficha.nombreUsuario,
                motivo = ficha.motivo,
            };
            var r01 = MyData.TransporteDocumento_GestionAliados_AnularAliado(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            //
            return result;
        }
        public OOB.Resultado.FichaEntidad<int> 
            TransporteDocumento_GestionAliados_InsertarAliado(OOB.Transporte.Documento.GestionAliados.InsertarAliado.Ficha ficha)
        {
            var result = new OOB.Resultado.FichaEntidad<int>();
            //
            var fichaDTO = new DtoTransporte.Documento.GestionAliados.InsertarAliado.Ficha()
            {
                codigoServ = ficha.codigoServ,
                descServ = ficha.descServ,
                detalleServ = ficha.detalleServ,
                docCodigo = ficha.docCodigo,
                docFecha = ficha.docFecha,
                docId = ficha.docId,
                docIdCliente = ficha.docIdCliente,
                docNombre = ficha.docNombre,
                docNumero = ficha.docNumero,
                idAliado = ficha.idAliado,
                idServ = ficha.idServ,
                montoDiv = ficha.montoDiv,
            };
            var r01 = MyData.TransporteDocumento_GestionAliados_InsertarAliado(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            //
            result.Entidad = r01.Entidad;
            //
            return result;
        }
    }
}