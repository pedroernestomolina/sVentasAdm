using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro
{
    
    public class dataItem
    {

        private decimal _monto;
        private decimal _factor;
        private string _banco;
        private string _nroCta;
        private string _cheqRefTransf;
        private string _detalleOp;
        private DateTime _fechaOp;
        private Gestion.ficha _metodo;
        private bool _aplicaFactor;
        private string _lote;
        private string _referencia;


        public decimal GetMonto { get { return _monto; } }
        public decimal GetFactorCambio { get { return _factor; } }
        public string  GetBanco { get { return _banco; } }
        public string GetNroCta { get { return _nroCta; } }
        public string GetCheqRefTranf { get { return _cheqRefTransf; } }
        public string GetDetalleOp { get { return _detalleOp; } }
        public DateTime GetFechaOp { get { return _fechaOp; } }
        public Gestion.ficha GetMetodo { get { return _metodo; } }
        public bool GetAplicaFactor { get { return _aplicaFactor; } }
        public decimal GetTasa { get { return _factor; } }
        public string GetReferencia { get {return _referencia;} }
        public string GetLote { get { return _lote; } }
        public decimal Importe 
        {
            get 
            {
                var rt = _monto;
                if (_aplicaFactor) 
                {
                    if (_factor > 0) 
                    {
                        rt = _monto / _factor;
                    }
                }
                return rt;
            }
        }
        public decimal ImporteMonedaLocal 
        {
            get 
            {
                var rt = _monto;
                if (!_aplicaFactor)
                {
                    rt = _monto * _factor;
                }
                return rt;
            }
        }


        public dataItem() 
        {
            limpiar();
        }


        public void Limpiar()
        {
            limpiar();
        }


        private void limpiar()
        {
            _monto = 0m;
            _factor = 1m;
            _banco = "";
            _nroCta = "";
            _cheqRefTransf = "";
            _detalleOp = "";
            _fechaOp = DateTime.Now.Date;
            _referencia="";
            _lote="";
            _metodo = null;
        }


        public void setMetCobro(Gestion.ficha ficha)
        {
            _metodo = ficha;
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

    }

}