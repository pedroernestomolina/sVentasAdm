using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Vista
{
    public interface IdataGuardar
    {
        object EntidadAplicarNtCredito { get; set; }
        DateTime FechaEmision { get; set; }
        string DocNumero { get; set; }
        decimal TasaCambio { get; set; }
        string Motivo { get; set; }
        decimal MontoBase { get; set; }
        decimal MontoImp { get; set; }
        decimal MontoTotal { get; set; }
        Generar.Vista.IFiscal Exento { get; set; }
        Generar.Vista.IFiscal MontoFisal_1 { get; set; }
        Generar.Vista.IFiscal MontoFisal_2 { get; set; }
        Generar.Vista.IFiscal MontoFisal_3 { get; set; }
    }
}