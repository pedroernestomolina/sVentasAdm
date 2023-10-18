using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Caja.Lista
{
    public class Ficha
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal saldoInicial { get; set; }
        public decimal montoPorIngresos { get; set; }
        public decimal montoPorEgresos { get; set; }
        public decimal montoPorAnulaciones { get; set; }
        public string estatusAnulado { get; set; }
        public string esDivisa { get; set; }
    }
}