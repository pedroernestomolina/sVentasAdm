using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Reportes.Vendedor.Detallado
{
    public class Ficha
    {
        public DateTime docFechaEmision { get; set; }
        public string docNumero { get; set; }
        public string docTipo { get; set; }
        public int docSigno { get; set; }
        public string docNombre { get; set; }
        public string razonSocial { get; set; }
        public decimal netoMonLocal { get; set; }
        public decimal netoDivisa { get; set; }
        //
        public string autoVend { get; set; }
        public string nombreVend { get; set; }
        public string codigoVend { get; set; }
    }
}