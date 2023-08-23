using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.GetAliados.Presupuesto
{
    public class Ficha
    {
        public int idAliado { get; set; }
        public string nombre { get; set; }
        public string ciRif { get; set; }
        public decimal importe { get; set; }
    }
}