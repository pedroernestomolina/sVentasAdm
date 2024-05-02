using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Agregar.Vistas
{
    public interface IMain: Src.IGestion
    {
        Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get; }
        Utils.Control.Boton.Procesar.IProcesar BtProcesar { get; }
        bool ProcesarIsOk { get; }
        IMainData GestionData { get; }
        //
        void Procesar();
        object dataAgregar();
    }
}