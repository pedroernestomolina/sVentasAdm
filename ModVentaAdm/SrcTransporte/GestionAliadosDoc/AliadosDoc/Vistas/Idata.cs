using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Vistas
{
    public interface Idata
    {
        int aliadoId { get; set; }
        string aliadoCiRif { get; set; }
        string aliadoNombre { get; set; }
        decimal importeDiv { get; set; }
        decimal acumuladoDiv { get; set; }
        int idRef { get; set; }
    }
}