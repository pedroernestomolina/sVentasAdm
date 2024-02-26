using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad.Venta
{
    public class DetTurno
    {
        public string idVenta { get; set; }
        public string servDesc { get; set; }
        public int cntDias { get; set; }
        public int cntVehic { get; set; }
        public decimal pnetoDiv { get; set; }
        public string notas { get; set; }
        public decimal importe { get; set; }
        public string descVehic { get; set; }
        public string servCod { get; set; }
        public string servDet { get; set; }
        public string turnEstatus { get; set; }
        public string turnDesc { get; set; }
        public int turnCntDias { get; set; }
        public string  docNroRef { get; set; }
        public string docTipoProcedencia { get; set; }
    }
}