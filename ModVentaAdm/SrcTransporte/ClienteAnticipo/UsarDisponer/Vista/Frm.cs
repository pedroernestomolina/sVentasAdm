using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.UsarDisponer.Vista
{
    public partial class Frm : Form
    {
        private CultureInfo _cult;
        private IHnd _controlador;


        public Frm()
        {
            InitializeComponent();
            _cult = CultureInfo.CurrentCulture;
        }
        public void setControlador(IHnd ctr)
        {
            _controlador = ctr;
        }
        private bool _modoInicializa;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializa = true;
            L_CLIENTE.Text = _controlador.Get_Cliente;
            TB_MONTO.Text = _controlador.Get_MontoADisponer.ToString();
            this.Refresh();
            _modoInicializa = false;
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }
        private void CTRL_KEYDOWN(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }


        private void TB_MONTO_Leave(object sender, EventArgs e)
        {
            var _monto= decimal.Parse(TB_MONTO.Text);
            _controlador.setMontoDisponer(_monto);
            TB_MONTO.Text = _controlador.Get_MontoADisponer.ToString("n2",_cult);
        }
        private void TB_MONTO_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if ((_controlador.Get_MontoADisponer <= _controlador.Get_Cliente_MontoAnticipo) && (_controlador.Get_MontoADisponer<=_controlador.Get_MontoDeudaVerificar))
            {
                e.Cancel=false;
            }
        }


        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            ProcesarFicha();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void ProcesarFicha()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                salir();
            }
        }
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK)
            {
                salir();
            }
        }
        private void salir()
        {
            this.Close();
        }
    }
}
