using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class dataTotales
    {
        private decimal _tasaDivisaActual;
        private decimal _montoNeto_MonedaActual;
        private decimal _montoIva_MonedaActual;
        private decimal _montoTotal_MonedaActual;
        private decimal _montoTotal_MonedaDivisa;
        private decimal _montoNeto_MonedaDivisa;
        private decimal _montoIva_MonedaDivisa;


        public decimal TasaDivisaActual_Get { get { return _tasaDivisaActual; } }
        public decimal MontoNeto_MonedaActual_Get { get { return _montoNeto_MonedaActual; } }
        public decimal MontoIva_MonedaActual_Get { get { return _montoIva_MonedaActual; } }
        public decimal MontoTotal_MonedaActual_Get { get { return _montoTotal_MonedaActual; } }
        public decimal MontoTotal_MonedaDivisa_Get { get { return _montoTotal_MonedaDivisa; } }


        public dataTotales()
        {
            limpiar();
        }


        public void Inicializa()
        {
            limpiar();
        }
        public void setTasaDivisa(decimal tasa)
        {
            _tasaDivisaActual = tasa;
        }

        private void limpiar() 
        {
            _montoNeto_MonedaActual = 0m;
            _montoIva_MonedaActual = 0m;
            //
            _montoNeto_MonedaDivisa = 0m;
            _montoIva_MonedaDivisa = 0m;
            //
            _montoTotal_MonedaActual = 0m;
            _montoTotal_MonedaDivisa = 0m;
        }

        public void setMontoNetoDivisa(decimal monto)
        {
            _montoNeto_MonedaDivisa = monto;
            _montoNeto_MonedaActual = monto*_tasaDivisaActual;
            CalcularTotales();
        }
        public void setMontoIvaDivisa(decimal monto)
        {
            _montoIva_MonedaDivisa = monto;
            _montoIva_MonedaActual = monto*_tasaDivisaActual;
            CalcularTotales();
        }
        private void CalcularTotales()
        {
            _montoTotal_MonedaActual = _montoNeto_MonedaActual + _montoIva_MonedaActual;
            _montoTotal_MonedaDivisa = _montoNeto_MonedaDivisa + _montoIva_MonedaDivisa;
        }


        public void LimpiarTodo()
        {
            limpiar();
        }
    }
}