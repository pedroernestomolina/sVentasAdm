using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar
{
    abstract public class baseDetalle
    {
        public string servicioDesc { get; set; }
        public int cntDias { get; set; }
        public int cntUnidades { get; set; }
        public decimal precioNetoDivisa { get; set; }
        public decimal dscto { get; set; }
        public string alicuotaId { get; set; }
        public decimal alicuotaTasa { get; set; }
        public string alicuotaDesc { get; set; }
        public string notas { get; set; }
        public int signoDoc { get; set; }
        public string tipoDoc { get; set; }
        public string estatusAnulado { get; set; }
        public decimal importe { get; set; }
        public string unidadesDesc { get; set; }
        public int servicioId { get; set; }
        public string servicioCodigo { get; set; }
        public string servicioDetalle { get; set; }
        public baseDetalle()
        {
            servicioDesc = "";
            cntDias = 0;
            cntUnidades = 0;
            precioNetoDivisa = 0m;
            dscto = 0m;
            alicuotaId = "";
            alicuotaTasa = 0m;
            alicuotaDesc = "";
            notas = "";
            signoDoc = 1;
            tipoDoc = "";
            estatusAnulado = "";
            importe = 0m;
            unidadesDesc = "";
            servicioId = -1;
            servicioCodigo = "";
            servicioDetalle = "";
        }
    }
}