using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Handler
{
    public class dataGuardar: Vista.IdataGuardar
    {
        public OOB.Transporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha DocAplicarNtCredito { get; set; }
        public string Motivo { get; set; }
        public decimal MontoBase { get; set; }
        public decimal MontoImp { get; set; }
        public decimal MontoTotal { get; set; }
        public Vista.IFiscal Exento { get; set; }
        public Vista.IFiscal MontoFisal_1 { get; set; }
        public Vista.IFiscal MontoFisal_2 { get; set; }
        public Vista.IFiscal MontoFisal_3 { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}