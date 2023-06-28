using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Aliado.Entidad
{
    public class Ficha: baseFicha
    {
        public int id { get; set; }
        public List<Telefono> telefonos { get; set; }
    }
}