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


        public DateTime FechaSistema_Get { get { return _fechaSistema; } }
        public DateTime FechaVencimiento_Get { get { return _fechaVencimiento; } }
        public int DiasValidez_Get { get { return _diasValidez; } }
        public int DiasCredito_Get { get { return _diasCredito; } }
        public LibUtilitis.CtrlCB.ICtrl CondicionPago { get { return _condPago; } }


        public data()
        {
            _condPago = new LibUtilitis.CtrlCB.ImpCB();
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
        }
    }
}