using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad.Presupuesto
{
    public class FichaDetalle
    {
        public int id { get; set; }
        public string servicioDesc { get; set; }
        public string solicitadoPor { get; set; }
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
        public decimal importe { get; set; }
        public List<FichaFechaServ> fechaServ { get; set; }
        public FichaDetalle()
        {
            id = -1;
            servicioDesc = "";
            solicitadoPor = "";
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
            importe = 0m;
            fechaServ = new List<FichaFechaServ>();
        }
    }
}