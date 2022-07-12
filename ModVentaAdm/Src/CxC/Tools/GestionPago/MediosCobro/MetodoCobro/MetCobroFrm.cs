using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro
{

    public partial class MetCobroFrm : Form
    {


        private IMetodoAgregarEditar _controlador;


        public MetCobroFrm()
        {
            InitializeComponent();
            InicializaCombo();
        }

        private void InicializaCombo()
        {
            CB_METODO_PAGO.ValueMember = "id";
            CB_METODO_PAGO.DisplayMember = "descripcion";
        }


        public void setControlador(IMetodoAgregarEditar ctr)
        {
            _controlador = ctr;
        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
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
        private void MetCobroFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }

        private bool _modoInicializa;
        private void MetCobroFrm_Load(object sender, EventArgs e)
        {
            _modoInicializa = true;
            L_TITULO.Text = _controlador.GetTituloFicha;
            L_RESTA.Text = _controlador.GetMontoResta.ToString("n2");
            CB_METODO_PAGO.DataSource = _controlador.GetMetCobroSource;
            CB_METODO_PAGO.SelectedValue = _controlador.GetMetCobroID;
            TB_MONTO.Text = _controlador.GetMonto.ToString();
            CHB_APLICA_FACTOR.Checked = _controlador.GetAplicaFactor;
            TB_FACTOR_CAMBIO.Text = _controlador.GetFactor.ToString();
            TB_BANCO.Text = _controlador.GetBanco;
            TB_NUM_CTA.Text = _controlador.GetNroCta;
            TB_NUM_CGEQ_REF.Text = _controlador.GetCheqRefTrans;
            DTP_FECHA_OPERACION.Value = _controlador.GetFechaOp;
            TB_DETALLE_OPERACION.Text = _controlador.GetDetalleOp;
            TB_REF.Text = _controlador.GetReferencia;
            TB_LOTE.Text = _controlador.GetLote;
            _modoInicializa = false;
        }

        private void CB_METODO_PAGO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializa) { return; }
            _controlador.setMetCobro("");
            if (CB_METODO_PAGO.SelectedIndex != -1) 
            {
                _controlador.setMetCobro(CB_METODO_PAGO.SelectedValue.ToString());
            }
        }
        private void TB_MONTO_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_MONTO.Text);
            _controlador.setMonto(_monto);
        }
        private void TB_FACTOR_CAMBIO_Leave(object sender, EventArgs e)
        {
            var _factor = decimal.Parse(TB_FACTOR_CAMBIO.Text);
            _controlador.setFactor(_factor);
        }
        private void TB_BANCO_Leave(object sender, EventArgs e)
        {
            _controlador.setBanco(TB_BANCO.Text.Trim().ToUpper());
        }
        private void TB_NUM_CTA_Leave(object sender, EventArgs e)
        {
            _controlador.setCtaNro(TB_NUM_CTA.Text.Trim().ToUpper());
        }
        private void TB_NUM_CGEQ_REF_Leave(object sender, EventArgs e)
        {
            _controlador.setChequeRefTranf(TB_NUM_CGEQ_REF.Text.Trim().ToUpper());
        }
        private void TB_LOTE_Leave(object sender, EventArgs e)
        {
            _controlador.setLote(TB_LOTE.Text.Trim().ToUpper());
        }
        private void TB_REF_Leave(object sender, EventArgs e)
        {
            _controlador.setReferencia(TB_REF.Text.Trim().ToUpper());
        }
        private void DTP_FECHA_OPERACION_Leave(object sender, EventArgs e)
        {
            _controlador.setFechaOperacion(DTP_FECHA_OPERACION.Value);
        }
        private void TB_DETALLE_OPERACION_Leave(object sender, EventArgs e)
        {
            _controlador.setDetalleOperacion(TB_DETALLE_OPERACION.Text);
        }
        private void CHB_APLICA_FACTOR_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.setAplicaFactor(CHB_APLICA_FACTOR.Checked);
        }
        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

    }

}