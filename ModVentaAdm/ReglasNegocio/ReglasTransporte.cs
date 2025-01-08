using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.ReglasNegocio
{
    public class ReglasTransporte: IReglasNegocio
    {
        public bool EditarItemsAntesImprimirDocumentoVenta { get { return true; } }
        public bool EditarEncabezadoAntesImprimirDocumentoVenta { get { return true; } }
    }
}
