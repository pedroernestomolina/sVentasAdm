using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Aliado.Editar
{
    public class Ficha: baseFicha
    {
        public int idAliado { get; set; }
        public List<Telefono> telefonos { get; set; }
    }
}