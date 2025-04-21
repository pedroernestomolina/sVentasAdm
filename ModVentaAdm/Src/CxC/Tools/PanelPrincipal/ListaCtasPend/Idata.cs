using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.ListaCtasPend
{
    public interface Idata
    {
        string ciRif { get; set; }
        string nombreRazonSocial { get; set; }
        decimal montoImporte { get; set; }
        decimal montoAcumulado { get; set; }
        int cntFactPend { get; set; }
        decimal montoResta { get; }
        string montoPorAnticipo { get; set; }
        string montoPorCreditos { get; set; }
    }
}
