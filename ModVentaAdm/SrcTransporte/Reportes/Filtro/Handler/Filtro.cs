using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.Filtro.Handler
{
    public class Filtro: Vista.IFiltro
    {
        public int idAliado { get; set; }
        public string idCliente { get; set; }
        public Filtro()
        {
            idAliado = -1;
            idCliente = "";
        }
    }
}
