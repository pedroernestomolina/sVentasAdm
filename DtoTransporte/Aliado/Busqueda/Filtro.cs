using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Aliado.Busqueda
{
    public class Filtro
    {
        public string cadena { get; set; }
        public Tipos.MetodoBusqueda metodoBusqueda { get; set; }
        public Tipos.TipoAliado tipoAliado { get; set; }
    }
}