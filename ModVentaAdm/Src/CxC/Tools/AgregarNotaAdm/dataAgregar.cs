using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.AgregarNotaAdm
{
    
    public class dataAgregar
    {

        private decimal _montoDoc;
        private string _notasDoc;
        private decimal _factor;
        private Gestion.ficha _vend;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;


        public OOB.Maestro.Cliente.Entidad.Ficha ClienteGet { get { return _cliente; } }
        public Gestion.ficha VendedorGet { get { return _vend; } }
        public string NotasDocGet { get { return _notasDoc; } }
        public decimal MontDivisaDocGet { get { return _montoDoc; } }
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
        public void setVend(Gestion.ficha ficha)
        {
            _vend=ficha;
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
            _montoDoc = 0m;
            _factor = 0m;
            _notasDoc = "";
            _vend = null;
            _cliente = null;
        }

    }

}