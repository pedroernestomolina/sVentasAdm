using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Componente.CajasUtilizar.Handler
{
    public class Imp: Vista.IHnd
    {
        private decimal _factorCambio;
        private decimal _montoPendDiv;
        private decimal _montoPendMonAct;
        private decimal _montoPendMonDiv;
        private List<Vista.Idata> _lst;
        private BindingList<Vista.Idata> _bl;
        private BindingSource _bs;


        public BindingSource Get_CajaSource { get { return _bs; } }
        public IEnumerable <Vista.Idata> Get_Lista { get { return _bl.ToList(); } }
        public IEnumerable<Vista.Idata> Get_CajasUsadas { get { return _bl.Where(w => w.montoAbonar > 0).ToList(); } }


        public Imp()
        {
            _factorCambio = 0m;
            _montoPendDiv = 0m;
            _montoPendMonAct = 0m;
            _montoPendMonDiv = 0m;
            _lst = new List<Vista.Idata>();
            _bl = new BindingList<Vista.Idata>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
        }
        public void Inicializa()
        {
            _montoPendDiv = 0m;
            _montoPendMonAct = 0m;
            _montoPendMonDiv = 0m;
            _lst.Clear();
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void CargarData()
        {
            try
            {
                var _lst = new List<Vista.Idata>();
                var r01 = Sistema.MyData.Transporte_Caja_GetLista();
                foreach (var rg in r01.ListaD.OrderBy(o => o.descripcion).ToList())
                {
                    var nr = new data(rg);
                    _lst.Add(nr);
                }
                setDataCargar(_lst);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void setDataCargar(IEnumerable<Vista.Idata> lst)
        {
            _lst.Clear();
            _bl.Clear();
            foreach (var rg in lst)
            {
                _lst.Add((data)rg);
            }
            _bs.CurrencyManager.Refresh();
        }
        public void EditarMontoAbonar()
        {
            if (_bs.Current != null) 
            {
                var item = (data)_bs.Current;
                var _monto= pedirMontoAbonar(item.montoAbonar);
                if (_monto  >= 0m)
                {
                    item.setMontoAbonar(_monto);
                }
                _bs.CurrencyManager.Refresh();
            }
        }


        private Utils.Componente.Monto.Vistas.IMonto _montoAbonar;
        private decimal pedirMontoAbonar(decimal monto)
        {
            if (_montoAbonar == null)
            {
                _montoAbonar = new Utils.Componente.Monto.Handler.Imp();
            }
            _montoAbonar.Inicializa();
            _montoAbonar.setMonto(monto);
            _montoAbonar.Inicia();
            if (_montoAbonar.ProcesarIsOK) 
            {
                return _montoAbonar.Get_Monto;
            }
            return 0m;
        }
        public void setFactorCambio(decimal factor)
        {
            _factorCambio = factor;
        }
        public void setMontoPendDiv(decimal montoDiv)
        {
            _montoPendDiv = montoDiv;
        }

        public decimal Get_MontoPendMonAct { get { return _montoPendMonAct; } }
        public decimal Get_MontoPendMonDiv { get { return _montoPendMonDiv; } }


        private decimal _restPend;
        public void ActualizarSaldosPend()
        {
            _restPend = 0m;
            var _pgMonDiv = _lst.Where(w => w.esDivisa).Sum(s => s.montoAbonar);
            var _pgMonAct = _lst.Where(w => !w.esDivisa).Sum(s => s.montoAbonar);
            var _pgTotal = (_pgMonDiv * _factorCambio) + _pgMonAct;
            _pgTotal = Math.Round(_pgTotal, 2, MidpointRounding.AwayFromZero);
            _restPend = (_montoPendDiv * _factorCambio) - _pgTotal;
            _restPend = Math.Round(_restPend,2, MidpointRounding.AwayFromZero);
            _montoPendMonAct = _restPend; 
            _montoPendMonDiv = 0m;
            if (_factorCambio > 0m) 
            {
                _montoPendMonDiv = Math.Round(_restPend / _factorCambio, 2, MidpointRounding.AwayFromZero);
            }
        }

        public decimal MontoCajaPago { get { return montoCajaPago(); } }
        private decimal montoCajaPago()
        {
            var _pgMonDiv = _lst.Where(w => w.esDivisa).Sum(s => s.montoAbonar);
            var _pgMonAct = _lst.Where(w => !w.esDivisa).Sum(s => s.montoAbonar);
            var _pgTotal = (_pgMonDiv * _factorCambio) + _pgMonAct;
            return _pgTotal;
        }
        public bool IsOk()
        {
            if (Math.Abs(_montoPendMonAct)>0m)
            {
                Helpers.Msg.Alerta("HAY MONTOS PENDIENTES");
                return false;
            }
            if (Math.Abs(_montoPendMonDiv) > 0m)
            {
                Helpers.Msg.Alerta("HAY MONTOS PENDIENTES");
                return false;
            }
            return true;
        }
    }
}