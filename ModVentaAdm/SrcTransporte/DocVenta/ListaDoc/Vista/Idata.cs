using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaDoc.Vista
{
    public interface Idata 
    {
        string DocumentoNro { get; set; }
        DateTime FechaEmision { get; set; }
        decimal MontoMonAct { get; set; }
        decimal MontoMonDiv { get; set; }
        string EntidadNombre { get; set; }
        string EntidadCiRif { get; set; }
        string TipoDocumento { get; set; }
    }
}