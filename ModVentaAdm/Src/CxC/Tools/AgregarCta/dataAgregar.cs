using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.AgregarCta
{
    
    public class dataAgregar
    {

        private string _serieDoc;
        private string _numDoc;
        private DateTime _fechaEmisionDoc;
        private int _diasCreditoDoc;
        private decimal _montoDoc;
        private string _notasDoc;
        private decimal _factor;
        private Gestion.ficha _vend;
        private Gestion.ficha _tipoDoc;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;


        public OOB.Maestro.Cliente.Entidad.Ficha ClienteGet { get { return _cliente; } }
        public Gestion.ficha VendedorGet { get { return _vend; } }
        public Gestion.ficha TipoDocGet { get { return _tipoDoc; } }
        public int DiasCreditoDocGet { get { return _diasCreditoDoc; } }
        public DateTime FechaEmisionDocGet { get { return _fechaEmisionDoc; } }
        public string SerieDocGet { get { return _serieDoc; } }
        public string NumeroDocGet { get { return _numDoc; } }
        public string NotasDocGet { get { return _notasDoc; } }
        public decimal MontDivisaDocGet { get { return _montoDoc; } }
        public DateTime FechaVencimientoDocGet { get { return _fechaEmisionDoc.AddDays(_diasCreditoDoc); } }
        public string ClienteDataGet { get { return _cliente == null ? "" : _cliente.ciRif + Environment.NewLine + _cliente.razonSocial; } }
        public decimal TasaFactorDocGet { get { return _factor; } }
        public decimal MontoDoc { get { return _montoDoc * _factor; } }


        public dataAgregar() 
        {
            limpiar();
        }


        public void Inicializa()
        {
            limpiar();
        }
        public void setSerieDoc(string p)
        {
            _serieDoc = p;
        }
        public void setNumDoc(string p)
        {
            _numDoc = p;
        }
        public void setFechaEmisionDoc(DateTime fecha)
        {
            _fechaEmisionDoc = fecha;
        }
        public void setVend(Gestion.ficha ficha)
        {
            _vend=ficha;
        }
        public void setTipoDoc(Gestion.ficha ficha)
        {
            _tipoDoc = ficha;
        }
        public void setDiasCreditoDoc(int cnt)
        {
            _diasCreditoDoc = cnt;
        }
        public void setMontoDoc(decimal monto)
        {
            _montoDoc = monto;
        }
        public void setFactor(decimal tasa)
        {
            _factor = tasa;
        }
        public void setNotas(string p)
        {
            _notasDoc = p;
        }
        public void setCliente(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _cliente = ficha;
        }
        public bool IsOk()
        {
            var rt = true;
            if (_cliente == null) 
            {
                Helpers.Msg.Error("CAMPO [ CLIENTE ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_tipoDoc == null)
            {
                Helpers.Msg.Error("CAMPO [ TIPO DE DOCUMENTO ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_serieDoc.Trim() == "")
            {
                Helpers.Msg.Error("CAMPO [ SERIE ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_numDoc.Trim() == "")
            {
                Helpers.Msg.Error("CAMPO [ DOCUMENTO NRO ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_vend == null)
            {
                Helpers.Msg.Error("CAMPO [ VENDEDOR ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_montoDoc <=0m)
            {
                Helpers.Msg.Error("CAMPO [ MONTO ] INCORRECTO");
                return false;
            }
            if (_factor <= 0m)
            {
                Helpers.Msg.Error("CAMPO [ TASA/FACTOR CAMBIO ] INCORRECTO");
                return false;
            }
            if (_notasDoc.Trim()=="")
            {
                Helpers.Msg.Error("CAMPO [ NOTAS ] NO PUEDE ESTAR VACIO");
                return false;
            }

            return rt;
        }


        private void limpiar()
        {
            _serieDoc = "";
            _numDoc = "";
            _fechaEmisionDoc = DateTime.Now.Date;
            _diasCreditoDoc = 0;
            _montoDoc = 0m;
            _factor = 0m;
            _notasDoc = "";
            _vend = null;
            _tipoDoc = null;
            _cliente = null;
        }

    }

}