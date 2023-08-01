using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ServPrestado.AgregarEditar
{
    public interface IAgregarEditar: ModVentaAdm.Src.IGestion, 
                    Src.Gestion.IProcesar, Src.Gestion.IAbandonar
    {
        Servicio Ficha { get; }
    }
}