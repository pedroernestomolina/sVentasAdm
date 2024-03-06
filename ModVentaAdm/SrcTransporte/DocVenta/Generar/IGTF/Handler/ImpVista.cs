using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.IGTF.Handler
{
    public class ImpVista: Vista.IVista
    {
        private decimal _tasaIGTF;
        private decimal _montoAplicarIGTF;
        private Utils.Control.Boton.Abandonar.IAbandonar _btAbandonar;
        private Utils.Control.Boton.Procesar.IProcesar _btAceptar;
        private bool _procesarIsOk;
        //
        public decimal Get_TasaIGTF { get { return _tasaIGTF; } }
        public decimal Get_MontoAplicarIGTF { get { return _montoAplicarIGTF; } }
        public Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get { return _btAbandonar; } }
        public Utils.Control.Boton.Procesar.IProcesar BtAceptar { get { return _btAceptar; } }
        public bool ProcesarIsOk { get { return _procesarIsOk; } }
        //
        public ImpVista()
        {
            _tasaIGTF=0m;
            _montoAplicarIGTF=0m;
            _btAbandonar = new Utils.Control.Boton.Abandonar.Imp();
            _btAceptar=new Utils.Control.Boton.Procesar.Imp();
            _procesarIsOk = false;
        }
        public void Inicializa()
        {
            _tasaIGTF = 0m;
            _montoAplicarIGTF = 0m;
            _procesarIsOk = false;
            _btAbandonar.Inicializa();
            _btAceptar.Inicializa();
        }
        private Vista.Frm frm;
        public void Inicia() 
        {
            if (cargarDataIsOk())
            {
                if (frm == null)
                {
                    frm = new Vista.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setTasaIGTF(decimal tasa)
        {
            _tasaIGTF = tasa;
        }
        public void setMontoAplicarIGTF(decimal monto)
        {
            _montoAplicarIGTF = monto;
        }
        public void Procesar()
        {
            _procesarIsOk = false;
            if (_tasaIGTF <= 0m) 
            {
                Helpers.Msg.Alerta("TASA IGTF INCORRECTA");
                return;
            }
            if (_montoAplicarIGTF <= 0m)
            {
                Helpers.Msg.Alerta("MONTO APLICAR IGTF INCORRECTO");
                return;
            }
            _btAceptar.Opcion();
            _procesarIsOk = _btAceptar.OpcionIsOK;
        }
        //
        private bool cargarDataIsOk()
        {
            return true;
        }
    }
}