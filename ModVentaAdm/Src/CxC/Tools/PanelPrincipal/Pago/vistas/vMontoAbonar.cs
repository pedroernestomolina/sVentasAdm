using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago.vistas
{
    public partial class vMontoAbonar: Form
    {
        private PanelPrincipal.Pago.IMontoAbonar _controlador;
        //
        public vMontoAbonar()
        {
            InitializeComponent();
        }
        public void setControlador(PanelPrincipal.Pago.IMontoAbonar ctr)
        {
            _controlador = ctr;
        }
        private void DetalleMontoFrm_Load(object sender, EventArgs e)
        {
            L_MONTO_PENDIENTE.Text = _controlador.GetMontoPendiente;
            TB_MONTO_ABONAR.Text = _controlador.GetMontoAbonar.ToString();
            TB_DETALLE.Text = _controlador.GetDetalle;
        }
        private void DetalleMontoFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }
        private void TB_MONTO_ABONAR_Leave(object sender, EventArgs e)
        {
            var rt = decimal.Parse(TB_MONTO_ABONAR.Text);
            _controlador.setMontoAbonar(rt);
        }
        private void TB_DETALLE_Leave(object sender, EventArgs e)
        {
            _controlador.setDetalle(TB_DETALLE.Text.Trim());
        }
        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            Aceptar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }
        //
        private void Aceptar()
        {
            _controlador.ProcesarFicha();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }
        private void Abandonar()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK)
            {
                Salir();
            }
        }
        private void Salir()
        {
            this.Close();
        }
    }
}