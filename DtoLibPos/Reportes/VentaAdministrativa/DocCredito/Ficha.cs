using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.DocCredito
{
    public class Ficha
    {
        public string idDoc { get; set; }
        public string numeroDoc { get; set; }
        public DateTime fechaEmiDoc { get; set; }
        public string moduloDoc { get; set; }
        public string nombreDoc { get; set; }
        public string codigoDoc { get; set; }
        public string ciRifEntidad { get; set; }
        public string razonSocialEntidad { get; set; }
        public string dirFiscalEntidad { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal tasaCambio { get; set; }
        public string codigoSuc { get; set; }
    }
}