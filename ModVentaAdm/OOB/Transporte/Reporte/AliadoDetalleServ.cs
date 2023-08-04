using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Reporte
{
    public class AliadoDetalleServ
    {
        public int aliadoId { get; set; }
        public string aliadoCiRif { get; set; }
        public string aliadoNombre { get; set; }
        public string docNumero { get; set; }
        public DateTime docFecha { get; set; }
        public string docCliente { get; set; }
        public string docNombre { get; set; }
        public int servId { get; set; }
        public string servCodigo { get; set; }
        public string servDesc { get; set; }
        public decimal servImporte { get; set; }
    }
}