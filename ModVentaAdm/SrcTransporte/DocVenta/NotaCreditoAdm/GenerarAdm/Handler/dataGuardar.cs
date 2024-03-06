using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Handler
{
    public class dataGuardar: Vista.IdataGuardar
    {
        public object EntidadAplicarNtCredito { get; set; }
        public DateTime FechaEmision { get; set; }
        public string DocNumero { get; set; }
        public decimal TasaCambio { get; set; }
        public string Motivo { get; set; }
        public decimal MontoBase { get; set; }
        public decimal MontoImp { get; set; }
        public decimal MontoTotal { get; set; }
        public Generar.Vista.IFiscal Exento { get; set; }
        public Generar.Vista.IFiscal MontoFisal_1 { get; set; }
        public Generar.Vista.IFiscal MontoFisal_2 { get; set; }
        public Generar.Vista.IFiscal MontoFisal_3 { get; set; }
    }
}