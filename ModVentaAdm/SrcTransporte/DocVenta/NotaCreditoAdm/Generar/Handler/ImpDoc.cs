using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Handler
{
    public class ImpDoc: Vista.IDoc
    {
        private string _cadenaBusq;
        private Vista.IDocGenerar _docGenerar;
        private OOB.Transporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha _docVenta_AplicarNotaCredito;
        private string _docAplicaNotaCredito_DatosCliente;
        private string _docAplicaNotaCredito_DatosDocumento;
        //
        public string Get_CadenaBusq { get { return _cadenaBusq; } }
        public bool BusquedaIsOk { get { return _docVenta_AplicarNotaCredito != null; } }
        public Vista.IDocGenerar DocGenerar { get { return _docGenerar; } }
        public Vista.IdataGuardar Get_DatosGuardar { get { return dataGuardar(); } }
        public string Get_DocAplicarNotaCredito_DatosCliente { get { return _docAplicaNotaCredito_DatosCliente; } }
        public string Get_DocAplicarNotaCredito_DatosDocumento { get { return _docAplicaNotaCredito_DatosDocumento; } }
        //
        public ImpDoc()
        {
            _cadenaBusq = "";
            _docVenta_AplicarNotaCredito = null;
            _docAplicaNotaCredito_DatosCliente = "";
            _docAplicaNotaCredito_DatosDocumento = "";
            _docGenerar = new ImpDocGenerar();
        }
        public void setCadenaBuscar(string cadena)
        {
            _cadenaBusq = cadena;
        }
        public void Inicializa()
        {
            _cadenaBusq = "";
            _docVenta_AplicarNotaCredito = null;
            _docAplicaNotaCredito_DatosCliente = "";
            _docAplicaNotaCredito_DatosDocumento = "";
            _docGenerar.Inicializa();
        }
        public void BuscarDocumentos()
        {
            if (_cadenaBusq.Trim() == "") { return; }
            cargar_DocVenta_AplicarNotaCredito(listar_Retornar_DocAplicar_NotaCredito());
            _cadenaBusq = "";
        }
        //
        private void cargar_DocVenta_AplicarNotaCredito(string idDoc)
        {
            if (idDoc == "") { return; }
            try
            {
                _docAplicaNotaCredito_DatosCliente = "";
                _docAplicaNotaCredito_DatosDocumento = "";
                var r01 = Sistema.Fabrica.DataDocumentos.ObtenerFicha_DocumentoVenta_AplicaNotaCredito_FiltradoByIdDoc(idDoc);
                _docVenta_AplicarNotaCredito = r01;
                _docAplicaNotaCredito_DatosCliente = r01.clienteCiRif + Environment.NewLine +
                                                        r01.clienteNombre + Environment.NewLine +
                                                        r01.clienteDirFiscal;
                _docAplicaNotaCredito_DatosDocumento = " Documento Nro: " + r01.docNumero + Environment.NewLine +
                                                       "      De Fecha: " + r01.docFechaEmision.ToShortDateString() + Environment.NewLine +
                                                       "     Por Monto: " + r01.docTotal.ToString("n2").PadLeft(14,' ')+ Environment.NewLine+
                                                       "  En Divisa($): " + r01.montoDivisa.ToString("n2").PadLeft(14, ' ') + Environment.NewLine +
                                                       " Factor/Cambio: " + r01.factorCambio.ToString("n2").PadLeft(14, ' ');
                _docGenerar.MontoExento.setTasa(0m);
                _docGenerar.MontoFiscal_1.setTasa(r01.tasa1);
                _docGenerar.MontoFiscal_2.setTasa(r01.tasa2);
                _docGenerar.MontoFiscal_3.setTasa(r01.tasa3);
                _docGenerar.MontoExento.setBase(r01.montoExento);
                _docGenerar.MontoFiscal_1.setBase(r01.montoBase1);
                _docGenerar.MontoFiscal_2.setBase(r01.montoBase2);
                _docGenerar.MontoFiscal_3.setBase(r01.montoBase3);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        ListaDoc.Vista.IVista _listDoc;
        private string listar_Retornar_DocAplicar_NotaCredito()
        {
            var idDoc = "";
            //
            if (_listDoc == null)
            {
                _listDoc = new ListaDoc.Handler.ImpVista();
            }
            _listDoc.Inicializa();
            _listDoc.setCargarDocFiltradoByCliente(_cadenaBusq);
            _listDoc.Inicia();
            if (_listDoc.ItemSeleccionadoIsOk)
            {
                var it = (ListaDoc.Handler.data)_listDoc.Get_ItemSeleccionado;
                idDoc = it.Ficha.Id;
            }
            //
            return idDoc;
        }
        public void Limpiar()
        {
            _cadenaBusq = "";
            _docAplicaNotaCredito_DatosCliente = "";
            _docAplicaNotaCredito_DatosDocumento = "";
            _docVenta_AplicarNotaCredito = null;
            _docGenerar.Limpiar();
        }
        private Vista.IdataGuardar dataGuardar()
        {
            Vista.IdataGuardar rt = new dataGuardar()
            {
                DocAplicarNtCredito = _docVenta_AplicarNotaCredito,
                Motivo = _docGenerar.Get_Motivo,
                MontoBase = _docGenerar.Get_Subt_Base,
                MontoImp = _docGenerar.Get_Subt_Imp,
                MontoTotal = _docGenerar.Get_Total,
                Exento = _docGenerar.MontoExento,
                MontoFisal_1 = _docGenerar.MontoFiscal_1,
                MontoFisal_2 = _docGenerar.MontoFiscal_2,
                MontoFisal_3 = _docGenerar.MontoFiscal_3,
            };
            return rt;
        }
        public bool ValidarDataIsOk()
        {
            try
            {
                _docGenerar.ValidarDataIsOk();
                if (DocGenerar.Get_Total > _docVenta_AplicarNotaCredito.docTotal)
                {
                    throw  new Exception("MONTO NOTA CREDITO ES MAYOR AL MONTO DOCUMENTO DE VENTA");
                }
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Alerta(e.Message);
                return false;
            }
        }
    }
}