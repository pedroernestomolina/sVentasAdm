using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.Presupuesto
{
    public class FichaDetalle : baseDetalle
    {
        public string turnoEstatus { get; set; }
        public string turnoId { get; set; }
        public string turnoDesc { get; set; }
        public int turnoCntDias { get; set; }
        public List<Fecha> fechas { get; set; }
        public List<Aliado> aliados { get; set; }
        public FichaDetalle()
            : base()
        {
            turnoEstatus = "";
            turnoId = "";
            turnoDesc = "";
            turnoCntDias = 0;
            fechas = new List<Fecha>();
            aliados = new List<Aliado>();
        }
    }
}
