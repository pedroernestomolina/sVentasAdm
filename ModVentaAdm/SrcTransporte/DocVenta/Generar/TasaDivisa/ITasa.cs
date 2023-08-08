using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.TasaDivisa
{
    public interface ITasa: Src.IGestion, Src.Gestion.IProcesar, Src.Gestion.IAbandonar
    {
        string Titulo_Get { get; }
        decimal  TasaActual_Get { get; }

        void setTasaDivisa(decimal tasa);
    }
}