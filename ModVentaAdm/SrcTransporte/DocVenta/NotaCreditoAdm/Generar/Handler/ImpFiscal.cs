using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Handler
{
    public class ImpFiscal: Vista.IFiscal
    {
        private decimal _tasaF;
        private decimal _baseF;
        private decimal _ivaF;
        private decimal _total;
        //
        public decimal Get_Tasa { get { return _tasaF; } }
        public decimal Get_Base { get { return _baseF; } }
        public decimal Get_Iva { get { return _ivaF; } }
        public decimal Get_Total { get { return _total; } }
        //
        public ImpFiscal()
        {
            _tasaF = 0m;
            _baseF = 0m;
            _ivaF = 0m;
            _total = 0m;
        }
        public void Inicializa()
        {
            _tasaF = 0m;
            _baseF = 0m;
            _ivaF = 0m;
            _total = 0m;
        }
        public void setBase(decimal monto)
        {
            _baseF=monto;
            calcular();
        }
        public void setTasa(decimal tasa)
        {
            _tasaF=tasa;
            calcular();
        }
        //
        private void calcular()
        {
            _ivaF = (_baseF * (_tasaF / 100m));
            _total = (_baseF + (_baseF * (_tasaF / 100m)));
        }
    }
}