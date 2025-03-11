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
    public partial class vCliente : Form
    {
        private PanelPrincipal.Pago.ICliente _controlador;
        //
        public vCliente()
        {
            InitializeComponent();
        }
        public void setControlador(PanelPrincipal.Pago.ICliente ctr)
        {
            _controlador = ctr;
        }
        private void vCliente_Load(object sender, EventArgs e)
        {
            L_CLIENTE.Text = _controlador.GetCliente;
            L_MONTO_ANTICIPO.Text = _controlador.GetMontoAnticipo.ToString("n2");
            TB_MONTO_ABONAR.Text = _controlador.GetMontoAbonar.ToString("n2");

        }
        private void vCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarFichaIsOk || _controlador.ProcesarFichaIsOk)
            {
                e.Cancel = false;
            }
        }
        private void TB_MONTO_ABONAR_Leave(object sender, EventArgs e)
        {
            var monto = decimal.Parse(TB_MONTO_ABONAR.Text);
            _controlador.setMontoAbonar(monto);
        }
        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            ProcesarFicha();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        //
        private void ProcesarFicha()
        {
            _controlador.ProcesarFicha();
            if (_controlador.ProcesarFichaIsOk) 
            {
                salir();
            }
        }
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarFichaIsOk) 
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