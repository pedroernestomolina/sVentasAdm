using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Presupuesto
{
    public class Ficha: baseFicha
    {
        public List<FichaDetalle> items { get; set; }
        public Ficha()
            :base()
        {
            items = new List<FichaDetalle>();
        }
    }
}