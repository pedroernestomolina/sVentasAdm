using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.DatosDocumento
{
    public partial class Frm : Form
    {
        private IDatosDoc _controlador;


        public Frm()
        {
            InitializeComponent();
            InicialzarCombos();
        }
        private void InicialzarCombos()
        {
            CB_COND_PAGO.ValueMember = "id";
            CB_COND_PAGO.DisplayMember = "desc";
        }


        private bool _modoEditar;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoEditar = true;
            CB_COND_PAGO.DataSource = _controlador.Data.CondicionPago.GetSource;
            L_CLIENTE.Text = _controlador.Data.Cliente_GetInf;
            TB_FECHA_EM.Value = _controlador.Data.FechaEmision_Get;
            TB_FECHA_VENCE.Text = _controlador.Data.FechaVencimiento_Get.ToShortDateString();
            TB_DIAS_VALIDEZ.Text = _controlador.Data.DiasValidez_Get.ToString("n0");
            TB_DIAS_CREDITO.Text = _controlador.Data.DiasCredito_Get.ToString("n0");
            TB_SOLICITADO_POR.Text = _controlador.Data.SolicitadoPor_Get;
            TB_MODULO_CARGAR.Text = _controlador.Data.ModuloCargar_Get;
            CB_COND_PAGO.SelectedValue = _controlador.Data.CondicionPago.GetId;
            _modoEditar = false;
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        public void setControlador(IDatosDoc ctr)
        {
            _controlador = ctr;
        }


        private void CB_COND_PAGO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoEditar) { return; }
            _controlador.Data.CondicionPago.setFichaById(CB_COND_PAGO.SelectedValue.ToString());
        }
        private void TB_DIAS_CREDITO_Leave(object sender, EventArgs e)
        {
            var _dias = int.Parse(TB_DIAS_CREDITO.Text);
            _controlador.Data.setDiasCredito(_dias);
            TB_FECHA_VENCE.Text = _controlador.Data.FechaVencimiento_Get.ToShortDateString();
        }
        private void TB_DIAS_VALIDEZ_Leave(object sender, EventArgs e)
        {
            var _dias = int.Parse(TB_DIAS_VALIDEZ.Text);
            _controlador.Data.setDiasValidez(_dias);
        }
        private void TB_SOLICITADO_POR_Leave(object sender, EventArgs e)
        {
            _controlador.Data.setSolicitadoPor(TB_SOLICITADO_POR.Text.Trim().ToUpper());
            TB_SOLICITADO_POR.Text = _controlador.Data.SolicitadoPor_Get;
        }
        private void TB_MODULO_CARGAR_Leave(object sender, EventArgs e)
        {
            _controlador.Data.setModuloCargar(TB_MODULO_CARGAR.Text.Trim().ToUpper());
            TB_MODULO_CARGAR.Text = _controlador.Data.ModuloCargar_Get;
        }


        private void BT_BUSCAR_CLIENTE_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }
        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            AceptarDatos();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarCambios();
        }


        private void BuscarCliente()
        {
            _controlador.BuscarCliente();
            L_CLIENTE.Text = _controlador.Data.Cliente_GetInf;
            _modoEditar = false;
            TB_DIAS_CREDITO.Text = _controlador.Data.DiasCredito_Get.ToString("n0");
            CB_COND_PAGO.SelectedValue = _controlador.Data.CondicionPago.GetId;
            TB_FECHA_VENCE.Text = _controlador.Data.FechaVencimiento_Get.ToShortDateString();
            _modoEditar = true;
        }
        private void AceptarDatos()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }
        private void AbandonarCambios()
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

        private void TB_FECHA_EM_Leave(object sender, EventArgs e)
        {
            _controlador.Data.setFechaEmision(TB_FECHA_EM.Value);
            TB_FECHA_VENCE.Text = _controlador.Data.FechaVencimiento_Get.ToShortDateString();
        }
        private void TB_FECHA_EM_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            if (TB_FECHA_EM.Value > _controlador.Data.FechaSistema_Get) 
            {
                e.Cancel = true;
            }
        }
    }
}