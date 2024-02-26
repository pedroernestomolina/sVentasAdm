using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Handler
{
    public class ImpDocGenerar : Vista.IDocGenerar
    {
        private string _motivo;
        private Vista.IFiscal _mExento;
        Vista.IFiscal _m1;
        Vista.IFiscal _m2;
        Vista.IFiscal _m3;
        //
        public Vista.IFiscal MontoExento { get { return _mExento; } }
        public Vista.IFiscal MontoFiscal_1 { get { return _m1; } }
        public Vista.IFiscal MontoFiscal_2 { get { return _m2; } }
        public Vista.IFiscal MontoFiscal_3 { get { return _m3; } }
        public decimal Get_Subt_Base { get { return _m1.Get_Base + _m2.Get_Base + _m3.Get_Base; } }
        public decimal Get_Subt_Imp { get { return _m1.Get_Iva + _m2.Get_Iva + _m3.Get_Iva; } }
        public decimal Get_Total { get { return _m1.Get_Total + _m2.Get_Total + _m3.Get_Total + _mExento.Get_Total; } }
        public string Get_Motivo { get { return _motivo; } }    
        //
        public ImpDocGenerar()
        {
            _mExento = new ImpFiscal();
            _m1 = new ImpFiscal();
            _m2 = new ImpFiscal();
            _m3 = new ImpFiscal();
            _motivo = "";
        }
        public void Inicializa()
        {
            _mExento.Inicializa();
            _m1.Inicializa();
            _m2.Inicializa();
            _m3.Inicializa();
            _motivo = "";
        }
        public void setMotivo(string mot)
        {
            _motivo = mot;
        }
        public void ValidarDataIsOk()
        {
            if (_motivo.Trim() == "")
            {
                throw new Exception("DEBES INDICAR UN MOTIVO");
            }
            if (Get_Total <= 0m)
            {
                throw new Exception("MONTO NOTA DE CREDITO INCORRECTO");
            }
        }
        public void Limpiar()
        {
            _motivo = "";
            _mExento.Inicializa();
            _m1.Inicializa();
            _m2.Inicializa();
            _m3.Inicializa();
        }
    }
}