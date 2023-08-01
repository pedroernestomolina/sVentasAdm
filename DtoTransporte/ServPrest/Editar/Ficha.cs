using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.ServPrest.Editar
{
    public class Ficha: baseFicha
    {
        public int idFicha { get; set; }
        public Ficha()
        {
            idFicha = -1;
            codigo = "";
            descripcion = "";
            detalle = "";
        }
    }
}