using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.GetDetalleTurnos.Presupuesto
{
    public class Ficha
    {
        public string servicio_desc { get; set; }
        public int cnt_dias { get; set; }
        public int cnt_unidades { get; set; }
        public decimal precio_neto_divisa { get; set; }
        public decimal dscto { get; set; }
        public string alicuota_id { get; set; }
        public decimal alicuota_tasa { get; set; }
        public string alicuota_desc { get; set; }
        public string notas { get; set; }
        public DateTime fecha_doc { get; set; }
        public string hora_doc { get; set; }
        public int signo { get; set; }
        public string tipo_doc { get; set; }
        public string estatus_anulado { get; set; }
        public decimal importe { get; set; }
        public string unidades_desc { get; set; }
        public int servicio_id { get; set; }
        public string servicio_codigo { get; set; }
        public string servicio_detalle { get; set; }
        public string turno_estatus { get; set; }
        public string turno_id { get; set; }
        public string turno_desc { get; set; }
        public int turno_cnt_dias { get; set; }
        public string id_doc_ref { get; set; }
        public string doc_num_ref { get; set; }
    }
}