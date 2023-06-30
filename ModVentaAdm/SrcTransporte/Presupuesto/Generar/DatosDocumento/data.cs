using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.DatosDocumento
{
    public class ficha: LibUtilitis.Opcion.IData 
    {
        public string id { get; set; }
        public string desc { get; set; }
        public string codigo{ get; set; }
    }
    public class data
    {
        private DateTime _fechaSistema;
        private DateTime _fechaVencimiento;
        private int _diasValidez;
        private int _diasCredito;
        private LibUtilitis.CtrlCB.ICtrl _condPago;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;


        public DateTime FechaSistema_Get { get { return _fechaSistema; } }
        public DateTime FechaVencimiento_Get { get { return _fechaVencimiento; } }
        public int DiasValidez_Get { get { return _diasValidez; } }
        public int DiasCredito_Get { get { return _diasCredito; } }
        public LibUtilitis.CtrlCB.ICtrl CondicionPago { get { return _condPago; } }
        public OOB.Maestro.Cliente.Entidad.Ficha Cliente { get { return _cliente; } }
        public string CondPago_Get { get { return _condPago.GetItem == null ? "" : _condPago.GetItem.desc; } }
        public string Cliente_ciRif_Get { get { return _cliente == null ? "" : _cliente.ciRif; } }
        public string Cliente_codigo_Get { get { return _cliente == null ? "" : _cliente.codigo; } }
        public string Cliente_razonSocial_Get { get { return _cliente == null ? "" : _cliente.razonSocial; } }
        public string Cliente_GetInf 
        { 
            get 
            {
                var inf = "";
                if (_cliente != null) 
                {
                    inf = _cliente.codigo.Trim() + Environment.NewLine + _cliente.ciRif + Environment.NewLine + _cliente.razonSocial;
                }
                return inf; 
            } 
        }


        public data()
        {
            _condPago = new LibUtilitis.CtrlCB.ImpCB();
            _cliente = null;
            limpiar();
        }


        public void Inicializa()
        {
            limpiar();
            _condPago.Inicializa();
        }
        public void setFechaSistema(DateTime fecha)
        {
            _fechaSistema = fecha;
        }
        public void setDiasCredito(int dias)
        {
            _diasCredito = dias;
            _fechaVencimiento = _fechaVencimiento.AddDays(dias);
        }
        public void setDiasValidez(int dias)
        {
            _diasValidez = dias;
        }
        public void CondicionPagoCargar(List<ficha> lst)
        {
            _condPago.CargarData(lst);
        }


        private void limpiar() 
        {
            _fechaSistema = DateTime.Now.Date;
            _fechaVencimiento = DateTime.Now.Date;
            _diasCredito = 0;
            _diasValidez = 0;
            _cliente = null;
        }

        public void setCliente(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _cliente = ficha;
        }

        public bool DatosIsOK()
        {
            if (_cliente == null) 
            {
                Helpers.Msg.Alerta("CAMPO [ CLIENTE ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_condPago.GetItem == null) 
            {
                Helpers.Msg.Alerta("CAMPO [ CONDICION DE PAGO ] NO PUEDE ESTAR VACIO");
                return false;
            }
            return true;
        }

        public void LimpiarTodo()
        {
            limpiar();
            _condPago.LimpiarOpcion();
        }
    }
}