using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.GestionAliados.ObtenerLista
{
    public class Item
    {
        public int idRefAliadoDoc { get; set; }
        public int aliadoId { get; set; }
        public string aliadoCod { get; set; }
        public string aliadoCiRif { get; set; }
        public string aliadoNombre { get; set; }
        public string estatusAnuladoAliadoDoc { get; set; }
        public decimal importeDivAliadoDoc { get; set; }
        public decimal acumuladoDivAliadoDoc { get; set; }
    }
}