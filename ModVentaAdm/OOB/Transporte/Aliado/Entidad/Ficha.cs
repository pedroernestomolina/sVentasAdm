using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Aliado.Entidad
{
    public class Ficha: baseFicha
    {
        public int id { get; set; }
        public List<Telefono> telefonos { get; set; }


        public Ficha()
        {
            telefonos = new List<Telefono>();
        }
    }
}