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
            ActualizarPanelAnticipo();
            ActualizarPanelMet();
            ActualizarPanelCtas();
            ActualizarPanelNtCred();
            ActualizarPanelResumen();
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
        //PANEL: NOTAS/CREDITO 
        private void BT_LISTAR_NT_CRED_Click(object sender, EventArgs e)
        {
            ListarNtCred();
        }
        //PANEL: ANTICIPO
        private void BT_ANTICIPO_Click(object sender, EventArgs e)
        {
            AgregarAnticipo();
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
        private void ListarNtCred()
        {
            _controlador.ListarNtCred();
            ActualizarPanelNtCred();
        }
        private void AgregarAnticipo()
        {
            _controlador.AgregarAnticipo();
            ActualizarPanelAnticipo();
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
        private void ActualizarPanelAnticipo() 
        {
            L_MONTO_ANTICIPO.Text = _controlador.GetMontoAnticipo.ToString("n2");
            ActualizarPanelResumen();
        }
        private void ActualizarPanelMet()
        {
            L_MET_CNT.Text = _controlador.GetCntMetRecibido;
            L_MONTO_RECIBIDO.Text = _controlador.GetMontoRecibido.ToString("n2");
            ActualizarPanelResumen();
        }
        private void ActualizarPanelCtas()
        {
            L_CNT_CTAS_PAGAR.Text = _controlador.GetCntCtasPagar;
            L_MONTO_PAGAR.Text = _controlador.GetMontoCtasPagar.ToString("n2");
            ActualizarPanelResumen();
        }
        private void ActualizarPanelNtCred()
        {
            L_CNT_NT_CRED.Text = _controlador.GetCntNtCred;
            L_MONTO_NT_CRED.Text = _controlador.GetMontoNtCred.ToString("n2");
            ActualizarPanelResumen();
        }
        private void ActualizarPanelResumen()
        {
            L_RESUMEN_ANTICIPO.Text = _controlador.GetResumenMontoAnticipo.ToString("n2");
            L_RESUMEN_MET_PAGO.Text = _controlador.GetResumenMontoMetPago.ToString("n2");
            L_RESUMEN_NT_CRED.Text = _controlador.GetResumenMontoNtCredito.ToString("n2");
            L_RESUMEN_ABONO.Text = _controlador.GetResumenMontoAbono.ToString("n2");
            L_RESUMEN_CTAS.Text = _controlador.GetResumenMontoCtasPend.ToString("n2");
            L_RESUMEN_SALDO.Text = _controlador.GetResumenSaldo.ToString("n2");
            L_RESUMEN_DES_SALDO.Text = _controlador.GetResumenSaldoDesc;
        }
    }
}
