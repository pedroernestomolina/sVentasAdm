using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ServPrest.Entidad
{
    public class Ficha: baseFicha
    {
        public int id { get; set; }
        public Ficha()
        {
            id = -1;
            codigo = "";
            descripcion = "";
            detalle = "";
        }
    }
}