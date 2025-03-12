using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CxC.CapturarData.Cliente
{
    public class Ficha
    {
        public string idClient { get; set; }
        public string ciRifClient { get; set; }
        public string codigoClient { get; set; }
        public string aliasClient { get; set; }
        public string nombreRazonSocialClient { get; set; }
        public string dirFiscalClient { get; set; }
        public string telefonoClient { get; set; }
        public string telefono2Client { get; set; }
        public string emailClient { get; set; }
        public string idVendedor { get; set; }
        public string idCobrador { get; set; }
        public string estatusCreditoClient { get; set; }
        public decimal limiteCreditoClient { get; set; }
        public int diasCreditoClient { get; set; }
        public DateTime fechaUltVentaClient { get; set; }
        public DateTime fechaUltPagoClient { get; set; }
        public decimal montoAnticiposClient { get; set; }
        public string estatusClient { get; set; }
        public string nombreVend { get; set; }
        public string nombreCobrad { get; set; }
        public string codigoCobrad { get; set; }
        public string codigoVend { get; set; }
    }
}