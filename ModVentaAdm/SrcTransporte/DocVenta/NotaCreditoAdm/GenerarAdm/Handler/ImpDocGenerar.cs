using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Handler
{
    public class ImpDocGenerar : Vista.IDocGenerar
    {
        private DateTime _fechaEmision;
        private string _motivo;
        private Generar.Vista.IFiscal _mExento;
        private Generar.Vista.IFiscal _m1;
        private Generar.Vista.IFiscal _m2;
        private Generar.Vista.IFiscal _m3;
        private string _docNro;
        private decimal _tasaCambio;
        //
        public Generar.Vista.IFiscal MontoExento { get { return _mExento; } }
        public Generar.Vista.IFiscal MontoFiscal_1 { get { return _m1; } }
        public Generar.Vista.IFiscal MontoFiscal_2 { get { return _m2; } }
        public Generar.Vista.IFiscal MontoFiscal_3 { get { return _m3; } }
        public decimal Get_Subt_Base { get { return _m1.Get_Base + _m2.Get_Base + _m3.Get_Base; } }
        public decimal Get_Subt_Imp { get { return _m1.Get_Iva + _m2.Get_Iva + _m3.Get_Iva; } }
        public decimal Get_Total { get { return _m1.Get_Total + _m2.Get_Total + _m3.Get_Total + _mExento.Get_Total; } }
        public string Get_Motivo { get { return _motivo; } }
        public DateTime Get_FechaEmision { get { return _fechaEmision; } }
        public string Get_DocNumero { get { return _docNro; } }
        public decimal Get_TasaCambio { get { return _tasaCambio; } }
        //
        public ImpDocGenerar()
        {
            _mExento = new Generar.Handler.ImpFiscal();
            _m1 = new Generar.Handler.ImpFiscal();
            _m2 = new Generar.Handler.ImpFiscal();
            _m3 = new Generar.Handler.ImpFiscal();
            _fechaEmision = DateTime.Now.Date;
            _docNro = "";
            _tasaCambio = 0m;
            _motivo = "";
        }
        public void Inicializa()
        {
            _mExento.Inicializa();
            _m1.Inicializa();
            _m2.Inicializa();
            _m3.Inicializa();
            _fechaEmision = DateTime.Now.Date;
            _docNro = "";
            _tasaCambio = 0m;
            _motivo = "";
        }
        public void setFechaEmision(DateTime fecha)
        {
            _fechaEmision = fecha;
        }
        public void setDocumentoNro(string docNro)
        {
            _docNro = docNro;
        }
        public void setFactorCambio(decimal tasaCamb)
        {
            _tasaCambio= tasaCamb;
        }
        public void setMotivo(string mot)
        {
            _motivo = mot;
        }
        public void ValidarDataIsOk()
        {
            if (_docNro.Trim() == "")
            {
                throw new Exception("DEBES INDICAR UN NUMERO DE DOCUMENTO");
            }
            if (_tasaCambio <=0m)
            {
                throw new Exception("DEBES INDICAR UN FACTOR / TASA DE CAMBIO");
            }
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
            _fechaEmision = DateTime.Now.Date;
            _docNro = "";
            _tasaCambio = 0m;
            _motivo = "";
            _mExento.Inicializa();
            _m1.Inicializa();
            _m2.Inicializa();
            _m3.Inicializa();
        }
    }
}