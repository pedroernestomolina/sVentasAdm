using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler
{
    public class MontoAbonar: PanelPrincipal.Pago.IMontoAbonar
    {
        private string _detalle;
        private decimal _monto;
        private decimal _montoPendiente;
        private bool _aceptartIsOk;
        private Utils.Control.Boton.Abandonar.IAbandonar _abandonar;
        private Utils.Control.Boton.Procesar.IProcesar _procesar;
        //
        public decimal MontoAbonado { get { return _monto; } }
        //
        public string GetMontoPendiente { get { return decToStr(_montoPendiente); } }
        public decimal GetMontoAbonar { get { return _monto; } }
        public string GetDetalle { get { return _detalle; } }
        public bool AbandonarIsOK { get { return _abandonar.OpcionIsOK; } }
        public bool ProcesarIsOK { get { return _aceptartIsOk; } }
        //
        public MontoAbonar() 
        {
            _abandonar = new Utils.Control.Boton.Abandonar.Imp();
            _procesar = new Utils.Control.Boton.Procesar.Imp();
            limpiar();
        }
        public void Inicializa()
        {
            _abandonar.Inicializa();
            _procesar.Inicializa();
            limpiar();
        }
        PanelPrincipal.Pago.vistas.vMontoAbonar frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new PanelPrincipal.Pago.vistas.vMontoAbonar ();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        private bool CargarData()
        {
            return true;
        }
        public void setMontoPend(decimal monto)
        {
            _montoPendiente = monto;
        }
        public void setMontoAbonar(decimal monto)
        {
            _monto = monto;
        }
        public void setDetalle(string p)
        {
            _detalle = p;
        }
        public void ProcesarFicha()
        {
            _aceptartIsOk = false;
            if (_monto > _montoPendiente)
            {
                Helpers.Msg.Error("MONTO A PAGAR INCORRECTO");
                return;
            }
            _procesar.Opcion();
            _aceptartIsOk = _procesar.OpcionIsOK;
        }
        public void AbandonarFicha()
        {
            _abandonar.Opcion();
        }
        private void limpiar()
        {
            _aceptartIsOk = false;
            _montoPendiente = 0m;
            _monto = 0m;
            _detalle = "";
        }
        private string decToStr(decimal p)
        {
            return p.ToString("n2");
        }
    }
}