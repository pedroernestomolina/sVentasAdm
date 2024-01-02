using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Reporte.Aliado.DetalleDoc
{
    public class Filtro: baseFiltro
    {
        public int IdAliado { get; set; }
        public string IdCliente { get; set; }
    }
}