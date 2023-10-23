using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.AplicaRetencionIslr.Handler
{
    public class Imp: Vista.IHnd
    {
        private decimal _tasaRet;
        private decimal _montoSustraendo;
        private decimal _montoRetencion;
        private bool _aplicaRet;
        private decimal _factorCambio;
        private decimal _montoAplicarRetMonAct;
        private decimal _totalRetencion;


        public decimal Get_FactorCambio { get { return _factorCambio; } }
        public decimal Get_MontoAplicarRetencionMonAct { get { return _montoAplicarRetMonAct; } }
        public decimal Get_TasaRetencion { get { return _tasaRet; } }
        public decimal Get_MontoSustraendo { get { return _montoSustraendo; } }
        public decimal Get_MontoRetencion { get { return _montoRetencion; } }
        public bool Get_AplicaRet { get { return _aplicaRet; } }
        public decimal Get_TotalRetencionMonAct { get { return _totalRetencion; } }
        public decimal Get_TotalRetencionMonDiv
        {
            get
            {
                var rt = 0m;
                if (_factorCambio > 0m)
                {
                    rt = _totalRetencion / _factorCambio;
                    rt = Math.Round(rt, 2, MidpointRounding.AwayFromZero);
                }
                return rt;
            }
        }

        public Imp()
        {
            _factorCambio = 0m;
            _montoSustraendo = 0m;
            _tasaRet = 0m;
            _montoRetencion = 0m;
            _aplicaRet = false;
            _montoAplicarRetMonAct = 0m;
            _totalRetencion = 0m;
        }
        public void Inicializa()
        {
            _montoSustraendo = 0m;
            _tasaRet = 0m;
            _montoRetencion = 0m;
            _aplicaRet = false;
            _montoAplicarRetMonAct = 0m;
            _totalRetencion = 0m;
        }


        public void setFactorCambio(decimal factor)
        {
            _factorCambio = factor;
        }
        public void setMontoAplicarRetencionMonAct(decimal monto)
        {
            _montoAplicarRetMonAct = monto;
        }
        public void setTasaRet(decimal monto)
        {
            _tasaRet = monto;
            calculoRet();
        }
        public void setMontoSustraendo(decimal monto)
        {
            _montoSustraendo = monto;
            calculoRet();
        }
        public void setAplicaRet(bool aplica)
        {
            _aplicaRet = !_aplicaRet;
            if (!_aplicaRet)
            {
                _montoSustraendo = 0m;
                _tasaRet = 0m;
                _montoRetencion = 0m;
            }
            calculoRet();
        }
        public void setTotalRetencionMonAct(decimal monto)
        {
            _totalRetencion = monto;
        }
        public bool IsOk()
        {
            if (_aplicaRet) 
            {
                if (_tasaRet <= 0m)
                {
                    Helpers.Msg.Alerta("CAMPO [ TASA RETENCION ] NO PUEDE SER CERO (0)");
                    return false;
                }
                if (_totalRetencion <= 0m)
                {
                    Helpers.Msg.Alerta("MONTO RETENCION NO PUEDE SER CERO (0)");
                    return false;
                }
            }
            return true;
        }


        private void calculoRet()
        {
            _totalRetencion = 0m;
            _totalRetencion = (_montoAplicarRetMonAct * _tasaRet / 100) + _montoSustraendo;
            _totalRetencion = Math.Round(_totalRetencion, 2, MidpointRounding.AwayFromZero);
        }
    }
}