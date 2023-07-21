using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.Presupuesto
{
    public class Ficha: baseFicha
    {
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public List<FichaDetalle> items { get; set; }
        public Ficha()
            : base()
        {
            items = new List<FichaDetalle>();
        }
    }
}