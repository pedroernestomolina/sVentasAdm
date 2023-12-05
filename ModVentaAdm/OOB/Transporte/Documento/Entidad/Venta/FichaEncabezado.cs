using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Entidad.Venta
{
    public class FichaEncabezado : baseEncabezado
    {
        public string docSolicitadoPor { get; set; }
        public string docModuloCargar { get; set; }
        public decimal igtfTasa { get; set; }
        public decimal igtfMontoMonAct { get; set; }
        public string notasPeriodoLapso { get; set; }
        public FichaEncabezado()
            :base()
        {
            docSolicitadoPor = "";
            docModuloCargar = "";
            igtfTasa = 0m;
            igtfMontoMonAct = 0m;
            notasPeriodoLapso = "";
        }
    }
}