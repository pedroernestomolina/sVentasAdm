using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Aliado
{
    public class Tipos
    {
        public enum MetodoBusqueda { SinDEfnir = -1, PorCodigo = 1, PorDescripcion = 2, PorCiRif = 3 };
        public enum TipoAliado { SinDefinir = 1, Interno = 1, Externo = 2 };
    }
}