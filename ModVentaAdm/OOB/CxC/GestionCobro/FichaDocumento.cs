using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.GestionCobro
{
    
    public class FichaDocumento
    {

        public int Id { get; set; }
        public string AutoCxC { get; set; }
        public string DocumentoNro { get; set; }
        public string TipoDocumento { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteDivisa { get; set; }
        public string EstatusDocCancelado { get; set; }
        public string Notas { get; set; }


        public FichaDocumento()
        {
            Id = 1;
            AutoCxC = "";
            EstatusDocCancelado = "";
            DocumentoNro = "";
            TipoDocumento = "";
            Importe = 0m;
            ImporteDivisa = 0m;
            Notas = "";
        }

    }

}