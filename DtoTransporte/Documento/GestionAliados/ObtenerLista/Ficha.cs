using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.GestionAliados.ObtenerLista
{
    public class Ficha
    {
        public Documento documento { get; set; }
        public List<Item> items { get; set; }
    }
}