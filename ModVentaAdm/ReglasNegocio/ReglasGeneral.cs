using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.ReglasNegocio
{
    public class ReglasGeneral: IReglasNegocio
    {
        public bool EditarItemsAntesImprimirDocumentoVenta { get { return false; } }
        public bool EditarEncabezadoAntesImprimirDocumentoVenta { get { return false; } }
    }
}