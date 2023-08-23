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
        public string aliadoCodigo { get; set; }
        public decimal importeServ { get; set; }
        public string clienteCiRif { get; set; }
        public string clienteNombre { get; set; }
        public DateTime fechaDoc { get; set; }
        public string numDoc { get; set; }
        public string nombreDoc { get; set; }
        public int servId { get; set; }
        public string servCodigo { get; set; }
        public string servDetalle { get; set; }
        public string servDesc { get; set; }
    }
}