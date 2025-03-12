using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Resumen
{
    public class HndPanelResumen: PanelPrincipal.Pago.IPanelResumen
    {
        private decimal _montoAnticipo;
        private decimal _montoMetPago;
        private decimal _montoNtCredito;
        private decimal _montoCtasPend;
        //
        public decimal GetResumenMontoAnticipo { get { return _montoAnticipo; } }
        public decimal GetResumenMontoMetPago { get { return _montoMetPago; } }
        public decimal GetResumenMontoNtCredito { get { return _montoNtCredito; } }
        public decimal GetResumenMontoAbono { get { return _montoAnticipo + _montoMetPago + _montoNtCredito; } }
        public decimal GetResumenMontoCtasPend { get { return _montoCtasPend; } }
        public decimal GetResumenSaldo { get { return (_montoAnticipo + _montoMetPago + _montoNtCredito) - _montoCtasPend; } }
        public string GetResumenSaldoDesc
        {
            get 
            {
                var rt = "";
                if (GetResumenSaldo > 0)
                    rt = "SOBRANTE";
                else if (GetResumenSaldo < 0)
                    rt = "FALTANTE";
                else
                    rt="";
                return rt;
            }
        }
        //
        public HndPanelResumen()
        {
            _montoAnticipo = 0m;
            _montoMetPago = 0m;
            _montoNtCredito = 0m;
            _montoCtasPend = 0m;
        }
        public void Inicializa()
        {
            _montoAnticipo = 0m;
            _montoMetPago = 0m;
            _montoNtCredito = 0m;
            _montoCtasPend = 0m;
        }
        public void setMontoMetPago(decimal p)
        {
            _montoMetPago = p;
        }
        public void setMontoAnticipo(decimal p)
        {
            _montoAnticipo = p;
        }
        public void setMontoNtCredito(decimal p)
        {
            _montoNtCredito = p;
        }
        public void setMontoCtasPend(decimal p)
        {
            _montoCtasPend =p;
        }
    }
}