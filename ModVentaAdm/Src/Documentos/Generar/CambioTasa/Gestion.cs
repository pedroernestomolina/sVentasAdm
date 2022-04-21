using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.CambioTasa
{
    
    public class Gestion:ICambioTasa
    {

        private bool _cambioTasaIsOk;
        private bool _abandonarIsOk;
        private decimal _tasaCambiar;


        public bool CambioTasaIsOk { get { return _cambioTasaIsOk; } }
        public bool AbandonarIsOk { get { return _abandonarIsOk; } }
        public decimal TasaCambiar { get { return _tasaCambiar; } }


        public Gestion() 
        {
            _cambioTasaIsOk = false;
            _abandonarIsOk = false;
            _tasaCambiar = 0m;
        }


        public void Inicializa() 
        {
            _cambioTasaIsOk = false;
            _abandonarIsOk = false;
        }

        private CambioTasaFrm frm;
        public void Inicia() 
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new CambioTasaFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var rt = true;
            return rt;
        }

        public void setTasa(decimal t)
        {
            _tasaCambiar = t;
        }

        public void Procesar()
        {
            if (_tasaCambiar > 0m)
            {
                var xmsg = "Asignar Esta Tasa Al Docmento ?";
                var msg = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (msg == System.Windows.Forms.DialogResult.Yes)
                {
                    _cambioTasaIsOk = true;
                }
            }
            else 
            {
                Helpers.Msg.Error("VALOR TASA DIVISA INCORRECTA");
                return;
            }
        }

        public void Abandonar()
        {
            var xmsg = "Abandonar Cambios ?";
            var msg = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (msg == System.Windows.Forms.DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }

    }

}