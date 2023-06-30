using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class data
    {
        private dataItem _items;
        private dataTotales _totales;
        private DatosDocumento.data _datosDoc;


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
            _items = new dataItem();
            _totales = new dataTotales();
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
    }
}