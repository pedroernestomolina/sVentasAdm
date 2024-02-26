using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.__.Documentos.IData
{
    public interface IData
    {
        List<OOB.Documento.Lista.Ficha>
            ObtenerLista_DocumentosVenta_AplicanNotaCredito_FiltradoByCliente(string cliente);
        OOB.Transporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha 
            ObtenerFicha_DocumentoVenta_AplicaNotaCredito_FiltradoByIdDoc(string idDoc);
        string
            Agregar_Nuevo_Documento_NotaCredito(OOB.Transporte.Documento.Agregar.NotaCredito.Nueva.Ficha ficha);
        OOB.Sistema.TipoDocumento.Entidad.Ficha
            ObtenerFicha_TipoDocumento_Venta(string idTipoDoc);
    }
}