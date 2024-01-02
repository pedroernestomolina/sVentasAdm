using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ClienteAnticipo.Entidad
{
    public class Movimiento
    {
        public int idMov { get; set; }
        public string idCliente { get; set; }
        public DateTime fechaEmision { get; set; }
        public string ciRifCliente { get; set; }
        public string nombreCliente { get; set; }
        public decimal montoMovMonAct { get; set; }
        public decimal montoMovMonDiv { get; set; }
        public decimal tasaFactor { get; set; }
        public string motivo { get; set; }
        public string aplicaRet { get; set; }
        public decimal tasaRet { get; set; }
        public decimal sustraendoRet { get; set; }
        public decimal montoRet { get; set; }
        public decimal totalRet { get; set; }
        public decimal montoRecMonAct { get; set; }
        public decimal montoRecMonDiv { get; set; }
        public string estatus { get; set; }
        public string reciboNro { get; set; }
    }
}