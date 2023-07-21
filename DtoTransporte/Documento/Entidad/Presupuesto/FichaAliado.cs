using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad.Presupuesto
{
    public class FichaAliado
    {
        public int idAliado { get; set; }
        public string ciRif { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal precioUnitDivisa { get; set; }
        public int cntDias { get; set; }
        public decimal importe  { get; set; }
        public FichaAliado()
        {
            idAliado = -1;
            ciRif = "";
            codigo = "";
            descripcion = "";
            precioUnitDivisa = 0m;
            cntDias = 0;
            importe = 0m;
        }
    }
}