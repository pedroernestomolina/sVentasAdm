using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Met
{
    public class itemMetPagoAgregar: PanelPrincipal.Pago.IItemMetPagoAgregar
    {
        private decimal _monto;
        private decimal _factor;
        private string _banco;
        private string _nroCta;
        private string _cheqRefTransf;
        private string _detalleOp;
        private DateTime _fechaOp;
        private LibUtilitis.Opcion.IData _metPago;
        private bool _aplicaFactor;
        private string _lote;
        private string _referencia;
        //
        public decimal GetMonto { get { return _monto; } }
        public decimal GetFactorCambio { get { return _factor; } }
        public string  GetBanco { get { return _banco; } }
        public string GetNroCta { get { return _nroCta; } }
        public string GetCheqRefTranf { get { return _cheqRefTransf; } }
        public string GetDetalleOp { get { return _detalleOp; } }
        public DateTime GetFechaOp { get { return _fechaOp; } }
        public LibUtilitis.Opcion.IData GetMetodo { get { return _metPago; } }
        public bool GetAplicaFactor { get { return _aplicaFactor; } }
        public decimal GetTasa { get { return _factor; } }
        public string GetReferencia { get {return _referencia;} }
        public string GetLote { get { return _lote; } }
        public LibUtilitis.Opcion.IData GetMetCobro { get { return _metPago; } }
        public decimal ImporteMonDiv { get { return calculoMonDiv(); } }
        public decimal ImporteMonAct { get { return calculoMonAct(); } }
        //
        public itemMetPagoAgregar() 
        {
            limpiar();
        }
        public void Inicializa()
        {
            limpiar();
        }
        public bool IsValido()
        {
            if (GetMetodo == null)
            {
                Helpers.Msg.Error("CAMPO [METODO DE COBRO] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (GetMonto <= 0m)
            {
                Helpers.Msg.Error("CAMPO [MONTO] NO PUEDE SER CERO (0)");
                return false;
            }
            return true;
        }
        //
        public void setMetCobro(LibUtilitis.Opcion.IData data)
        {
            _metPago = data;
        }
        public void setMonto(decimal monto)
        {
            _monto = monto;
        }
        public void setFactor(decimal factor)
        {
            _factor = factor;
        }
        public void setBanco(string banco)
        {
            _banco = banco;
        }
        public void setCtaNro(string cta)
        {
            _nroCta = cta;
        }
        public void setChequeRefTranf(string cheqRefTranf)
        {
            _cheqRefTransf = cheqRefTranf;
        }
        public void setFechaOperacion(DateTime fecha)
        {
            _fechaOp = fecha;
        }
        public void setDetalleOperacion(string detalleOp)
        {
            _detalleOp = detalleOp;
        }
        public void setAplicaFactor(bool p)
        {
            _aplicaFactor = p;
        }
        public void setLote(string lote)
        {
            _lote=lote;
        }
        public void setReferencia(string referenc)
        {
            _referencia=referenc;
        }
        //
        private void limpiar()
        {
            _monto = 0m;
            _factor = 1m;
            _banco = "";
            _nroCta = "";
            _cheqRefTransf = "";
            _detalleOp = "";
            _fechaOp = DateTime.Now.Date;
            _referencia = "";
            _lote = "";
            _metPago = null;
            _aplicaFactor = false;
        }
        private decimal calculoMonDiv()
        {
            var rt = _monto;
            if (_aplicaFactor)
            {
                if (_factor > 0m)
                {
                    rt = _monto / _factor;
                }
            }
            return rt;
        }
        private decimal calculoMonAct()
        {
            var rt = _monto;
            if (!_aplicaFactor)
            {
                rt = _monto * _factor;
            }
            return rt;
        }
    }
}