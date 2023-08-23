using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Entidad.Presupuesto
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
        public string notas { get; set; }
        public decimal importe { get; set; }
        public string unidadesDesc { get; set; }
        public int servicioId { get; set; }
        public string servicioCodigo { get; set; }
        public string servicioDetalle { get; set; }
        public string turnoEstatus { get; set; }
        public string turnoId { get; set; }
        public string turnoDesc { get; set; }
        public int turnoCntDias { get; set; }
        public List<FichaFechaServ> fechaServ { get; set; }
        public List<FichaAliado> aliados { get; set; }
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
            notas = "";
            importe = 0m;
            unidadesDesc = "";
            servicioId = -1;
            servicioCodigo = "";
            servicioDetalle = "";
            turnoEstatus = "";
            turnoId = "";
            turnoDesc = "";
            turnoCntDias=0;
            fechaServ = new List<FichaFechaServ>();
            aliados = new List<FichaAliado>();
        }
    }
}