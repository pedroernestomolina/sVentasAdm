using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Entidad.Presupuesto
{
    public class Ficha
    {
        public FichaEncabezado encabezado { get; set; }
        public List<FichaDetalle> items { get; set; }
        public Ficha()
        {
            encabezado = new FichaEncabezado();
            items = new List<FichaDetalle>();
        }
    }
}