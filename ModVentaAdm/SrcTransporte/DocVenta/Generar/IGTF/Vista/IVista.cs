using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.IGTF.Vista
{
    public interface IVista: Src.IGestion
    {
        decimal Get_TasaIGTF { get; }
        decimal Get_MontoAplicarIGTF { get; }
        Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get; }
        Utils.Control.Boton.Procesar.IProcesar BtAceptar { get; }
        bool ProcesarIsOk { get; }
        //
        void setTasaIGTF(decimal tasa);
        void setMontoAplicarIGTF(decimal monto);
        void Procesar();
    }
}