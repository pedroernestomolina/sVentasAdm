using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.Factura
{
    public class FichaDocRef
    {
        public string idDoc { get; set; }
        public string numDoc { get; set; }
        public DateTime fechaDoc { get; set; }
        public decimal montoDivisaDoc { get; set; }
        public string codigoDoc { get; set; }
        public string tipoDoc { get; set; }
        public FichaDocRef()
        {
            idDoc = "";
            numDoc = "";
            fechaDoc = DateTime.Now.Date;
            montoDivisaDoc = 0m;
            codigoDoc = "";
            tipoDoc = "";
        }
    }
}