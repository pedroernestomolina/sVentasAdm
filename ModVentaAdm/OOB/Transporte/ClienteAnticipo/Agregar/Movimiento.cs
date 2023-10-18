using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ClienteAnticipo.Agregar
{
    public class Movimiento
    {
        public string idCliente { get; set; }
        public string ciRifCliente { get; set; }
        public string nombreRazonSocialCliente { get; set; }
        public DateTime fechaEmision { get; set; }
        public decimal montoAnticipoMonAct { get; set; }
        public decimal montoAnticipoMonDiv { get; set; }
        public decimal tasaFactor { get; set; }
        public string motivo { get; set; }
        public string aplicaRet { get; set; }
        public decimal tasaRet { get; set; }
        public decimal sustraendoRet { get; set; }
        public decimal retencion { get; set; }
        public decimal totalRet { get; set; }
        public decimal montoRecibidoMonAct { get; set; }
        public decimal montoRecibidoMonDiv { get; set; }
        public string reciboNumero { get; set; }
    }
}