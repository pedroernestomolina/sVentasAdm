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
    public partial class vPago : Form
    {
        private PanelPrincipal.Pago.IPago _controlador;
        //
        public vPago()
        {
            InitializeComponent();
        }
        private void vPago_Load(object sender, EventArgs e)
        {
            ActualizarPanelMet();
            ActualizarPanelCtas();
        }
        private void vPago_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarFichaIsOk || _controlador.IsPagoExitoso) 
            {
                e.Cancel = false;
            }
        }
        //
        public void setControlador(PanelPrincipal.Pago.IPago ctr)
        {
            _controlador = ctr;
        }
        //PANEL: METODOS 
        private void BT_AGREGAR_Click(object sender, EventArgs e)
        {
            AgregarMetPago();
        }
        private void BT_LISTAR_Click(object sender, EventArgs e)
        {
            ListarMetPago();
        }
        //PANEL: CTAS 
        private void BT_LISTAR_CTAS_PAGAR_Click(object sender, EventArgs e)
        {
            ListarCtasPagar();
        }
        //
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            BtProcesar();
        }
        private void BT_SALIDA_Click(object sender, EventArgs e)
        {
            BtAbandonar();
        }
        //
        private void AgregarMetPago()
        {
            _controlador.AgregarMetPago();
            ActualizarPanelMet();
        }
        private void ListarMetPago()
        {
            _controlador.ListarMetPago();
            ActualizarPanelMet();
        }
        private void ListarCtasPagar()
        {
            _controlador.ListarCtasPagar();
            ActualizarPanelCtas();
        }
        private void BtProcesar()
        {
            _controlador.ProcesarPago();
            if (_controlador.IsPagoExitoso) 
            {
                salir();
            }
        }
        private void BtAbandonar()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarFichaIsOk) 
            {
                salir();
            }
        }
        //
        private void salir()
        {
            Close();
        }
        private void ActualizarPanelMet()
        {
            L_MET_CNT.Text = _controlador.GetCntMetRecibido;
            L_MONTO_RECIBIDO.Text = _controlador.GetMontoRecibido;
        }
        private void ActualizarPanelCtas()
        {
            L_CNT_CTAS_PAGAR.Text = _controlador.GetCntCtasPagar;
            L_MONTO_PAGAR.Text =_controlador.GetMontoCtasPagar;
        }
    }
}
