using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.MargenGanancia
{
    public class data
    {
        private decimal _montoDoc;
        private decimal _islr;
        private decimal _anticipoIslr;
        private decimal _igtfBs;
        private decimal _igtfDivisa;
        private decimal _impMunicipal;
        private bool _igtfBsActivo;
        private bool _igtfDivisaActivo;
        private decimal _subTotal;
        private decimal _pagoAliado;
        private decimal _margen;


        public decimal MontoDoc_Get { get { return _montoDoc; } }
        public decimal ISLR_Get { get { return _islr; } }
        public decimal AnticipoISLR_Get { get { return _anticipoIslr; } }
        public decimal IGTFbs_Get { get { return _igtfBs; } }
        public decimal IGTFdivisa_Get { get { return _igtfDivisa; } }
        public decimal IMP_MUNICIPAL_Get { get { return _impMunicipal; } }
        public bool IGTF_BS_ACTIVO_Get { get { return _igtfBsActivo; } }
        public bool IGTF_DIVISA_ACTIVO_Get { get { return _igtfDivisaActivo; } }
        public decimal SubTotal_Get { get { return _subTotal; } }
        public decimal PagoAliado_Get { get { return _pagoAliado; } }
        public decimal MargenBeneficio_Get { get { return _margen; } }


        public data()
        {
            _montoDoc = 0m;
            _islr = 0m;
            _anticipoIslr = 0m;
            _igtfBs = 0m;
            _igtfDivisa = 0m;
            _impMunicipal = 0m;
            _igtfBsActivo  = false;
            _igtfDivisaActivo = false;
            _subTotal = 0m;
            _pagoAliado = 0m;
            _margen = 0m;
        }
        public void Inicializa()
        {
            _montoDoc = 0m;
            _islr = 2m;
            _anticipoIslr = 1m;
            _igtfBs = 2m;
            _igtfDivisa = 3m;
            _impMunicipal = 2.5m;
            _igtfBsActivo = false;
            _igtfDivisaActivo = false;
            _subTotal = 0m;
            _pagoAliado = 0m;
            _margen = 0m;
        }


        public void setMontoDoc(decimal monto)
        {
            _montoDoc = monto;
            calcular();
        }
        public void setPagoAliado(decimal monto)
        {
            _pagoAliado = monto;
            calcular();
        }
        public void setISLR(decimal v)
        {
            _islr = v;
            calcular();
        }
        public void setAnticipoISLR(decimal v)
        {
            _anticipoIslr = v;
            calcular();
        }
        public void setIGTFbs(decimal v)
        {
            _igtfBs = v;
            calcular();
        }
        public void setIGTFdivisa(decimal v)
        {
            _igtfDivisa = v;
            calcular();
        }
        public void setImpMunicipal(decimal v)
        {
            _impMunicipal = v;
            calcular();
        }
        public void setActivarIGTFbs(bool p)
        {
            _igtfBsActivo = p;
            calcular();
        }
        public void setActivarIGTFDivisa(bool p)
        {
            _igtfDivisaActivo= p;
            calcular();
        }


        private void calcular()
        {
            var r=0m;
            var d = 0m;
            r = _montoDoc;

            var isrl = (r * (_islr / 100));
            r = r - isrl;

            var antisrl = (r * (_anticipoIslr / 100));
            r = r - antisrl;

            if (_igtfBsActivo) 
            {
                var igtfbs = (r * (_igtfBs / 100));
                r = r - igtfbs;
            }
            if (_igtfDivisaActivo)
            {
                var igtfdivisa = (r * (_igtfDivisa / 100));
                r = r - igtfdivisa;
            }
            d =_montoDoc * (_impMunicipal / 100);
            d += (_montoDoc - r);
            _subTotal = _montoDoc - d;
            _margen = _subTotal - _pagoAliado;
        }
    }
}