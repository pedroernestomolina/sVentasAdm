using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.ServPrest.Editar
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