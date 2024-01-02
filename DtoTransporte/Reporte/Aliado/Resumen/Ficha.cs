using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Reporte.Aliado.Resumen
{
    public class Ficha
    {
        public string codigo { get; set; }
        public string ciRif { get; set; }
        public string aliado { get; set; }
        public decimal importe { get; set; }
        public decimal acumulado{ get; set; }
        public Ficha()
        {
            ciRif = "";
            codigo = "";
            aliado = ""; 
            importe = 0m;
            acumulado = 0m;
        }
    }
}