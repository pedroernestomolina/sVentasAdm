using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Presupuesto
{
    public class Ficha: baseFicha
    {
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public bool estatusPendiente { get; set; }
        public List<FichaDetalle> items { get; set; }
        public Ficha()
            :base()
        {
            docSolicitadoPor = "";
            docModuloCargar = "";
            estatusPendiente = false;
            items = new List<FichaDetalle>();
        }
    }
}