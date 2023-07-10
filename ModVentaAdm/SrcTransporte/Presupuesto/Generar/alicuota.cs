using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class alicuota: Utils.dataFiltro
    {
        public decimal  tasa { get; set; }
        public alicuota()
            :base()
        {
            tasa = 0m;
        }
    }
}
