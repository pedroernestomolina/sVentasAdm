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
        public string solicitadorPor { get; set; }
        public string moduloCargar { get; set; }
        public int cntDias { get; set; }
        public int cntUnidades { get; set; }
        public decimal precioNetoDivisa { get; set; }
        public decimal dscto { get; set; }
        public string alicuotaId { get; set; }
        public decimal alicuotaTasa { get; set; }
        public string alicuotaDesc { get; set; }
        public int aliadoId { get; set; }
        public string aliadoCirif { get; set; }
        public string aliadoCodigo { get; set; }
        public string aliadoDesc { get; set; }
        public decimal aliadoPrecioDivisa { get; set; }
        public string notas { get; set; }
        public int signoDoc { get; set; }
        public string tipoDoc { get; set; }
        public string estatusAnulado { get; set; }
        public decimal importe { get; set; }
        public baseDetalle()
        {
            servicioDesc = "";
            solicitadorPor = "";
            moduloCargar = "";
            cntDias = 0;
            cntUnidades = 0;
            precioNetoDivisa = 0m;
            dscto = 0m;
            alicuotaId = "";
            alicuotaTasa = 0m;
            alicuotaDesc = "";
            aliadoId = -1;
            aliadoCirif = "";
            aliadoCodigo = "";
            aliadoDesc = "";
            aliadoPrecioDivisa = 0m;
            notas = "";
            signoDoc = 1;
            tipoDoc = "";
            estatusAnulado = "";
            importe = 0m;
        }
    }
}