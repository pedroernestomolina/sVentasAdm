using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class dataTotales: IItemObservador
    {
        private decimal _tasaDivisaActual;
        private decimal _montoNeto_MonedaActual;
        private decimal _montoIva_MonedaActual;
        private decimal _montoTotal_MonedaActual;
        private decimal _montoTotal_MonedaDivisa;
        private decimal _montoNeto_MonedaDivisa;
        private decimal _subTotalNeto;
        private decimal _exento;
        private decimal _base1;
        private decimal _base2;
        private decimal _base3;
        private decimal _iva1;
        private decimal _iva2;
        private decimal _iva3;
        private decimal _tasa1;
        private decimal _tasa2;
        private decimal _tasa3;
        private dataItem _items;
        private List<ITotalObservador> _observadores;


        public decimal TasaDivisaActual_Get { get { return _tasaDivisaActual; } }
        public decimal MontoNeto_MonedaActual_Get { get { return _montoNeto_MonedaActual; } }
        public decimal MontoIva_MonedaActual_Get { get { return _montoIva_MonedaActual; } }
        public decimal MontoTotal_MonedaActual_Get { get { return _montoTotal_MonedaActual; } }
        public decimal MontoNeto_MonedaDivisa_Get { get { return _montoNeto_MonedaDivisa; } }
        public decimal MontoTotal_MonedaDivisa_Get { get { return _montoTotal_MonedaDivisa; } }


        public dataTotales(dataItem items)
        {
            _observadores = new List<ITotalObservador>();
            _items = items;
            items.RegistrarObservador(this);
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
        public void setTasaFiscal(List<OOB.Sistema.Fiscal.Entidad.Ficha> list)
        {
            if (list == null)
            {
                return;
            }
            if (list.Count == 0) 
            {
                return;
            }
            if (list.Count < 3)
            {
                return;
            }
            _tasa1 = list[0].tasa;
            _tasa2 = list[1].tasa;
            _tasa3 = list[2].tasa;
        }

        private void limpiar() 
        {
            _exento = 0m;
            _base1 = 0m;
            _base2 = 0m;
            _base3 = 0m;
            _iva1 = 0m;
            _iva2 = 0m;
            _iva3 = 0m;
            //
            _montoNeto_MonedaActual = 0m;
            _montoNeto_MonedaDivisa = 0m;
            //
            _montoIva_MonedaActual = 0m;
            //
            _montoTotal_MonedaActual = 0m;
            _montoTotal_MonedaDivisa = 0m;
        }

        public void setMontoNetoDivisa(decimal monto)
        {
            _montoNeto_MonedaActual = monto * _tasaDivisaActual;
            _subTotalNeto = _montoNeto_MonedaActual;
            CalcularTotales();
        }
        private void CalcularTotales()
        {
            _montoTotal_MonedaDivisa = 0m;
            _montoIva_MonedaActual = _iva1 + _iva2 + _iva3;
            _montoTotal_MonedaActual = _montoNeto_MonedaActual + _montoIva_MonedaActual;
            if (_tasaDivisaActual > 0m) 
            {
                _montoNeto_MonedaDivisa = _montoNeto_MonedaActual / _tasaDivisaActual;
                _montoTotal_MonedaDivisa = _montoTotal_MonedaActual / _tasaDivisaActual;
            }
        }


        public void LimpiarTodo()
        {
            limpiar();
        }
        public bool DataIsOk()
        {
            if (_montoTotal_MonedaActual == 0m)
            {
                Helpers.Msg.Alerta("MONTO TOTAL (Bs) INCORRECTO");
                return false;
            }
            if (_montoTotal_MonedaDivisa == 0m)
            {
                Helpers.Msg.Alerta("MONTO TOTAL ($) INCORRECTO");
                return false;
            }
            return true;
        }

        public void setMontoNetoDivisa_Exento(decimal monto)
        {
            _exento = monto * _tasaDivisaActual;
            CalcularTotales();
        }
        public void setMontoNetoDivisa_Tasa1(decimal monto)
        {
            _base1 = monto * _tasaDivisaActual;
            _iva1=calculaIva(_base1, _tasa1);
            CalcularTotales();
        }
        public void setMontoNetoDivisa_Tasa2(decimal monto)
        {
            _base2 = monto * _tasaDivisaActual;
            _iva2=calculaIva(_base2, _tasa2);
            CalcularTotales();
        }
        public void setMontoNetoDivisa_Tasa3(decimal monto)
        {
            _base3 = monto * _tasaDivisaActual;
            _iva3 = calculaIva(_base3, _tasa3);
            CalcularTotales();
        }
        private decimal calculaIva(decimal neto, decimal tasa)
        {
            var r = 0m;
            if (tasa > 0m) 
            {
                r = neto * tasa / 100;
            }
            return r;
        }

        //
        public decimal SubTotalNeto_Get { get { return _subTotalNeto; } }
        public decimal MontoExento_Get { get { return _exento; } }
        public decimal MontoBase1_Get { get { return _base1; } }
        public decimal MontoBase2_Get { get { return _base2; } }
        public decimal MontoBase3_Get { get { return _base3; } }
        public decimal MontoIva1_Get { get { return _iva1; } }
        public decimal MontoIva2_Get { get { return _iva2; } }
        public decimal MontoIva3_Get { get { return _iva3; } }


        public void OnItemAgregado(Item.IItem item)
        {
            ActualizarMontos();
            NotificarObservadores();
        }
        public void OnItemEliminado(Item.IItem item)
        {
            ActualizarMontos();
            NotificarObservadores();
        }

        private void ActualizarMontos()
        {
            setMontoNetoDivisa(_items.MontoNetoDivisa);
            setMontoNetoDivisa_Exento(_items.MontoNetoDivisa_Exento);
            setMontoNetoDivisa_Tasa1(_items.MontoNetoDivisa_Tasa1);
            setMontoNetoDivisa_Tasa2(_items.MontoNetoDivisa_Tasa2);
            setMontoNetoDivisa_Tasa3(_items.MontoNetoDivisa_Tasa3);
        }
        private void NotificarObservadores()
        {
            foreach (var obs in _observadores) 
            {
                obs.ActualizarTotal();
            }
        }

        public void RegistrarObservador(MargenGanancia.Imp observador)
        {
            _observadores.Add(observador);
        }
        public void EliminarObservador(MargenGanancia.Imp observador)
        {
            _observadores.Remove(observador);
        }
    }
}