using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Vista
{
    public interface IdataGuardar
    {
        OOB.Transporte.Documento.Agregar.NotaCredito.ObtenerDataDocAplica.Ficha DocAplicarNtCredito { get; set; }
        string Motivo { get; set; }
        decimal MontoBase { get; set; }
        decimal MontoImp { get; set; }
        decimal MontoTotal { get; set; }
        Vista.IFiscal Exento { get; set; }
        Vista.IFiscal MontoFisal_1 { get; set; }
        Vista.IFiscal MontoFisal_2 { get; set; }
        Vista.IFiscal MontoFisal_3 { get; set; }
        DateTime FechaEmision { get; set; }
    }
}