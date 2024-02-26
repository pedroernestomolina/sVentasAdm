using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Vista
{
    public interface IVista: SrcComun.Documento.NotaCreditoAdm.Generar.Vista.IVista 
    {
        IDoc Doc { get; }
        bool ProcesarDocIsOk { get; }
        //
        void ProcesarDoc();
        void LimpiarDoc();
    }
}