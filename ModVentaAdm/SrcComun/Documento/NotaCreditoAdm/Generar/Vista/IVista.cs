using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.NotaCreditoAdm.Generar.Vista
{
    public interface IVista: Src.IGestion
    {
        Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get; }
        Utils.Control.Boton.Procesar.IProcesar BtProcesar { get; }
    }
}