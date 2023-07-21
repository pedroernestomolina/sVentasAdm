using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Presupuesto
{
    public class FichaDetalle: baseDetalle
    {
        public List<Fecha> fechas { get; set; }
        public List<Aliado> alidos { get; set; }
        public FichaDetalle()
            :base()
        {
            fechas = new List<Fecha>();
            alidos = new List<Aliado>();
        }
    }
}
