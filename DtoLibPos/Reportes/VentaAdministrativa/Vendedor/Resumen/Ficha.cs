using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.Vendedor.Resumen
{
    public class Ficha
    {
        public string autoVend { get; set; }
        public string nombreVend { get; set; }
        public string codigoVend { get; set; }
        public int cntDoc { get; set; }
        public decimal netoMonLocal { get; set; }
        public decimal netoDivisa { get; set; }
    }
}