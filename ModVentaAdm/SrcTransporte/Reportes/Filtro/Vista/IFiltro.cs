using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.Filtro.Vista
{
    public interface IFiltro
    {
        int idAliado { get; set; }
        string idCliente { get; set; }
    }
}