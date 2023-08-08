using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.Presupuesto
{
    public class Ficha: baseFicha
    {
        public DateTime fechaEmision { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public bool estatusPendiente { get; set; }
        public List<FichaDetalle> items { get; set; }
        public Ficha()
            : base()
        {
            fechaEmision = DateTime.Now.Date;
            fechaVencimiento = DateTime.Now.Date;
            docSolicitadoPor = "";
            docModuloCargar = "";
            estatusPendiente = false;
            items = new List<FichaDetalle>();
        }
    }
}