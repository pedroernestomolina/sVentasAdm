using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ClienteAnticipo.Obtener
{
    public class Ficha
    {
        public string id { get; set; }
        public string ciRif { get; set; }
        public string nombreRazonSocial { get; set; }
        public decimal montoDiv { get; set; }
        public string Info { get { return "Monto Disponible: "+montoDiv.ToString("n2")+
            Environment.NewLine+ ciRif.Trim().ToUpper() + 
            Environment.NewLine + nombreRazonSocial.Trim(); } }
    }
}