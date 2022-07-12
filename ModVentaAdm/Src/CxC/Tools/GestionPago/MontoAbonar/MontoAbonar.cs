using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MontoAbonar
{

    public class MontoAbonar: IMontoAbonar
    {

        private string _detalle;
        private decimal _monto;
        private decimal _montoPendiente;
        private bool _aceptartIsOk;
        private bool _abandonarIsOk;


        public decimal GetMontoPendiente { get { return _montoPendiente; } }
        public decimal GetMontoAbonar { get { return _monto; } }
        public string GetDetalle { get { return _detalle; } }
        public bool AbandonarIsOK { get { return _abandonarIsOk; } }
        public bool ProcesarIsOK { get { return _aceptartIsOk; } }
        public bool MontoAbonarIsOk { get { return _aceptartIsOk; } }


        public MontoAbonar() 
        {
            limpiar();
        }


        public void Inicializa()
        {
            limpiar();
        }
        MontoAbonarFrm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new MontoAbonarFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return true;
        }

        public void setData(decimal montoPendiente, decimal montoPagar, string detalle)
        {
            if (montoPagar == 0m)
                _monto = montoPendiente;
            else
                _monto = montoPagar;

            _montoPendiente = montoPendiente;
            _detalle = detalle;
        }


        public void setMontoAbonar(decimal rt)
        {
            _monto = rt;
        }
        public void setDetalle(string p)
        {
            _detalle = p;
        }


        public void Procesar()
        {
            _aceptartIsOk = false;
            if (_monto > _montoPendiente) 
            {
                Helpers.Msg.Error("MONTO A PAGAR INCORRECTO");
                return;
            }
            _aceptartIsOk = true;
        }
        public void AbandonarFicha()
        {
            _abandonarIsOk = false;
            var msg = "Abandonar y Perder Los Cambios ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }


        private void limpiar()
        {
            _aceptartIsOk = false;
            _abandonarIsOk = false;
            _montoPendiente = 0m;
            _monto = 0m;
            _detalle = "";
        }

    }

}