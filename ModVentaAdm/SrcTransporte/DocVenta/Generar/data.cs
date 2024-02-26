using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar
{
    public class data
    {
        private decimal _tasaDivisa;
        private dataItem _items;
        private dataTotales _totales;
        private Presupuesto.Generar.DatosDocumento.data _datosDoc;
        private OOB.Sistema.Fiscal.Entidad.Ficha _tasaFiscal_1;
        private OOB.Sistema.Fiscal.Entidad.Ficha _tasaFiscal_2;
        private OOB.Sistema.Fiscal.Entidad.Ficha _tasaFiscal_3;
        private decimal _tasaIGTF;
        private decimal _tasaISLR;


        public decimal TasaDivisa_Get { get { return _tasaDivisa; } }
        public OOB.Sistema.Fiscal.Entidad.Ficha TasasFiscal_1 { get { return _tasaFiscal_1; } }
        public OOB.Sistema.Fiscal.Entidad.Ficha TasasFiscal_2 { get { return _tasaFiscal_2; } }
        public OOB.Sistema.Fiscal.Entidad.Ficha TasasFiscal_3 { get { return _tasaFiscal_3; } }
        public dataItem Items { get { return _items; } }
        public dataTotales Totales { get { return _totales; } }
        public Presupuesto.Generar.DatosDocumento.data DatosDoc { get { return _datosDoc; } }
        public string Cliente_ciRif_Get { get { return _datosDoc == null ? "" : _datosDoc.Cliente_ciRif_Get; } }
        public string Cliente_codigo_Get { get { return _datosDoc == null ? "" : _datosDoc.Cliente_codigo_Get; } }
        public string Cliente_razonSocial_Get { get { return _datosDoc == null ? "" : _datosDoc.Cliente_razonSocial_Get; } }
        public string DatosDoc_FechaEmi_Get { get { return _datosDoc == null ? "" : _datosDoc.FechaEmision_Get.ToShortDateString(); } }
        public string DatosDoc_CondPago_Get { get { return _datosDoc == null ? "" : _datosDoc.CondPago_Get; } }
        public string DatosDoc_FechaVenc_Get { get { return _datosDoc == null ? "" : _datosDoc.FechaVencimiento_Get.ToShortDateString(); } }
        public bool DocumentoIsOk { get { return _datosDoc != null; } }


        public data()
        {
            _tasaIGTF = 0m;
            _tasaISLR = 0m;
            _tasaDivisa = 0m;
            _tasaFiscal_1 = null;
            _tasaFiscal_2 = null;
            _tasaFiscal_3 = null;
            _items = new dataItem();
            _totales = new dataTotales(_items);
            _datosDoc = null;
            DocNumeroGenerar = "";
        }


        public void Inicializa()
        {
            _items.Inicializa();
            _totales.Inicializa();
            _datosDoc = null;
            DocNumeroGenerar = "";
        }
        public void setDatosDoc(Presupuesto.Generar.DatosDocumento.data data)
        {
            _datosDoc = data;
        }
        public void LimpiarTodo()
        {
            _datosDoc = null;
            _items.LimpiarTodo();
            _totales.LimpiarTodo();
            DocNumeroGenerar = "";
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
            foreach (var rg in _items.GetItems)
            {
                if (rg.Item.Get_ItemServicio != null) 
                {
                    if (rg.Item.Get_ItemServicio.Item.Get_TurnoIsActivo) 
                    {
                        Helpers.Msg.Alerta("NO PUEDEN HABER SERVICIOS POR TURNO CARGADOS MANUALMENTE YA SEA PARA UNA HOJA DE SERVICIO/ FACTURA, DEBEN VENIR POR UN PRESUPUESTO/HOJA DE SERVICIO");
                        return false;
                    }
                }
            }
            return true;
        }
        public void setTasaDivisa(decimal factor)
        {
            _tasaDivisa = factor;
            _totales.setTasaDivisa(factor);
        }
        public void setTasaFiscal(List<OOB.Sistema.Fiscal.Entidad.Ficha> list)
        {
            _tasaFiscal_1 = list[0];
            _tasaFiscal_2 = list[1];
            _tasaFiscal_3 = list[2];
        }

        //
        public decimal Get_TasaIGTF { get { return _tasaIGTF; } }
        public void setTasaIgtf(decimal tasa)
        {
            _tasaIGTF = tasa;
        }
        public decimal Get_TasaISLR { get { return _tasaISLR; } }
        public void setTasaIslr(decimal tasa)
        {
            _tasaISLR = tasa;
        }

        public string DocNumeroGenerar { get; set; }
    }
}