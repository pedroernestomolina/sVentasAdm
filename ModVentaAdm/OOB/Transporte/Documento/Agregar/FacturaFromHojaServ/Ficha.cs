using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.FacturaFromHojaServ
{
    public class Ficha: baseFicha
    {
        public DateTime fechaEmision { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string serieDocId { get; set; }
        public string serieDocDesc { get; set; }
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public decimal subTotalMonDivisa { get; set; }
        public string tipoDocSiglas { get; set; }
        public List<FichaItem> items { get; set; }
        public List<FichaDocRef> docRef { get; set; }
        public Ficha()
            :base()
        {
            fechaEmision = DateTime.Now.Date;
            fechaVencimiento = DateTime.Now.Date;
            serieDocId = "";
            serieDocDesc = "";
            docSolicitadoPor = "";
            docModuloCargar = "";
            subTotalMonDivisa = 0m;
            tipoDocSiglas = "";
            items = new List<FichaItem>();
            docRef = new List<FichaDocRef>();
        }
    }
}