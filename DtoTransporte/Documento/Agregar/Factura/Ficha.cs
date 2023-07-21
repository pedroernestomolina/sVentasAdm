using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Factura
{
    public class Ficha: baseFicha
    {
        public string serieDocId { get; set; }
        public string serieDocDesc { get; set; }
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public decimal subTotalMonDivisa { get; set; }
        public List<FichaDocRef> docRef { get; set; }
        public List<FichaAliado> aliados { get; set; }
        public Ficha()
            :base()
        {
            serieDocId = "";
            serieDocDesc = "";
            docSolicitadoPor = "";
            docModuloCargar = "";
            subTotalMonDivisa = 0m;
            docRef = new List<FichaDocRef>();
            aliados = new List<FichaAliado>();
        }
    }
}