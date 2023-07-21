using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Factura
{
    public class FichaAliado
    {
        public string idCliente { get; set; }
        public string idAliado { get; set; }
        public string ciRifAliado { get; set; }
        public string codigoAliado { get; set; }
        public string nombreDescAliado { get; set; }
        public decimal  precioUnitDivisa { get; set; }
        public int cntDias { get; set; }
        public decimal importeDivisa { get; set; }
        public FichaAliado()
        {
            idCliente = "";
            idAliado="";
            ciRifAliado="";
            codigoAliado="";
            nombreDescAliado="";
            precioUnitDivisa=0m;
            cntDias=0;
            importeDivisa = 0m;
        }
    }
}