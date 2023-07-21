using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Agregar.Presupuesto
{
    public class Aliado
    {
        public int id { get; set; }
        public string ciRif { get; set; }
        public string codigo { get; set; }
        public string desc { get; set; }
        public decimal precioUnitDivisa { get; set; }
        public int cntDias { get; set; }
        public decimal importe  { get; set; }
        public Aliado()
        {
            id = -1;
            ciRif = "";
            codigo = "";
            desc = "";
            precioUnitDivisa = 0m;
            cntDias = 0;
            importe = 0m;
        }
    }
}