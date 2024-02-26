using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.EntradaNumeroDoc.Vista
{
    public interface IVista: Src.IGestion
    {
        string Get_NumDocGenerar { get; }
        Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get; }
        Utils.Control.Boton.Procesar.IProcesar BtAceptar { get; }
        //
        void setNumeroDoc(string doc);
    }
}