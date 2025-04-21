using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.ListaCtasPend
{
    public class data : Idata
    {
        private OOB.CxC.Tools.CtasPendiente.Lista.Ficha _ficha;
        //
        public string ciRif { get; set; }
        public string nombreRazonSocial { get; set; }
        public decimal montoImporte { get; set; }
        public decimal montoAcumulado { get; set; }
        public int cntFactPend { get; set; }
        public string montoPorAnticipo { get; set; }
        public string montoPorCreditos { get; set; }
        public decimal montoResta { get { return montoImporte - montoAcumulado; } }
        public OOB.CxC.Tools.CtasPendiente.Lista.Ficha Ficha { get { return _ficha; } }
        //
        public data(object rg)
        {
            _ficha = (OOB.CxC.Tools.CtasPendiente.Lista.Ficha)rg;
            ciRif = _ficha.ciRif;
            cntFactPend = _ficha.cntFactPend;
            montoAcumulado = _ficha.acumulado;
            montoImporte = _ficha.importe;
            nombreRazonSocial = _ficha.nombreRazonSocial;
            montoPorAnticipo = _ficha.anticiposCliente.ToString("n2");
            montoPorCreditos = _ficha.importePorNtCreditoPendPorSaldarMonDiv.ToString("n2");
        }
    }
}