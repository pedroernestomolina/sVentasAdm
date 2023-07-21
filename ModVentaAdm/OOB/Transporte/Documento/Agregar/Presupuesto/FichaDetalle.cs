using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.Presupuesto
{
    public class FichaDetalle : baseDetalle
    {
        public List<Fecha> fechas { get; set; }
        public List<Aliado> aliados { get; set; }
        public FichaDetalle()
            : base()
        {
            fechas = new List<Fecha>();
            aliados = new List<Aliado>();
        }
    }
}
