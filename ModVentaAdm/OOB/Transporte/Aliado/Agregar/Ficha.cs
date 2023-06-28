using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Aliado.Agregar
{
    public class Ficha: baseFicha
    {
        public List<Telefono> telefonos { get; set; }


        public Ficha()
        {
            telefonos = new List<Telefono>();
        }
    }
}