using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class data
    {
        private decimal _tasaDivisa;
        private dataItem _items;
        private dataTotales _totales;
        private DatosDocumento.data _datosDoc;
        private OOB.Sistema.Fiscal.Entidad.Ficha _tasaFiscal_1;
        private OOB.Sistema.Fiscal.Entidad.Ficha _tasaFiscal_2;
        private OOB.Sistema.Fiscal.Entidad.Ficha _tasaFiscal_3;

        
        public decimal TasaDivisa_Get { get { return _tasaDivisa; } }
        public OOB.Sistema.Fiscal.Entidad.Ficha TasasFiscal_1 { get { return _tasaFiscal_1; } }
        public OOB.Sistema.Fiscal.Entidad.Ficha TasasFiscal_2 { get { return _tasaFiscal_2; } }
        public OOB.Sistema.Fiscal.Entidad.Ficha TasasFiscal_3 { get { return _tasaFiscal_3; } }
        public dataItem Items { get { return _items; } }
        public dataTotales Totales { get { return _totales; } }
        public DatosDocumento.data DatosDoc { get { return _datosDoc; } }
        public string Cliente_ciRif_Get { get { return _datosDoc == null ? "" : _datosDoc.Cliente_ciRif_Get; } }
        public string Cliente_codigo_Get { get { return _datosDoc == null ? "" : _datosDoc.Cliente_codigo_Get; } }
        public string Cliente_razonSocial_Get { get { return _datosDoc == null ? "" : _datosDoc.Cliente_razonSocial_Get; } }
        public string DatosDoc_FechaEmi_Get { get { return _datosDoc == null ? "" : _datosDoc.FechaSistema_Get.ToShortDateString(); } }
        public string DatosDoc_CondPago_Get { get { return _datosDoc == null ? "" : _datosDoc.CondPago_Get; } }
        public string DatosDoc_FechaVenc_Get { get { return _datosDoc == null ? "" : _datosDoc.FechaVencimiento_Get.ToShortDateString(); } }
        public bool DocumentoIsOk { get { return _datosDoc != null; } }


        public data()
        {
            _tasaDivisa = 0m;
            _tasaFiscal_1 = null;
            _tasaFiscal_2 = null;
            _tasaFiscal_3 = null;
            _items = new dataItem();
            _totales = new dataTotales(_items);
            _datosDoc = null;
        }


        public void Inicializa()
        {
            _items.Inicializa();
            _totales.Inicializa();
            _datosDoc = null;
        }
        public void setDatosDoc(DatosDocumento.data data)
        {
            _datosDoc = data;
        }
        public void LimpiarTodo()
        {
            _datosDoc = null;
            _items.LimpiarTodo();
            _totales.LimpiarTodo();
        }
        public bool DataIsOk()
        {
            if (_datosDoc == null) 
            {
                Helpers.Msg.Alerta("PROBLEMA CON LOS DATOS DEL DOCUMENTO, VERIFIQUE POR FAVOR");
                return false;
            }
            var _docOk = _datosDoc.DatosIsOK();
            if (!_docOk)
            {
                Helpers.Msg.Alerta("PROBLEMA CON LOS DATOS DEL DOCUMENTO, VERIFIQUE POR FAVOR");
                return false;
            }
            var _itemsOK = _items.DataIsOk();
            if (!_itemsOK) 
            {
                Helpers.Msg.Alerta("PROBLEMA CON LOS ITEMS DEL DOCUMENTO, VERIFIQUE POR FAVOR");
                return false;
            }
            var _totOK = _totales.DataIsOk();
            if (!_totOK)
            {
                Helpers.Msg.Alerta("PROBLEMA CON LOS MONTOS TOTALES DEL DOCUMENTO, VERIFIQUE POR FAVOR");
                return false;
            }
            return true;
        }
        public void setTasaDivisa(decimal factor)
        {
            _tasaDivisa = factor;
        }
        public void setTasaFiscal(List<OOB.Sistema.Fiscal.Entidad.Ficha> list)
        {
            _tasaFiscal_1 = list[0];
            _tasaFiscal_2 = list[1];
            _tasaFiscal_3 = list[2];
        }
    }
}