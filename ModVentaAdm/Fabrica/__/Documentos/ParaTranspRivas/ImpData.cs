using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.__.Documentos.ParaTranspRivas
{
    public class ImpData: IData.IData
    {
        public List<OOB.Documento.Lista.Ficha> 
            ObtenerLista_DocumentosVenta_AplicanNotaCredito_FiltradoByCliente(string cliente)
        {
            var r01 = Sistema.MyData.TransporteDocumento_Documento_AplicanNotaCredito_FiltradoByCliente(cliente);
            return r01.ListaD.OrderBy(o => o.DocNumero).ToList();
        }
        public OOB.Transporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha 
            ObtenerFicha_DocumentoVenta_AplicaNotaCredito_FiltradoByIdDoc(string idDoc)
        {
            var r01 = Sistema.MyData.TransporteDocumento_Documento_AplicaNotaCredito_GetData(idDoc);
            return r01.Entidad;
        }
        public string
            Agregar_Nuevo_Documento_NotaCredito(OOB.Transporte.Documento.Agregar.NotaCredito.Nueva.Ficha ficha)
        {
            var r01= Sistema.MyData.TransporteDocumento_Documento_NotaCredito_Agregar(ficha);
            return r01.Entidad;
        }
        public OOB.Sistema.TipoDocumento.Entidad.Ficha
            ObtenerFicha_TipoDocumento_Venta(string idTipoDoc) 
        {
            var r01 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById(idTipoDoc);
            return r01.Entidad;
        }
    }
}