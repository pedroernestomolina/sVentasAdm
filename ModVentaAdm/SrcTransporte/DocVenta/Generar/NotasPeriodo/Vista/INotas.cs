using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.NotasPeriodo.Vista
{
    public interface INotas: Src.IGestion, Src.Gestion.IAbandonar, Src.Gestion.IProcesar
    {
        string Titulo_Get { get; }

        string Notas_Get { get; }
        void setNotas(string desc);
    }
}